namespace Tomato.NewTempProject.Model
{
    using Tomato.StandardLib.MyAttribute;
    using System;
    using System.Data;
    using Tomato.StandardLib.MyModel;
    using System.ComponentModel.DataAnnotations;
    using Tomato.StandardLib.FrameWork;

	/// <summary>
    /// 考核项目表
    /// </summary>
    [DBTableInfo("tb_Appraisals")]
    [Serializable]
    public partial class AppraisalsModel : BasePageModel
    {


        ///<summary>
        ///考核项目ID（Guid）
        ///</summary>
              
        public Guid? AppraisalsID
        {
            get { return _appraisalsID; }
            set { _appraisalsID = value; }
        }

        ///<summary>
        ///考核项目名称
        ///</summary>
        [RequiredAll(ErrorMessage = "考核项目名称不能为空")]        [StringLength(128,ErrorMessage = "考核项目名称长度不能超过128")]
        public string AppraisalsName
        {
            get { return _appraisalsName; }
            set { _appraisalsName = value; }
        }

        ///<summary>
        ///项目编号（模块项目ID对应）
        ///</summary>
              
        public Guid? ModelID
        {
            get { return _modelID; }
            set { _modelID = value; }
        }

        ///<summary>
        ///上级ID
        ///</summary>
              
        public Guid? ParentID
        {
            get { return _parentID; }
            set { _parentID = value; }
        }

        ///<summary>
        ///是否最下级(扩展字段，可选)
        ///</summary>
              
        public bool? IsEnd
        {
            get { return _isEnd; }
            set { _isEnd = value; }
        }

        ///<summary>
        ///计分规则取值SQL（对最下级节点设置）,用于考核系统
        ///</summary>
              
        public string ConfigSqlText1
        {
            get { return _configSqlText1; }
            set { _configSqlText1 = value; }
        }

        ///<summary>
        ///计分规则取值SQL（对最下级节点设置），用于工作量系统
        ///</summary>
              
        public string ConfigSqlText2
        {
            get { return _configSqlText2; }
            set { _configSqlText2 = value; }
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
        /// 考核项目ID（Guid）
        /// </summary>
        [DBFieldInfo(ColumnName = "AppraisalsID", IsIdentity = false, 
IsKey = true, SqlDbType =SqlDbType.UniqueIdentifier, OrderIndex = -1, OrderAsc = true)]
        protected Guid? _appraisalsID;


		/// <summary>
        /// 考核项目名称
        /// </summary>
        [DBFieldInfo(ColumnName = "AppraisalsName", IsIdentity = false, 
IsKey = false, SqlDbType =SqlDbType.NVarChar, OrderIndex = -1, OrderAsc = true, LikeEqual = EnumLikeMode.AllLike)]
        protected string _appraisalsName;


		/// <summary>
        /// 项目编号（模块项目ID对应）
        /// </summary>
        [DBFieldInfo(ColumnName = "ModelID", IsIdentity = false, 
IsKey = false, SqlDbType =SqlDbType.UniqueIdentifier, OrderIndex = -1, OrderAsc = true)]
        protected Guid? _modelID;


		/// <summary>
        /// 上级ID
        /// </summary>
        [DBFieldInfo(ColumnName = "ParentID", IsIdentity = false, 
IsKey = false, SqlDbType =SqlDbType.UniqueIdentifier, OrderIndex = -1, OrderAsc = true)]
        protected Guid? _parentID;


		/// <summary>
        /// 是否最下级(扩展字段，可选)
        /// </summary>
        [DBFieldInfo(ColumnName = "IsEnd", IsIdentity = false, 
IsKey = false, SqlDbType =SqlDbType.Bit, OrderIndex = -1, OrderAsc = true)]
        protected bool? _isEnd;


		/// <summary>
        /// 计分规则取值SQL（对最下级节点设置）,用于考核系统
        /// </summary>
        [DBFieldInfo(ColumnName = "ConfigSqlText1", IsIdentity = false, 
IsKey = false, SqlDbType =SqlDbType.NVarChar, OrderIndex = -1, OrderAsc = true, LikeEqual = EnumLikeMode.AllLike)]
        protected string _configSqlText1;


		/// <summary>
        /// 计分规则取值SQL（对最下级节点设置），用于工作量系统
        /// </summary>
        [DBFieldInfo(ColumnName = "ConfigSqlText2", IsIdentity = false, 
IsKey = false, SqlDbType =SqlDbType.NVarChar, OrderIndex = -1, OrderAsc = true, LikeEqual = EnumLikeMode.AllLike)]
        protected string _configSqlText2;


		/// <summary>
        /// 是否删除
        /// </summary>
        [DBFieldInfo(ColumnName = "IsDelete", IsIdentity = false, 
IsKey = false, SqlDbType =SqlDbType.Bit, OrderIndex = -1, OrderAsc = true)]
        protected bool? _isDelete=false;


        #endregion

          }
}
