namespace Tomato.NewTempProject.Model
{
    using Tomato.StandardLib.MyAttribute;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Tomato.StandardLib.MyModel;

    /// <summary>
    /// 模块明细表输入扩展类
    /// </summary>
    public partial class ModelDetailInputModel : ModelDetailModel
    {
        public bool OrderAscByColIndex { get; set; }
    }

}
