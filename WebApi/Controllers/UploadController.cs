using Microsoft.Practices.Unity;
using Tomato.StandardLib.MyModel;
using System.Collections.Generic;
using System.Web.Http;
using Tomato.NewTempProject.BLL;
using Tomato.NewTempProject.Model;
using System;
using Tomato.NewTempProject.WebApi.Log;
using System.Net.Http;
using Tomato.StandardLib.MyExtensions;
using Tomato.SumhsAuditPreWarning.WebApi;
using System.Linq;
using System.Data;
using Tomato.SumhsAuditPreWarning.WebApi.Common;
using Tomato.SumhsAuditPreWarning.WebApi.Common;
using System.IO;
using Tomato.NewTempProject.Model.Enum;
using NPOI.SS.UserModel;
using System.Runtime.InteropServices;
//using Spire.Xls;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using Newtonsoft.Json;

namespace Tomato.NewTempProject.WebApi.Controllers
{

    public class UploadController : ApiController
    {

        //private readonly IAcademicYearService AcademicYearService = ApplicationContext.Current.UnityContainer.Resolve<IAcademicYearService>();
        //private readonly IDicService IDicService = ApplicationContext.Current.UnityContainer.Resolve<IDicService>();



        ///// <summary>
        ///// 替换word模板
        ///// </summary>
        ///// <param name="model">ViewModel</param>
        ///// <returns>ViewModel</returns>
        //public BaseResultModel<string> WordReplace(string vchYear, string vchMonth)
        //{
        //    try
        //    {
        //        Dictionary<string, string> dic = new Dictionary<string, string>{
        //         { "@VchYear",vchYear},
        //         { "@VchMonth",vchMonth},
        //    };

        //        string templatePath = System.AppDomain.CurrentDomain.BaseDirectory + @"upload/template.docx";
        //        string savePath = System.AppDomain.CurrentDomain.BaseDirectory + @"upload/AAA.docx";
        //        WordTemplateHelper.WriteToPublicationOfResult(templatePath, savePath, dic);

        //        return new SuccessResultModel<string>(savePath);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ErrorResultModel<string>(EnumErrorCode.业务执行失败, "error:" + ex.Message);
        //    }
        //}

        ///// <summary>
        ///// 操作office
        ///// </summary>
        ///// <param name="ID">ID</param>
        ///// <returns></returns>
        //[HttpPost]
        //[NoLog]
        //public BaseResultModel<string> OfficeAction(AcademicYearViewModel model)
        //{
        //    try
        //    {
        //        string baseUrl = System.AppDomain.CurrentDomain.BaseDirectory;
        //        List<DicViewModel> listResult = IDicService.ListViewPageDic(null)?.Data?.ListData?.ToList();
        //        DataTable excelTalbe = DataTableHelper.ToDataTable<DicViewModel>(listResult);

        //        #region 导出表头
        //        Dictionary<string, string> dic = new Dictionary<string, string>(){
        //    { "UnitCode","单位编号"},
        //    { "UnitName","单位名称"}
        //};
        //        NopiExcel.ListToExcel(dic, "Template.xls");
        //        #endregion


        //        #region List导出excel
        //        dic = new Dictionary<string, string>(){
        //    { "AcademicYearName","学年"},
        //    { "Term","学期"}
        //};
        //        if (listResult.Count > 0)
        //        {
        //            NopiExcel.ListToExcel(listResult, "AAAA.xls", dic);
        //        }
        //        #endregion

        //        #region DataTable导出Excel
        //        PdfConverHelper.ExcelTableColModify(excelTalbe);
        //        //DataTable  -->  文件流
        //        Stream sam = NopiExcel.StreamFromDataTable(excelTalbe, ".xls");
        //        MemoryStream ms = NopiExcel.StreamToMemoryStream(sam);
        //        NopiExcel.DownloadUploadFile(ms, "情况表", ".xls");
        //        #endregion

        //        #region Excel、Word 转Pdf

        //        // 依赖于office 服务器需要安装office组件
        //        PdfConverHelper.WordToPDF(baseUrl + "upload/AAA.docx", baseUrl + "upload/template.pdf");
        //        PdfConverHelper.ExcelToPDF(baseUrl + "upload/BBB.xls", baseUrl + "upload/template1.pdf");

        //        // 内存占用高，大文件容易溢出
        //        PdfConverHelper.ConverToPdf(baseUrl + "upload/BBB.xls", baseUrl + "upload/template3.pdf");
        //        PdfConverHelper.ConverToPdf(baseUrl + "upload/AAA.docx", baseUrl + "upload/template2.pdf");
        //        #endregion

        //        #region 常规流操作
        //        //DataTable  -->  文件流
        //        Stream stream = NopiExcel.StreamFromDataTable(excelTalbe, ".xls");

        //        //stream一个文件流只能使用一次 ,多次使用需拷贝流，且只能拷贝一次 
        //        //文件流  -->  DataTable
        //        DataTable dt = NopiExcel.ExcelToTable(stream, ".xls", 0, 0);

        //        //Excel文件  -->  DataTable      通过文件路径转为DataTable
        //        DataTable dt1 = NopiExcel.ExcelToDataTable(baseUrl + @"upload/CCC.xls");

        //        //stream  -->  Excel文件
        //        NopiExcel.WriteSteamToFile(NopiExcel.StreamToMemoryStream(stream), baseUrl + @"/upload/123.xls");
        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        LogWriter.WriteLog(EnumLogLevel.Fatal, "OfficeAction", JsonConvert.SerializeObject(model), "UploadController", "操作office 发生错误.", ex);
        //        return new ErrorResultModel<string>(EnumErrorCode.系统异常, "操作office 发生错误！");
        //    }
        //    return null;
        //}
    }


}
