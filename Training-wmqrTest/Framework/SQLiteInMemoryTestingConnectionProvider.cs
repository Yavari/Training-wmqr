using System.Data;
using NHibernate.Connection;

namespace Training_wmqrTest.Framework
{
    internal class SQLiteInMemoryTestingConnectionProvider : DriverConnectionProvider
    {
        public static IDbConnection Connection;

        public override IDbConnection GetConnection()
        {
            if (Connection == null)
            {
                Connection = base.GetConnection();
            }

            return Connection;
        }

        public override void CloseConnection(IDbConnection conn)
        {
        }
    }
}