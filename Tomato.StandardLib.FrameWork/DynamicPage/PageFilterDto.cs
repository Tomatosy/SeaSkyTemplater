using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tomato.StandardLib.DynamicPage
{
    public class PageFilterDto
    {
        private static Dictionary<string, string> dicOperator;

        private string _innerType;

        public List<PageFilterDto> Filters
        {
            get;
            set;
        }

        public List<Condition> Conditions
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public string InnerType
        {
            get
            {
                if (string.IsNullOrEmpty(_innerType))
                {
                    return Type;
                }
                return _innerType;
            }
            set => _innerType = value;
        }

        public int paramLocation
        {
            get;
            set;
        }

        public List<object> paramValues
        {
            get;
            set;
        }

        static PageFilterDto()
        {
            dicOperator = new Dictionary<string, string>();
            dicOperator["eq"] = "=={0}";
            dicOperator["ne"] = "!={0}";
            dicOperator["gt"] = ">{0}";
            dicOperator["ge"] = ">={0}";
            dicOperator["lt"] = "<{0}";
            dicOperator["le"] = "<={0}";
            dicOperator["like"] = "like %{0}%";
            dicOperator["llike"] = "like %{0}";
            dicOperator["rlike"] = "like {0}%";
            //dicOperator["like"] = ".Contains({0})";
            //dicOperator["llike"] = ".StartsWith({0})";
            //dicOperator["rlike"] = ".EndsWith({0})";
            dicOperator["null"] = "=null";
            dicOperator["not-null"] = "!=null";
        }

        public string ToWhere()
        {
            if (paramValues == null)
            {
                paramValues = new List<object>();
            }
            string strWhere = GetStrWhere();
            if (Filters != null)
            {
                //strWhere = string.Format(strWhere, Type + " {0}");
                foreach (PageFilterDto filter in Filters)
                {
                    // todo
                    strWhere += Type + " {0}";


                    filter.paramLocation = paramLocation;
                    filter.paramValues = paramValues;
                    strWhere = string.Format(strWhere, "(" + filter.ToWhere() + ")");
                }
            }
            else
            {
                strWhere = string.Format(strWhere, "");
            }
            if (string.IsNullOrEmpty(strWhere))
            {
                strWhere = "1=2";
            }
            return strWhere;
        }

        private string GetStrWhere()
        {
            List<string> list = new List<string>();
            foreach (Condition item in Conditions)
            {
                if (dicOperator.Keys.FirstOrDefault((string x) => x == item.Operatoer) == null)
                {
                    throw new ArgumentException("未识别的运算符!");
                }
                string item2 = item.Attribute + string.Format(dicOperator[item.Operatoer] + " ", "@" + paramLocation.ToString());
                if (!string.IsNullOrEmpty(item.Value))
                {
                    object item3 = item.Value.ToObjectValue(item.Datatype.ToCSharpTypeStr());
                    paramValues.Add(item3);
                    paramLocation++;
                }
                list.Add(item2);
            }
            return string.Join(" " + InnerType + " ", list);
            //return string.Join(" " + InnerType + " ", list) + " {0}";
            //return "(" + string.Join(" " + InnerType + " ", list) + "  ) {0}";
        }
    }
}