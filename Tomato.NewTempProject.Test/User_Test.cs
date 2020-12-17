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

    [TestClass]
    public class User_Test
    {

        private IUserService UserService = ApplicationContext.Current.UnityContainer.Resolve<IUserService>();

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
        /// 获取用户 列表分页
        /// </summary>
        [TestMethod]
        public void ListPageUser_Test()
        {
            UserViewModel testModel = null;
            BaseResultModel<PageModel<UserViewModel>> result = UserService.ListViewPageUser(testModel);
            Assert.IsTrue(result.IsSuccess && result.Data.DataCount > 0, result.ErrorMessage);

            testModel = new UserViewModel()
            {
                PageNO = 1,
                PageSize = 2,
                UserName = "测试UserName",
            };
            result = UserService.ListViewPageUser(testModel);
            Assert.IsTrue(result.IsSuccess, result.ErrorMessage);
        }

        /// <summary>
        /// 新增、修改用户 
        /// </summary>
        [TestMethod]
        public void ModifyUser_Test()
        {
            UserInputModel testModel = new UserInputModel()
            {
                UserName = "测试UserName",
            };
            BaseResultModel<UserOutputModel> result = UserService.ModifyUser(testModel);
            Assert.IsTrue(result.IsSuccess, result.ErrorMessage);

            testModel = new UserInputModel()
            {
                UserID = result.Data.UserID,
                UserName = "测试UserName",
            };
            result = UserService.ModifyUser(testModel);
            Assert.IsTrue(result.IsSuccess, result.ErrorMessage);
        }

        /// <summary>
        /// 删除用户  (逻辑删除)
        /// </summary>
        [TestMethod]
        public void DeleteUser_Test()
        {
            UserInputModel testModel = new UserInputModel()
            {
                UserName = "测试UserName",

            };
            BaseResultModel<UserOutputModel> insModelResult = UserService.ModifyUser(testModel);
            Assert.IsTrue(insModelResult.IsSuccess, insModelResult.ErrorMessage);

            BaseResultModel<int> result = UserService.DeleteUser(new List<Guid?>() { insModelResult.Data.UserID });
            Assert.IsTrue(result.IsSuccess, result.ErrorMessage);
        }

        /// <summary>
        /// 获取单个用户  
        /// </summary>
        [TestMethod]
        public void GetUser_Test()
        {
            UserInputModel testModel = new UserInputModel()
            {
                UserName = "测试UserName",

            };
            BaseResultModel<UserOutputModel> insModelResult = UserService.ModifyUser(testModel);
            Assert.IsTrue(insModelResult.IsSuccess, insModelResult.ErrorMessage);

            BaseResultModel<UserOutputModel> result = UserService.GetUser(insModelResult.Data.UserID);
            Assert.IsTrue(result.IsSuccess, result.ErrorMessage);
        }
    }


}
