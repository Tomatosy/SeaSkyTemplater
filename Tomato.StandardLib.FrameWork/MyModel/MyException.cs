using System;

namespace Tomato.StandardLib.MyModel
{
    /// <summary>
    /// �Զ����쳣��
    /// </summary>
    [Serializable]
    public class MyException : ApplicationException
    {
        /// <summary>
        /// ����д�����ݿ���
        /// </summary>
        private bool m_writeToDatabase = true;

        /// <summary>
        /// �����Ƿ���ʾ���
        /// </summary>
        private bool m_showError = true;

        /// <summary>
        /// ���ݴ������������Զ����쳣��
        /// </summary>
        /// <param name="message">��������</param>
        public MyException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// ���ݲ��������Զ����쳣��
        /// </summary>
        /// <param name="message">��������</param>
        /// <param name="writeToDatabase">�Ƿ�д�����ݿ�</param>
        /// <param name="showError">�Ƿ���ʾ����</param>
        public MyException(string message, bool writeToDatabase, bool showError)
            : base(message)
        {
            this.m_writeToDatabase = writeToDatabase;
            this.m_showError = showError;
        }

        /// <summary>
        /// ���ݲ��������Զ����쳣��
        /// </summary>
        /// <param name="message">��������</param>
        /// <param name="writeToDatabase">�Ƿ�д�����ݿ�</param>
        public MyException(string message, bool writeToDatabase)
            : base(message)
        {
            this.m_writeToDatabase = writeToDatabase;
        }

        /// <summary>
        /// �Ƿ�Ѵ˴�����ϸ��Ϣд�����ݿ�
        /// </summary>
        public bool WriteToDatabase
        {
            get { return this.m_writeToDatabase; }
            set { this.m_writeToDatabase = value; }
        }

        /// <summary>
        /// �Ƿ���ʾ�˴���
        /// </summary>
        public bool ShowError
        {
            get { return this.m_showError; }
            set { this.m_showError = value; }
        }
    }
}
