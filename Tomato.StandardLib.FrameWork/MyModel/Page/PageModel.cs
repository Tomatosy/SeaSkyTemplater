using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tomato.StandardLib.MyModel
{
    /// <summary>
    /// 分页model
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class PageModel<T> : BasePageModel
    {
        /// <summary>
        /// 数据
        /// </summary>
        public IEnumerable<T> ListData { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages
        {
            get
            {
                return (DataCount + (PageSize ?? 10) - 1) / (PageSize ?? 10);
            }
        }

        /// <summary>
        /// 数据量
        /// </summary>
        public int DataCount { get; set; }
    }
}
