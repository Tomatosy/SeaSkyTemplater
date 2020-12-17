namespace Tomato.NewTempProject.Model
{
    using Tomato.StandardLib.MyAttribute;
    using System;
    using System.Data;
    using Tomato.StandardLib.MyModel;
    using System.ComponentModel.DataAnnotations;
    using Tomato.StandardLib.FrameWork;

    /// <summary>
    /// 模块表
    /// </summary>
    [DBTableInfo("tb_Model")]
    [Serializable]
    public partial class ModelModel : BasePageModel
    {


        ///<summary>
        ///主键ID
        ///</summary>

        public Guid? ModelID
        {
            get { return _modelID; }
            set { _modelID = value; }
        }

        ///<summary>
        ///主键编号
        ///</summary>
        [RequiredAll(ErrorMessage = "主键编号不能为空")]
        [StringLength(50, ErrorMessage = "主键编号长度不能超过50个字符")]
        public string ModelCode
        {
            get { return _modelCode; }
            set { _modelCode = value; }
        }

        ///<summary>
        ///项目名称
        ///</summary>
        [RequiredAll(ErrorMessage = "项目名称不能为空")]
        [StringLength(50, ErrorMessage = "项目名称长度不能超过50个字符")]
        public string ModelName
        {
            get { return _modelName; }
            set { _modelName = value; }
        }

        ///<summary>
        ///明细表模板ID（需要明细表时）
        ///</summary>

        public Guid? MouldID
        {
            get { return _mouldID; }
            set { _mouldID = value; }
        }

        ///<summary>
        ///排序号
        ///</summary>

        public int? OrderNum
        {
            get { return _orderNum; }
            set { _orderNum = value; }
        }

        ///<summary>
        ///是否删除
        ///</summary>
        [RequiredAll(ErrorMessage = "是否删除不能为空")]
        public bool? IsDelete
        {
            get { return _isDelete; }
            set { _isDelete = value; }
        }

        #region private property


        /// <summary>
        /// 主键ID
        /// </summary>
        [DBFieldInfo(ColumnName = "ModelID", IsIdentity = false,
IsKey = true, SqlDbType = SqlDbType.UniqueIdentifier, OrderIndex = -1, OrderAsc = true)]
        protected Guid? _modelID;


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


        /// <summary>
        /// 明细表模板ID（需要明细表时）
        /// </summary>
        [DBFieldInfo(ColumnName = "MouldID", IsIdentity = false,
IsKey = false, SqlDbType = SqlDbType.UniqueIdentifier, OrderIndex = -1, OrderAsc = true)]
        protected Guid? _mouldID;


        /// <summary>
        /// 排序号
        /// </summary>
        [DBFieldInfo(ColumnName = "OrderNum", IsIdentity = false,
IsKey = false, SqlDbType = SqlDbType.Int, OrderIndex = -1, OrderAsc = true)]
        protected int? _orderNum;


        /// <summary>
        /// 是否删除
        /// </summary>
        [DBFieldInfo(ColumnName = "IsDelete", IsIdentity = false,
IsKey = false, SqlDbType = SqlDbType.Bit, OrderIndex = -1, OrderAsc = true)]
        protected bool? _isDelete = false;


        #endregion

    }
}
