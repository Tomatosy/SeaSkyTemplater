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

    public class ModelService : IModelService
    {
        [Dependency]
        public IModelRepository ModelRepository { get; set; }

        [Dependency]
        public IModelDetailRepository ModelDetailRepository { get; set; }



        /// <summary>
        /// 查询动态连接表分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResultModel<ResultModel> ListPageDynamicJoinModelList(TableSelModel model)
        {
            try
            {
                System.Data.DataTable selTable = this.ModelRepository.GetDynamicJoinTableData(model);

                if (selTable.Columns.Contains(model.OrderByName ?? "~!@"))
                {
                    selTable.DefaultView.Sort = model.OrderByName + " ASC";
                    selTable = selTable.DefaultView.ToTable();
                }
                int dataCount = (selTable?.Rows?.Count + string.Empty).ToInt(0);
                int? totalPages = 0;
                if (dataCount > 0)
                {
                    model.PageSize = model.PageSize ?? 999999;
                    totalPages = (dataCount + model.PageSize - 1) / model.PageSize;
                }
                selTable = DynamicTableHelper.GetPagedTable(selTable, model.PageNo ?? 1, model.PageSize ?? 999999);
                return new SuccessResultModel<ResultModel>(new ResultModel() { DT = selTable, dataCount = dataCount, totalPages = totalPages });
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(EnumLogLevel.Fatal, "ListViewPageDynamicModelList", JsonConvert.SerializeObject(model), "Model", "查询动态表视图分页.", e);
                //return new ErrorResultModel<ResultModel>(EnumErrorCode.系统异常, "查询动态表视图分页!");
                return new ErrorResultModel<ResultModel>(EnumErrorCode.系统异常, e.Message);
            }
        }

        /// <summary>
        /// 查询动态表分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResultModel<ResultModel> ListPageDynamicModelList(TableSelModel model)
        {
            try
            {
                System.Data.DataTable selTable = this.ModelRepository.GetDynamicTableData(model);

                if (selTable.Columns.Contains(model.OrderByName ?? "~!@"))
                {
                    selTable.DefaultView.Sort = model.OrderByName + " ASC";
                    selTable = selTable.DefaultView.ToTable();
                }
                int dataCount = (selTable?.Rows?.Count + string.Empty).ToInt(0);
                int? totalPages = 0;
                if (dataCount > 0)
                {
                    model.PageSize = model.PageSize ?? 999999;
                    totalPages = (dataCount + model.PageSize - 1) / model.PageSize;
                }
                selTable = DynamicTableHelper.GetPagedTable(selTable, model.PageNo ?? 1, model.PageSize ?? 999999);
                return new SuccessResultModel<ResultModel>(new ResultModel() { DT = selTable, dataCount = dataCount, totalPages = totalPages });
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(EnumLogLevel.Fatal, "ListViewPageDynamicModelList", JsonConvert.SerializeObject(model), "Model", "查询动态表视图分页.", e);
                //return new ErrorResultModel<ResultModel>(EnumErrorCode.系统异常, "查询动态表视图分页!");
                return new ErrorResultModel<ResultModel>(EnumErrorCode.系统异常, e.Message);
            }
        }

        /// <summary>
        /// 新增动态模块表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResultModel<int> InsertDynamicModel(TableSelModel model)
        {
            try
            {
                if (!model.ColSel.Exists(x => x.ColName == model.TableName + "ID"))
                {
                    model.ColSel.Add(new TableColSelModel
                    {
                        ColName = model.TableName + "ID",
                        ColValue = Guid.NewGuid() + string.Empty
                    });
                }
                return this.ModelRepository.InsertDynamicModel(model);
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(EnumLogLevel.Fatal, "InsertDynamicModel", JsonConvert.SerializeObject(model), "Model", "新增动态模块表 异常.", e);
                //return new ErrorResultModel<int>(EnumErrorCode.系统异常, "新增动态模块表 异常!");
                return new ErrorResultModel<int>(EnumErrorCode.系统异常, e.Message);
            }
        }

        /// <summary>
        /// 多行修改动态模块表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResultModel<int> MultiLineUpdateDynamicModel(List<TableSelModel> model)
        {
            try
            {
                return this.ModelRepository.MultiLineUpdateDynamicModel(model);
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(EnumLogLevel.Fatal, "MultiLineUpdateDynamicModel", JsonConvert.SerializeObject(model), "Model", "多行修改动态模块表 异常.", e);
                //return new ErrorResultModel<int>(EnumErrorCode.系统异常, "多行修改动态模块表 异常!");
                return new ErrorResultModel<int>(EnumErrorCode.系统异常, e.Message);
            }
        }

        /// <summary>
        /// 批量删除动态表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResultModel<int> DelDynamicTableList(List<TableSelModel> model)
        {
            try
            {
                return this.ModelRepository.DelDynamicTableList(model);
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(EnumLogLevel.Fatal, "DelDynamicTableList", JsonConvert.SerializeObject(model), "Model", "批量删除动态表 异常.", e);
                //return new ErrorResultModel<int>(EnumErrorCode.系统异常, "批量删除动态表 异常!");
                return new ErrorResultModel<int>(EnumErrorCode.系统异常, e.Message);
            }
        }
        #region 基础方法
        /// <summary>
        /// 获取模块表视图列表分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResultModel<PageModel<ModelViewModel>> ListViewPageModel(ModelViewModel model)
        {
            try
            {
                model.PageNO = model.PageNO ?? 1;
                model.PageSize = model.PageSize ?? int.MaxValue;
                model.IsDelete = false;
                // 开启查询outModel里面的视图
                using (this.ModelRepository.BeginSelView())
                {
                    using (this.ModelRepository.BeginLikeMode())
                    {
                        return new SuccessResultModel<PageModel<ModelViewModel>>(this.ModelRepository.ListViewPage(model));
                    }
                }
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(EnumLogLevel.Fatal, "ListViewPageModel", JsonConvert.SerializeObject(model), "Model", "获取模块表视图列表分页查询数据时发生错误.", e);
                return new ErrorResultModel<PageModel<ModelViewModel>>(EnumErrorCode.系统异常, "获取模块表视图列表分页查询数据时发生错误!");
            }
        }

        /// <summary>
        /// 获取模块表列表分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResultModel<PageModel<ModelOutputModel>> ListPageModel(ModelInputModel model)
        {
            try
            {
                model.PageNO = model.PageNO ?? 1;
                model.PageSize = model.PageSize ?? int.MaxValue;
                model.IsDelete = false;
                using (this.ModelRepository.BeginLikeMode())
                {
                    return new SuccessResultModel<PageModel<ModelOutputModel>>(this.ModelRepository.ListPage(model));
                }
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(EnumLogLevel.Fatal, "ListPageModel", JsonConvert.SerializeObject(model), "Model", "获取模块表列表分页查询数据时发生错误.", e);
                return new ErrorResultModel<PageModel<ModelOutputModel>>(EnumErrorCode.系统异常, "获取模块表列表分页查询数据时发生错误!");
            }
        }

        /// <summary>
        /// 新增、修改模块表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResultModel<ModelOutputModel> ModifyModel(ModelInputModel model)
        {
            SuccessResultModel<ModelOutputModel> result = new SuccessResultModel<ModelOutputModel>();
            try
            {

                ModelOutputModel selModel = this.ModelRepository.SelectWithModel(new ModelModel()
                {
                    ModelName = model.ModelName
                });
                ModelOutputModel selModelCode = this.ModelRepository.SelectWithModel(new ModelModel()
                {
                    ModelCode = model.ModelCode
                });
                if (model.ModelID.IsNullOrEmpty())
                {
                    if (selModel != null)
                    {
                        return new ErrorResultModel<ModelOutputModel>(EnumErrorCode.参数校验未通过, "名称已存在");
                    }
                    if (selModelCode != null)
                    {
                        return new ErrorResultModel<ModelOutputModel>(EnumErrorCode.参数校验未通过, "编号已存在");
                    }

                    model.ModelID = model.ModelID ?? Guid.NewGuid();
                    List<ModelDetailInputModel> initModelDeatil = GetInitModelDetailList(model);

                    TransactionOptions option = new TransactionOptions();
                    option.IsolationLevel = IsolationLevel.ReadCommitted;
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
                    {
                        // 初始化表-- 》 全局固定字段: 工号、审核状态、审核时间、确认标识、确认人、确认时间
                        this.ModelDetailRepository.InsertCol(initModelDeatil.ToArray());
                        this.ModelDetailRepository.InitDBTable(model);
                        result.Data = this.ModelRepository.InsertAndReturn(model);
                        scope.Complete();
                    }
                }
                else
                {
                    if (selModel != null && selModel.ModelID != model.ModelID)
                    {
                        return new ErrorResultModel<ModelOutputModel>(EnumErrorCode.参数校验未通过, "名称已存在");
                    }
                    if (selModelCode != null && selModelCode.ModelID != model.ModelID)
                    {
                        return new ErrorResultModel<ModelOutputModel>(EnumErrorCode.参数校验未通过, "编号已存在");
                    }
                    result.Data = this.ModelRepository.UpdateWithKeysAndReturn(model);
                }
                return result;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(EnumLogLevel.Fatal, "ModifyModel", JsonConvert.SerializeObject(model), "Model", "新增、修改模块表异常！", ex);
                return new ErrorResultModel<ModelOutputModel>(EnumErrorCode.系统异常, "新增、修改模块表异常");
            }
        }
        /// <summary>
        /// 删除模块表 (逻辑删除)
        /// </summary>
        /// <param name="IDs"></param>
        /// <returns></returns>
        public BaseResultModel<int> DeleteModel(List<Guid?> IDs)
        {
            SuccessResultModel<int> result = new SuccessResultModel<int>();
            ErrorResultModel<int> error = new ErrorResultModel<int>();
            try
            {
                ModelOutputModel selModel = new ModelOutputModel();
                ModelInputModel inputModel = new ModelInputModel();
                TransactionOptions option = new TransactionOptions();
                option.IsolationLevel = IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    foreach (Guid? item in IDs)
                    {
                        selModel = this.ModelRepository.SelectWithKeys(new ModelModel()
                        {
                            ModelID = item
                        });

                        inputModel = new ModelInputModel()
                        {
                            ModelID = selModel.ModelID,
                            ModelCode = selModel.ModelCode,
                            IsDelete = true
                        };

                        result.Data = this.ModelDetailRepository.UpdateWithModel(new ModelDetailModel() { IsDelete = true }, new ModelDetailModel() { ModelID = selModel.ModelID });
                        result.Data = this.ModelRepository.UpdateWithKeys(inputModel);

                        this.ModelRepository.DelDBTable(inputModel);
                    }
                    scope.Complete();
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
                LogWriter.WriteLog(EnumLogLevel.Fatal, "DeleteModel", JsonConvert.SerializeObject(IDs), "Model", "删除模块表 (逻辑删除)异常！", ex);
                error.ErrorCode = EnumErrorCode.系统异常;
                error.ErrorMessage = "删除模块表 (逻辑删除)异常!";
                return error;
            }
        }

        /// <summary>
        /// 获取单个模块表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public BaseResultModel<ModelViewModel> GetModel(Guid? ID)
        {
            try
            {
                // 开启查询outModel里面的视图
                using (this.ModelRepository.BeginSelView())
                {
                    using (this.ModelRepository.BeginLikeMode())
                    {
                        return new SuccessResultModel<ModelViewModel>(
                            this.ModelRepository.SelectWithViewModel(new ModelViewModel()
                            {
                                ModelID = ID
                            }
                        ));
                    }
                }
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(EnumLogLevel.Fatal, "GetModel", ID + string.Empty, "Model", "获取单个模块表异常", e);
                return new ErrorResultModel<ModelViewModel>(EnumErrorCode.系统异常, "获取单个模块表异常!");
            }
        }
        #endregion
        private static List<ModelDetailInputModel> GetInitModelDetailList(ModelInputModel model)
        {
            List<ModelDetailInputModel> initModelDeatil = new List<ModelDetailInputModel>();

            initModelDeatil.Add(new ModelDetailInputModel()
            {
                ModelID = model.ModelID,
                ColIndex = 1,
                ColName = $"{model.ModelCode}ID",
                ColMemo = "主键ID",
                ColType = "uniqueidentifier"
            });
            initModelDeatil.Add(new ModelDetailInputModel()
            {
                ModelID = model.ModelID,
                ColIndex = 2,
                ColName = "IsDelete",
                ColMemo = "是否删除",
                ColType = "bit"
            });
            return initModelDeatil;
        }
    }
}
