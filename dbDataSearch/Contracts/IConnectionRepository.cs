using System;
using System.Collections.Generic;

namespace dbDataSearch.Contracts
{
    interface IConnectionRepository
    {
        List<String> GetAllConnectionNames();
        TConnectionDetails GetConnectionDetails(string connectionName);
    }
}
