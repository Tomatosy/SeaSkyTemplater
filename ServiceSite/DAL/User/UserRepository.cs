namespace SeaSky.SyTemplater.DAL
{
    using Microsoft.Practices.Unity;
    using SeaSky.StandardLib.MyBaseClass;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SeaSky.StandardLib.DAL.Base;
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Data.Common;
    using SeaSky.StandardLib.MyModel;
    using SeaSky.SyTemplater.Model;

    public class UserRepository : DALPageBase<UserModel, UserOutputModel, UserViewModel>, IUserRepository
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public UserRepository() : base("BaseConn", DatabaseMode.SqlClient)
        {

            //        !licenseFileModel.ListSystem[0].ListWebApi.Exists(x=>x.WebApiUrl=="CC/00")
            //true
            // licenseFileModel.ListSystem[0].ListWebApi.Find(x=>x.WebApiUrl=="CC/00")==null
            //true
            // licenseFileModel.ListSystem[0].ListWebApi.Find(x=>x.WebApiUrl=="CC/DDDD")==null
            //false
            //!licenseFileModel.ListSystem[0].ListWebApi.Exists(x=>x.WebApiUrl=="CC/DDDD")
            //false

        }

        /// <summary>
        /// 获取数据库操作人
        /// </summary>
        /// <returns>操作人</returns>
        public override string GetOperater()
        {
            return ServiceContext.Current.UserName;
        }
    }


}
