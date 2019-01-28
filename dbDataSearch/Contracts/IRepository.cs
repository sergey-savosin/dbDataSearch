using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbDataSearch.Contracts
{
    public interface IRepository
    {
        List<string> GetAllConnectionNames();
        ConnectionDetails GetConnectionDetails(string connectionName);
        List<IEntity> GetAllRootEntities();
    }
}
