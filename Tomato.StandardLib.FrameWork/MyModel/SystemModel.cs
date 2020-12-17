using Tomato.StandardLib.MyAttribute;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tomato.StandardLib.MyModel
{
    /// <summary>
    /// 系统字段
    /// </summary>
    [Serializable]
    public class SystemModel
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public string GmtCreateUser { get => gmtCreateUser; set => gmtCreateUser = value; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? GmtCreateDate { get => gmtCreateDate; set => gmtCreateDate = value; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string GmtUpdateUser { get => gmtUpdateUser; set => gmtUpdateUser = value; }
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? GmtUpdateDate { get => gmtUpdateDate; set => gmtUpdateDate = value; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public byte[] Timestamp { get => timestamp; set => timestamp = value; }

        #region
        /// <summary>
        /// 创建人
        /// </summary>
        [DBFieldInfo(ColumnName = "GmtCreateUser", IsIdentity = false, IsKey = false, SqlDbType = SqlDbType.NVarChar
                 , OrderIndex = -1, OrderAsc = true)]
        protected string gmtCreateUser;

        /// <summary>
        /// 创建日期
        /// </summary>
        [DBFieldInfo(ColumnName = "GmtCreateDate", IsIdentity = false, IsKey = false, SqlDbType = SqlDbType.DateTime
            , OrderIndex = -1, OrderAsc = true)]
        protected DateTime? gmtCreateDate;

        /// <summary>
        /// 修改人
        /// </summary>
        [DBFieldInfo(ColumnName = "GmtUpdateUser", IsIdentity = false, IsKey = false, SqlDbType = SqlDbType.NVarChar
            , OrderIndex = -1, OrderAsc = true)]
        protected string gmtUpdateUser;

        /// <summary>
        /// 修改日期
        /// </summary>
        [DBFieldInfo(ColumnName = "GmtUpdateDate", IsIdentity = false, IsKey = false, SqlDbType = SqlDbType.DateTime
            , OrderIndex = -1, OrderAsc = true)]
        protected DateTime? gmtUpdateDate;

        /// <summary>
        /// 时间戳
        /// </summary>
        [DBFieldInfo(ColumnName = "Timestamp", IsIdentity = false, IsKey = false, SqlDbType = SqlDbType.Timestamp
            , OrderIndex = -1, OrderAsc = true)]
        protected byte[] timestamp;
        #endregion

    }
}
