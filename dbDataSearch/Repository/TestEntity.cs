using dbDataSearch.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbDataSearch.Repository
{
    public class TestEntity : IEntity
    {
        readonly string EntityName = "TestEntity";

        public List<FindAllResult> FindByString(string strValue)
        {
            return new List<FindAllResult>
            {
                new FindAllResult { Id = 1, StrValue = "one", EntityName = EntityName},
                new FindAllResult { Id = 21, StrValue = "second", EntityName = EntityName},
                new FindAllResult { Id = 31, StrValue = "another world", EntityName = EntityName},
            };
        }

        DataTable IEntity.GetDetailsByKey(long keyValue)
        {
            throw new NotImplementedException();
        }
    }
}
