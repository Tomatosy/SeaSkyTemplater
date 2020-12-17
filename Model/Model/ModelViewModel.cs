namespace Tomato.NewTempProject.Model
{
    using Tomato.StandardLib.MyAttribute;
    using System.Data;
    using Tomato.StandardLib.MyModel;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 学年表输出扩展类
    /// </summary>
    [DBTableInfo("View_tb_Model")]
    [Serializable]
    public partial class ModelViewModel : ModelModel
    {
        public List<ModelViewModel> Child { get; set; }

    }
}
