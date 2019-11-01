namespace SeaSky.SyTemplater.Model
{
    using SeaSky.StandardLib.MyAttribute;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SeaSky.StandardLib.MyModel;
    using System.ComponentModel.DataAnnotations;

	/// <summary>
    /// 用户
    /// </summary>
    [DBTableInfo("tb_User")]
    [Serializable]
    public partial class UserModel : BasePageModel
    {
		///<summary>
		///用户唯一标识ID
		///</summary>
		public Guid? UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }
		///<summary>
		///用户姓名
		///</summary>
		[Required(ErrorMessage = "用户姓名不能为空!")]
		[StringLength(50, ErrorMessage = "用户姓名长度不能超过50!")]
		public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
		///<summary>
		///工号
		///</summary>
		[StringLength(100, ErrorMessage = "工号长度不能超过100!")]
		public string UserNo
        {
            get { return _userNo; }
            set { _userNo = value; }
        }
		///<summary>
		///登录账号
		///</summary>
		[StringLength(100, ErrorMessage = "登录账号长度不能超过100!")]
		public string LoginNo
        {
            get { return _loginNo; }
            set { _loginNo = value; }
        }
		///<summary>
		///登录密码
		///</summary>
		[StringLength(100, ErrorMessage = "登录密码长度不能超过100!")]
		public string LoginPwd
        {
            get { return _loginPwd; }
            set { _loginPwd = value; }
        }
		///<summary>
		///用户类型
		///</summary>
		[StringLength(50, ErrorMessage = "用户类型长度不能超过50!")]
		public string UserType
        {
            get { return _userType; }
            set { _userType = value; }
        }
		///<summary>
		///邮箱
		///</summary>
		[StringLength(100, ErrorMessage = "邮箱长度不能超过100!")]
		public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
		///<summary>
		///手机
		///</summary>
		[StringLength(50, ErrorMessage = "手机长度不能超过50!")]
		public string Telephone
        {
            get { return _telephone; }
            set { _telephone = value; }
        }
		///<summary>
		///锁定状态
		///</summary>
		public bool? LockState
        {
            get { return _lockState; }
            set { _lockState = value; }
        }
		///<summary>
		///启用状态
		///</summary>
		public bool? UseState
        {
            get { return _useState; }
            set { _useState = value; }
        }
		///<summary>
		///扩展属性1
		///</summary>
		[StringLength(50, ErrorMessage = "扩展属性1长度不能超过50!")]
		public string Extend1
        {
            get { return _extend1; }
            set { _extend1 = value; }
        }
		///<summary>
		///扩展属性2
		///</summary>
		[StringLength(50, ErrorMessage = "扩展属性2长度不能超过50!")]
		public string Extend2
        {
            get { return _extend2; }
            set { _extend2 = value; }
        }
		///<summary>
		///扩展属性3
		///</summary>
		[StringLength(50, ErrorMessage = "扩展属性3长度不能超过50!")]
		public string Extend3
        {
            get { return _extend3; }
            set { _extend3 = value; }
        }
		///<summary>
		///扩展属性4
		///</summary>
		[StringLength(50, ErrorMessage = "扩展属性4长度不能超过50!")]
		public string Extend4
        {
            get { return _extend4; }
            set { _extend4 = value; }
        }
		///<summary>
		///扩展属性5
		///</summary>
		[StringLength(50, ErrorMessage = "扩展属性5长度不能超过50!")]
		public string Extend5
        {
            get { return _extend5; }
            set { _extend5 = value; }
        }
		///<summary>
		///扩展属性6
		///</summary>
		[StringLength(50, ErrorMessage = "扩展属性6长度不能超过50!")]
		public string Extend6
        {
            get { return _extend6; }
            set { _extend6 = value; }
        }
		///<summary>
		///扩展属性7
		///</summary>
		[StringLength(50, ErrorMessage = "扩展属性7长度不能超过50!")]
		public string Extend7
        {
            get { return _extend7; }
            set { _extend7 = value; }
        }
		///<summary>
		///扩展属性8
		///</summary>
		[StringLength(50, ErrorMessage = "扩展属性8长度不能超过50!")]
		public string Extend8
        {
            get { return _extend8; }
            set { _extend8 = value; }
        }
		///<summary>
		///扩展属性9
		///</summary>
		[StringLength(50, ErrorMessage = "扩展属性9长度不能超过50!")]
		public string Extend9
        {
            get { return _extend9; }
            set { _extend9 = value; }
        }
		///<summary>
		///扩展属性10
		///</summary>
		[StringLength(50, ErrorMessage = "扩展属性10长度不能超过50!")]
		public string Extend10
        {
            get { return _extend10; }
            set { _extend10 = value; }
        }

	
		#region private property
		/// <summary>
		///用户唯一标识ID
        /// </summary>
        [DBFieldInfo(ColumnName = "UserID", IsIdentity = false, 
		IsKey = true, SqlDbType =SqlDbType.UniqueIdentifier, OrderIndex = -1, OrderAsc = true )]
        protected Guid? _userID ;
		/// <summary>
		///用户姓名
        /// </summary>
        [DBFieldInfo(ColumnName = "UserName", IsIdentity = false, 
		IsKey = false, SqlDbType =SqlDbType.NVarChar, OrderIndex = -1, OrderAsc = true , LikeEqual = EnumLikeMode.AllLike)]
        protected string _userName ;
		/// <summary>
		///工号
        /// </summary>
        [DBFieldInfo(ColumnName = "UserNo", IsIdentity = false, 
		IsKey = false, SqlDbType =SqlDbType.NVarChar, OrderIndex = -1, OrderAsc = true , LikeEqual = EnumLikeMode.AllLike)]
        protected string _userNo ;
		/// <summary>
		///登录账号
        /// </summary>
        [DBFieldInfo(ColumnName = "LoginNo", IsIdentity = false, 
		IsKey = false, SqlDbType =SqlDbType.NVarChar, OrderIndex = -1, OrderAsc = true , LikeEqual = EnumLikeMode.AllLike)]
        protected string _loginNo ;
		/// <summary>
		///登录密码
        /// </summary>
        [DBFieldInfo(ColumnName = "LoginPwd", IsIdentity = false, 
		IsKey = false, SqlDbType =SqlDbType.NVarChar, OrderIndex = -1, OrderAsc = true , LikeEqual = EnumLikeMode.AllLike)]
        protected string _loginPwd ;
		/// <summary>
		///用户类型
        /// </summary>
        [DBFieldInfo(ColumnName = "UserType", IsIdentity = false, 
		IsKey = false, SqlDbType =SqlDbType.NVarChar, OrderIndex = -1, OrderAsc = true , LikeEqual = EnumLikeMode.AllLike)]
        protected string _userType ;
		/// <summary>
		///邮箱
        /// </summary>
        [DBFieldInfo(ColumnName = "Email", IsIdentity = false, 
		IsKey = false, SqlDbType =SqlDbType.NVarChar, OrderIndex = -1, OrderAsc = true , LikeEqual = EnumLikeMode.AllLike)]
        protected string _email ;
		/// <summary>
		///手机
        /// </summary>
        [DBFieldInfo(ColumnName = "Telephone", IsIdentity = false, 
		IsKey = false, SqlDbType =SqlDbType.NVarChar, OrderIndex = -1, OrderAsc = true , LikeEqual = EnumLikeMode.AllLike)]
        protected string _telephone ;
		/// <summary>
		///锁定状态
        /// </summary>
        [DBFieldInfo(ColumnName = "LockState", IsIdentity = false, 
		IsKey = false, SqlDbType =SqlDbType.Bit, OrderIndex = -1, OrderAsc = true )]
        protected bool? _lockState ;
		/// <summary>
		///启用状态
        /// </summary>
        [DBFieldInfo(ColumnName = "UseState", IsIdentity = false, 
		IsKey = false, SqlDbType =SqlDbType.Bit, OrderIndex = -1, OrderAsc = true )]
        protected bool? _useState ;
		/// <summary>
		///扩展属性1
        /// </summary>
        [DBFieldInfo(ColumnName = "Extend1", IsIdentity = false, 
		IsKey = false, SqlDbType =SqlDbType.NVarChar, OrderIndex = -1, OrderAsc = true , LikeEqual = EnumLikeMode.AllLike)]
        protected string _extend1 ;
		/// <summary>
		///扩展属性2
        /// </summary>
        [DBFieldInfo(ColumnName = "Extend2", IsIdentity = false, 
		IsKey = false, SqlDbType =SqlDbType.NVarChar, OrderIndex = -1, OrderAsc = true , LikeEqual = EnumLikeMode.AllLike)]
        protected string _extend2 ;
		/// <summary>
		///扩展属性3
        /// </summary>
        [DBFieldInfo(ColumnName = "Extend3", IsIdentity = false, 
		IsKey = false, SqlDbType =SqlDbType.NVarChar, OrderIndex = -1, OrderAsc = true , LikeEqual = EnumLikeMode.AllLike)]
        protected string _extend3 ;
		/// <summary>
		///扩展属性4
        /// </summary>
        [DBFieldInfo(ColumnName = "Extend4", IsIdentity = false, 
		IsKey = false, SqlDbType =SqlDbType.NVarChar, OrderIndex = -1, OrderAsc = true , LikeEqual = EnumLikeMode.AllLike)]
        protected string _extend4 ;
		/// <summary>
		///扩展属性5
        /// </summary>
        [DBFieldInfo(ColumnName = "Extend5", IsIdentity = false, 
		IsKey = false, SqlDbType =SqlDbType.NVarChar, OrderIndex = -1, OrderAsc = true , LikeEqual = EnumLikeMode.AllLike)]
        protected string _extend5 ;
		/// <summary>
		///扩展属性6
        /// </summary>
        [DBFieldInfo(ColumnName = "Extend6", IsIdentity = false, 
		IsKey = false, SqlDbType =SqlDbType.NVarChar, OrderIndex = -1, OrderAsc = true , LikeEqual = EnumLikeMode.AllLike)]
        protected string _extend6 ;
		/// <summary>
		///扩展属性7
        /// </summary>
        [DBFieldInfo(ColumnName = "Extend7", IsIdentity = false, 
		IsKey = false, SqlDbType =SqlDbType.NVarChar, OrderIndex = -1, OrderAsc = true , LikeEqual = EnumLikeMode.AllLike)]
        protected string _extend7 ;
		/// <summary>
		///扩展属性8
        /// </summary>
        [DBFieldInfo(ColumnName = "Extend8", IsIdentity = false, 
		IsKey = false, SqlDbType =SqlDbType.NVarChar, OrderIndex = -1, OrderAsc = true , LikeEqual = EnumLikeMode.AllLike)]
        protected string _extend8 ;
		/// <summary>
		///扩展属性9
        /// </summary>
        [DBFieldInfo(ColumnName = "Extend9", IsIdentity = false, 
		IsKey = false, SqlDbType =SqlDbType.NVarChar, OrderIndex = -1, OrderAsc = true , LikeEqual = EnumLikeMode.AllLike)]
        protected string _extend9 ;
		/// <summary>
		///扩展属性10
        /// </summary>
        [DBFieldInfo(ColumnName = "Extend10", IsIdentity = false, 
		IsKey = false, SqlDbType =SqlDbType.NVarChar, OrderIndex = -1, OrderAsc = true , LikeEqual = EnumLikeMode.AllLike)]
        protected string _extend10 ;

		#endregion
	}
}
