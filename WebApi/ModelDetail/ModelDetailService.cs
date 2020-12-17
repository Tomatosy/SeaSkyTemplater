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
    using Tomato.StandardLib.MyModel;
    using Tomato.NewTempProject.Model;
    using Tomato.NewTempProject.Model.Enum;
    using Tomato.NewTempProject.DAL;
    using Tomato.StandardLib.MyExtensions;
    using Newtonsoft.Json;

    public class ModelDetailService : IModelDetailService
    {
        [Dependency]
        public IModelDetailRepository ModelDetailRepository { get; set; }


        [Dependency]
        public IModelRepository ModelRepository { get; set; }


        #region 基础方法
        /// <summary>
        /// 获取模块明细表视图列表分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResultModel<PageModel<ModelDetailViewModel>> ListViewPageModelDetail(ModelDetailViewModel model)
        {
            try
            {
                model.PageNO = model.PageNO ?? 1;
                model.PageSize = model.PageSize ?? int.MaxValue;
                model.IsDelete = false;
                // 开启查询outModel里面的视图
                using (this.ModelDetailRepository.BeginSelView())
                {
                    //using (this.ModelDetailRepository.BeginLikeMode())
                    //{
                    return new SuccessResultModel<PageModel<ModelDetailViewModel>>(this.ModelDetailRepository.ListViewPage(model));
                    //}
                }
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(EnumLogLevel.Fatal, "ListViewPageModelDetail", JsonConvert.SerializeObject(model), "ModelDetail", "获取模块明细表视图列表分页查询数据时发生错误.", e);
                return new ErrorResultModel<PageModel<ModelDetailViewModel>>(EnumErrorCode.系统异常, "获取模块明细表视图列表分页查询数据时发生错误!");
            }
        }

        /// <summary>
        /// 获取模块明细表列表分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResultModel<PageModel<ModelDetailOutputModel>> ListPageModelDetail(ModelDetailInputModel model)
        {
            try
            {
                model.PageNO = model.PageNO ?? 1;
                model.PageSize = model.PageSize ?? int.MaxValue;
                model.IsDelete = false;
                using (this.ModelDetailRepository.BeginLikeMode())
                {
                    PageModel<ModelDetailOutputModel> resultList = this.ModelDetailRepository.ListPage(model);
                    List<ModelDetailOutputModel> resultData = resultList.ListData?.ToList();
                    if (model.OrderAscByColIndex && resultData.Count > 0)
                    {
                        resultList.ListData = resultData.OrderBy(x => x.ColIndex);
                    }
                    return new SuccessResultModel<PageModel<ModelDetailOutputModel>>(resultList);
                }
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(EnumLogLevel.Fatal, "ListPageModelDetail", JsonConvert.SerializeObject(model), "ModelDetail", "获取模块明细表列表分页查询数据时发生错误.", e);
                return new ErrorResultModel<PageModel<ModelDetailOutputModel>>(EnumErrorCode.系统异常, "获取模块明细表列表分页查询数据时发生错误!");
            }
        }

        /// <summary>
        /// 新增、修改模块明细表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResultModel<ModelDetailOutputModel> ModifyModelDetail(ModelDetailInputModel model)
        {
            SuccessResultModel<ModelDetailOutputModel> result = new SuccessResultModel<ModelDetailOutputModel>();
            ErrorResultModel<ModelDetailOutputModel> error = new ErrorResultModel<ModelDetailOutputModel>();
            try
            {
                ModelOutputModel selModel = this.ModelRepository.SelectWithModel(new ModelModel()
                {
                    ModelID = model.ModelID
                });
                if (selModel == null)
                {
                    return new ErrorResultModel<ModelDetailOutputModel>(EnumErrorCode.参数校验未通过, "明细表对应的模块项目不存在!");
                }

                ModelDetailViewModel selDetailModel = new ModelDetailViewModel();
                using (this.ModelDetailRepository.BeginSelView())
                {
                    selDetailModel = this.ModelDetailRepository.SelectWithViewModel(new ModelDetailViewModel()
                    {
                        ModelID = model.ModelID,
                        ColName = model.ColName
                    });
                }

                if (selDetailModel != null)
                {
                    if (model.ModelDetailID.IsNullOrEmpty())
                    {
                        return new ErrorResultModel<ModelDetailOutputModel>(EnumErrorCode.参数校验未通过, "名称已存在!");
                    }
                    else if (selDetailModel.ModelDetailID != model.ModelDetailID)
                    {
                        return new ErrorResultModel<ModelDetailOutputModel>(EnumErrorCode.参数校验未通过, "名称已存在!");
                    }
                }

                selDetailModel = new ModelDetailViewModel();
                selDetailModel.ModelCode = selModel.ModelCode;
                selDetailModel.ColName = model.ColName;
                selDetailModel.ColType = model.ColType;
                TransactionOptions option = new TransactionOptions();
                option.IsolationLevel = IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    if (model.ModelDetailID.IsNullOrEmpty())
                    {
                        result.Data = this.ModelDetailRepository.InsertAndReturn(model);
                        this.ModelDetailRepository.AddDynamicTableCol(selDetailModel);
                    }
                    else
                    {
                        result.Data = this.ModelDetailRepository.UpdateWithKeysAndReturn(model);
                        this.ModelDetailRepository.ModifyDynamicTableCol(selDetailModel);
                    }
                    scope.Complete();
                }
                return result;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(EnumLogLevel.Fatal, "ModifyModelDetail", JsonConvert.SerializeObject(model), "ModelDetail", "新增、修改模块明细表异常！", ex);
                return new ErrorResultModel<ModelDetailOutputModel>(EnumErrorCode.系统异常, "新增、修改模块明细表异常!");
            }
        }

        /// <summary>
        /// 删除模块明细表 (逻辑删除)
        /// </summary>
        /// <param name="IDs"></param>
        /// <returns></returns>
        public BaseResultModel<int> DeleteModelDetail(List<Guid?> IDs)
        {
            SuccessResultModel<int> result = new SuccessResultModel<int>();
            ErrorResultModel<int> error = new ErrorResultModel<int>();
            try
            {
                ModelDetailViewModel selDetailModel = new ModelDetailViewModel();
                ModelOutputModel selModel = new ModelOutputModel();

                TransactionOptions option = new TransactionOptions();
                option.IsolationLevel = IsolationLevel.ReadCommitted;

                foreach (Guid? item in IDs)
                {
                    using (this.ModelDetailRepository.BeginSelView())
                    {

                        selDetailModel = this.ModelDetailRepository.SelectWithViewModel(new ModelDetailViewModel()
                        {
                            ModelDetailID = item,
                        });
                    }

                    if (this.ModelDetailRepository.ListDynamicTableListByPer(selDetailModel))
                    {
                        error.ErrorCode = EnumErrorCode.业务执行失败;
                        error.ErrorMessage = "该数据字段已有业务数据，不可删除";
                        return error;
                    }
                    selDetailModel.IsDelete = true;

                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
                    {
                        result.Data = this.ModelDetailRepository.UpdateWithKeys(selDetailModel);
                        this.ModelDetailRepository.DropDynamicTableCol(selDetailModel);
                        scope.Complete();
                    }
                }

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
                LogWriter.WriteLog(EnumLogLevel.Fatal, "DeleteModelDetail", JsonConvert.SerializeObject(IDs), "ModelDetail", "删除模块明细表 (逻辑删除)异常！", ex);
                error.ErrorCode = EnumErrorCode.系统异常;
                error.ErrorMessage = "删除模块明细表 (逻辑删除)异常!";
                return error;
            }
        }

        /// <summary>
        /// 获取单个模块明细表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public BaseResultModel<ModelDetailViewModel> GetModelDetail(Guid? ID)
        {
            try
            {
                // 开启查询outModel里面的视图
                using (this.ModelDetailRepository.BeginSelView())
                {
                    using (this.ModelDetailRepository.BeginLikeMode())
                    {
                        return new SuccessResultModel<ModelDetailViewModel>(
                            this.ModelDetailRepository.SelectWithViewModel(new ModelDetailViewModel()
                            {
                                ModelDetailID = ID
                            }
                        ));
                    }
                }
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(EnumLogLevel.Fatal, "GetModelDetail", ID + string.Empty, "ModelDetail", "获取单个模块明细表异常", e);
                return new ErrorResultModel<ModelDetailViewModel>(EnumErrorCode.系统异常, "获取单个模块明细表异常!");
            }
        }
        #endregion
    }
}
