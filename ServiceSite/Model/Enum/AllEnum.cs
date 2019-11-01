using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaSky.SyTemplater.Model.Enum
{
    public enum EnumLogLevel
    {
        Fatal,
        Error,
        Warning,
        Info,
        Debug
    }
    public enum EnumPersonnelMaintenanceType
    {
        /// <summary>
        /// 无
        /// </summary>
        [Description("无")]
        None = 0,

        /// <summary>
        /// 事务所
        /// </summary>
        [Description("事务所")]
        Office = 1,

        /// <summary>
        /// 老师
        /// </summary>
        [Description("老师")]
        Teacher = 2,
    }


    // 值== item变量截出来的         枚举的值用来排序（循环是根据枚举值来的）
    public enum EnumQuestionnaireType
    {

        /// <summary>
        /// 无
        /// </summary>
        [Description("无")]
        None = 0,

        /// <summary>
        /// 本单位经济目标（如KPI目标）完成情况
        /// </summary>
        [Description("本单位经济目标（如KPI目标）完成情况")]
        Value1 = 1,

        /// <summary>
        /// 本单位经济活动中所做出的业绩及创新举措
        /// </summary>
        [Description("本单位经济活动中所做出的业绩及创新举措")]
        Value2 = 2,

        /// <summary>
        /// 本单位遵守国家财经法规、履行领导干部经济责任和执行学校财经制度情况
        /// </summary>
        [Description("本单位遵守国家财经法规、履行领导干部经济责任和执行学校财经制度情况")]
        Value3 = 3,

        /// <summary>
        /// 本单位“三重一大”决策的制定和执行情况
        /// </summary>
        [Description("本单位“三重一大”决策的制定和执行情况")]
        Value4 = 4,

        /// <summary>
        /// 本单位预算执行以及财务收支的真实、合法和效益情况
        /// </summary>
        [Description("本单位预算执行以及财务收支的真实、合法和效益情况")]
        Value5 = 5,

        /// <summary>
        /// 本单位资产质量情况（如资产安全完整性）	
        /// </summary>
        [Description("本单位资产质量情况（如资产安全完整性）	")]
        Value6 = 6,

        /// <summary>
        /// 本单位内部管理制度（如资产管理、合同管理、公章管理、绩效分配管理等）的建立情况
        /// </summary>
        [Description("本单位内部管理制度（如资产管理、合同管理、公章管理、绩效分配管理等）的建立情况")]
        Value7 = 7,

        /// <summary>
        /// 本单位内部管理制度的执行情况和管理工作公开情况
        /// </summary>
        [Description("本单位内部管理制度的执行情况和管理工作公开情况")]
        Value8 = 8,

        /// <summary>
        /// 被审计领导干部本人遵守有关廉政规定的情况（包括遵守廉洁从业规定和执行薪酬纪律情况）
        /// </summary>
        [Description("被审计领导干部本人遵守有关廉政规定的情况（包括遵守廉洁从业规定和执行薪酬纪律情况）")]
        Value9 = 9,

        /// <summary>
        /// 对上一轮审计中发现问题的整改落实情况
        /// </summary>
        [Description("对上一轮审计中发现问题的整改落实情况")]
        Value10 = 10,
    }
    public enum EnumQuestionnaireValue
    {
        /// <summary>
        /// 好
        /// </summary>
        [Description("好")]
        Good = 1,

        /// <summary>
        /// 较好
        /// </summary>
        [Description("较好")]
        Better = 2,

        /// <summary>
        /// 中
        /// </summary>
        [Description("中")]
        Medium = 3,

        /// <summary>
        /// 较差
        /// </summary>
        [Description("较差")]
        ComparePoor = 4,

        /// <summary>
        /// 差
        /// </summary>
        [Description("差")]
        Poor = 5,

        /// <summary>
        /// 弃权
        /// </summary>
        [Description("弃权")]
        Abstained = 6,
    }

    /// <summary>
    /// 枚举Model
    /// </summary>
    public class EnumModel
    {
        /// <summary>
        /// 枚举值   
        /// </summary>
        public int EnumValue { get; set; }

        /// <summary>
        /// 枚举介绍
        /// </summary>
        public string EnumDes { get; set; }
    }

}
