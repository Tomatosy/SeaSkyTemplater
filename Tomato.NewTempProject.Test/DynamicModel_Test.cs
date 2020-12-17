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
        /// 查询动态表分页
        /// </summary>
        [TestMethod]
        public void ListPageDynamicModelList()
        {
            TableSelModel selTable = new TableSelModel()
            {
                PageNO = 1,
                PageSize = 20,
                TableName = "tb_AcademicYear",
                WhereSel = new List<TableColSelModel>() {
                    new TableColSelModel(){
                        ColName="Status",
                        ColValue="2019"
                    }
                }
            };
            BaseResultModel<ResultModel> result = this.ModelService.ListPageDynamicModelList(selTable);
            Assert.IsTrue(result.IsSuccess, result.ErrorMessage);
        }



        /// <summary>
        /// 新增动态表数据
        /// </summary>
        [TestMethod]
        public void InsertDynamicModel()
        {
            TableSelModel selTable = new TableSelModel()
            {
                TableName = "tb_AcademicYear",
                ColSel = new List<TableColSelModel>() {
                    new TableColSelModel(){
                        ColName="AcademicYearName",
                        ColValue="AAA"
                    },
                    new TableColSelModel(){
                        ColName="Term",
                        ColValue="AAA"
                    },
                    new TableColSelModel(){
                        ColName="InputStatus",
                        ColValue="AAA"
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
            TableSelModel selTable = new TableSelModel()
            {
                TableName = "tb_AcademicYear",
                ColSel = new List<TableColSelModel>() {
                    new TableColSelModel(){
                        ColName="AcademicYearName",
                        ColValue="AAA"
                    },
                    new TableColSelModel(){
                        ColName="Term",
                        ColValue="AAA"
                    },
                    new TableColSelModel(){
                        ColName="InputStatus",
                        ColValue="AAA"
                    }
                }
            };
            BaseResultModel<int> result = this.ModelService.InsertDynamicModel(selTable);
            Assert.IsTrue(result.IsSuccess, result.ErrorMessage);

            List<TableSelModel> inputModel = new List<TableSelModel>()
            {
                new TableSelModel(){
                TableName = "tb_AcademicYear",
                ColSel = new List<TableColSelModel>() {
                    new TableColSelModel(){
                        ColName="AcademicYearName",
                        ColValue="BBB"
                    }
                },
                WhereSel=new List<TableColSelModel>(){
                        ColName="AcademicYearName",
                        ColValue="AAA"
                }
                }
            };

            BaseResultModel<int> result = this.ModelService.MultiLineUpdateDynamicModel(inputModel);
            Assert.IsTrue(result.IsSuccess, result.ErrorMessage);
        }

        /// <summary>
        /// 删除动态表数据
        /// </summary>
        [TestMethod]
        public void DelDynamicTableList()
        {
            List<TableSelModel> inputModel = new List<TableSelModel>()
            {
                new TableSelModel(){
                PageNO = 1,
                PageSize = 20,
                TableName = "tb_AcademicYear",
                WhereSel = new List<TableColSelModel>() {
                    new TableColSelModel(){
                        ColName="Status",
                        ColValue="2019"
                    }
                }
                }
            };
            BaseResultModel<int> result = this.ModelService.DelDynamicTableList(inputModel);
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
            // 新增动态表字段
            ModelDetailInputModel inputModel = new ModelDetailInputModel()
            {
                ColIndex = 1,
                ColMemo = "AA",
                ColName = "BB",
                ColType = "nvarchar(256)",
                ModelID = Guid.Parse("b3bbdcc0-26c4-4756-b13b-e56918e49de4")
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
                ModelID = Guid.Parse("b3bbdcc0-26c4-4756-b13b-e56918e49de4")
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
            // 新增动态字段表
            ModelDetailInputModel inputModel = new ModelDetailInputModel()
            {
                ColIndex = 1,
                ColMemo = "AA",
                ColName = "BB",
                ColType = "nvarchar(256)",
                ModelID = Guid.Parse("b3bbdcc0-26c4-4756-b13b-e56918e49de4")
            };

            BaseResultModel<ModelDetailOutputModel> result = this.ModelDetailService.ModifyModelDetail(inputModel);
            Assert.IsTrue(result.IsSuccess, result.ErrorMessage);

            // 删除动态字段表
            BaseResultModel<int> delResult = this.ModelDetailService.DeleteModelDetail(new List<Guid?> { result.Data.ModelID });
            Assert.IsTrue(delResult.IsSuccess && delResult.Data > 0, delResult.ErrorMessage);
        }

        #endregion
    }


}
