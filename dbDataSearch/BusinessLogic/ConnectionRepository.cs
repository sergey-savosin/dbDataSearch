using dbDataSearch.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbDataSearch.BusinessLogic
{
    public class ConnectionRepository : IConnectionRepository
    {
        public List<string> GetAllConnectionNames()
        {
            List<string> list = new List<string>()
            {
                "WideWorldImportersDW","other"
            };
            return list;
        }

        public TConnectionDetails GetConnectionDetails(string connectionName)
        {
            return new TConnectionDetails()
            {
                ServerName = @"SergeyHome\SQLExpress",
                ConnectionName = connectionName,
                DatabaseName = "WideWorldImportersDW"
            };
        }
    }
}
