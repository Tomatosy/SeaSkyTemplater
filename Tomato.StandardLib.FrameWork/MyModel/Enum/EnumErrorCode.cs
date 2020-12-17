using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tomato.StandardLib.MyModel
{
    /// <summary>
    /// 错误代码枚举
    /// </summary>
    public struct EnumErrorCode
    {
        /// <summary>
        /// 系统异常
        /// </summary>
        public const string 系统异常 = "1000";
        /// <summary>
        /// 请求参数错误
        /// </summary>
        public const string 请求参数错误 = "2000";
        /// <summary>
        /// 参数校验未通过
        /// </summary>
        public const string 参数校验未通过 = "3000";
        /// <summary>
        /// 业务执行失败
        /// </summary>
        public const string 业务执行失败 = "4000";
        /// <summary>
        /// 未登入
        /// </summary>
        public const string 未登入 = "5000";
        /// <summary>
        /// 没有权限
        /// </summary>
        public const string 没有权限 = "6000";
    }
}
