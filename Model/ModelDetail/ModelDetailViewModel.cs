namespace Tomato.NewTempProject.Model
{
    using Tomato.StandardLib.MyAttribute;
    using System.Data;
    using Tomato.StandardLib.MyModel;
    using System;

    /// <summary>
    /// 学年表输出扩展类
    /// </summary>
    [DBTableInfo("View_tb_ModelDetail")]
    public partial class ModelDetailViewModel : ModelDetailModel
    {
        ///<summary>
        ///主键编号
        ///</summary>
        public string ModelCode
        {
            get { return _modelCode; }
            set { _modelCode = value; }
        }

        ///<summary>
        ///项目名称
        ///</summary>
        public string ModelName
        {
            get { return _modelName; }
            set { _modelName = value; }
        }
        #region private property

        /// <summary>
        /// 主键编号
        /// </summary>
        [DBFieldInfo(ColumnName = "ModelCode", IsIdentity = false,
IsKey = false, SqlDbType = SqlDbType.NVarChar, OrderIndex = -1, OrderAsc = true, LikeEqual = EnumLikeMode.AllLike)]
        protected string _modelCode;


        /// <summary>
        /// 项目名称
        /// </summary>
        [DBFieldInfo(ColumnName = "ModelName", IsIdentity = false,
IsKey = false, SqlDbType = SqlDbType.NVarChar, OrderIndex = -1, OrderAsc = true, LikeEqual = EnumLikeMode.AllLike)]
        protected string _modelName;


        #endregion

    }
}
