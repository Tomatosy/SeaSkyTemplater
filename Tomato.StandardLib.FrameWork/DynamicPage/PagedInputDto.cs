using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tomato.StandardLib.DynamicPage
{
    public class PagedInputDto
    {
        public int PageIndex
        {
            get;
            set;
        }

        public int PageSize
        {
            get;
            set;
        }

        public string Order
        {
            get;
            set;
        }

        public PageFilterDto Filter
        {
            get;
            set;
        }

        public string Select
        {
            get;
            set;
        }

        public int SkipCount => (PageIndex - 1) * PageSize;

        /// <summary>
        /// autoMapper需要使用的
        /// </summary>
        public IConfigurationProvider configurationProvider { get; set; }
    }

}