using Tomato.StandardLib.MyAttribute;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tomato.StandardLib.MyModel;
using Tomato.NewTempProject.Model.Enum;

namespace Tomato.NewTempProject.Model
{
    [Serializable]
    public class TableColSelModel
    {
        /// <summary>
        /// 列名称
        /// </summary>
        public string ColName { get; set; }

        /// <summary>
        /// 列值
        /// </summary>
        public string ColValue { get; set; }
    }
}
