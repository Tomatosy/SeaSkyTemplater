using System;
using System.Collections.Generic;
using System.Linq;

namespace Tomato.StandardLib.MyBaseClass
{
    public abstract partial class DALBase<Model, OutputModel>
    {
        /// <summary>
        /// ����condition��value���ֵ���£�condition�����в�Ϊnull���ֶζ�����Ϊ��ѯ����
        /// ��value�е����в�Ϊnull���ֶν��ᱻ����
        /// ֧��ģ����ѯ
        /// </summary>
        /// <param name="value">��Ҫ���µ�ֵ</param>
        /// <param name="condition">����ʱ������</param>
        /// <returns>Ӱ��ļ�¼��</returns>
        public virtual int UpdateWithModel(Model value, Model condition)
        {
            this.setSystemField(value);
            ParmCollection conditionParms = this.Table.PrepareUpdateParms(value, this.IsLikeMode, condition);
            return this.Internal_DataHelper.ExecuteNonQuery(string.Format(conditionParms.UpdateSql, conditionParms.WhereSql), conditionParms);
        }

        /// <summary>
        /// ����condition��value���ֵ���£�condition�����в�Ϊnull���ֶζ�����Ϊ��ѯ����
        /// ��value�е����в�Ϊnull���ֶν��ᱻ����
        /// ֧��ģ����ѯ
        /// </summary>
        /// <param name="value">��Ҫ���µ�ֵ</param>
        /// <param name="condition">����ʱ������</param>
        /// <returns>���º��ֵ����</returns>
        public virtual IEnumerable<OutputModel> UpdateWithModelAndReturn(Model value, Model condition)
        {
            this.setSystemField(value);
            ParmCollection conditionParms = this.Table.PrepareUpdateParms(value, this.IsLikeMode, condition);
            return this.List(string.Format(conditionParms.UpdateSql, conditionParms.WhereSql), conditionParms).ToList();
        }

        /// <summary>
        /// ���������ֶ�ֵ���£�model��������κ�ֵ��������Ϊ��������
        /// ��model�г��Զ������ֶ���������в�Ϊnull���ֶν��ᱻ����
        /// </summary>
        /// <param name="models">Ҫ���µ�ֵ����</param>
        /// <returns>Ӱ��ļ�¼��</returns>
        public virtual int UpdateWithIdentity(params Model[] models)
        {
            int returnCount = 0;
            foreach (var model in models)
            {
                this.setSystemField(model);
                ParmCollection conditionParms = this.Table.PrepareUpdateParms(model);
                conditionParms.AddRange(this.Table.PrepareAllParms(model));
                returnCount += this.Internal_DataHelper.ExecuteNonQuery(string.Format(conditionParms.UpdateSql, this.Table.IdentityParm.WhereSql), conditionParms);
            }
            return returnCount;
        }

        /// <summary>
        /// ���������ֶ�ֵ���£�model��������κ�ֵ��������Ϊ��������
        /// ��model�г��Զ������ֶ���������в�Ϊnull���ֶν��ᱻ����
        /// </summary>
        /// <param name="model">Ҫ���µ�ֵ</param>
        /// <returns>Ӱ��ļ�¼��</returns>
        public virtual OutputModel UpdateWithIdentityAndReturn(Model model)
        {
            this.setSystemField(model);
            ParmCollection conditionParms = this.Table.PrepareUpdateParms(model);
            conditionParms.AddRange(this.Table.PrepareAllParms(model));
            return this.Select(string.Format(conditionParms.UpdateSql, this.Table.IdentityParm.WhereSql), conditionParms);
        }

        /// <summary>
        /// ���������ֶ�ֵ���£�model��������κ�ֵ��������Ϊ��������
        /// ��model�����г��Զ������ֶ���������в�Ϊnull���ֶν��ᱻ����
        /// </summary>
        /// <param name="models">Ҫ���µ�ֵ����</param>
        /// <returns>���º�Ľ��</returns>
        public virtual IEnumerable<OutputModel> UpdateWithIdentityAndReturn(Model[] models)
        {
            var resultList = new List<OutputModel>();
            foreach (var model in models)
            {
                this.setSystemField(model);
                ParmCollection conditionParms = this.Table.PrepareUpdateParms(model);
                conditionParms.AddRange(this.Table.PrepareAllParms(model));
                var result = this.Select(string.Format(conditionParms.UpdateSql, this.Table.IdentityParm.WhereSql), conditionParms);
                resultList.Add(result);
            }
            return resultList;
        }

        /// <summary>
        /// ���������ֶ�ֵ���£�model��������κ�ֵ��������Ϊ��������
        /// ��model�г������ֶ���������в�Ϊnull���ֶν��ᱻ����
        /// </summary>
        /// <param name="models">Ҫ���µ�ֵ����</param>
        /// <returns>Ӱ��ļ�¼��</returns>
        public virtual int UpdateWithKeys(params Model[] models)
        {
            int returnCount = 0;
            foreach (var model in models)
            {
                this.setSystemField(model);
                ParmCollection conditionParms = this.Table.PrepareUpdateParms(model);
                conditionParms.AddRange(this.Table.PrepareKeysParms(model));
                returnCount += this.Internal_DataHelper.ExecuteNonQuery(string.Format(conditionParms.UpdateSql, this.Table.KeysParms.WhereSql), conditionParms);
            }
            return returnCount;
        }

        /// <summary>
        /// ���������ֶ�ֵ���£�model��������κ�ֵ��������Ϊ��������
        /// ��model�г������ֶ���������в�Ϊnull���ֶν��ᱻ����
        /// </summary>
        /// <param name="model">Ҫ���µ�ֵ</param>
        /// <returns>���º��ֵ</returns>
        public virtual OutputModel UpdateWithKeysAndReturn(Model model)
        {
            this.setSystemField(model);
            ParmCollection conditionParms = this.Table.PrepareUpdateParms(model);
            conditionParms.AddRange(this.Table.PrepareKeysParms(model));
            return this.Select(string.Format(conditionParms.UpdateSql, this.Table.KeysParms.WhereSql), conditionParms);
        }

        /// <summary>
        /// ���������ֶ�ֵ���£�model��������κ�ֵ��������Ϊ��������
        /// ��model�����г������ֶ���������в�Ϊnull���ֶν��ᱻ����
        /// </summary>
        /// <param name="models">Ҫ���µ�ֵ����</param>
        /// <returns>���º��ֵ</returns>
        public virtual IEnumerable<OutputModel> UpdateWithKeysAndReturn(Model[] models)
        {
            var resultList = new List<OutputModel>();
            foreach (var model in models)
            {
                this.setSystemField(model);
                ParmCollection conditionParms = this.Table.PrepareUpdateParms(model);
                conditionParms.AddRange(this.Table.PrepareKeysParms(model));
                var result = this.Select(string.Format(conditionParms.UpdateSql, this.Table.KeysParms.WhereSql), conditionParms);
                resultList.Add(result);
            }
            return resultList;
        }

        private void setSystemField(Model model)
        {
            model.GmtCreateUser = null;
            model.GmtCreateDate = null;
            model.GmtUpdateUser = this.Operater;
            model.GmtUpdateDate = DateTime.Now;
        }
    }
}
