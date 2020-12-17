﻿namespace Tomato.NewTempProject.Model
{
    using Tomato.StandardLib.MyAttribute;
    using System.Data;
    using Tomato.StandardLib.MyModel;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 应用输出扩展类
    /// </summary>
    [DBTableInfo("View_tb_Menu")]
    public partial class MenuOutputModel : MenuModel
    {
        #region private property
        /// <summary>
        /// 子集
        /// </summary>
        public List<MenuOutputModel> Children { get; set; }

        #endregion

    }
}