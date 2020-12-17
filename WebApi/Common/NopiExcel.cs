using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Tomato.StandardLib.MyExtensions;

/// <summary>
/// @Tomato  Npoi流处理
/// </summary>
namespace Tomato.SumhsAuditPreWarning.WebApi.Common
{
    public class NopiExcel
    {
        public const string FILE_PATH = "/upload/";
        /// <summary>
        /// excel导入成DataTable
        /// </summary>
        /// <param name="ExcelFileStream">文件流</param>
        /// <param name="SheetIndex">开始的sheet</param>
        /// <param name="HeaderRowIndex">从第行开始读取</param>
        /// <param name="fileExt">文件名，带.</param>
        /// <returns>返回DataTable</returns>
        public static DataTable ExcelToTable(Stream ExcelFileStream, string fileExt, int SheetIndex = 0, int HeaderRowIndex = 0)
        {
            DataTable dt = new DataTable();
            IWorkbook workbook;
            //XSSFWorkbook 适用XLSX格式，HSSFWorkbook 适用XLS格式
            if (fileExt == ".xlsx")
            {
                workbook = new XSSFWorkbook(ExcelFileStream);
            }
            else if (fileExt == ".xls")
            {
                workbook = new HSSFWorkbook(ExcelFileStream);
            }
            else
            {
                workbook = null;
            }
            if (workbook == null)
            {
                return null;
            }
            ISheet sheet = workbook.GetSheetAt(SheetIndex);

            //表头  
            IRow header = sheet.GetRow(HeaderRowIndex);
            List<int> columns = new List<int>();
            //填充列明
            for (int i = 0; i < header.LastCellNum; i++)
            {
                object obj = GetValueType(header.GetCell(i));
                if (obj == null || obj.ToString() == string.Empty)
                {
                    dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                }
                else
                {
                    dt.Columns.Add(new DataColumn(obj.ToString()));
                }

                columns.Add(i);
            }
            //数据  
            for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
            {
                DataRow dr = dt.NewRow();
                bool hasValue = false;
                foreach (int j in columns)
                {
                    dr[j] = GetValueType(sheet.GetRow(i).GetCell(j));
                    if (dr[j] != null && dr[j].ToString() != string.Empty)
                    {
                        hasValue = true;
                    }
                }
                if (hasValue)
                {
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        /// <summary>
        /// excel导入成DataTable
        /// </summary>
        /// <param name="ExcelFileStream">文件流</param>
        /// <param name="SheetIndex">开始的sheet</param>
        /// <param name="HeaderRowIndex">从第行开始读取</param>
        /// <param name="fileExt">文件名，带.</param>
        /// <returns>返回DataTable</returns>
        public static DataTable ExcelToTable(HttpPostedFile ExcelFileStream, int SheetIndex = 0, int HeaderRowIndex = 0, string fileExt = null)
        {
            Stream fileStream = ExcelFileStream.InputStream;
            if (fileExt.IsNullOrEmpty())
            {
                fileExt = System.IO.Path.GetExtension(ExcelFileStream.FileName);
                if (!fileExt.Contains("xls") && !fileExt.Contains("xlsx"))
                {
                    throw new Exception("文件格式不正确");
                }
            }
            DataTable dt = new DataTable();
            IWorkbook workbook;
            //XSSFWorkbook 适用XLSX格式，HSSFWorkbook 适用XLS格式
            if (fileExt == ".xlsx")
            {
                workbook = new XSSFWorkbook(fileStream);
            }
            else if (fileExt == ".xls")
            {
                workbook = new HSSFWorkbook(fileStream);
            }
            else
            {
                workbook = null;
            }
            if (workbook == null)
            {
                return null;
            }
            ISheet sheet = workbook.GetSheetAt(SheetIndex);

            //表头  
            IRow header = sheet.GetRow(HeaderRowIndex);
            List<int> columns = new List<int>();
            //填充列明
            for (int i = 0; i < header.LastCellNum; i++)
            {
                object obj = GetValueType(header.GetCell(i));
                if (obj == null || obj.ToString() == string.Empty)
                {
                    dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                }
                else
                {
                    dt.Columns.Add(new DataColumn(obj.ToString()));
                }

                columns.Add(i);
            }
            //数据  
            for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
            {
                DataRow dr = dt.NewRow();
                bool hasValue = false;
                foreach (int j in columns)
                {
                    dr[j] = GetValueType(sheet.GetRow(i).GetCell(j));
                    if (dr[j] != null && dr[j].ToString() != string.Empty)
                    {
                        hasValue = true;
                    }
                }
                if (hasValue)
                {
                    dt.Rows.Add(dr);
                }
            }


            //for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
            //{
            //    IRow row = sheet.GetRow(i);
            //    if (row == null) continue; //没有数据的行默认是null　　　　　　　

            //    DataRow dataRow = data.NewRow();
            //    for (int j = row.FirstCellNum; j < cellCount; j++)
            //    {
            //        if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
            //            dataRow[j] = row.GetCell(j).ToString();
            //    }
            //    data.Rows.Add(dataRow);
            //}

            return dt;
        }


        /// <summary>
        /// DataTable转Excel转流
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="file">.xls,.xlsx</param>
        public static Stream StreamFromDataTable(DataTable dt, string fileExt)
        {
            //转为字节数组  
            MemoryStream ms = new MemoryStream();
            IWorkbook workbook = new XSSFWorkbook();
            if (fileExt == ".xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (fileExt == ".xls")
            {
                workbook = new HSSFWorkbook();
            }
            else
            {
                workbook = null;
            }
            if (workbook == null)
            {
                return null;
            }
            //添加页
            ISheet sheet = string.IsNullOrEmpty(dt.TableName) ? workbook.CreateSheet("Sheet1") : workbook.CreateSheet(dt.TableName);

            //表头  
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].ColumnName);
            }

            //数据  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                }
            }
            workbook.Write(ms);
            //XSSFWorkbook 直接Write得不到数据，我们需要ToArray赋给新的内存流
            MemoryStream stream = new MemoryStream(ms.ToArray());
            return stream;
        }

