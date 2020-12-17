namespace Tomato.NewTempProject.Model
{
    using Tomato.StandardLib.MyAttribute;
    using System;
    using System.Data;
    using Tomato.StandardLib.MyModel;
    using System.ComponentModel.DataAnnotations;
    using Tomato.StandardLib.FrameWork;

	/// <summary>
    /// 模块明细表
    /// </summary>
    [DBTableInfo("tb_ModelDetail")]
    [Serializable]
    public partial class ModelDetailModel : BasePageModel
    {


        ///<summary>
        ///主键ID
        ///</summary>
              
        public Guid? ModelDetailID
        {
            get { return _modelDetailID; }
            set { _modelDetailID = value; }
        }

        ///<summary>
        ///项目编号
        ///</summary>
        [RequiredAll(ErrorMessage = "项目编号不能为空")]       
        public Guid? ModelID
        {
            get { return _modelID; }
            set { _modelID = value; }
        }

        ///<summary>
        ///列序号
        ///</summary>
        [RequiredAll(ErrorMessage = "列序号不能为空")]       
        public int? ColIndex
        {
            get { return _colIndex; }
            set { _colIndex = value; }
        }

        ///<summary>
        ///列名
        ///</summary>
        [RequiredAll(ErrorMessage = "列名不能为空")]        [StringLength(50,ErrorMessage = "列名长度不能超过50个字符")]
        public string ColName
        {
            get { return _colName; }
            set { _colName = value; }
        }

        ///<summary>
        ///文本备注
        ///</summary>
              
        public string ColMemo
        {
            get { return _colMemo; }
            set { _colMemo = value; }
        }

        ///<summary>
        ///字段类型（文本、数字、日期、布尔、下拉框、附件）
        ///</summary>
        [RequiredAll(ErrorMessage = "字段类型（文本、数字、日期、布尔、下拉框、附件）不能为空")]        [StringLength(50,ErrorMessage = "字段类型（文本、数字、日期、布尔、下拉框、附件）长度不能超过50个字符")]
        public string ColType
        {
            get { return _colType; }
            set { _colType = value; }
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
        [DBFieldInfo(ColumnName = "ModelDetailID", IsIdentity = false, 
IsKey = true, SqlDbType =SqlDbType.UniqueIdentifier, OrderIndex = -1, OrderAsc = true)]
        protected Guid? _modelDetailID;


		/// <summary>
        /// 项目编号
        /// </summary>
        [DBFieldInfo(ColumnName = "ModelID", IsIdentity = false, 
IsKey = false, SqlDbType =SqlDbType.UniqueIdentifier, OrderIndex = -1, OrderAsc = true)]
        protected Guid? _modelID;


		/// <summary>
        /// 列序号
        /// </summary>
        [DBFieldInfo(ColumnName = "ColIndex", IsIdentity = false, 
IsKey = false, SqlDbType =SqlDbType.Int, OrderIndex = -1, OrderAsc = true)]
        protected int? _colIndex;


		/// <summary>
        /// 列名
        /// </summary>
        [DBFieldInfo(ColumnName = "ColName", IsIdentity = false, 
IsKey = false, SqlDbType =SqlDbType.NVarChar, OrderIndex = -1, OrderAsc = true, LikeEqual = EnumLikeMode.AllLike)]
        protected string _colName;


		/// <summary>
        /// 文本备注
        /// </summary>
        [DBFieldInfo(ColumnName = "ColMemo", IsIdentity = false, 
IsKey = false, SqlDbType =SqlDbType.NVarChar, OrderIndex = -1, OrderAsc = true, LikeEqual = EnumLikeMode.AllLike)]
        protected string _colMemo;


		/// <summary>
        /// 字段类型（文本、数字、日期、布尔、下拉框、附件）
        /// </summary>
        [DBFieldInfo(ColumnName = "ColType", IsIdentity = false, 
IsKey = false, SqlDbType =SqlDbType.NVarChar, OrderIndex = -1, OrderAsc = true, LikeEqual = EnumLikeMode.AllLike)]
        protected string _colType;


		/// <summary>
        /// 是否删除
        /// </summary>
        [DBFieldInfo(ColumnName = "IsDelete", IsIdentity = false, 
IsKey = false, SqlDbType =SqlDbType.Bit, OrderIndex = -1, OrderAsc = true)]
        protected bool? _isDelete=false;


        #endregion

          }
}
