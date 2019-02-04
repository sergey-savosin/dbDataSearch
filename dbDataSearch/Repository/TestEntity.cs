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
        public List<FindAllResult> FindByString(string strValue)
        {
            return new List<FindAllResult>
            {
                new FindAllResult { Id = 1, StrValue = "one"},
                new FindAllResult { Id = 21, StrValue = "second"},
                new FindAllResult { Id = 31, StrValue = "another world"},
            };
        }

        DataTable IEntity.GetDetailsByKey(long keyValue)
        {
            throw new NotImplementedException();
        }
    }
}
