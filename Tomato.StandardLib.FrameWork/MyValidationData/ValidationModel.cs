/**************************************************************************
*   
*   =================================
*   CLR版本    ：4.0.30319.42000
*   命名空间    ：Tomato.StandardLib.FrameWork.MyValidationData
*   文件名称    ：ValidationModel.cs
*   =================================
*   创 建 者    ：Aeck
*   创建日期    ：2019/11/09 8:40:23 
*   邮箱        ：Aeck499@aliyun.com
*   个人主站    ：https://my.oschina.net/u/4123699
*   功能描述    ：
*   使用说明    ：
*   =================================
*   修改者    ：
*   修改日期    ：
*   修改内容    ：
*   =================================
*  
***************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Tomato.StandardLib.FrameWork
{
    #region 值类型

    #region 非空验证

    /// <summary>
    /// Guid?验证不等于NULL和EMPTY
    /// </summary>
    public class GuidNotNullAndEmptyAttribute : ValidationAttribute
    {
        public GuidNotNullAndEmptyAttribute() { }

        public override bool IsValid(object value)
        {
            if (value is Guid?)
            {
                Guid? obj = (Guid?)value;
                if (obj != null && obj != Guid.Empty)
                {
                    return true;
                }
            }
            return false;
        }
    }

    /// <summary>
    /// Bool?验证不等于NULL
    /// </summary>
    public class BoolNotNullAttribute : ValidationAttribute
    {
        public BoolNotNullAttribute() { }

        public override bool IsValid(object value)
        {
            if (value is bool?)
            {
                bool? obj = (bool?)value;
                if (obj != null)
                {
                    return true;
                }
            }
            return false;
        }
    }

    /// <summary>
    /// Decimal?验证不等于NULL
    /// </summary>
    public class DecimalNotNullAttribute : ValidationAttribute
    {

        public DecimalNotNullAttribute() { }

        public override bool IsValid(object value)
        {
            if (value is decimal?)
            {
                decimal? obj = (decimal?)value;
                if (obj != null)
                {
                    return true;
                }
            }
            return false;
        }
    }

    #endregion

    #region 值验证最小值和最大值验证

    /// <summary>
    /// 验证Decimal最小值
    /// </summary>
    public class DecimalMinValueAttribute : ValidationAttribute
    {
        public DecimalMinValueAttribute(decimal minValue) { MinValue = minValue; }

        public decimal MinValue { get; set; }

        public decimal? MaxValue { get; set; }

        public override bool IsValid(object value)
        {
            if (value is decimal?)
            {
                decimal? obj = (decimal?)value;
                if (MaxValue == null && obj != null && obj >= MinValue)
                {
                    return true;
                }
                else if (MaxValue >= obj && obj != null && obj >= MinValue)
                {
                    return true;
                }
            }
            return false;
        }
    }

    /// <summary>
    /// 验证INT最小值
    /// </summary>
    public class IntMinValueAttribute : ValidationAttribute
    {
        public IntMinValueAttribute(int minValue) { MinValue = minValue; }

        public int MinValue { get; set; }

        public int? MaxValue { get; set; }

        public override bool IsValid(object value)
        {
            if (value is int?)
            {
                int? obj = (int?)value;
                if (obj != null && MaxValue == null && obj >= MinValue)
                {
                    return true;
                }
                else if (obj != null && MaxValue >= obj && obj >= MinValue)
                {
                    return true;
                }
            }
            return false;
        }
    }

    #endregion

    #endregion


}
