namespace UnitTest_Tomato.NewTempProject
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Practices.Unity;
    using Tomato.StandardLib.MyModel;
    using System.Collections.Generic;
    using System.Transactions;
    using Tomato.NewTempProject.Model;
    using Tomato.NewTempProject.BLL;
    using Tomato.StandardLib.DynamicPage;

    [TestClass]
    public class DynamicModel_Test
    {

        private IModelService ModelService = ApplicationContext.Current.UnityContainer.Resolve<IModelService>();
        private IModelDetailService ModelDetailService = ApplicationContext.Current.UnityContainer.Resolve<IModelDetailService>();

        private TransactionScope scope;

        #region 附加测试特性

        [TestInitialize()]
        public void MyTestInitialize()
        {
            TransactionOptions transaction = new TransactionOptions();
            transaction.IsolationLevel = IsolationLevel.ReadCommitted;
            scope = new TransactionScope(TransactionScopeOption.Required, transaction);
        }

        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        [TestCleanup()]
        public void MyTestCleanup()
        {
            scope.Dispose();
        }
        #endregion



        /// <summary>
        /// 查询动态连接表分页
        /// </summary>
        [TestMethod]
        public void ListPageDynamicJoinModelList()
        {
            // 新增表
            ModelInputModel inputMaster = new ModelInputModel()
            {
                ModelCode = "tb_test",
                ModelName = "测试表"
            };
            BaseResultModel<ModelOutputModel> resultMaster = this.ModelService.ModifyModel(inputMaster);
            Assert.IsTrue(resultMaster.IsSuccess, resultMaster.ErrorMessage);

            ModelInputModel inputSon = new ModelInputModel()
            {
                ModelCode = "tb_testDetail",
                ModelName = "测试子表"
            };
            BaseResultModel<ModelOutputModel> resultSon = this.ModelService.ModifyModel(inputSon);
            Assert.IsTrue(resultSon.IsSuccess, resultSon.ErrorMessage);

            // 新增动态表字段
            ModelDetailInputModel inputSonModel = new ModelDetailInputModel()
            {
                ColIndex = 1,
                ColMemo = "备注",
                ColName = "dynamicID",
                ColType = "nvarchar(256)",
                ModelID = resultSon?.Data?.ModelID
            };
            BaseResultModel<ModelDetailOutputModel> resultSonDetail = this.ModelDetailService.ModifyModelDetail(inputSonModel);
            Assert.IsTrue(resultSonDetail.IsSuccess, resultSonDetail.ErrorMessage);

            // 新增动态表数据
            string tempGuid = Guid.NewGuid() + string.Empty;
            TableSelModel selMasterTable = new TableSelModel()
            {
                TableName = inputMaster.ModelCode,
                ColSel = new List<TableColSelModel>() {
                    new TableColSelModel(){
                        ColName="testID",
                        ColValue=tempGuid
                    }
                }
            };
            BaseResultModel<int> resultMasterTab = this.ModelService.InsertDynamicModel(selMasterTable);
            Assert.IsTrue(resultMasterTab.IsSuccess, resultMasterTab.ErrorMessage);

            TableSelModel selSonTable = new TableSelModel()
            {
                TableName = inputSon.ModelCode,
                ColSel = new List<TableColSelModel>() {
                    new TableColSelModel(){
                        ColName="dynamicID",
                        ColValue=tempGuid
                    }
                }
            };
            BaseResultModel<int> resultSonTab = this.ModelService.InsertDynamicModel(selSonTable);
            Assert.IsTrue(resultSonTab.IsSuccess, resultSonTab.ErrorMessage);

            // 查询动态表分页
            TableSelModel selTableList = new TableSelModel()
            {
                PageNO = 1,
                PageSize = 20,
                TableName = inputSon.ModelCode,
                JoinMasterTableName = inputMaster.ModelCode,
                WhereSel = new List<TableColSelModel>() {
                    new TableColSelModel(){
                        ColName="dynamicID",
                        ColValue=tempGuid
                    }
                }
            };
            BaseResultModel<ResultModel> resultTableList = this.ModelService.ListPageDynamicJoinModelList(selTableList);
            Assert.IsTrue(resultTableList.IsSuccess && resultTableList.Data.dataCount > 0, resultTableList.ErrorMessage);
        }

        /// <summary>
        /// 查询动态表分页
        /// </summary>
        [TestMethod]
        public void ListPageDynamicModelList()
        {
            // 新增表
            ModelInputModel inputMaster = new ModelInputModel()
            {
                ModelCode = "tb_test",
                ModelName = "测试表"
            };
            BaseResultModel<ModelOutputModel> resultMaster = this.ModelService.ModifyModel(inputMaster);
            Assert.IsTrue(resultMaster.IsSuccess, resultMaster.ErrorMessage);

            // 新增动态表字段
            ModelDetailInputModel inputModel = new ModelDetailInputModel()
            {
                ColIndex = 1,
                ColMemo = "AA",
                ColName = "BB",
                ColType = "nvarchar(256)",
                ModelID = resultMaster?.Data?.ModelID
            };

            BaseResultModel<ModelDetailOutputModel> resultDetail = this.ModelDetailService.ModifyModelDetail(inputModel);
            Assert.IsTrue(resultDetail.IsSuccess, resultDetail.ErrorMessage);

            // 新增动态表数据
            TableSelModel selTable = new TableSelModel()
            {
                TableName = inputMaster.ModelCode,
                ColSel = new List<TableColSelModel>() {
                    new TableColSelModel(){
                        ColName=inputModel.ColName,
                        ColValue="BBValue"
                    }
                }
            };
            BaseResultModel<int> result = this.ModelService.InsertDynamicModel(selTable);
            Assert.IsTrue(result.IsSuccess, result.ErrorMessage);

            // 查询动态表分页
            TableSelModel selTableList = new TableSelModel()
            {
                PageNO = 1,
                PageSize = 20,
                TableName = "tb_test",
                WhereSel = new List<TableColSelModel>() {
                    new TableColSelModel(){
                        ColName=inputModel.ColName,
                        ColValue="BBValue"
                    }
                }
            };
            BaseResultModel<ResultModel> resultTableList = this.ModelService.ListPageDynamicModelList(selTableList);
            Assert.IsTrue(resultTableList.IsSuccess && resultTableList.Data.dataCount > 0, resultTableList.ErrorMessage);
        }



        /// <summary>
        /// 新增动态表数据
        /// </summary>
        [TestMethod]
        public void InsertDynamicModel()
        {
            // 新增表
            ModelInputModel inputMaster = new ModelInputModel()
            {
                ModelCode = "tb_test",
                ModelName = "测试表"
            };
            BaseResultModel<ModelOutputModel> resultMaster = this.ModelService.ModifyModel(inputMaster);
            Assert.IsTrue(resultMaster.IsSuccess, resultMaster.ErrorMessage);

            // 新增动态表字段
            ModelDetailInputModel inputModel = new ModelDetailInputModel()
            {
                ColIndex = 1,
                ColMemo = "AA",
                ColName = "BB",
                ColType = "nvarchar(256)",
                ModelID = resultMaster?.Data?.ModelID
            };

            BaseResultModel<ModelDetailOutputModel> resultDetail = this.ModelDetailService.ModifyModelDetail(inputModel);
            Assert.IsTrue(resultDetail.IsSuccess, resultDetail.ErrorMessage);

            //  新增动态表数据
            TableSelModel selTable = new TableSelModel()
            {
                TableName = inputMaster.ModelCode,
                ColSel = new List<TableColSelModel>() {
                    new TableColSelModel(){
                        ColName=inputModel.ColName,
                        ColValue="BBValue"
                    }
                }
            };
            BaseResultModel<int> result = this.ModelService.InsertDynamicModel(selTable);
            Assert.IsTrue(result.IsSuccess, result.ErrorMessage);
        }

        /// <summary>
        /// 修改动态表数据
        /// </summary>
        [TestMethod]
        public void MultiLineUpdateDynamicModel()
        {
            // 新增表
            ModelInputModel inputMaster = new ModelInputModel()
            {
                ModelCode = "tb_test",
                ModelName = "测试表"
            };
            BaseResultModel<ModelOutputModel> resultMaster = this.ModelService.ModifyModel(inputMaster);
            Assert.IsTrue(resultMaster.IsSuccess, resultMaster.ErrorMessage);

            // 新增动态表字段
            ModelDetailInputModel inputModel = new ModelDetailInputModel()
            {
                ColIndex = 1,
                ColMemo = "AA",
                ColName = "BB",
                ColType = "nvarchar(256)",
                ModelID = resultMaster?.Data?.ModelID
            };

            BaseResultModel<ModelDetailOutputModel> resultDetail = this.ModelDetailService.ModifyModelDetail(inputModel);
            Assert.IsTrue(resultDetail.IsSuccess, resultDetail.ErrorMessage);

            // 新增动态表数据
            TableSelModel selTable = new TableSelModel()
            {
                TableName = inputMaster.ModelCode,
                ColSel = new List<TableColSelModel>() {
                    new TableColSelModel(){
                        ColName=inputModel.ColName,
                        ColValue="BBValue"
                    }
                }
            };
            BaseResultModel<int> result = this.ModelService.InsertDynamicModel(selTable);
            Assert.IsTrue(result.IsSuccess, result.ErrorMessage);

            // 修改动态表数据
            List<TableSelModel> inputModelTab = new List<TableSelModel>()
            {
                new TableSelModel(){
                TableName = inputMaster.ModelCode,
                ColSel = new List<TableColSelModel>() {
                    new TableColSelModel(){
                        ColName=selTable.ColSel[0].ColName,
                        ColValue="CCCValue"
                    }
                },
                WhereSel=new List<TableColSelModel>(){
                    new TableColSelModel(){
                        ColName=selTable.ColSel[0].ColName,
                        ColValue=selTable.ColSel[0].ColValue
                }
                }
                }
            };

            result = this.ModelService.MultiLineUpdateDynamicModel(inputModelTab);
            Assert.IsTrue(result.IsSuccess, result.ErrorMessage);
        }

        /// <summary>
        /// 删除动态表数据
        /// </summary>
        [TestMethod]
        public void DelDynamicTableList()
        {
            // 新增表
            ModelInputModel inputMaster = new ModelInputModel()
            {
                ModelCode = "tb_test",
                ModelName = "测试表"
            };
            BaseResultModel<ModelOutputModel> resultMaster = this.ModelService.ModifyModel(inputMaster);
            Assert.IsTrue(resultMaster.IsSuccess, resultMaster.ErrorMessage);

            // 新增动态表字段
            ModelDetailInputModel inputModel = new ModelDetailInputModel()
            {
                ColIndex = 1,
                ColMemo = "AA",
                ColName = "BB",
                ColType = "nvarchar(256)",
                ModelID = resultMaster?.Data?.ModelID
            };

            BaseResultModel<ModelDetailOutputModel> resultDetail = this.ModelDetailService.ModifyModelDetail(inputModel);
            Assert.IsTrue(resultDetail.IsSuccess, resultDetail.ErrorMessage);

            //  新增动态表数据
            TableSelModel selTable = new TableSelModel()
            {
                TableName = inputMaster.ModelCode,
                ColSel = new List<TableColSelModel>() {
                    new TableColSelModel(){
                        ColName=inputModel.ColName,
                        ColValue="BBValue"
                    }
                }
            };
            BaseResultModel<int> result = this.ModelService.InsertDynamicModel(selTable);
            Assert.IsTrue(result.IsSuccess, result.ErrorMessage);

            // 删除动态表数据
            List<TableSelModel> inputTabModel = new List<TableSelModel>()
            {
                new TableSelModel(){
                PageNO = 1,
                PageSize = 20,
                TableName = inputMaster.ModelCode,
                WhereSel = new List<TableColSelModel>() {
                    new TableColSelModel(){
                        ColName=inputModel.ColName,
                        ColValue="BBValue"
                    }
                }
                }
            };
            result = this.ModelService.DelDynamicTableList(inputTabModel);
            Assert.IsTrue(result.IsSuccess, result.ErrorMessage);
        }

        #region 基础方法
        /// <summary>
        /// 查询动态表分页
        /// </summary>
        [TestMethod]
        public void ListPageModel()
        {
            // 新增表
            ModelInputModel inputModel = new ModelInputModel()
            {
                PageNO = 1,
                PageSize = 20,
            };
            BaseResultModel<PageModel<ModelOutputModel>> result = this.ModelService.ListPageModel(inputModel);
            Assert.IsTrue(result.IsSuccess, result.ErrorMessage);
        }


        /// <summary>
        /// 编辑动态表
        /// </summary>
        [TestMethod]
        public void ModifyModel()
        {
            // 新增表
            ModelInputModel inputModel = new ModelInputModel()
            {
                ModelCode = "tb_test",
                ModelName = "测试表"
            };
            BaseResultModel<ModelOutputModel> result = this.ModelService.ModifyModel(inputModel);
            Assert.IsTrue(result.IsSuccess, result.ErrorMessage);

            // 编辑表
            inputModel = new ModelInputModel()
            {
                ModelID = result?.Data?.ModelID,
                ModelCode = "tb_test1",
                ModelName = "测试表1"
            };
            result = this.ModelService.ModifyModel(inputModel);
            Assert.IsTrue(result.IsSuccess, result.ErrorMessage);
        }

        /// <summary>
        /// 删除动态表
        /// </summary>
        [TestMethod]
        public void DeleteModel()
        {
            // 新增表
            ModelInputModel inputModel = new ModelInputModel()
            {
                ModelCode = "tb_test",
                ModelName = "测试表"
            };
            BaseResultModel<ModelOutputModel> result = this.ModelService.ModifyModel(inputModel);
            Assert.IsTrue(result.IsSuccess, result.ErrorMessage);

            // 删除表
            BaseResultModel<int> delResult = this.ModelService.DeleteModel(new List<Guid?> { result.Data.ModelID });
            Assert.IsTrue(delResult.IsSuccess && delResult.Data > 0, delResult.ErrorMessage);
        }


        /// <summary>
        /// 查询动态字段表分页
        /// </summary>
        [TestMethod]
        public void ListPageModelDetail()
        {
            // 新增表
            ModelDetailInputModel inputModel = new ModelDetailInputModel()
            {
                PageNO = 1,
                PageSize = 20,
            };
            BaseResultModel<PageModel<ModelDetailOutputModel>> result = this.ModelDetailService.ListPageModelDetail(inputModel);
            Assert.IsTrue(result.IsSuccess, result.ErrorMessage);
        }

        /// <summary>
        /// 编辑动态表字段
        /// </summary>
        [TestMethod]
        public void ModifyModelDetail()
        {
            // 新增表
            ModelInputModel inputMaster = new ModelInputModel()
            {
                ModelCode = "tb_test",
                ModelName = "测试表"
            };
            BaseResultModel<ModelOutputModel> resultMaster = this.ModelService.ModifyModel(inputMaster);
            Assert.IsTrue(resultMaster.IsSuccess, resultMaster.ErrorMessage);

            // 新增动态表字段
            ModelDetailInputModel inputModel = new ModelDetailInputModel()
            {
                ColIndex = 1,
                ColMemo = "AA",
                ColName = "BB",
                ColType = "nvarchar(256)",
                ModelID = resultMaster?.Data?.ModelID
            };

            BaseResultModel<ModelDetailOutputModel> result = this.ModelDetailService.ModifyModelDetail(inputModel);
            Assert.IsTrue(result.IsSuccess, result.ErrorMessage);

            // 编辑动态表字段
            inputModel = new ModelDetailInputModel()
            {
                ModelDetailID = result?.Data?.ModelDetailID,
                ColIndex = 1,
                ColMemo = "AA",
                ColName = "BB",
                ColType = "nvarchar(256)",
                ModelID = resultMaster?.Data?.ModelID
            };

            result = this.ModelDetailService.ModifyModelDetail(inputModel);
            Assert.IsTrue(result.IsSuccess, result.ErrorMessage);

        }


        /// <summary>
        /// 删除动态字段表
        /// </summary>
        [TestMethod]
        public void DeleteModelDetail()
        {
            // 新增表
            ModelInputModel inputMaster = new ModelInputModel()
            {
                ModelCode = "tb_test",
                ModelName = "测试表"
            };
            BaseResultModel<ModelOutputModel> resultMaster = this.ModelService.ModifyModel(inputMaster);
            Assert.IsTrue(resultMaster.IsSuccess, resultMaster.ErrorMessage);

            // 新增动态表字段
            ModelDetailInputModel inputModel = new ModelDetailInputModel()
            {
                ColIndex = 1,
                ColMemo = "AA",
                ColName = "BB",
                ColType = "nvarchar(256)",
                ModelID = resultMaster?.Data?.ModelID
            };

            BaseResultModel<ModelDetailOutputModel> result = this.ModelDetailService.ModifyModelDetail(inputModel);
            Assert.IsTrue(result.IsSuccess, result.ErrorMessage);

            // 删除动态字段表
            BaseResultModel<int> delResult = this.ModelDetailService.DeleteModelDetail(new List<Guid?> { result.Data.ModelDetailID });
            Assert.IsTrue(delResult.IsSuccess && delResult.Data > 0, delResult.ErrorMessage);
        }

        #endregion
    }


}
