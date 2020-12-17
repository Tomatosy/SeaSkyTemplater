using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tomato.StandardLib.MyModel
{
    /// <summary>
    /// 模糊查询模式
    /// </summary>
    public enum EnumLikeMode
    {
        /// <summary>
        /// 前%模糊查询
        /// </summary>
        ForwardLike,   
        /// <summary>
        /// 后%模糊查询
        /// </summary>
        BackwardLike,
        /// <summary>
        /// 前后%%模糊查询
        /// </summary>
        AllLike,
        /// <summary>
        /// 不模糊查询
        /// </summary>
        NoLike
    }
}
