namespace SeaSky.SyTemplater.BLL
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using Microsoft.Practices.Unity;
    using System.Transactions;
    using SeaSky.StandardLib.MyModel;
    using SeaSky.SyTemplater.Model;
    using SeaSky.SyTemplater.Model.Enum;
    using SeaSky.SyTemplater.DAL;

    public class MenuService : IMenuService
    {
        [Dependency]
        public IMenuRepository MenuRepository { get; set; }

        /// <summary>
        /// 获取菜单列表分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResultModel<PageModel<MenuOutputModel>> ListPageMenu(MenuOutputModel model)
        {
            try
            {
                if (model == null)
                {
                    model = new MenuOutputModel()
                    {
                        PageNO = 1,
                        PageSize = 9999
                    };
                }
                // 开启查询outModel里面的视图
                using (this.MenuRepository.BeginSelView())
                {
                    using (this.MenuRepository.BeginLikeMode())
                    {
                        return new SuccessResultModel<PageModel<MenuOutputModel>>(this.MenuRepository.ListPage(model));
                    }
                }
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(EnumLogLevel.Fatal, "ListPageMenu", "", "", "获取菜单列表分页查询数据时发生错误.", e);
                return new ErrorResultModel<PageModel<MenuOutputModel>>(EnumErrorCode.系统异常, "获取菜单列表分页查询数据时发生错误!");
            }
        }

        /// <summary>
        /// 新增、修改菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResultModel<MenuOutputModel> ModifyMenu(MenuInputModel model)
        {
            SuccessResultModel<MenuOutputModel> result = new SuccessResultModel<MenuOutputModel>();
            ErrorResultModel<MenuOutputModel> error = new ErrorResultModel<MenuOutputModel>();
            try
            {
                if (model.MenuID.IsNullOrEmpty())
                {
                    model.ApplicationID = Guid.Empty;
                    result.Data = this.MenuRepository.InsertAndReturn(model);
                }
                else
                {
                    result.Data = this.MenuRepository.UpdateWithKeysAndReturn(model);
                }
                return result;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(EnumLogLevel.Fatal, "ModifyMenu", "", "", "新增、修改菜单异常！", ex);
                error.ErrorCode = EnumErrorCode.系统异常;
                error.ErrorMessage = "新增、修改菜单异常!";
                return error;
            }
        }

        /// <summary>
        /// 删除菜单 (逻辑删除)
        /// </summary>
        /// <param name="IDs"></param>
        /// <returns></returns>
        public BaseResultModel<int> DeleteMenu(List<Guid?> IDs)
        {
            SuccessResultModel<int> result = new SuccessResultModel<int>();
            ErrorResultModel<int> error = new ErrorResultModel<int>();
            try
            {
                List<MenuInputModel> delList = new List<MenuInputModel>();
                foreach (Guid? item in IDs)
                {
                    delList.Add(new MenuInputModel()
                    {
                        MenuID = item
                    });
                }
                result.Data = this.MenuRepository.UpdateWithKeys(delList.ToArray());

                if (result.Data == 0)
                {
                    error.ErrorCode = EnumErrorCode.业务执行失败;
                    error.ErrorMessage = "请确认需要删除的数据！";
                    return error;
                }
                return result;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(EnumLogLevel.Fatal, "DeleteMenu", "", "", "删除菜单 (逻辑删除)异常！", ex);
                error.ErrorCode = EnumErrorCode.系统异常;
                error.ErrorMessage = "删除菜单 (逻辑删除)异常!";
                return error;
            }
        }

        /// <summary>
        /// 获取单个菜单
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public BaseResultModel<MenuOutputModel> GetMenu(Guid? ID)
        {
            try
            {
                // 开启查询outModel里面的视图
                using (this.MenuRepository.BeginSelView())
                {
                    return new SuccessResultModel<MenuOutputModel>(
                        this.MenuRepository.SelectWithModel(new MenuOutputModel()
                        {
                            MenuID = ID
                        }
                    ));
                }
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(EnumLogLevel.Fatal, "GetMenu", "", "", "获取单个菜单异常", e);
                return new ErrorResultModel<MenuOutputModel>(EnumErrorCode.系统异常, "获取单个菜单异常!");
            }
        }


        /// <summary>
        /// 查询所有有效的菜单树
        /// </summary>
        /// <returns></returns>
        public BaseResultModel<List<MenuOutputModel>> ListAllMenuTree()
        {
            try
            {
                List<MenuOutputModel> listAllMenu = this.MenuRepository.List().ToList();
                if (listAllMenu.Count == 0)
                {
                    return new ErrorResultModel<List<MenuOutputModel>>(EnumErrorCode.业务执行失败, "未找到菜单集合");
                }
                MenuOutputModel menuTree = new MenuOutputModel();
                menuTree.Children = new List<MenuOutputModel>();
                FillTree(Guid.Empty, listAllMenu, ref menuTree);
                return new SuccessResultModel<List<MenuOutputModel>>(menuTree.Children);
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(EnumLogLevel.Fatal, "ListAllMenuTree", "", "", "获取菜单集合异常！", ex);
                return new ErrorResultModel<List<MenuOutputModel>>(EnumErrorCode.系统异常, "获取菜单集合异常");
            }
        }

        /// <summary>
        /// 递归菜单树
        /// </summary>
        /// <param name="parentID">菜单父节点ID</param>
        /// <param name="listAllMenu">有效菜单数据源</param>
        /// <param name="menuTree">菜单Model</param>
        private void FillTree(Guid parentID, List<MenuOutputModel> listAllMenu, ref MenuOutputModel menuTree)
        {
            List<MenuOutputModel> listNodes = listAllMenu.Where(p => p.ParentID == parentID).ToList();
            foreach (MenuOutputModel menuNode in listNodes)
            {
                MenuOutputModel m = menuNode;
                m.Children = new List<MenuOutputModel>();
                menuTree.Children.Add(m);
                FillTree(m.MenuID.Value, listAllMenu, ref m);
                menuTree.Children = menuTree.Children.OrderBy(e => e.MenuNo).ToList();
            }
        }
    }
}
