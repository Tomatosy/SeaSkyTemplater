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
    public class Appraisals_Test
    {

        private IAppraisalsService AppraisalsService = ApplicationContext.Current.UnityContainer.Resolve<IAppraisalsService>();

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


        #region 基础方法
        [TestMethod]
        public void AAA()
        {
            PagedInputDto pagedInputDto = new PagedInputDto()
            {
                PageIndex = 1,
                PageSize = 10,
                Order = "StuName desc"
            };
            pagedInputDto.Filter = new PageFilterDto()
            {
                Type = "and",
                Conditions = new List<Condition>()                            {                                new Condition() { Attribute = "StuName", Datatype = "nvarchar", Operatoer = "like", Value = "0" },                                new Condition() { Attribute = "Birthday", Datatype = "int", Operatoer = "null" }                            },
                Filters = new List<PageFilterDto>()                            {                                new PageFilterDto()                                {                                    Type = "or",                                    Conditions = new List<Condition>()                                    {                                        new Condition() { Attribute = "ApproveState", Datatype = "nvarchar", Operatoer = "eq", Value = "审核中" },                                        new Condition() { Attribute = "ApproveState", Datatype = "nvarchar", Operatoer = "eq", Value = "已审核" }                                    }                                },                                new PageFilterDto()                                {                                    Type = "and",                                    Conditions = new List<Condition>()                                    {                                        new Condition() { Attribute = "aaa", Datatype = "nvarchar", Operatoer = "eq", Value = "AAA" },                                        new Condition() { Attribute = "bbb", Datatype = "nvarchar", Operatoer = "eq", Value = "BB" }                                    }                                }                            }
            };

            string AA = pagedInputDto.Select;
            string BBB = pagedInputDto.Filter.ToString();
            string CCC = pagedInputDto.Filter.ToWhere();
            //string DDD = pagedInputDto.Filter.Filters.ToWhere();
            //string DDD = pagedInputDto.Filter.Filters.ToWhere();


            //var pagedResult = AppraisalsService.GetPage(pagedInputDto);
            //Assert.IsTrue(result.IsSuccess, result.ErrorMessage);
        }
        /// <summary>
        /// 获取考核项目表视图列表分页
        /// </summary>
        [TestMethod]
        public void ListViewPageAppraisals_Test()
        {
            AppraisalsViewModel testModel = null;
            BaseResultModel<PageModel<AppraisalsViewModel>> result = AppraisalsService.ListViewPageAppraisals(testModel);
            Assert.IsTrue(result.IsSuccess && result.Data.DataCount > 0, result.ErrorMessage);

            testModel = new AppraisalsViewModel()
            {
                PageNO = 1,
                PageSize = 2,
                AppraisalsName = "测试AppraisalsName",
            };
            result = AppraisalsService.ListViewPageAppraisals(testModel);
            Assert.IsTrue(result.IsSuccess, result.ErrorMessage);
        }

        /// <summary>
        /// 获取考核项目表列表分页
        /// </summary>
        [TestMethod]
        public void ListPageAppraisals_Test()
        {
            AppraisalsInputModel testModel = null;
            BaseResultModel<PageModel<AppraisalsOutputModel>> result = AppraisalsService.ListPageAppraisals(testModel);
            Assert.IsTrue(result.IsSuccess && result.Data.DataCount > 0, result.ErrorMessage);

            testModel = new AppraisalsInputModel()
            {
                PageNO = 1,
                PageSize = 2,
                AppraisalsName = "测试AppraisalsName",
            };
            result = AppraisalsService.ListPageAppraisals(testModel);
            Assert.IsTrue(result.IsSuccess, result.ErrorMessage);
        }

        /// <summary>
        /// 新增、修改考核项目表
        /// </summary>
        [TestMethod]
        public void ModifyAppraisals_Test()
        {
            AppraisalsInputModel testModel = new AppraisalsInputModel()
            {
                AppraisalsName = "测试AppraisalsName",
            };
            BaseResultModel<AppraisalsOutputModel> result = AppraisalsService.ModifyAppraisals(testModel);
            Assert.IsTrue(result.IsSuccess, result.ErrorMessage);

            testModel = new AppraisalsInputModel()
            {
                AppraisalsID = result.Data.AppraisalsID,
                AppraisalsName = "测试AppraisalsName",
            };
            result = AppraisalsService.ModifyAppraisals(testModel);
            Assert.IsTrue(result.IsSuccess, result.ErrorMessage);

            BaseResultModel<int> delResult = AppraisalsService.DeleteAppraisals(new List<Guid?>() { testModel.AppraisalsID });
            Assert.IsTrue(delResult.IsSuccess, delResult.ErrorMessage);
        }

        /// <summary>
        /// 获取单个考核项目表 
        /// </summary>
        [TestMethod]
        public void GetAppraisals_Test()
        {
            AppraisalsInputModel testModel = new AppraisalsInputModel()
            {
                AppraisalsName = "测试AppraisalsName",
            };
            BaseResultModel<AppraisalsOutputModel> insModelResult = AppraisalsService.ModifyAppraisals(testModel);
            Assert.IsTrue(insModelResult.IsSuccess, insModelResult.ErrorMessage);

            BaseResultModel<AppraisalsViewModel> result = AppraisalsService.GetAppraisals(insModelResult.Data.AppraisalsID);
            Assert.IsTrue(result.IsSuccess, result.ErrorMessage);

            BaseResultModel<int> delResult = AppraisalsService.DeleteAppraisals(new List<Guid?>() { insModelResult.Data.AppraisalsID });
            Assert.IsTrue(delResult.IsSuccess, delResult.ErrorMessage);
        }
        #endregion
    }


}
