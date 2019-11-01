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

    public class UserService : IUserService
    {
        [Dependency]
        public IUserRepository UserRepository { get; set;}

        /// <summary>
        /// 获取用户列表分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResultModel<PageModel<UserOutputModel>> ListPageUser(UserOutputModel model)
        {
            try
            {
                if (model == null)
                {
                    model = new UserOutputModel()
                    {
                        PageNO = 1,
                        PageSize = 9999
                   };
               }
                // 开启查询outModel里面的视图
                using (this.UserRepository.BeginSelView())
                {
                  using (this.UserRepository.BeginLikeMode())
                  {
                      return new SuccessResultModel<PageModel<UserOutputModel>>(this.UserRepository.ListPage(model));
                 }
               }
           }
            catch (Exception e)
            {
                LogWriter.WriteLog(EnumLogLevel.Fatal, "ListPageUser", "", "", "获取用户列表分页查询数据时发生错误.", e);
                return new ErrorResultModel<PageModel<UserOutputModel>>(EnumErrorCode.系统异常, "获取用户列表分页查询数据时发生错误!");
           }
       }

        /// <summary>
        /// 新增、修改用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResultModel<UserOutputModel> ModifyUser(UserInputModel model)
        {
            SuccessResultModel<UserOutputModel> result = new SuccessResultModel<UserOutputModel>();
            ErrorResultModel<UserOutputModel> error = new ErrorResultModel<UserOutputModel>();
            try
            {
                if (model.UserID.IsNullOrEmpty())
                {
                    result.Data = this.UserRepository.InsertAndReturn(model);
               }
                else
                {
                    result.Data = this.UserRepository.UpdateWithKeysAndReturn(model);
               }
                return result;
           }
            catch (Exception ex)
            {
                LogWriter.WriteLog(EnumLogLevel.Fatal, "ModifyUser", "", "", "新增、修改用户异常！", ex);
                error.ErrorCode = EnumErrorCode.系统异常;
                error.ErrorMessage = "新增、修改用户异常!";
                return error;
           }
       }

        /// <summary>
        /// 删除用户 (逻辑删除)
        /// </summary>
        /// <param name="IDs"></param>
        /// <returns></returns>
        public BaseResultModel<int> DeleteUser(List<Guid?> IDs)
        {
            SuccessResultModel<int> result = new SuccessResultModel<int>();
            ErrorResultModel<int> error = new ErrorResultModel<int>();
            try
            {
                List<UserInputModel> delList = new List<UserInputModel>();
                foreach (Guid? item in IDs)
                {
                    delList.Add(new UserInputModel()
                    {
                        UserID = item
                   });
               }
                result.Data = this.UserRepository.DeleteWithKeys(delList.ToArray());

                if(result.Data==0)
                {
                    error.ErrorCode=EnumErrorCode.业务执行失败;
                    error.ErrorMessage="请确认需要删除的数据！";
                    return error;
               }
                return result;
           }
            catch (Exception ex)
            {
                LogWriter.WriteLog(EnumLogLevel.Fatal, "DeleteUser", "", "", "删除用户 (逻辑删除)异常！", ex);
                error.ErrorCode = EnumErrorCode.系统异常;
                error.ErrorMessage = "删除用户 (逻辑删除)异常!";
                return error;
           }
       }

        /// <summary>
        /// 获取单个用户
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public BaseResultModel<UserOutputModel> GetUser(Guid? ID)
        {
            try
            {
                // 开启查询outModel里面的视图
                using (this.UserRepository.BeginSelView())
                {
                  return new SuccessResultModel<UserOutputModel>(
                      this.UserRepository.SelectWithModel(new UserOutputModel()
                      {
                          UserID = ID
                     }
                  ));
               }
           }
            catch (Exception e)
            {
                LogWriter.WriteLog(EnumLogLevel.Fatal, "GetUser", "", "", "获取单个用户异常", e);
                return new ErrorResultModel<UserOutputModel>(EnumErrorCode.系统异常, "获取单个用户异常!");
           }
       }
   }
}
