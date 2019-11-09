using System;
using System.Collections.ObjectModel;

namespace SeaSky.StandardLib.MyBaseClass
{
    /// <summary>
    /// Model集合泛型
    /// </summary>
    /// <typeparam name="T">泛型Model类型</typeparam>
    [Serializable]
    public class CollectionBase<T> : Collection<T>
    {
        /// <summary>
        /// 添加Model对象数组
        /// </summary>
        /// <param name="values">Model对象数组</param>
        public virtual void AddRange(params T[] values)
        {
            foreach (T v in values)
            {
                Items.Add(v);
            }
        }

        /// <summary>
        /// 添加Model对象集合
        /// </summary>
        /// <param name="values">Model对象集合</param>
        public virtual void AddRange(Collection<T> values)
        {
            foreach (T v in values)
            {
                Items.Add(v);
            }
        }

        /// <summary>
        /// 移除Model对象数组
        /// </summary>
        /// <param name="values">Model对象数组</param>
        public virtual void RemoveRange(params T[] values)
        {
            foreach (T v in values)
            {
                Items.Remove(v);
            }
        }

        /// <summary>
        /// 移除Model对象集合
        /// </summary>
        /// <param name="values">Model对象集合</param>
        public virtual void RemoveRange(Collection<T> values)
        {
            foreach (T v in values)
            {
                Items.Remove(v);
            }
        }

        /// <summary>
        /// 添加单个Model对象
        /// </summary>
        /// <param name="item">Model对象</param>
        public virtual void AddItem(T item)
        {
            Add(item);
        }

        /// <summary>
        /// 移除单个Model对象
        /// </summary>
        /// <param name="item">Model对象</param>
        /// <returns>移除结果</returns>
        public virtual bool RemoveItem(T item)
        {
            return Remove(item);
        }

        /// <summary>
        /// 移除单个Model对象
        /// </summary>
        /// <param name="index">Model对象序号</param>
        public virtual void RemoveItemAt(int index)
        {
            RemoveAt(index);
        }
    }
}
