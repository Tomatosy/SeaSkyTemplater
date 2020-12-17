using System;
using System.Collections.Generic;
using System.Text;

namespace Tomato.StandardLib.MyModel
{
    /// <summary>
    /// �Զ�����������
    /// </summary>
    [Serializable]
    public class ResultInfo
    {
        /// <summary>
        /// �Ƿ�����ɹ�
        /// </summary>
        private bool m_successed = true;

        /// <summary>
        /// �����������
        /// </summary>
        private int m_failReason = -1;

        /// <summary>
        /// ��������
        /// </summary>
        private string m_failReasonDesc;

        /// <summary>
        /// Ӱ����������
        /// </summary>
        private int m_rowAffected = 1;

        /// <summary>
        /// ������Դ
        /// </summary>
        private object m_originValue;

        /// <summary>
        /// ��������ǩ
        /// </summary>
        private object m_tag;

        /// <summary>
        /// ����Ĭ�ϲ������
        /// Ĭ�Ͻ��Ϊ�ɹ�
        /// </summary>
        public ResultInfo()
        {
        }

        /// <summary>
        /// ����Ĭ�ϴ���������
        /// </summary>
        /// <param name="failReason">��������</param>
        public ResultInfo(int failReason)
        {
            this.m_successed = false;
            this.m_failReason = failReason;
        }

        /// <summary>
        /// �������������
        /// </summary>
        /// <param name="failReason">��������</param>
        /// <param name="originValue">������Դ</param>
        public ResultInfo(int failReason, object originValue) : this(failReason)
        {
            this.m_originValue = originValue;
        }

        /// <summary>
        /// ��ȡ�������Ƿ�ɹ�
        /// </summary>
        public bool Successed
        {
            get { return this.m_successed; }
            set { this.m_successed = value; }
        }

        /// <summary>
        /// ��ȡ������Ӱ����������
        /// </summary>
        public int RowAffected
        {
            get { return this.m_rowAffected; }
            set { this.m_rowAffected = value; }
        }

        /// <summary>
        /// ��ȡ�����ô�������
        /// </summary>
        public string FailReasonDesc
        {
            get 
            {
                if (this.m_failReasonDesc == null)
                {
                    switch (this.m_failReason)
                    {
                        case 1:
                            return string.Format("�Ѵ��ڸü�¼: {0}��", this.m_originValue);
                        case 2:
                            return string.Format("�Ѵ�����ͬ�����ļ�¼{0}{1}", this.m_originValue == null ? "��" : ": ", this.m_originValue);
                        case 3:
                            return "û�и����κμ�¼��";
                        case 4:
                            return "û��ɾ���κμ�¼��";
                        case 5:
                            return string.Format("�����ڸü�¼: {0}��", this.m_originValue);
                        case 6:
                            return "Parent����ָ���Լ���Child��";
                        default:
                            return "δ֪ԭ��";
                    }
                }
                else
                {
                    return this.m_failReasonDesc;
                }
            }

            set 
            {
                this.m_failReasonDesc = value;
            }
        }

        /// <summary>
        /// ��ȡ�����ô������
        /// </summary>
        public int FailReason
        {
            get { return this.m_failReason; }
            set { this.m_failReason = value; }
        }

        /// <summary>
        /// ��ȡ�����ô�����Դ
        /// </summary>
        public object OriginValue
        {
            get { return this.m_originValue; }
            set { this.m_originValue = value; }
        }

        /// <summary>
        /// ��ȡ�����ô�������ǩ
        /// </summary>
        public object Tag
        {
            get { return this.m_tag; }
            set { this.m_tag = value; }
        }

        /// <summary>
        /// ��������������
        /// </summary>
        /// <param name="errorMsg">��������</param>
        /// <returns>����������</returns>
        public static ResultInfo UnSuccessResultInfo(string errorMsg)
        {
            ResultInfo rst = new ResultInfo();
            rst.Successed = false;
            rst.FailReasonDesc = errorMsg;
            return rst;
        }

        /// <summary>
        /// �����ɹ��������
        /// </summary>
        /// <returns>�ɹ��������</returns>
        public static ResultInfo SuccessResultInfo()
        {
            return SuccessResultInfo(string.Empty);
        }

        /// <summary>
        /// �������������ĳɹ��������
        /// </summary>
        /// <param name="infoMsg">�ɹ�����</param>
        /// <returns>���������ĳɹ��������</returns>
        public static ResultInfo SuccessResultInfo(string infoMsg)
        {
            ResultInfo rst = new ResultInfo();
            rst.Successed = true;
            rst.FailReasonDesc = infoMsg;
            return rst;
        }
    }
}
