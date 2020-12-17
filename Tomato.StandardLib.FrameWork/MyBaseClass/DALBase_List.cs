using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace Tomato.StandardLib.MyBaseClass
{
    public abstract partial class DALBase<Model, OutputModel>
    {
        /// <summary>
        /// �Լ��Ϸ�ʽ����Model��������Ӧ���ݿ������������
        /// </summary>
        /// <returns>�����������ݵ�Model����</returns>
        public virtual IEnumerable<OutputModel> List()
        {
            return List(null);
        }

        /// <summary>
        /// ����model��ֵ��Ϊ������ѯ������model��ÿһ����null�ֶζ�����Ϊ��ѯ����
        /// ������ѯ���м�¼����modelΪnull����
        /// ֧��ģ����ѯ
        /// </summary>
        /// <param name="model">��Ϊ������model</param>
        /// <returns>���ϲ�ѯ���������ݼ���</returns>
        public virtual IEnumerable<OutputModel> List(Model model)
        {
            ParmCollection parms = this.Table.PrepareConditionParms(model, this.IsLikeMode);
            return List(string.Format(this.Table.Select, parms.WhereSql, this.OrderBy), parms);
        }

        /// <summary>
        /// �Զ���sql��ѯ����ѯ����Լ�����ʽ����
        /// </summary>
        /// <param name="sql">�Զ���sql���</param>
        /// <param name="parms">ִ�в�������</param>
        /// <returns>���ϲ�ѯ�����ļ���</returns>
        public virtual IEnumerable<OutputModel> List(string sql, Collection<IDataParameter> parms = null)
        {
            if (parms == null)
            {
                parms = new Collection<IDataParameter>();
            }
            using (IDataReader rdr = Internal_DataHelper.ExecuteReader(sql, parms))
            {
                while (rdr.Read())
                {
                    yield return (OutputModel)this.Table.ReadDataReader(rdr, new OutputModel());
                }
            }
        }

        /// <summary>
        /// �����ݱ���ʽ����Model��������Ӧ���ݿ������������
        /// </summary>
        /// <returns>�����������ݵ�DataTable</returns>
        public virtual DataTable ListTable()
        {
            DataTable dt = ListTable(null);
            return dt;
        }

        /// <summary>
        /// ����model��ֵ��ѯ��model��ÿһ����null�ֶζ�����Ϊ��ѯ����
        /// ������ѯ���м�¼����modelΪnull����
        /// ֧��ģ����ѯ
        /// </summary>
        /// <param name="model">��Ϊ��ѯ������model</param>
        /// <returns>���ϲ�ѯ���������ݱ�</returns>
        public virtual DataTable ListTable(Model model)
        {
            ParmCollection parms = this.Table.PrepareConditionParms(model, this.IsLikeMode);
            DataTable dt = ListTable(
                string.Format(this.Table.Select, parms.WhereSql, this.OrderBy)
                , parms);
            return dt;
        }

        /// <summary>
        /// �Զ���sql��ѯ����ѯ��������ݱ���ʽ����
        /// </summary>
        /// <param name="sql">�Զ���sql���</param>
        /// <param name="parms">ִ�в�������</param>
        /// <returns>���ϲ�ѯ���������ݱ�</returns>
        public virtual DataTable ListTable(string sql, Collection<IDataParameter> parms = null)
        {
            if (parms == null)
            {
                parms = new Collection<IDataParameter>();
            }
            DataTable dt = Internal_DataHelper.FillDataTable(sql, parms);
            dt.TableName = this.Table.TableName;
            return dt;
        }

        /// <summary>
        /// �Զ���sql��ѯ����ѯ�����DataSet��ʽ����
        /// </summary>
        /// <param name="sql">�Զ���sql���</param>
        /// <param name="parms">ִ�в�������</param>
        /// <returns>���ϲ�ѯ���������ݱ�����</returns>
        public virtual DataSet ListDataSet(string sql, Collection<IDataParameter> parms = null)
        {
            if (parms == null)
            {
                parms = new Collection<IDataParameter>();
            }
            DataSet ds = Internal_DataHelper.FillDataSet(sql, parms);
            return ds;
        }

        /// <summary>
        /// ����model��ֵ�Ͳ��������������model���ϣ�model��ÿһ����null�ֶζ�����Ϊ��ѯ����
        /// ֧��ģ����ѯ
        /// </summary>
        /// <param name="model">��Ϊ��ѯ������model</param>
        /// <param name="or">��������trueΪor��falseΪand</param>
        /// <returns>���ϲ�ѯ���������ݼ���</returns>
        public virtual IEnumerable<OutputModel> Search(Model model, bool or)
        {
            ParmCollection parms = this.Table.PrepareSearchParms(model, or, this.IsLikeMode);
            return List(string.Format(this.Table.Select, parms.WhereSql, this.OrderBy), parms);
        }
    }
}