        /// <summary>
        /// 获取单元格类型
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static object GetValueType(ICell cell)
        {
            if (cell == null)
            {
                return null;
            }

            switch (cell.CellType)
            {
                case CellType.Blank: //BLANK:  
                    return null;
                case CellType.Boolean: //BOOLEAN:  
                    return cell.BooleanCellValue;
                case CellType.Numeric: //NUMERIC:  
                    return cell.NumericCellValue;
                case CellType.String: //STRING:  
                    return cell.StringCellValue;
                case CellType.Error: //ERROR:  
                    return cell.ErrorCellValue;
                case CellType.Formula: //FORMULA:  
                default:
                    return "=" + cell.CellFormula;
            }
        }

        /// <summary>
        /// 通过文件路径转为DataTable
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="sheetName">设置文件名称</param>
        /// <param name="isFirstRowColumn"></param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string filePath, string sheetName = null, bool isFirstRowColumn = true)
        {
            IWorkbook val = null;
            ISheet val2 = null;
            DataTable dataTable = new DataTable();
            int num = 0;
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            if (Path.GetExtension(filePath) == ".xlsx")
            {
                val = new XSSFWorkbook(fileStream);
            }
            else if (Path.GetExtension(filePath) == ".xls")
            {
                val = new HSSFWorkbook(fileStream);
            }
            if (sheetName != null)
            {
                val2 = val.GetSheet(sheetName);
                if (val2 == null)
                {
                    val2 = val.GetSheetAt(0);
                }
            }
            else
            {
                val2 = val.GetSheetAt(0);
            }
            if (val2 != null)
            {
                IRow row = val2.GetRow(0);
                int lastCellNum = row.LastCellNum;
                if (isFirstRowColumn)
                {
                    for (int i = row.FirstCellNum; i < lastCellNum; i++)
                    {
                        ICell cell = row.GetCell(i);
                        if (cell != null)
                        {
                            string stringCellValue = cell.StringCellValue;
                            if (!string.IsNullOrEmpty(stringCellValue))
                            {
                                DataColumn column = new DataColumn(stringCellValue);
                                dataTable.Columns.Add(column);
                            }
                        }
                    }
                    num = val2.FirstRowNum + 1;
                }
                else
                {
                    num = val2.FirstRowNum;
                }
                int lastRowNum = val2.LastRowNum;
                for (int j = num; j <= lastRowNum; j++)
                {
                    IRow row2 = val2.GetRow(j);
                    if (row2 == null)
                    {
                        continue;
                    }
                    DataRow dataRow = dataTable.NewRow();
                    for (int k = row2.FirstCellNum; k < lastCellNum; k++)
                    {
                        if (row2.GetCell(k) != null)
                        {
                            dataRow[k] = GetCellValue(row2.GetCell(k));
                        }
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }
            return dataTable;
        }

        private static string GetCellValue(ICell cell)
        {
            if (cell == null)
            {
                return string.Empty;
            }
            CellType cellType = cell.CellType;
            switch ((int)cellType - 1)
            {
                case 4:
                    return string.Empty;
                case 5:
                    return cell.BooleanCellValue.ToString();
                case 6:
                    return cell.ErrorCellValue.ToString();
                case 1:
                    if (DateUtil.IsCellDateFormatted(cell))
                    {
                        return cell.DateCellValue.ToString();
                    }
                    return cell.NumericCellValue.ToString();
                default:
                    return cell.ToString();
                case 2:
                    return cell.StringCellValue;
                case 3:
                    try
                    {
                        new HSSFFormulaEvaluator(cell.Sheet.Workbook).EvaluateInCell(cell);
                        return cell.ToString();
                    }
                    catch
                    {
                        return cell.NumericCellValue.ToString();
                    }
            }
        }


        /// <summary>
        /// 流转文件
        /// </summary>
        /// <param name="ms"></param>
        /// <param name="FileName"></param>
        public static void WriteSteamToFile(MemoryStream ms, string FileName)
        {
            FileStream fs = new FileStream(FileName, FileMode.Create, FileAccess.Write);
            byte[] data = ms.ToArray();

            fs.Write(data, 0, data.Length);
            fs.Flush();
            fs.Close();

            data = null;
            ms = null;
            fs = null;
        }

        /// <summary>
        /// 字节   转文件
        /// </summary>
        /// <param name="data"></param>
        /// <param name="FileName"></param>
        public static void WriteSteamToFile(byte[] data, string FileName)
        {
            FileStream fs = new FileStream(FileName, FileMode.Create, FileAccess.Write);
            fs.Write(data, 0, data.Length);
            fs.Flush();
            fs.Close();
            data = null;
            fs = null;
        }

        /// <summary>
        /// HSSFWorkbook     -->Stream 
        /// </summary>
        /// <param name="InputWorkBook"></param>
        /// <returns></returns>
        public static Stream WorkBookToStream(HSSFWorkbook InputWorkBook)
        {
            MemoryStream ms = new MemoryStream();
            InputWorkBook.Write(ms);
            ms.Flush();
            ms.Position = 0;
            return ms;
        }

        /// <summary>
        /// Stream    -->HSSFWorkbook
        /// </summary>
        /// <param name="InputStream"></param>
        /// <returns></returns>
        public static HSSFWorkbook StreamToWorkBook(Stream InputStream)
        {
            HSSFWorkbook WorkBook = new HSSFWorkbook(InputStream);
            return WorkBook;
        }

        /// <summary>
        /// MemoryStream    -->HSSFWorkbook
        /// </summary>
        /// <param name="InputStream"></param>
        /// <returns></returns>
        public static HSSFWorkbook MemoryStreamToWorkBook(MemoryStream InputStream)
        {
            HSSFWorkbook WorkBook = new HSSFWorkbook(InputStream as Stream);
            return WorkBook;
        }

        /// <summary>
        /// HSSFWorkbook    -->MemoryStream
        /// </summary>
        /// <param name="InputStream"></param>
        /// <returns></returns>
        public static MemoryStream WorkBookToMemoryStream(HSSFWorkbook InputStream)
        {
            //Write the stream data of workbook to the root directory
            MemoryStream file = new MemoryStream();
            InputStream.Write(file);
            return file;
        }

        /// <summary>
        /// File   -->   Stream
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static Stream FileToStream(string FileName)
        {
            FileInfo fi = new FileInfo(FileName);
            if (fi.Exists == true)
            {
                FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                return fs;
            }
            else return null;
        }

        /// <summary>
        /// MemoryStream   -->   Stream
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public static Stream MemoryStreamToStream(MemoryStream ms)
        {
            return ms as Stream;
        }

        /// <summary>
        /// Stream   -->   MemoryStream
        /// </summary>
        /// <param name="instream"></param>
        /// <returns></returns>

        public static MemoryStream StreamToMemoryStream(Stream instream)
        {
            MemoryStream outstream = new MemoryStream();
            const int bufferLen = 4096;
            byte[] buffer = new byte[bufferLen];
            int count = 0;
            while ((count = instream.Read(buffer, 0, bufferLen)) > 0)
            {
                outstream.Write(buffer, 0, count);
            }
            return outstream;
        }
        
        // <summary> 
        /// 序列化 
        /// </summary> 
        /// <param name="data">要序列化的对象</param> 
        /// <returns>返回存放序列化后的数据缓冲区</returns> 
        public static byte[] Serialize(object data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream rems = new MemoryStream();
            formatter.Serialize(rems, data);
            return rems.GetBuffer();
        }

        public static void DownloadExcel(MemoryStream ms, string fileName)
        {
            #region 
            ////处理IE、火狐等浏览器文件名乱码
            //if (System.Web.HttpContext.Current.Request.ServerVariables["http_user_agent"].IndexOf("Firefox", StringComparison.Ordinal) != -1)
            //{
            //    fileName = "=?UTF-8?B?" + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(fileName)) + "?=";
            //}
            //else
            //{
            //    fileName = System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8);
            //    fileName = fileName.Replace("+", "%20");
            //}
            //#endregion
            //HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", fileName));
            //HttpContext.Current.Response.AddHeader("Content-Length", ms.Length.ToString());
            //HttpContext.Current.Response.AddHeader("Content-Transfer-Encoding", "binary");
            //HttpContext.Current.Response.ContentType = "application/octet-stream;charset=utf-8";
            //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            //HttpContext.Current.Response.Clear();
            //HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            //HttpContext.Current.Response.End();
            //var upload = HttpUtility.UrlEncode(fileNameAll, System.Text.Encoding.UTF8);
            //HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + upload + ";charset=UTF-8");
            //HttpContext.Current.Response.AddHeader("Content-Length", ms.Length.ToString());
            //HttpContext.Current.Response.ContentType = "application/vnd.ms-excel;charset=UTF-8";
            //HttpContext.Current.Response.Clear();
            //HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            //HttpContext.Current.Response.End();

            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel;charset=UTF-8";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8)));
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            HttpContext.Current.Response.End();
            #endregion
        }



        public static void DownloadUploadFile(MemoryStream ms, string fileName, string types)
        {

            string[] typesInfo = types.Split('.');
            string fileNameAll = fileName + types;
            //int intStart = a.LastIndexOf("\\") + 1;
            //string saveFileName = a.Substring(intStart, a.Length - intStart);

            //System.IO.FileInfo fi = new System.IO.FileInfo(a);
            //string fileextname = fi.Extension;
            //string DEFAULT_CONTENT_TYPE = "application/unknown";
            //RegistryKey regkey, fileextkey;
            //string filecontenttype;
            //try
            //{
            //    regkey = Registry.ClassesRoot;
            //    fileextkey = regkey.OpenSubKey(fileextname);
            //    filecontenttype = fileextkey.GetValue("Content Type", DEFAULT_CONTENT_TYPE).ToString();
            //}
            //catch
            //{
            //    filecontenttype = DEFAULT_CONTENT_TYPE;
            //}

            //HttpContext.Current.Response.Clear();
            //HttpContext.Current.Response.Charset = "utf-8";
            //HttpContext.Current.Response.Buffer = true;
            //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;

            //HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(saveFileName, System.Text.Encoding.UTF8));
            //HttpContext.Current.Response.ContentType = filecontenttype;

            //HttpContext.Current.Response.WriteFile(a);
            //HttpContext.Current.Response.Flush();
            //HttpContext.Current.Response.Close();

            //HttpContext.Current.Response.End();
            #region 
            //////处理IE、火狐等浏览器文件名乱码
            //if (System.Web.HttpContext.Current.Request.ServerVariables["http_user_agent"].IndexOf("Firefox", StringComparison.Ordinal) != -1)
            //{
            //    a = "=?UTF-8?B?" + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(a)) + "?=";
            //}
            //else
            //{
            //    a = System.Web.HttpUtility.UrlEncode(a, System.Text.Encoding.UTF8);
            //    a = a.Replace("+", "%20");
            //}
            #endregion
            string upload = HttpUtility.UrlEncode(fileNameAll, System.Text.Encoding.UTF8);
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + upload);
            //HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + upload + ";charset=UTF-8");
            HttpContext.Current.Response.AddHeader("Content-Length", ms.Length.ToString());
            HttpContext.Current.Response.ContentType = "application/octet-stream;charset=UTF-8";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// List对象导出Excel至浏览器下载
        /// </summary>
        /// <param name="list">泛型集合类</param>
        /// <param name="fileName">生成的Excel文件名</param>
        /// <param name="propertyName">Excel的字段列表</param>
        public static void ListToExcel<T>(IList<T> list, string fileName, Dictionary<string, string> propertyName)
        {
            if (list == null || list.Count <= 0)
            {
                throw new Exception("list不能为空");
            }
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel;charset=UTF-8";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", HttpUtility.UrlEncode(fileName)));
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.BinaryWrite(ListToExcel(list, propertyName, fileName).GetBuffer());
            HttpContext.Current.Response.End();
        }


