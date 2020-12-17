using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tomato.StandardLib.MyModel
{
    /// <summary>
    /// 分页model
    /// </summary>
    [Serializable]
    public abstract class BasePageModel : SystemModel
    {
        /// <summary>
        /// 每页显示
        /// </summary>
        public int? PageSize { get; set; }
        /// <summary>
        /// 页数
        /// </summary>
        public int? PageNO { get; set; }
    }
}
