using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tomato.StandardLib.MyBaseClass
{
    /// <summary>
    /// 视图查询模式
    /// </summary>
    public class SelectViewMode : IDisposable
    {
        internal bool IsSelectViewMode { get; private set; }
        internal TableInfo SelViewTable { get; set; }

        /// <summary>
        /// 开始视图查询模式
        /// </summary>
        internal SelectViewMode()
        {
            this.IsSelectViewMode = true;
        }

        /// <summary>
        /// 关闭视图查询模式
        /// </summary>
        public void Dispose()
        {
            this.IsSelectViewMode = false;
        }
    }
}