        /// <summary>
        /// 导出表头
        /// </summary>
        public static void ListToExcel(Dictionary<string, string> propertyNameList, string fileName)
        {

            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel;charset=UTF-8";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", HttpUtility.UrlEncode(fileName)));
            HttpContext.Current.Response.Clear();
            MemoryStream ms;
            //创建流对象
            using (ms = new MemoryStream())
            {
                //NOPI的相关对象
                IWorkbook workbook;
                if (fileName.Contains("xlsx"))
                {
                    workbook = new XSSFWorkbook(); // 2007版本
                }
                else
                {
                    workbook = new HSSFWorkbook(); // 2003版本
                }
                ISheet sheet = workbook.CreateSheet("Sheet1");
                IRow headerRow = sheet.CreateRow(0);
                int columnIndex = 0;
                //遍历属性集合生成excel的表头标题
                foreach (KeyValuePair<string, string> item in propertyNameList)
                {
                    headerRow.CreateCell(columnIndex).SetCellValue(item.Value);
                    columnIndex++;
                }
                workbook.Write(ms);
                ms.Flush();
            }

            HttpContext.Current.Response.BinaryWrite(ms.GetBuffer());
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// List对象导出Excel至服务器
        /// </summary>
        /// <param name="list">泛型集合类</param>
        /// <param name="fileName">生成的Excel文件名</param>
        /// <param name="propertyName">Excel的字段列表</param>
        public static void ListToExcelSaveServer<T>(IList<T> list, string fileName, Dictionary<string, string> propertyName)
        {
            if (list == null || list.Count <= 0)
            {
                throw new Exception("list数据不能为空");
            }
            byte[] file = ListToExcel(list, propertyName, fileName).GetBuffer();
            string path = HttpContext.Current.Server.MapPath("~" + FILE_PATH);
            if (Directory.Exists(path) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(path);
            }
            FileStream fileHSSF = new FileStream(path + fileName, FileMode.Create);
            fileHSSF.Write(file, 0, file.Length);
            fileHSSF.Close();
        }

