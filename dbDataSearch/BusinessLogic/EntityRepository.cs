using dbDataSearch.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbDataSearch.BusinessLogic
{
    public class EntityRepository : IEntityRepository
    {
        public DataTable GetEntityDetailsByKey(string EntityName, long key, TConnectionDetails connectionDetails)
        {
            throw new NotImplementedException();
        }

        public List<TSearchEntityResult> SearchEntitiesByString(string strValue, TConnectionDetails connectionDetails)
        {
            throw new NotImplementedException();
        }
    }
}
