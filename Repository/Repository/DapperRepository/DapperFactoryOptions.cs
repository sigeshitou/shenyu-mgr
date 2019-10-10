using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.DapperRepository
{
    public class ConnectionConfig
    {
        public string ConnectionString { get; set; }
        public DbStoreType DbType { get; set; } = DbStoreType.SqlServer;
    }

    public enum DbStoreType
    {
        MySql = 0,
        SqlServer = 1,
        Sqlite = 2,
        Oracle = 3
    }

    public class DapperFactoryOptions
    {
        public IList<Action<ConnectionConfig>> DapperActions { get; } = new List<Action<ConnectionConfig>>();
    }
   

}
