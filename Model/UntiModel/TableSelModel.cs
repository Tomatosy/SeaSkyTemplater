using Tomato.StandardLib.MyAttribute;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tomato.StandardLib.MyModel;
using Tomato.NewTempProject.Model.Enum;

namespace Tomato.NewTempProject.Model
{
    [Serializable]
    public class TableSelModel
    {
        /// <summary>
        /// 页面条数
        /// </summary>
        public int? PageSize { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int? PageNO { get; set; }

        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 排序名称
        /// </summary>
        public string OrderByName { get; set; }

        /// <summary>
        /// 左连接主表名称
        /// </summary>
        public string JoinMasterTableName { get; set; }

        /// <summary>
        /// 列
        /// </summary>
        public List<TableColSelModel> ColSel { get; set; } = new List<TableColSelModel>();

        /// <summary>
        /// 列查询条件
        /// </summary>
        public List<TableColSelModel> WhereSel { get; set; } = new List<TableColSelModel>();
    }
}
