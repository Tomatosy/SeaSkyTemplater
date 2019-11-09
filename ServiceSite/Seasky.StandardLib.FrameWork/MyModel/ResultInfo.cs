using System;
using System.Collections.Generic;
using System.Text;

namespace SeaSky.StandardLib.MyModel
{
    /// <summary>
    /// 自定义操作结果类
    /// </summary>
    [Serializable]
    public class ResultInfo
    {
        /// <summary>
        /// 是否操作成功
        /// </summary>
        private bool m_successed = true;

        /// <summary>
        /// 错误描述序号
        /// </summary>
        private int m_failReason = -1;

        /// <summary>
        /// 错误描述
        /// </summary>
        private string m_failReasonDesc;

        /// <summary>
        /// 影响数据行数
        /// </summary>
        private int m_rowAffected = 1;

        /// <summary>
        /// 错误来源
        /// </summary>
        private object m_originValue;

        /// <summary>
        /// 错误辅助标签
        /// </summary>
        private object m_tag;

        /// <summary>
        /// 构造默认操作结果
        /// 默认结果为成功
        /// </summary>
        public ResultInfo()
        {
        }

        /// <summary>
        /// 构造默认错误操作结果
        /// </summary>
        /// <param name="failReason">错误描述</param>
        public ResultInfo(int failReason)
        {
            this.m_successed = false;
            this.m_failReason = failReason;
        }

        /// <summary>
        /// 构造错误操作结果
        /// </summary>
        /// <param name="failReason">错误描述</param>
        /// <param name="originValue">错误来源</param>
        public ResultInfo(int failReason, object originValue) : this(failReason)
        {
            this.m_originValue = originValue;
        }

        /// <summary>
        /// 获取或设置是否成功
        /// </summary>
        public bool Successed
        {
            get { return this.m_successed; }
            set { this.m_successed = value; }
        }

        /// <summary>
        /// 获取或设置影响数据行数
        /// </summary>
        public int RowAffected
        {
            get { return this.m_rowAffected; }
            set { this.m_rowAffected = value; }
        }

        /// <summary>
        /// 获取或设置错误描述
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
                            return string.Format("已存在该记录: {0}。", this.m_originValue);
                        case 2:
                            return string.Format("已存在相同主键的纪录{0}{1}", this.m_originValue == null ? "。" : ": ", this.m_originValue);
                        case 3:
                            return "没有更新任何记录。";
                        case 4:
                            return "没有删除任何记录。";
                        case 5:
                            return string.Format("不存在该记录: {0}。", this.m_originValue);
                        case 6:
                            return "Parent不能指向自己的Child。";
                        default:
                            return "未知原因。";
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
        /// 获取或设置错误序号
        /// </summary>
        public int FailReason
        {
            get { return this.m_failReason; }
            set { this.m_failReason = value; }
        }

        /// <summary>
        /// 获取或设置错误来源
        /// </summary>
        public object OriginValue
        {
            get { return this.m_originValue; }
            set { this.m_originValue = value; }
        }

        /// <summary>
        /// 获取或设置错误辅助标签
        /// </summary>
        public object Tag
        {
            get { return this.m_tag; }
            set { this.m_tag = value; }
        }

        /// <summary>
        /// 创建错误操作结果
        /// </summary>
        /// <param name="errorMsg">错误描述</param>
        /// <returns>错误操作结果</returns>
        public static ResultInfo UnSuccessResultInfo(string errorMsg)
        {
            ResultInfo rst = new ResultInfo();
            rst.Successed = false;
            rst.FailReasonDesc = errorMsg;
            return rst;
        }

        /// <summary>
        /// 创建成功操作结果
        /// </summary>
        /// <returns>成功操作结果</returns>
        public static ResultInfo SuccessResultInfo()
        {
            return SuccessResultInfo(string.Empty);
        }

        /// <summary>
        /// 创建带有描述的成功操作结果
        /// </summary>
        /// <param name="infoMsg">成功描述</param>
        /// <returns>带有描述的成功操作结果</returns>
        public static ResultInfo SuccessResultInfo(string infoMsg)
        {
            ResultInfo rst = new ResultInfo();
            rst.Successed = true;
            rst.FailReasonDesc = infoMsg;
            return rst;
        }
    }
}
