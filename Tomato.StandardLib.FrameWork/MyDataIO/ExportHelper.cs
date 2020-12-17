using Tomato.StandardLib.MyModel;
using System.Data;
using System.Text;

namespace Tomato.StandardLib.MyDataIO
{
    /// <summary>
    /// 数据导出接口
    /// </summary>
    public interface IExportBase
    {
        /// <summary>
        /// 数据导出
        /// </summary>
        /// <param name="descCol">导出列描述集合</param>
        /// <param name="dataSource">导出数据源</param>
        /// <returns>导出结果</returns>
        ResultInfo Export(DataExportDescCol descCol, DataTable dataSource);

        /// <summary>
        /// 数据导出
        /// </summary>
        /// <param name="descCol">导出列描述集合</param>
        /// <param name="dataSource">导出数据源</param>
        /// <param name="Encoding">Encoding</param>
        /// <returns>导出结果</returns>
        ResultInfo Export(DataExportDescCol descCol, DataTable dataSource,Encoding Encoding);
    }

    /// <summary>
    /// 数据导出接口工厂类
    /// </summary>
    public class ExportHelper
    {
        /// <summary>
        /// 根据导出命令描述，创建数据导出操作类
        /// </summary>
        /// <param name="orderInfo">导出命令描述</param>
        /// <returns>数据导出操作类</returns>
        public static IExportBase CreateExportHelper(DataExportOrderInfo orderInfo)
        {
            IExportBase ex = null;
            switch (orderInfo.FileType)
            {
                case EnumIOFileType.Excel:
                    ex = new ExportToExcel(orderInfo);
                    break;
                case EnumIOFileType.Txt:
                    ex = new ExportToTxt(orderInfo);
                    break;
            }

            return ex;
        }
    }
}
