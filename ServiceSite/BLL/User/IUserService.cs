namespace SeaSky.SyTemplater.BLL
{
    using System;
    using System.Collections.Generic;
    using SeaSky.StandardLib.MyModel;
    using SeaSky.SyTemplater.Model;

    public interface IUserService
    {

        /// <summary>
        /// 获取用户列表分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        BaseResultModel<PageModel<UserViewModel>> ListViewPageUser(UserViewModel model);

        /// <summary>
        /// 新增、修改用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        BaseResultModel<UserOutputModel> ModifyUser(UserInputModel model);

        /// <summary>
        /// 删除用户 (逻辑删除)
        /// </summary>
        /// <param name="IDs"></param>
        /// <returns></returns>
        BaseResultModel<int> DeleteUser(List<Guid?> IDs);

        /// <summary>
        /// 获取单个用户
        /// </summary>
        BaseResultModel<UserOutputModel> GetUser(Guid? ID);
    }


}
