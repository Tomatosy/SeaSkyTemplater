namespace Tomato.NewTempProject.DAL
{
    using Microsoft.Practices.Unity;
    using Tomato.StandardLib.MyBaseClass;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Tomato.StandardLib.DAL.Base;
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Data.Common;
    using Tomato.StandardLib.MyModel;
    using Tomato.NewTempProject.Model;

    using Tomato.StandardLib.MyBaseClass;

    public class MenuRepository : DALPageBaseNew<MenuModel, MenuOutputModel, MenuViewModel>, IMenuRepository
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public MenuRepository() : base("BaseConn", DatabaseMode.SqlClient)
        {

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
