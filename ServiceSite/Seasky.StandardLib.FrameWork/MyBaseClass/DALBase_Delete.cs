namespace SeaSky.StandardLib.MyBaseClass
{
    public abstract partial class DALBase<Model, OutputModel, ViewModel>
    {
        /// <summary>
        /// 根据model的值作为条件删除，model里每一个非null字段都会作为删除条件
        /// 支持模糊条件
        /// </summary>
        /// <param name="models">作为条件的model集合</param>
        /// <returns>影响的记录数</returns>
        public virtual int DeleteWithModel(params Model[] models)
        {
            int returnCount = 0;
            foreach (Model model in models)
            {
                ParmCollection parms = this.Table.PrepareConditionParms(model, this.IsLikeMode);
                returnCount += Internal_DataHelper.ExecuteNonQuery(string.Format(this.Table.Delete, parms.WhereSql), parms);
            }
            return returnCount;
        }

        /// <summary>
        /// 根据自增字段作为删除条件
        /// </summary>
        /// <param name="identitys">自动增量值集合</param>
        /// <returns>影响的记录数</returns>
        public virtual int DeleteWithIdentity(params long[] identitys)
        {
            int returnCount = 0;
            foreach (long identity in identitys)
            {
                ParmCollection parm = this.Table.PrepareIdentityParm(identity);
                returnCount += Internal_DataHelper.ExecuteNonQuery(string.Format(this.Table.Delete, parm.WhereSql), parm);
            }
            return returnCount;
        }

        /// <summary>
        /// 根据Key字段作为删除条件，model里的其他任何值均不会作为删除条件，只有标识为Key的字段才会作为删除条件
        /// </summary>
        /// <param name="models">作为条件的model集合</param>
        /// <returns>影响的记录数</returns>
        public virtual int DeleteWithKeys(params Model[] models)
        {
            int returnCount = 0;
            foreach (Model model in models)
            {
                ParmCollection parms = this.Table.PrepareKeysParms(model);
                returnCount += Internal_DataHelper.ExecuteNonQuery(string.Format(this.Table.Delete, parms.WhereSql), parms);
            }
            return returnCount;
        }

        /// <summary>
        /// 删除全部记录
        /// </summary>
        /// <returns>影响的记录数</returns>
        public virtual int DeleteAll()
        {
            return Internal_DataHelper.ExecuteNonQuery(string.Format(this.Table.Delete, string.Empty));
        }

        /// <summary>
        /// 快速清空表内全部数据
        /// 不经过日志处理，Truncate Table 慎用
        /// </summary>
        public virtual void TruncateTable()
        {
            Internal_DataHelper.ExecuteNonQuery(string.Format("TRUNCATE TABLE {0}", this.Table.TableName));
        }
    }
}
