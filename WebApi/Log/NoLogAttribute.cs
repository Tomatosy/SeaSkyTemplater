using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tomato.NewTempProject.WebApi.Log
{
    /// <summary>
    /// 不需要日志记录的过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true)]
    public class NoLogAttribute : Attribute
    {

    }
}