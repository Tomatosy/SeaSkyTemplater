using System.Collections.Generic;

namespace Tomato.StandardLib.DAL.Base
{
    /// <summary>
    /// 表单信息
    /// </summary>
    public class TableInfo
    {
        private List<ColumnInfo> m_columns;

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 列集合
        /// </summary>
        public List<ColumnInfo> Columns
        {
            get
            {
                if (this.m_columns == null)
                {
                    this.m_columns = new List<ColumnInfo>();
                }

                return this.m_columns;
            }
        }
    }
}
