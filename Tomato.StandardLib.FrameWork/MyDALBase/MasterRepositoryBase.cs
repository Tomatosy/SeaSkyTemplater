using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Tomato.StandardLib.DAL.Base
{
    /// <summary>
    /// Database基类
    /// </summary>
    public abstract class MasterRepositoryBase
    {
        /// <summary>
        /// 数据库Database实例
        /// </summary>
        public Database Database { get; set; }
        private static DatabaseProviderFactory factory;

        /// <summary>
        /// 以默认连接名BaseConn,创建Database实例
        /// </summary>
        public MasterRepositoryBase() : this("BaseConn") { }

        /// <summary>
        /// 根据连接名创建Database实例
        /// </summary>
        /// <param name="connectionName">连接名</param>
        public MasterRepositoryBase(string connectionName)
        {
            factory = new DatabaseProviderFactory();
            this.Database = factory.Create(connectionName);
        }
    }
}
