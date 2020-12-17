using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tomato.StandardLib.DynamicPage
{

    public static class QueryableExtensions
    {
        public static IQueryable<T> IncludeIf<T>(this IQueryable<T> source, bool condition, string path) where T : class
        {
            if (!condition)
            {
                return source;
            }
            return EntityFrameworkQueryableExtensions.Include<T>(source, path);
        }

        public static IQueryable<T> IncludeIf<T, TProperty>(this IQueryable<T> source, bool condition, Expression<Func<T, TProperty>> path) where T : class
        {
            if (!condition)
            {
                return source;
            }
            return source.Include<T, TProperty>(path);
        }

        public static IQueryable<T> IncludeIf<T>(this IQueryable<T> source, bool condition, Func<IQueryable<T>, IIncludableQueryable<T, object>> include) where T : class
        {
            if (!condition)
            {
                return source;
            }
            return include(source);
        }

        /// <summary>
        /// 获取分页信息，不使用Mapper
        /// </summary>
        /// <typeparam name="TEntityOrView"></typeparam>
        /// <param name="query"></param>
        /// <param name="pagedInputDto"></param>
        /// <returns></returns>
        public static async Task<MyPagedResult<TEntityOrView>> GetPageEntityOrViewAsync<TEntityOrView>(this IQueryable<TEntityOrView> query, PagedInputDto pagedInputDto) where TEntityOrView : class
        {
            query = EntityFrameworkQueryableExtensions.AsNoTracking<TEntityOrView>(query);
            //排序
            if (!string.IsNullOrEmpty(pagedInputDto.Order))
            {
                List<string> strList = pagedInputDto.Order.Split(new char[1]
                {
                ','
                }).ToList();
                for (int i = 0; i < strList.Count; i++)
                {
                    query = ((i != 0) ? ((!strList[i].ToLower().Contains("desc")) ? DynamicQueryable.OrderBy<TEntityOrView>(query, pagedInputDto.Order, Array.Empty<object>()) : DynamicQueryable.ThenByDescending<TEntityOrView>(query, pagedInputDto.Order, Array.Empty<object>())) : ((!strList[i].ToLower().Contains("desc")) ? DynamicQueryable.OrderBy<TEntityOrView>(query, pagedInputDto.Order, Array.Empty<object>()) : DynamicQueryable.OrderByDescending<TEntityOrView>(query, pagedInputDto.Order, Array.Empty<object>())));
                }
            }
            if (pagedInputDto.Filter != null)
            {
                string text = pagedInputDto.Filter.ToWhere();
                object[] array = pagedInputDto.Filter.paramValues.ToArray();
                query = DynamicQueryable.Where<TEntityOrView>(query, text, array);
            }
            IQueryable<TEntityOrView> queryable = query.Skip(pagedInputDto.SkipCount).Take(pagedInputDto.PageSize);

            List<TEntityOrView> dataList = await EntityFrameworkQueryableExtensions.ToListAsync<TEntityOrView>(queryable, default(CancellationToken));
            MyPagedResult<TEntityOrView> obj = new MyPagedResult<TEntityOrView>();
            obj.PageSize = pagedInputDto.PageSize;
            obj.PageIndex = pagedInputDto.PageIndex;
            obj.DataList = dataList;
            obj.RowCount = query.Count();
            return obj;
        }

        /// <summary>
        /// 带mapper
        /// </summary>
        /// <typeparam name="TEntityOrView"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="query"></param>
        /// <param name="pagedInputDto"></param>
        /// <param name="configurationProvider"></param>
        /// <returns></returns>
        public static async Task<MyPagedResult<TDto>> GetPageAsync<TEntityOrView, TDto>(this IQueryable<TEntityOrView> query, PagedInputDto pagedInputDto) where TEntityOrView : class where TDto : class
        {
            query = EntityFrameworkQueryableExtensions.AsNoTracking<TEntityOrView>(query);
            //排序
            if (!string.IsNullOrEmpty(pagedInputDto.Order))
            {
                List<string> strList = pagedInputDto.Order.Split(new char[1]
                {
                ','
                }).ToList();
                for (int i = 0; i < strList.Count; i++)
                {
                    query = ((i != 0) ? ((!strList[i].ToLower().Contains("desc")) ? DynamicQueryable.OrderBy<TEntityOrView>(query, pagedInputDto.Order, Array.Empty<object>()) : DynamicQueryable.ThenByDescending<TEntityOrView>(query, pagedInputDto.Order, Array.Empty<object>())) : ((!strList[i].ToLower().Contains("desc")) ? DynamicQueryable.OrderBy<TEntityOrView>(query, pagedInputDto.Order, Array.Empty<object>()) : DynamicQueryable.OrderByDescending<TEntityOrView>(query, pagedInputDto.Order, Array.Empty<object>())));
                }
            }
            //if (!string.IsNullOrEmpty(pagedInputDto.Order))
            //{
            //    query = DynamicQueryable.OrderBy<TEntityOrView>(query, pagedInputDto.Order, Array.Empty<object>());
            //}
            if (pagedInputDto.Filter != null)
            {
                string text = pagedInputDto.Filter.ToWhere();
                object[] array = pagedInputDto.Filter.paramValues.ToArray();
                query = DynamicQueryable.Where<TEntityOrView>(query, text, array);
            }
            IQueryable<TEntityOrView> queryable = query.Skip(pagedInputDto.SkipCount).Take(pagedInputDto.PageSize);

            IQueryable<TDto> queryable2;

            if (!string.IsNullOrEmpty(pagedInputDto.Select))
            {
                queryable2 = DynamicQueryable.Select(queryable, pagedInputDto.Select, Array.Empty<object>()).Cast<TDto>();

            }
            else
            {
                queryable2 = Extensions.ProjectTo<TDto>(queryable, pagedInputDto.configurationProvider, Array.Empty<Expression<Func<TDto, object>>>());
            }
            List<TDto> dataList = await EntityFrameworkQueryableExtensions.ToListAsync<TDto>(queryable2, default(CancellationToken));
            MyPagedResult<TDto> obj = new MyPagedResult<TDto>();
            obj.PageSize = pagedInputDto.PageSize;
            obj.PageIndex = pagedInputDto.PageIndex;
            obj.DataList = dataList;
            obj.RowCount = query.Count();
            return obj;
        }


        //public static async Task<MyPagedResult<TDto>> GetPageAsync<TEntityOrView, TDto>(this IQueryable<TEntityOrView> query, PagedInputDto pagedInputDto) where TEntityOrView : class where TDto : class
        //{
        //    query = EntityFrameworkQueryableExtensions.AsNoTracking<TEntityOrView>(query);
        //    query = ((!string.IsNullOrEmpty(pagedInputDto.Order)) ? DynamicQueryable.OrderBy<TEntityOrView>(query, pagedInputDto.Order, Array.Empty<object>()) : DynamicQueryable.OrderBy<TEntityOrView>(query, "Id", Array.Empty<object>()));
        //    if (pagedInputDto.Filter != null)
        //    {
        //        string text = pagedInputDto.Filter.ToWhere();
        //        object[] array = pagedInputDto.Filter.paramValues.ToArray();
        //        query = DynamicQueryable.Where<TEntityOrView>(query, text, array);
        //    }
        //    IQueryable<TEntityOrView> queryable = query.Skip(pagedInputDto.SkipCount).Take(pagedInputDto.PageSize);
        //    IQueryable<TDto> queryable2 = (!string.IsNullOrEmpty(pagedInputDto.Select)) ? DynamicQueryable.Select(queryable, pagedInputDto.Select, Array.Empty<object>()).Cast<TDto>() : AutoMapper.QueryableExtensions.Extensions.ProjectTo<TDto>(queryable, Array.Empty<Expression<Func<TDto, object>>>());
        //    List<TDto> dataList = await EntityFrameworkQueryableExtensions.ToListAsync<TDto>(queryable2, default(CancellationToken));
        //    MyPagedResult<TDto> obj = new MyPagedResult<TDto>();
        //    obj.PageSize = pagedInputDto.PageSize;
        //    obj.PageIndex = pagedInputDto.PageIndex;
        //    obj.DataList = dataList;
        //    obj.RowCount = query.Count();
        //    return obj;
        //}
    }
}
