using System;
using System.Collections.ObjectModel;

namespace Tomato.StandardLib.MyBaseClass
{
    /// <summary>
    /// Model���Ϸ���
    /// </summary>
    /// <typeparam name="T">����Model����</typeparam>
    [Serializable]
    public class CollectionBase<T> : Collection<T>
    {
        /// <summary>
        /// ����Model��������
        /// </summary>
        /// <param name="values">Model��������</param>
        public virtual void AddRange(params T[] values)
        {
            foreach (T v in values)
            {
                Items.Add(v);
            }
        }

        /// <summary>
        /// ����Model���󼯺�
        /// </summary>
        /// <param name="values">Model���󼯺�</param>
        public virtual void AddRange(Collection<T> values)
        {
            foreach (T v in values)
            {
                Items.Add(v);
            }
        }

        /// <summary>
        /// �Ƴ�Model��������
        /// </summary>
        /// <param name="values">Model��������</param>
        public virtual void RemoveRange(params T[] values)
        {
            foreach (T v in values)
            {
                Items.Remove(v);
            }
        }

        /// <summary>
        /// �Ƴ�Model���󼯺�
        /// </summary>
        /// <param name="values">Model���󼯺�</param>
        public virtual void RemoveRange(Collection<T> values)
        {
            foreach (T v in values)
            {
                Items.Remove(v);
            }
        }

        /// <summary>
        /// ���ӵ���Model����
        /// </summary>
        /// <param name="item">Model����</param>
        public virtual void AddItem(T item)
        {
            Add(item);
        }

        /// <summary>
        /// �Ƴ�����Model����
        /// </summary>
        /// <param name="item">Model����</param>
        /// <returns>�Ƴ����</returns>
        public virtual bool RemoveItem(T item)
        {
            return Remove(item);
        }

        /// <summary>
        /// �Ƴ�����Model����
        /// </summary>
        /// <param name="index">Model�������</param>
        public virtual void RemoveItemAt(int index)
        {
            RemoveAt(index);
        }
    }
}
