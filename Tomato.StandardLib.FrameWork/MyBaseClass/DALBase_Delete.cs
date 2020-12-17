namespace Tomato.StandardLib.MyBaseClass
{
    public abstract partial class DALBase<Model, OutputModel>
    {
        /// <summary>
        /// ����model��ֵ��Ϊ����ɾ����model��ÿһ����null�ֶζ�����Ϊɾ������
        /// ֧��ģ������
        /// </summary>
        /// <param name="models">��Ϊ������model����</param>
        /// <returns>Ӱ��ļ�¼��</returns>
        public virtual int DeleteWithModel(params Model[] models)
        {
            int returnCount = 0;
            foreach (var model in models)
            {
                ParmCollection parms = this.Table.PrepareConditionParms(model, this.IsLikeMode);
                returnCount += Internal_DataHelper.ExecuteNonQuery(string.Format(this.Table.Delete, parms.WhereSql), parms);
            }
            return returnCount;
        }

        /// <summary>
        /// ���������ֶ���Ϊɾ������
        /// </summary>
        /// <param name="identitys">�Զ�����ֵ����</param>
        /// <returns>Ӱ��ļ�¼��</returns>
        public virtual int DeleteWithIdentity(params long[] identitys)
        {
            int returnCount = 0;
            foreach (var identity in identitys)
            {
                ParmCollection parm = this.Table.PrepareIdentityParm(identity);
                returnCount += Internal_DataHelper.ExecuteNonQuery(string.Format(this.Table.Delete, parm.WhereSql), parm);
            }
            return returnCount;
        }

        /// <summary>
        /// ����Key�ֶ���Ϊɾ��������model��������κ�ֵ��������Ϊɾ��������ֻ�б�ʶΪKey���ֶβŻ���Ϊɾ������
        /// </summary>
        /// <param name="models">��Ϊ������model����</param>
        /// <returns>Ӱ��ļ�¼��</returns>
        public virtual int DeleteWithKeys(params Model[] models)
        {
            int returnCount = 0;
            foreach (var model in models)
            {
                ParmCollection parms = this.Table.PrepareKeysParms(model);
                returnCount += Internal_DataHelper.ExecuteNonQuery(string.Format(this.Table.Delete, parms.WhereSql), parms);
            }
            return returnCount;
        }

        /// <summary>
        /// ɾ��ȫ����¼
        /// </summary>
        /// <returns>Ӱ��ļ�¼��</returns>
        public virtual int DeleteAll()
        {
            return Internal_DataHelper.ExecuteNonQuery(string.Format(this.Table.Delete, string.Empty));
        }

        /// <summary>
        /// ������ձ���ȫ������
        /// ��������־������Truncate Table ����
        /// </summary>
        public virtual void TruncateTable()
        {
            Internal_DataHelper.ExecuteNonQuery(string.Format("TRUNCATE TABLE {0}", this.Table.TableName));
        }
    }
}
