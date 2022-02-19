using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DbContextFactoryWebApiApp.DataAccess
{
    public class DbContextFactory : IDbContextFactory
    {
        private readonly Dictionary<string, string> _connectionStringMap;

        public DbContextFactory(Dictionary<string, string> connectionStringMap)
        {
            _connectionStringMap = connectionStringMap;
        }

        public int? ServiceId
        {
            get;
            set;
        }

        public CRMContext Create()
        {
            if (ServiceId == null)
            {
                throw new System.Exception("ServiceId was not supplied");
            }

            var connectionStringKey = $"{ServiceId.Value}_MainDb";

            if (!_connectionStringMap.ContainsKey(connectionStringKey))
            {
                throw new System.Exception($"Unsupported ServiceId : [{ServiceId.Value}]");
            }

            var connectionString =
                _connectionStringMap[connectionStringKey];

            var dbContextOptionsBuilder = new DbContextOptionsBuilder();
            dbContextOptionsBuilder.UseSqlServer(connectionString);

            var crmContext = new CRMContext(dbContextOptionsBuilder.Options);

            var canConnect = crmContext.Database.CanConnect();

            if (!canConnect)
            {
                throw new System.Exception($"Unable to connect to Main database for ServiceId : [{ServiceId.Value}]");
            }

            return crmContext;
        }
    }
}
