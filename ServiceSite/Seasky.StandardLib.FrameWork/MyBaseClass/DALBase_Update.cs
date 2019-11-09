using System;
using System.Collections.Generic;
using System.Linq;

namespace SeaSky.StandardLib.MyBaseClass
{
    public abstract partial class DALBase<Model, OutputModel, ViewModel>
    {
        /// <summary>
        /// 根据condition把value里的值更新，condition中所有不为null的字段都将作为查询条件
        /// 而value中的所有不为null的字段将会被更新
        /// 支持模糊查询
        /// </summary>
        /// <param name="value">需要更新的值</param>
        /// <param name="condition">更新时的条件</param>
        /// <returns>影响的记录数</returns>
        public virtual int UpdateWithModel(Model value, Model condition)
        {
            this.setSystemField(value);
            ParmCollection conditionParms = this.Table.PrepareUpdateParms(value, this.IsLikeMode, condition);
            return this.Internal_DataHelper.ExecuteNonQuery(string.Format(conditionParms.UpdateSql, conditionParms.WhereSql), conditionParms);
        }

        /// <summary>
        /// 根据condition把value里的值更新，condition中所有不为null的字段都将作为查询条件
        /// 而value中的所有不为null的字段将会被更新
        /// 支持模糊查询
        /// </summary>
        /// <param name="value">需要更新的值</param>
        /// <param name="condition">更新时的条件</param>
        /// <returns>更新后的值集合</returns>
        public virtual IEnumerable<OutputModel> UpdateWithModelAndReturn(Model value, Model condition)
        {
            this.setSystemField(value);
            ParmCollection conditionParms = this.Table.PrepareUpdateParms(value, this.IsLikeMode, condition);
            return this.List(string.Format(conditionParms.UpdateSql, conditionParms.WhereSql), conditionParms).ToList();
        }

        /// <summary>
        /// 根据自增字段值更新，model里的其他任何值均不会作为更新条件
        /// 而model中除自动增量字段以外的所有不为null的字段将会被更新
        /// </summary>
        /// <param name="models">要更新的值集合</param>
        /// <returns>影响的记录数</returns>
        public virtual int UpdateWithIdentity(params Model[] models)
        {
            int returnCount = 0;
            foreach (Model model in models)
            {
                this.setSystemField(model);
                ParmCollection conditionParms = this.Table.PrepareUpdateParms(model);
                conditionParms.AddRange(this.Table.PrepareAllParms(model));
                returnCount += this.Internal_DataHelper.ExecuteNonQuery(string.Format(conditionParms.UpdateSql, this.Table.IdentityParm.WhereSql), conditionParms);
            }
            return returnCount;
        }

        /// <summary>
        /// 根据自增字段值更新，model里的其他任何值均不会作为更新条件
        /// 而model中除自动增量字段以外的所有不为null的字段将会被更新
        /// </summary>
        /// <param name="model">要更新的值</param>
        /// <returns>影响的记录数</returns>
        public virtual OutputModel UpdateWithIdentityAndReturn(Model model)
        {
            this.setSystemField(model);
            ParmCollection conditionParms = this.Table.PrepareUpdateParms(model);
            conditionParms.AddRange(this.Table.PrepareAllParms(model));
            return this.Select(string.Format(conditionParms.UpdateSql, this.Table.IdentityParm.WhereSql), conditionParms);
        }

        /// <summary>
        /// 根据自增字段值更新，model里的其他任何值均不会作为更新条件
        /// 而model集合中除自动增量字段以外的所有不为null的字段将会被更新
        /// </summary>
        /// <param name="models">要更新的值集合</param>
        /// <returns>更新后的结果</returns>
        public virtual IEnumerable<OutputModel> UpdateWithIdentityAndReturn(Model[] models)
        {
            List<OutputModel> resultList = new List<OutputModel>();
            foreach (Model model in models)
            {
                this.setSystemField(model);
                ParmCollection conditionParms = this.Table.PrepareUpdateParms(model);
                conditionParms.AddRange(this.Table.PrepareAllParms(model));
                OutputModel result = this.Select(string.Format(conditionParms.UpdateSql, this.Table.IdentityParm.WhereSql), conditionParms);
                resultList.Add(result);
            }
            return resultList;
        }

        /// <summary>
        /// 根据主键字段值更新，model里的其他任何值均不会作为更新条件
        /// 而model中除主键字段以外的所有不为null的字段将会被更新
        /// </summary>
        /// <param name="models">要更新的值集合</param>
        /// <returns>影响的记录数</returns>
        public virtual int UpdateWithKeys(params Model[] models)
        {
            int returnCount = 0;
            foreach (Model model in models)
            {
                this.setSystemField(model);
                ParmCollection conditionParms = this.Table.PrepareUpdateParms(model);
                conditionParms.AddRange(this.Table.PrepareKeysParms(model));
                returnCount += this.Internal_DataHelper.ExecuteNonQuery(string.Format(conditionParms.UpdateSql, this.Table.KeysParms.WhereSql), conditionParms);
            }
            return returnCount;
        }

        /// <summary>
        /// 根据主键字段值更新，model里的其他任何值均不会作为更新条件
        /// 而model中除主键字段以外的所有不为null的字段将会被更新
        /// </summary>
        /// <param name="model">要更新的值</param>
        /// <returns>更新后的值</returns>
        public virtual OutputModel UpdateWithKeysAndReturn(Model model)
        {
            this.setSystemField(model);
            ParmCollection conditionParms = this.Table.PrepareUpdateParms(model);
            conditionParms.AddRange(this.Table.PrepareKeysParms(model));
            return this.Select(string.Format(conditionParms.UpdateSql, this.Table.KeysParms.WhereSql), conditionParms);
        }

        /// <summary>
        /// 根据主键字段值更新，model里的其他任何值均不会作为更新条件
        /// 而model集合中除主键字段以外的所有不为null的字段将会被更新
        /// </summary>
        /// <param name="models">要更新的值集合</param>
        /// <returns>更新后的值</returns>
        public virtual IEnumerable<OutputModel> UpdateWithKeysAndReturn(Model[] models)
        {
            List<OutputModel> resultList = new List<OutputModel>();
            foreach (Model model in models)
            {
                this.setSystemField(model);
                ParmCollection conditionParms = this.Table.PrepareUpdateParms(model);
                conditionParms.AddRange(this.Table.PrepareKeysParms(model));
                OutputModel result = this.Select(string.Format(conditionParms.UpdateSql, this.Table.KeysParms.WhereSql), conditionParms);
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
