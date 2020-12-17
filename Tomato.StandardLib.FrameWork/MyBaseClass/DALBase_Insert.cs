using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;

namespace Tomato.StandardLib.MyBaseClass
{
    public abstract partial class DALBase<Model, OutputModel>
    {
        /// <summary>
        /// ��model��ĳ��˱�ʶΪIdentity,Timestamp֮�������ֵ�������ݿ�
        /// </summary>
        /// <param name="model">��Ҫ�������ݿ��model</param>
        /// <returns>Ӱ��ļ�¼��</returns>
        public virtual int Insert(Model model)
        {
            model.GmtCreateUser = this.Operater;
            model.GmtCreateDate = DateTime.Now;
            model.GmtUpdateUser = this.Operater;
            model.GmtUpdateDate = DateTime.Now;
            ParmCollection parms = this.Table.PrepareNotIdentityParms(model);
            return Internal_DataHelper.ExecuteNonQuery(this.Table.Insert, parms);
        }

        /// <summary>
        /// ��model��ĳ��˱�ʶΪIdentity,Timestamp֮�������ֵ�������ݿ�
        /// </summary>
        /// <param name="model">��Ҫ�������ݿ��model</param>
        /// <returns>�����Ľ��</returns>
        public virtual OutputModel InsertAndReturn(Model model)
        {
            model.GmtCreateUser = this.Operater;
            model.GmtCreateDate = DateTime.Now;
            model.GmtUpdateUser = this.Operater;
            model.GmtUpdateDate = DateTime.Now;
            ParmCollection parms = this.Table.PrepareNotIdentityParms(model);

            using (IDataReader rdr = Internal_DataHelper.ExecuteReader(this.Table.Insert, parms))
            {
                return rdr.Read() ? (OutputModel)this.Table.ReadDataReader(rdr, new OutputModel()) : null;

            }
        }

        /// <summary>
        /// ��model�������modelÿ�����˱�ʶΪIdentity,Timestamp֮�������ֵ�������ݿ�
        /// </summary>
        /// <param name="models">��Ҫ�������ݿ��</param>
        /// <returns>Ӱ��ļ�¼��</returns>
        public virtual int InsertCol(Model[] models)
        {
            int num = 0;
            Collection<StringBuilder> sc = this.Table.PrepareInsertCol<Model, OutputModel>(models, this.Operater);
            foreach (StringBuilder s in sc)
            {
                num += Internal_DataHelper.ExecuteNonQuery(s.ToString());
            }
            return num;
        }
    }
}
