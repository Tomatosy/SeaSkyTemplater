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
    /// 菜单
    /// </summary>
    [DBTableInfo("tb_Menu")]
    [Serializable]
    public partial class MenuModel : BasePageModel
    {
        ///<summary>
        ///菜单ID（主键）
        ///</summary>
        public Guid? MenuID
        {
            get { return _menuID; }
            set { _menuID = value; }
        }
        ///<summary>
        ///应用ID （外键）
        ///</summary>
        public Guid? ApplicationID
        {
            get { return _applicationID; }
            set { _applicationID = value; }
        }
        ///<summary>
        ///菜单编号
        ///</summary>
        [StringLength(50, ErrorMessage = "菜单编号长度不能超过50!")]
        public string MenuNo
        {
            get { return _menuNo; }
            set { _menuNo = value; }
        }
        ///<summary>
        ///菜单名称
        ///</summary>
        [StringLength(50, ErrorMessage = "菜单名称长度不能超过50!")]
        public string MenuName
        {
            get { return _menuName; }
            set { _menuName = value; }
        }
        ///<summary>
        ///菜单图标
        ///</summary>
        [StringLength(50, ErrorMessage = "菜单图标长度不能超过50!")]
        public string MenuIcon
        {
            get { return _menuIcon; }
            set { _menuIcon = value; }
        }
        ///<summary>
        ///父级菜单ID
        ///</summary>
        public Guid? ParentID
        {
            get { return _parentID; }
            set { _parentID = value; }
        }
        ///<summary>
        ///菜单路由地址
        ///</summary>
        [StringLength(50, ErrorMessage = "菜单路由地址长度不能超过50!")]
        public string RoutingUrl
        {
            get { return _routingUrl; }
            set { _routingUrl = value; }
        }
        ///<summary>
        ///跳转地址
        ///</summary>
        [StringLength(300, ErrorMessage = "跳转地址长度不能超过300!")]
        public string RedirectUrl
        {
            get { return _redirectUrl; }
            set { _redirectUrl = value; }
        }
        ///<summary>
        ///路由（地址）参数
        ///</summary>
        [StringLength(300, ErrorMessage = "路由（地址）参数长度不能超过300!")]
        public string UrlParameter
        {
            get { return _urlParameter; }
            set { _urlParameter = value; }
        }
        ///<summary>
        ///是否打开新页面
        ///</summary>
        public bool? IsNewWindow
        {
            get { return _isNewWindow; }
            set { _isNewWindow = value; }
        }
        ///<summary>
        ///是否初始化（系统）菜单
        ///</summary>
        public bool? IsSystem
        {
            get { return _isSystem; }
            set { _isSystem = value; }
        }
        ///<summary>
        ///是否启用
        ///</summary>
        public bool? IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }


        #region private property
        /// <summary>
        ///菜单ID（主键）
        /// </summary>
        [DBFieldInfo(ColumnName = "MenuID", IsIdentity = false,
        IsKey = true, SqlDbType = SqlDbType.UniqueIdentifier, OrderIndex = -1, OrderAsc = true)]
        protected Guid? _menuID;
        /// <summary>
        ///应用ID （外键）
        /// </summary>
        [DBFieldInfo(ColumnName = "ApplicationID", IsIdentity = false,
        IsKey = false, SqlDbType = SqlDbType.UniqueIdentifier, OrderIndex = -1, OrderAsc = true)]
        protected Guid? _applicationID;
        /// <summary>
        ///菜单编号
        /// </summary>
        [DBFieldInfo(ColumnName = "MenuNo", IsIdentity = false,
        IsKey = false, SqlDbType = SqlDbType.NVarChar, OrderIndex = -1, OrderAsc = true, LikeEqual = EnumLikeMode.AllLike)]
        protected string _menuNo;
        /// <summary>
        ///菜单名称
        /// </summary>
        [DBFieldInfo(ColumnName = "MenuName", IsIdentity = false,
        IsKey = false, SqlDbType = SqlDbType.NVarChar, OrderIndex = -1, OrderAsc = true, LikeEqual = EnumLikeMode.AllLike)]
        protected string _menuName;
        /// <summary>
        ///菜单图标
        /// </summary>
        [DBFieldInfo(ColumnName = "MenuIcon", IsIdentity = false,
        IsKey = false, SqlDbType = SqlDbType.NVarChar, OrderIndex = -1, OrderAsc = true, LikeEqual = EnumLikeMode.AllLike)]
        protected string _menuIcon;
        /// <summary>
        ///父级菜单ID
        /// </summary>
        [DBFieldInfo(ColumnName = "ParentID", IsIdentity = false,
        IsKey = false, SqlDbType = SqlDbType.UniqueIdentifier, OrderIndex = -1, OrderAsc = true)]
        protected Guid? _parentID;
        /// <summary>
        ///菜单路由地址
        /// </summary>
        [DBFieldInfo(ColumnName = "RoutingUrl", IsIdentity = false,
        IsKey = false, SqlDbType = SqlDbType.NVarChar, OrderIndex = -1, OrderAsc = true, LikeEqual = EnumLikeMode.AllLike)]
        protected string _routingUrl;
        /// <summary>
        ///跳转地址
        /// </summary>
        [DBFieldInfo(ColumnName = "RedirectUrl", IsIdentity = false,
        IsKey = false, SqlDbType = SqlDbType.NVarChar, OrderIndex = -1, OrderAsc = true, LikeEqual = EnumLikeMode.AllLike)]
        protected string _redirectUrl;
        /// <summary>
        ///路由（地址）参数
        /// </summary>
        [DBFieldInfo(ColumnName = "UrlParameter", IsIdentity = false,
        IsKey = false, SqlDbType = SqlDbType.NVarChar, OrderIndex = -1, OrderAsc = true, LikeEqual = EnumLikeMode.AllLike)]
        protected string _urlParameter;
        /// <summary>
        ///是否打开新页面
        /// </summary>
        [DBFieldInfo(ColumnName = "IsNewWindow", IsIdentity = false,
        IsKey = false, SqlDbType = SqlDbType.Bit, OrderIndex = -1, OrderAsc = true)]
        protected bool? _isNewWindow;
        /// <summary>
        ///是否初始化（系统）菜单
        /// </summary>
        [DBFieldInfo(ColumnName = "IsSystem", IsIdentity = false,
        IsKey = false, SqlDbType = SqlDbType.Bit, OrderIndex = -1, OrderAsc = true)]
        protected bool? _isSystem;
        /// <summary>
        ///是否启用
        /// </summary>
        [DBFieldInfo(ColumnName = "IsUse", IsIdentity = false,
        IsKey = false, SqlDbType = SqlDbType.Bit, OrderIndex = -1, OrderAsc = true)]
        protected bool? _isUse;

        #endregion
    }
}
