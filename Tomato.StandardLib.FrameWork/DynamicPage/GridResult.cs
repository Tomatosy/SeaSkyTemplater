using System;
using System.Collections.Generic;
using System.Text;

namespace Tomato.StandardLib.DynamicPage
{
    /// <summary>
    /// 列表返回数据结构
    /// </summary>
    public class GridResult
    {
        public GridResult()
        {
            msg = "success";
            code = 0;//如果StateCode=0，则layUI-Table不显示Message
        }
        /// <summary>
        /// code码
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 记录总条数
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// 记录
        /// </summary>
        public object data { get; set; }
    }
}
