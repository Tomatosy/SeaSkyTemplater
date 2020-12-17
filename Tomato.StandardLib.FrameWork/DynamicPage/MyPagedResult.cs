using System;
using System.Collections.Generic;
using System.Text;

namespace Tomato.StandardLib.DynamicPage
{

    public class MyPagedResult<TData> where TData : class
    {
        public List<TData> DataList
        {
            get;
            set;
        }

        public int PageIndex
        {
            get;
            set;
        }

        public int PageCount => (int)Math.Ceiling(RowCount * 1.0 / PageSize);

        public int PageSize
        {
            get;
            set;
        }

        public int RowCount
        {
            get;
            set;
        }
    }
}