        /// <summary>
        /// List对象转流
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="list">List对象</param>
        /// <param name="propertyNameList">表头属性</param>
        /// <param name="fileName">文件名</param>
        /// <returns>Excel文件流</returns>
        public static MemoryStream ListToExcel<T>(IList<T> list, Dictionary<string, string> propertyNameList, string fileName)
        {
            //创建流对象
            using (MemoryStream ms = new MemoryStream())
            {
                //NOPI的相关对象
                IWorkbook workbook;
                if (fileName.Contains("xlsx"))
                {
                    workbook = new XSSFWorkbook(); // 2007版本
                }
                else
                {
                    workbook = new HSSFWorkbook(); // 2003版本
                }
                ISheet sheet = workbook.CreateSheet("Sheet1");
                IRow headerRow = sheet.CreateRow(0);
                int columnIndex = 0;
                if (list.Count > 0)
                {
                    //通过反射得到对象的属性集合
                    List<PropertyInfo> propertys = list[0].GetType().GetProperties().ToList();
                    //遍历属性集合生成excel的表头标题
                    foreach (KeyValuePair<string, string> item in propertyNameList)
                    {
                        headerRow.CreateCell(columnIndex).SetCellValue(item.Value);
                        columnIndex++;
                    }
                    int rowIndex = 1;
                    //遍历集合生成excel的行集数据
                    foreach (T row in list)
                    {
                        IRow dataRow = sheet.CreateRow(rowIndex);
                        columnIndex = 0;
                        //遍历集合生成excel的列数据
                        foreach (KeyValuePair<string, string> item in propertyNameList)
                        {
                            object obj = GetModelValue(item.Key, row);
                            dataRow.CreateCell(columnIndex).SetCellValue(obj == null ? "" : obj.ToString());
                            columnIndex++;
                        }
                        rowIndex++;
                    }
                }
                workbook.Write(ms);
                ms.Flush();
                return ms;
            }
        }

