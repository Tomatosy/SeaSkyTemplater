namespace SeaSky.SyTemplater.BLL
{
    using System;
    using System.Collections.Generic;
    using SeaSky.StandardLib.MyModel;
    using SeaSky.SyTemplater.Model;

    public interface IMenuService
    {

        /// <summary>
        /// 获取菜单列表分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        BaseResultModel<PageModel<MenuOutputModel>> ListPageMenu(MenuOutputModel model);

        /// <summary>
        /// 新增、修改菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        BaseResultModel<MenuOutputModel> ModifyMenu(MenuInputModel model);

        /// <summary>
        /// 删除菜单 (逻辑删除)
        /// </summary>
        /// <param name="IDs"></param>
        /// <returns></returns>
        BaseResultModel<int> DeleteMenu(List<Guid?> IDs);

        /// <summary>
        /// 获取单个菜单
        /// </summary>
        BaseResultModel<MenuOutputModel> GetMenu(Guid? ID);

        /// <summary>
        /// 查询所有有效的菜单树
        /// </summary>
        /// <returns></returns>
        BaseResultModel<List<MenuOutputModel>> ListAllMenuTree();
    }


}
