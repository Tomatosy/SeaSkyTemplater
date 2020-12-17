using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tomato.StandardLib.MyModel
{
    /// <summary>
    /// 查询分页描述类
    /// </summary>
    public class PageInfo
    {
        /// <summary>
        /// 初始化分页描述
        /// </summary>
        /// <param name="requirSQL">原始sql</param>
        /// <param name="orderBy">排序</param>
        /// <param name="pageSize">每页显示数</param>
        /// <param name="pageNO">页数</param>
        /// <param name="preSql">前置sql语句</param>
        public PageInfo(string requirSQL, string orderBy, int pageSize = 10, int pageNO = 1, string preSql = "")
        {
            this.PageSize = pageSize;
            this.CurrentPageIndex = pageNO;
            this.RequirSQL = requirSQL;
            this.OrderBy = orderBy;
            this.PreSql = preSql;
        }

        private const string defaultOrderBy = " ORDER BY [GmtCreateDate] DESC ";

        private string PreSql { get; set; }

        private int PageSize { get; set; }

        private int CurrentPageIndex { get; set; }

        private int CurrentStartRecordIndex
        {
            get
            {
                return (this.CurrentPageIndex - 1) * this.PageSize;
            }
        }

        private int CurrentEndRecordIndex
        {
            get
            {
                return this.CurrentPageIndex * this.PageSize;
            }
        }

        private string RequirSQL { get; set; }

        private string OrderBy { get; set; }

        /// <summary>
        /// 分页sql语句
        /// </summary>
        public string PageSQL
        {
            get
            {
                string s = @"{4}
                            WITH RequirSQL AS
                            ({0}),
                            PageSQL AS
                            (SELECT * FROM (SELECT ROW_NUMBER() OVER({1}) AS PAGEID,* FROM RequirSQL ) RequirPage
                            WHERE RequirPage.PAGEID > {2} AND RequirPage.PAGEID <= {3})
                            SELECT * FROM PageSQL";
                this.OrderBy = string.IsNullOrWhiteSpace(this.OrderBy) ? defaultOrderBy : this.OrderBy;
                return string.Format(s, this.RequirSQL, this.OrderBy, this.CurrentStartRecordIndex, this.CurrentEndRecordIndex, this.PreSql);
            }
        }

        /// <summary>
        /// 查询记录数sql语句
        /// </summary>
        public string PageCountSQL
        {
            get
            {
                string s = @"{1}                            
                            WITH RequirSQL AS
                            ({0}),
                            PageCountSQL AS
                            (SELECT COUNT(*) AS PAGECOUNT FROM RequirSQL )
                            SELECT PAGECOUNT FROM PageCountSQL";
                return string.Format(s, this.RequirSQL, this.PreSql);
            }
        }
    }
}
