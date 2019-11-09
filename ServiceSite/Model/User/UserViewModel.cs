namespace SeaSky.SyTemplater.Model
{
    using SeaSky.StandardLib.MyAttribute;
    using System.Data;
    using SeaSky.StandardLib.MyModel;
    using System;

    /// <summary>
    /// 日志表输出扩展类
    /// </summary>
    [DBTableInfo("View_tb_User")]
    public partial class UserViewModel : UserModel
    {

        ///// <summary>
        /////用户唯一标识ID
        ///// </summary>
        //[DBFieldInfo(ColumnName = "TestView", SqlDbType = SqlDbType.Int)]
        //public int? TestView;


        /////<summary>
        /////扩展属性10
        /////</summary>
        //public int? TestView
        //{
        //    get { return testView; }
        //    set { testView = value; }
        //}


        /// <summary>
        ///用户唯一标识ID
        /// </summary>
        [DBFieldInfo(ColumnName = "TestView", IsIdentity = false,
        IsKey = true, SqlDbType = SqlDbType.Int, OrderIndex = -1, OrderAsc = true)]
        public int? testView;
    }
}
