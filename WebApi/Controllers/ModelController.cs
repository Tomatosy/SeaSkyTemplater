using Microsoft.Practices.Unity;
using Tomato.StandardLib.MyModel;
using System.Collections.Generic;
using System.Web.Http;
using Tomato.NewTempProject.BLL;
using Tomato.NewTempProject.Model;
using System;
using Tomato.NewTempProject.WebApi.Log;
using System.Net.Http;
using Tomato.StandardLib.MyExtensions;
using System.Transactions;

namespace Tomato.NewTempProject.WebApi.Controllers
{

    public class ModelController : ApiController
    {

        private IModelService ModelService = ApplicationContext.Current.UnityContainer.Resolve<IModelService>();
        /// <summary>
        /// 查询动态连接表分页
        /// </summary>
        /// <param name="model">ViewModel</param>
        /// <returns>ViewModel</returns>
        public BaseResultModel<ResultModel> ListPageDynamicJoinModelList(TableSelModel model)
        {
            if (model == null || model.TableName.IsNullOrEmpty())
            {
                return new ErrorResultModel<ResultModel>(EnumErrorCode.系统异常, "传入表名不能为空!");
            }
            if (model.JoinMasterTableName.IsNullOrEmpty())
            {
                return new ErrorResultModel<ResultModel>(EnumErrorCode.系统异常, "传入要左连接的表名不能为空!");
            }
            return ModelService.ListPageDynamicJoinModelList(model);
        }

        /// <summary>
        /// 查询动态表分页
        /// </summary>
        /// <param name="model">ViewModel</param>
        /// <returns>ViewModel</returns>
        public BaseResultModel<ResultModel> ListPageDynamicModelList(TableSelModel model)
        {
            if (model == null || model.TableName.IsNullOrEmpty())
            {
                return new ErrorResultModel<ResultModel>(EnumErrorCode.系统异常, "传入表名不能为空!");
            }
            return ModelService.ListPageDynamicModelList(model);
        }

        /// <summary>
        /// 新增主子动态模块表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResultModel<int> InsertJoinDynamicModel(List<TableSelModel> model)
        {
            int? execCount = 0;
            TransactionOptions option = new TransactionOptions();
            option.IsolationLevel = IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                foreach (TableSelModel item in model)
                {
                    BaseResultModel<int> result = InsertDynamicModel(item);
                    if (!result.IsSuccess)
                        return result;
                    execCount += InsertDynamicModel(item)?.Data;
                }
                scope.Complete();
            }
            return new SuccessResultModel<int>((execCount + string.Empty).ToInt(0));
        }

        /// <summary>
        /// 新增动态模块表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResultModel<int> InsertDynamicModel(TableSelModel model)
        {
            if (model == null || model.TableName.IsNullOrEmpty() || model.ColSel.Count == 0)
            {
                return new ErrorResultModel<int>(EnumErrorCode.参数校验未通过, "新增表名列名不能为空!");
            }
            return ModelService.InsertDynamicModel(model);
        }

        /// <summary>
        /// 删除动态模块表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResultModel<int> DelDynamicTableList(List<TableSelModel> model)
        {
            if (model == null)
            {
                return new ErrorResultModel<int>(EnumErrorCode.参数校验未通过, "删除表名、主键都不能为空!");
            }
            return ModelService.DelDynamicTableList(model);
        }

        /// <summary>
        /// 多行修改动态模块表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResultModel<int> MultiLineUpdateDynamicModel(List<TableSelModel> model)
        {
            if (model.Count == 0)
            {
                return new ErrorResultModel<int>(EnumErrorCode.参数校验未通过, "新增表名列名不能为空!");
            }
            foreach (TableSelModel item in model)
            {
                if (item == null || item.TableName.IsNullOrEmpty() || item.ColSel.Count == 0)
                {
                    return new ErrorResultModel<int>(EnumErrorCode.参数校验未通过, "新增表名列名不能为空!");
                }
                if (item == null || item.TableName.IsNullOrEmpty() || item.ColSel.Count == 0)
                {
                    return new ErrorResultModel<int>(EnumErrorCode.参数校验未通过, "修改条件不能为空!");
                }
            }
            return ModelService.MultiLineUpdateDynamicModel(model);
        }
        #region 基础方法

        /// <summary>
        /// 获取模块表视图列表分页
        /// </summary>
        /// <param name="model">ViewModel</param>
        /// <returns>ViewModel</returns>
        public BaseResultModel<PageModel<ModelViewModel>> ListViewPageModel(ModelViewModel model)
        {
            return ModelService.ListViewPageModel(model);
        }

        /// <summary>
        /// 获取模块表列表分页
        /// </summary>
        /// <param name="model">InputModel</param>
        /// <returns>OutputModel</returns>
        public BaseResultModel<PageModel<ModelOutputModel>> ListPageModel(ModelInputModel model)
        {
            return ModelService.ListPageModel(model);
        }

        /// <summary>
        /// 新增、修改模块表
        /// </summary>
        /// <param name="model">OutputModel</param>
        /// <returns>OutputModel</returns>
        public BaseResultModel<ModelOutputModel> ModifyModel(ModelInputModel model)
        {
            if (model == null)
            {
                return new ErrorResultModel<ModelOutputModel>(EnumErrorCode.请求参数错误, "参数不能为空");
            }
            ModelAttrEx arrtEx = new ModelAttrEx();
            string modelErrorMes = "";
            if (model.ModelID.IsNullOrEmpty())
            {
                modelErrorMes += arrtEx.AddAttrVaild<ModelInputModel>(ModelState, model);
            }
            else
            {
                modelErrorMes += arrtEx.EditAttrVaild<ModelInputModel>(ModelState, model);
            }
            if (!modelErrorMes.IsNullOrEmpty())
            {
                return new ErrorResultModel<ModelOutputModel>(EnumErrorCode.请求参数错误, modelErrorMes);
            }
            return ModelService.ModifyModel(model);
        }

        /// <summary>
        /// 删除模块表 (逻辑删除)
        /// </summary>
        /// <param name="IDs">List-->Guid?</param>
        /// <returns>受影响行数</returns>
        [HttpPost]
        public BaseResultModel<int> DeleteModel(List<Guid?> IDs)
        {
            if (IDs == null || IDs.Count == 0)
            {
                return new ErrorResultModel<int>(EnumErrorCode.请求参数错误, "参数不能为空");
            }
            return ModelService.DeleteModel(IDs);
        }

        /// <summary>
        /// 获取单个模块表
        /// </summary>
        /// <param name="ID">Guid?</param>
        /// <returns>ViewModel</returns>
        [HttpPost]
        public BaseResultModel<ModelViewModel> GetModel(Guid? ID)
        {
            if (ID.IsNullOrEmpty())
            {
                return new ErrorResultModel<ModelViewModel>(EnumErrorCode.请求参数错误, "参数不能为空");
            }
            return ModelService.GetModel(ID);
        }
        #endregion
    }


}
