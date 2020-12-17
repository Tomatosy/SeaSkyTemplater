using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Tomato.NewTempProject.Model
{
    public class ResultModel
    {
        public DataTable DT { get; set; }
        public int? dataCount { get; set; }
        public int? totalPages { get; set; }
    }
}
