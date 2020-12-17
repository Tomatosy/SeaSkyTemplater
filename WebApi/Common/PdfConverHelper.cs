using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Cells;
using System.IO;
using Microsoft.Office.Interop.Excel;

public class PdfConverHelper
{


    // 自定义操作DataTable
    public static void ExcelTableColModify(System.Data.DataTable excelTalbe)
    {
        excelTalbe.Columns.Remove("PageSize");
        excelTalbe.Columns.Remove("PageNO");
        excelTalbe.Columns.Remove("GmtCreateUser");
        excelTalbe.Columns.Remove("GmtCreateDate");
        excelTalbe.Columns.Remove("GmtUpdateUser");
        excelTalbe.Columns.Remove("GmtUpdateDate");
        excelTalbe.Columns.Remove("Timestamp");

        //excelTalbe.Columns["Test"].ColumnName = "单位名称";
    }

    /// <summary>
    /// word转pdf
    /// </summary>
    /// <param name="wordPath">word路径</param>
    /// <param name="pdfPath">pdf路径</param>
    /// <returns></returns>
    public static bool WordToPDF(string wordPath, string pdfPath)
    {
        bool result = false;
        Microsoft.Office.Interop.Word.Application application = new Microsoft.Office.Interop.Word.Application();
        Microsoft.Office.Interop.Word.Document document = null;
        try
        {
            //document.PageSetup.PaperSize = Microsoft.Office.Interop.Word.WdPaperSize.wdPaperA4;
            application.Visible = false;
            document = application.Documents.Open(wordPath);
            document.ExportAsFixedFormat(pdfPath, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF);
            result = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            result = false;
        }
        finally
        {
            document.Close();
        }
        return result;
    }

    /// <summary>
    /// Excel转Pdf
    /// </summary>
    /// <param name="sourcePath">word路径</param>
    /// <param name="targetPath">pdf路径</param>
    /// <param name="targetType"></param>
    /// <returns></returns>
    public static bool ExcelToPDF(string sourcePath, string targetPath, XlFixedFormatType targetType = Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF)
    {
        bool result;
        object missing = Type.Missing;
        Microsoft.Office.Interop.Excel.Application application = null;
        Microsoft.Office.Interop.Excel.Workbook workBook = null;
        try
        {
            application = new Microsoft.Office.Interop.Excel.Application();
            object target = targetPath;
            object type = targetType;
            workBook = application.Workbooks.Open(sourcePath, missing, missing, missing, missing, missing,
                    missing, missing, missing, missing, missing, missing, missing, missing, missing);

            workBook.ExportAsFixedFormat(targetType, target, XlFixedFormatQuality.xlQualityStandard, true, false, missing, missing, missing, missing);
            result = true;
        }
        catch
        {
            result = false;
        }
        finally
        {
            if (workBook != null)
            {
                workBook.Close(true, missing, missing);
                workBook = null;
            }
            if (application != null)
            {
                application.Quit();
                application = null;
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        return result;
    }

    /// <summary>
    /// 转pdf
    /// </summary>
    /// <param name="inputPath">word路径</param>
    /// <param name="pdfPath">pdf路径</param>
    /// <returns></returns>
    public static bool ConverToPdf(string inputPath, string pdfPath)
    {
        try
        {
            FileInfo f1 = new FileInfo(inputPath);
            string ext1 = f1.Extension.ToLower().Substring(1);

            switch (ext1)
            {
                case "xls":
                case "xlsx":
                    Aspose.Cells.Workbook book1 = new Aspose.Cells.Workbook(inputPath);
                    book1.Save(pdfPath, Aspose.Cells.SaveFormat.Pdf);
                    break;
                case "doc":
                case "docx":
                    Aspose.Words.Document doc = new Aspose.Words.Document(inputPath);
                    doc.Save(pdfPath);
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex1)
        {
            return false;
        }

        return true;
    }
}