        /// <summary>
        /// 将Excel文件流导入到DataTable中
        /// </summary>
        /// <param name="file">指定Excel流文件访问权限</param>
        /// <param name="sheetName">Excel工作薄sheet的名称</param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(HttpPostedFile file, string sheetName)
        {
            Stream fs = file.InputStream;
            string fsName = file.FileName;
            if (!fsName.Contains("xls") && !fsName.Contains("xlsx"))
            {
                throw new Exception("文件格式不正确");
            }
            IWorkbook workbook;
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                if (fsName.Contains("xlsx"))
                {
                    workbook = new XSSFWorkbook(fs); // 2007版本
                }
                else
                {
                    workbook = new HSSFWorkbook(fs); // 2003版本
                }
                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数
                    for (int i = firstRow.FirstCellNum; i < cellCount; i++)
                    {
                        ICell cell = firstRow.GetCell(i);
                        if (cell != null)
                        {
                            string cellValue = cell.StringCellValue;
                            if (!string.IsNullOrEmpty(cellValue))
                            {
                                DataColumn column = new DataColumn(cellValue);
                                data.Columns.Add(column);
                            }
                        }
                    }
                    startRow = sheet.FirstRowNum + 1;
                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }
                return data;
            }
            catch (Exception)
            {
                throw new Exception("Excel转DataTable时发生错误");
            }
        }

        /// <summary>
        /// 将Excel文件流导入到List中
        /// </summary>
        /// <param name="file">指定Excel流文件访问权限</param>
        /// <param name="sheetName">Excel工作薄sheet的名称</param>
        /// <returns></returns>
        public static List<T> ExcelToList<T>(HttpPostedFile file, string sheetName, Dictionary<string, string> propertyNameList) where T : new()
        {
            Stream fs = file.InputStream;
            string fsName = file.FileName;
            if (!fsName.Contains("xls") && !fsName.Contains("xlsx"))
            {
                throw new Exception("文件格式不正确");
            }
            IWorkbook workbook;
            ISheet sheet = null;

            List<T> data = new List<T>();

            int startRow = 0;
            try
            {
                if (fsName.Contains("xlsx"))
                {
                    workbook = new XSSFWorkbook(fs); // 2007版本
                }
                else
                {
                    workbook = new HSSFWorkbook(fs); // 2003版本
                }
                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    Dictionary<int, string> ColumnsNameList = new Dictionary<int, string>();
                    for (int i = firstRow.FirstCellNum; i < cellCount; i++)
                    {
                        ICell cell = firstRow.GetCell(i);
                        if (cell != null)
                        {
                            string cellValue = cell.StringCellValue;
                            if (!string.IsNullOrEmpty(cellValue))
                            {
                                ColumnsNameList.Add(i, cellValue);
                            }
                        }
                    }

                    startRow = sheet.FirstRowNum + 1;
                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        T dataRow = new T();
                        List<PropertyInfo> propertys = dataRow.GetType().GetProperties().ToList();
                        foreach (KeyValuePair<int, string> ColumnsNameItem in ColumnsNameList)
                        {
                            ICell columnval = row.GetCell(ColumnsNameItem.Key);
                            if (columnval == null)
                            {
                                continue;
                            }

                            string itemValue = columnval.ToString();
                            KeyValuePair<string, string> propertyName = propertyNameList.ToList().Find(t => t.Key == ColumnsNameItem.Value);
                            if (propertyName.Value != null && propertyName.Key != null)
                            {
                                string modelPropertyName = propertyName.Value;
                                SetModelValue(modelPropertyName, propertys, itemValue, dataRow);
                            }
                        }
                        data.Add(dataRow);
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Excel转List时发生错误：" + ex.Message);
            }
        }

        /// <summary>
        /// 遍历对象中的属性取值（ListToExcel）
        /// </summary>
        /// <param name="item">属性路径</param>
        /// <param name="model">对象</param>
        /// <returns>返回对应属性值</returns>
        private static object GetModelValue(string item, object model)
        {
            object obj = "";
            string[] columnName = item.Split('.');
            // 获取该对象下所有属性
            List<PropertyInfo> propertys = model.GetType().GetProperties().ToList();
            PropertyInfo property = propertys.Find(t => t.Name == columnName[0]);
            if (property != null)
            {
                // 取该属性的值
                obj = property.GetValue(model, null);
                // 判断为属性对象则递归
                if (columnName.Length > 1)
                {
                    string newitem = item.Replace(string.Format("{0}.", columnName[0]), "");
                    obj = GetModelValue(newitem, obj);
                }
            }
            return obj;
        }

        /// <summary>
        /// 反射属性值类型转换（ExcelToList）
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="model">属性对象</param>
        /// <param name="obj">绑定值</param>
        private static void StringToOtherType(string value, PropertyInfo model, object obj)
        {
            string type = model.PropertyType.Name;
            switch (type)
            {
                case "String":
                    model.SetValue(obj, value, null);
                    return;
                case "Int32":
                    int intValue;
                    if (int.TryParse(value, out intValue))
                        model.SetValue(obj, intValue, null);
                    return;
                case "Boolean":
                    bool boolValue;
                    if (bool.TryParse(value, out boolValue))
                        model.SetValue(obj, boolValue, null);
                    return;
                case "DateTime?":
                case "DateTime":
                    DateTime dateTimeValue;
                    if (DateTime.TryParse(value, out dateTimeValue))
                        model.SetValue(obj, dateTimeValue, null);
                    return;
                case "Decimal":
                    decimal decimalValue;
                    if (decimal.TryParse(value, out decimalValue))
                        model.SetValue(obj, decimalValue, null);
                    return;
                case "Double":
                    double doubleValue;
                    if (double.TryParse(value, out doubleValue))
                        model.SetValue(obj, doubleValue, null);
                    return;
                default:
                    return;
            }
        }

        /// <summary>
        /// 遍历对象中的属性赋值（ExcelToList）
        /// </summary>
        /// <param name="modelPropertyNames"></param>
        /// <param name="propertys"></param>
        /// <param name="itemValue"></param>
        /// <param name="dataRow"></param>
        private static void SetModelValue(string modelPropertyNames, List<PropertyInfo> propertys, string itemValue, object dataRow)
        {
            string[] modelPropertyNameList = modelPropertyNames.Split('.');
            // 获取传入对象下所有属性
            PropertyInfo property = propertys.Find(t => t.Name == modelPropertyNameList[0]);
            if (property != null)
            {
                // 只是属性则赋值
                if (modelPropertyNameList.Length == 1)
                {
                    StringToOtherType(itemValue, property, dataRow);
                }
                // 是属性对象则取对象递归该对象下的属性赋值
                if (modelPropertyNameList.Length > 1)
                {
                    // 获取该对象中的属性对象
                    object dataRowChildren = property.GetValue(dataRow, null);
                    // 对象中的属性对象未初始化则初始化一个属性对象
                    if (dataRowChildren == null)
                    {
                        Type type = property.PropertyType;
                        // 使用构造器对象来创建对象
                        ConstructorInfo constructor = type.GetConstructor(new Type[0]);
                        // 初始化对象
                        dataRowChildren = constructor.Invoke(new object[0]);
                    }
                    // 去掉上级绑定值
                    string newitem = modelPropertyNames.Replace(string.Format("{0}.", modelPropertyNameList[0]), "");
                    // 获取对象下的属性
                    List<PropertyInfo> propertysChildren = dataRowChildren.GetType().GetProperties().ToList();
                    // 递归
                    SetModelValue(newitem, propertysChildren, itemValue, dataRowChildren);
                    // 将赋好值的属性对象赋值给父级对象
                    property.SetValue(dataRow, dataRowChildren, null);
                }
            }
        }


    }
}
