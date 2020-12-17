using System;
using System.Collections.ObjectModel;
using System.Data;

namespace Tomato.StandardLib.MyBaseClass
{
    public abstract partial class DALBase<Model, OutputModel>
    {
        /// <summary>
        /// ����model��ֵ��Ϊ��ѯ����,model��ÿһ����null�ֶζ�����Ϊ��ѯ����
        /// ���ط��������ĵ�һ��model
        /// ֧��ģ����ѯ
        /// </summary>
        /// <param name="model">��Ϊ������model</param>
        /// <returns>���ϲ�ѯ�����ĵ�һ��model</returns>
        public virtual OutputModel SelectWithModel(Model model)
        {
            ParmCollection parms = this.Table.PrepareConditionParms(model, this.IsLikeMode);
            return this.Select(string.Format(this.Table.Select, parms.WhereSql, this.OrderBy), parms);
        }

        /// <summary>
        /// ���Զ������ֶ�Ϊ��ѯ��������ȡ��������model
        /// </summary>
        /// <param name="identity">��Ϊ�������Զ�����</param>
        /// <returns>��������model</returns>
        public virtual OutputModel SelectWithIdentity(long identity)
        {
            ParmCollection parms = this.Table.PrepareIdentityParm(identity);
            return this.Select(string.Format(this.Table.Select, parms.WhereSql, this.OrderBy), parms);
        }

        /// <summary>
        /// �������ֶ�Ϊ��ѯ������model�еķ������ֶξ�����Ϊ��ѯ��������ȡ��������model
        /// </summary>
        /// <param name="model">��Ϊ������model</param>
        /// <returns>��������model</returns>
        public virtual OutputModel SelectWithKeys(Model model)
        {
            ParmCollection parms = this.Table.PrepareKeysParms(model);
            return this.Select(string.Format(this.Table.Select, parms.WhereSql, this.OrderBy), parms);
        }

        /// <summary>
        /// ��ȡModel��Ӧ�����ݿ�����������ɵ��Զ�����ֵ
        /// </summary>
        /// <returns>�������ɵ��Զ�����ֵ</returns>
        public virtual int GetIdentity()
        {
            return this.Internal_DataHelper.ExecuteForIdentity(this.Table.TableName);
        }

        /// <summary>
        /// ͨ��SQL����Guid����Model�޹�
        /// </summary>
        /// <returns>Guid</returns>
        public Guid GetGuid()
        {
            string guid_str = this.Internal_DataHelper.ExecuteScalar("select newid()").ToString();
            return new Guid(guid_str);
        }

        /// <summary>
        /// ͨ��SQL��ȡ���ݿ�������ĵ�ǰʱ�䣬��Model�޹�
        /// </summary>
        /// <returns>���ݿ�������ĵ�ǰʱ��</returns>
        public DateTime GetServerTime()
        {
            string time_str = this.Internal_DataHelper.ExecuteScalar("select getdate()").ToString();
            return DateTime.Parse(time_str);
        }

        /// <summary>
        /// ��ȡModel��Ӧ�����ݿ�������м�¼���ϼ�
        /// </summary>
        /// <returns>��¼���ϼ�</returns>
        public virtual int GetCount()
        {
            return GetCount(null);
        }

        /// <summary>
        /// ����model��ֵ��Ϊ��ѯ����,model��ÿһ����null�ֶζ�����Ϊ��ѯ������
        /// ��ȡModel��Ӧ�����ݿ���ڷ��������ļ�¼���ϼ�
        /// ֧��ģ����ѯ
        /// </summary>
        /// <param name="model">��Ϊ������model</param>
        /// <returns>��¼���ϼ�</returns>
        public virtual int GetCount(Model model)
        {
            ParmCollection parms = this.Table.PrepareConditionParms(model, this.IsLikeMode);
            object o = Internal_DataHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT COUNT(*) FROM {0} {1}", this.Table.TableName, parms.WhereSql), parms);
            if (o != null)
            {
                return int.Parse(o.ToString());
            }

            return 0;
        }

        /// <summary>
        /// �Զ���sql��ѯ���������������ĵ�һ��Model
        /// </summary>
        /// <param name="sql">�Զ���sql</param>
        /// <param name="parms">ִ�в�������</param>
        /// <returns>���ϲ�ѯ������model</returns>
        public virtual OutputModel Select(string sql, Collection<IDataParameter> parms = null)
        {
            if (parms == null)
            {
                parms = new Collection<IDataParameter>();
            }
            using (IDataReader rdr = this.Internal_DataHelper.ExecuteReader(sql, parms))
            {
                return rdr.Read() ? (OutputModel)this.Table.ReadDataReader(rdr, new OutputModel()) : null;
            }
        }
    }
}
