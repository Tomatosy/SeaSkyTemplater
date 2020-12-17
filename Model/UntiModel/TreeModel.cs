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
    public class TreeModel
    {
        public Guid? ID { get; set; }
        public string Label { get; set; }
        public string Icon { get; set; }
        public List<TreeModel> Children { get; set; } = new List<TreeModel>();
    }
}
