using System;

namespace SeaSky.StandardLib.MyModel
{
    /// <summary>
    /// 自定义异常类
    /// </summary>
    [Serializable]
    public class MyException : ApplicationException
    {
        /// <summary>
        /// 错误写入数据库标记
        /// </summary>
        private bool m_writeToDatabase = true;

        /// <summary>
        /// 错误是否显示标记
        /// </summary>
        private bool m_showError = true;

        /// <summary>
        /// 根据错误描述构造自定义异常类
        /// </summary>
        /// <param name="message">错误描述</param>
        public MyException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 根据参数构造自定义异常类
        /// </summary>
        /// <param name="message">错误描述</param>
        /// <param name="writeToDatabase">是否写入数据库</param>
        /// <param name="showError">是否显示错误</param>
        public MyException(string message, bool writeToDatabase, bool showError)
            : base(message)
        {
            this.m_writeToDatabase = writeToDatabase;
            this.m_showError = showError;
        }

        /// <summary>
        /// 根据参数构造自定义异常类
        /// </summary>
        /// <param name="message">错误描述</param>
        /// <param name="writeToDatabase">是否写入数据库</param>
        public MyException(string message, bool writeToDatabase)
            : base(message)
        {
            this.m_writeToDatabase = writeToDatabase;
        }

        /// <summary>
        /// 是否把此错误详细信息写入数据库
        /// </summary>
        public bool WriteToDatabase
        {
            get { return this.m_writeToDatabase; }
            set { this.m_writeToDatabase = value; }
        }

        /// <summary>
        /// 是否显示此错误
        /// </summary>
        public bool ShowError
        {
            get { return this.m_showError; }
            set { this.m_showError = value; }
        }
    }
}
