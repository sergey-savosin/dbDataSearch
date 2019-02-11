using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbDataSearch.Contracts
{
    public interface IRepository
    {
        List<IEntity> GetAllRootEntities();
        DataTable GetEntityDetails(string entityName, long entityKey);
    }
}
