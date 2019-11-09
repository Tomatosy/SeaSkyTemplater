using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;

namespace SeaSky.StandardLib.MyBaseClass
{
        public abstract partial class DALBase<Model, OutputModel, ViewModel>
    {
        /// <summary>
        /// 把model里的除了标识为Identity,Timestamp之外的所有值插入数据库
        /// </summary>
        /// <param name="model">需要插入数据库的model</param>
        /// <returns>影响的记录数</returns>
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
        /// 把model里的除了标识为Identity,Timestamp之外的所有值插入数据库
        /// </summary>
        /// <param name="model">需要插入数据库的model</param>
        /// <returns>插入后的结果</returns>
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
        /// 把model集合里的model每个除了标识为Identity,Timestamp之外的所有值插入数据库
        /// </summary>
        /// <param name="models">需要插入数据库的</param>
        /// <returns>影响的记录数</returns>
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
