namespace SeaSky.SyTemplater.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SeaSky.StandardLib.MyBaseClass;
    using SeaSky.StandardLib.MyModel;
    using SeaSky.SyTemplater.Model;

    public interface IMenuRepository : IDALBase<MenuModel, MenuOutputModel, MenuViewModel>, IDALPageBase<MenuModel, MenuOutputModel, MenuViewModel>
    {

    }


}

