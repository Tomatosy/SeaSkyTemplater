using System;

namespace Tomato.StandardLib.MyBaseClass
{
    /// <summary>
    /// 模糊查询模式
    /// </summary>
    public class LikeMode : IDisposable
    {
        internal bool IsLikeMode { get; private set; }

        /// <summary>
        /// 开始模糊查询模式
        /// </summary>
        internal LikeMode()
        {
            this.IsLikeMode = true;
        }

        /// <summary>
        /// 关闭模糊查询模式
        /// </summary>
        public void Dispose()
        {
            this.IsLikeMode = false;
        }
    }
}
