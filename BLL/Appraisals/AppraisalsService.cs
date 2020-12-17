namespace Tomato.NewTempProject.BLL
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
using Newtonsoft.Json;
using Tomato.StandardLib.MyModel;
using Tomato.NewTempProject.Model;
using Tomato.NewTempProject.Model.Enum;
using Tomato.NewTempProject.DAL;
using Tomato.StandardLib.MyExtensions;

    public class AppraisalsService : IAppraisalsService
    {
        [Dependency]
        public IAppraisalsRepository AppraisalsRepository { get; set; }


        #region 基础方法
        /// <summary>
        /// 获取考核项目表视图列表分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResultModel<PageModel<AppraisalsViewModel>> ListViewPageAppraisals(AppraisalsViewModel model)
        {
            try
            {
                if (model == null)
                {
                    model = new AppraisalsViewModel()
                    {
                        PageNO = 1,
                        PageSize = int.MaxValue
                    };
                }
                model.IsDelete = false;
                // 开启查询outModel里面的视图
                using (this.AppraisalsRepository.BeginSelView())
                {
                  using (this.AppraisalsRepository.BeginLikeMode())
                  {
                      return new SuccessResultModel<PageModel<AppraisalsViewModel>>(this.AppraisalsRepository.ListViewPage(model));
                  }
                }
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(EnumLogLevel.Fatal, "ListViewPageAppraisals", JsonConvert.SerializeObject(model), "Appraisals", "获取考核项目表视图列表分页查询数据时发生错误.", e);
                return new ErrorResultModel<PageModel<AppraisalsViewModel>>(EnumErrorCode.系统异常, "获取考核项目表视图列表分页查询数据时发生错误!");
            }
        }
		
        /// <summary>
        /// 获取考核项目表列表分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResultModel<PageModel<AppraisalsOutputModel>> ListPageAppraisals(AppraisalsInputModel model)
        {
            try
            {
                if (model == null)
                {
                    model = new AppraisalsInputModel()
                    {
                        PageNO = 1,
                        PageSize = int.MaxValue
                    };
                }
                model.IsDelete = false;
                using (this.AppraisalsRepository.BeginLikeMode())
                {
                    return new SuccessResultModel<PageModel<AppraisalsOutputModel>>(this.AppraisalsRepository.ListPage(model));
                }
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(EnumLogLevel.Fatal, "ListPageAppraisals", JsonConvert.SerializeObject(model), "Appraisals", "获取考核项目表列表分页查询数据时发生错误.", e);
                return new ErrorResultModel<PageModel<AppraisalsOutputModel>>(EnumErrorCode.系统异常, "获取考核项目表列表分页查询数据时发生错误!");
            }
        }

        /// <summary>
        /// 新增、修改考核项目表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResultModel<AppraisalsOutputModel> ModifyAppraisals(AppraisalsInputModel model)
        {
            SuccessResultModel<AppraisalsOutputModel> result = new SuccessResultModel<AppraisalsOutputModel>();
            ErrorResultModel<AppraisalsOutputModel> error = new ErrorResultModel<AppraisalsOutputModel>();
            try
            {
                if (model.AppraisalsID.IsNullOrEmpty())
                {
                    result.Data = this.AppraisalsRepository.InsertAndReturn(model);
                }
                else
                {
                    result.Data = this.AppraisalsRepository.UpdateWithKeysAndReturn(model);
                }
                return result;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(EnumLogLevel.Fatal, "ModifyAppraisals", JsonConvert.SerializeObject(model), "Appraisals", "新增、修改考核项目表异常！", ex);
                error.ErrorCode = EnumErrorCode.系统异常;
                error.ErrorMessage = "新增、修改考核项目表异常!";
                return error;
            }
        }

        /// <summary>
        /// 删除考核项目表 (逻辑删除)
        /// </summary>
        /// <param name="IDs"></param>
        /// <returns></returns>
        public BaseResultModel<int> DeleteAppraisals(List<Guid?> IDs)
        {
            SuccessResultModel<int> result = new SuccessResultModel<int>();
            ErrorResultModel<int> error = new ErrorResultModel<int>();
            try
            {
                List<AppraisalsInputModel> delList = new List<AppraisalsInputModel>();
                foreach (Guid? item in IDs)
                {
                    delList.Add(new AppraisalsInputModel()
                    {
                        AppraisalsID = item,
                        IsDelete = true
                    });
                }
                result.Data = this.AppraisalsRepository.UpdateWithKeys(delList.ToArray());

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
                LogWriter.WriteLog(EnumLogLevel.Fatal, "DeleteAppraisals", JsonConvert.SerializeObject(IDs), "Appraisals", "删除考核项目表 (逻辑删除)异常！", ex);
                error.ErrorCode = EnumErrorCode.系统异常;
                error.ErrorMessage = "删除考核项目表 (逻辑删除)异常!";
                return error;
            }
        }

        /// <summary>
        /// 获取单个考核项目表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public BaseResultModel<AppraisalsViewModel> GetAppraisals(Guid? ID)
        {
            try
            {
                // 开启查询outModel里面的视图
                using (this.AppraisalsRepository.BeginSelView())
                {
                    using (this.AppraisalsRepository.BeginLikeMode())
                    {
                      return new SuccessResultModel<AppraisalsViewModel>(
                          this.AppraisalsRepository.SelectWithViewModel(new AppraisalsViewModel()
                          {
                              AppraisalsID = ID
                          }
                      ));
                    }
                }
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(EnumLogLevel.Fatal, "GetAppraisals", ID+ string.Empty, "Appraisals", "获取单个考核项目表异常", e);
                return new ErrorResultModel<AppraisalsViewModel>(EnumErrorCode.系统异常, "获取单个考核项目表异常!");
            }
        }
        #endregion
    }
}
