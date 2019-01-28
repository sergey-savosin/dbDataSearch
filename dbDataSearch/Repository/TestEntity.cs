using dbDataSearch.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbDataSearch.Repository
{
    public class TestEntity : IEntity
    {
        public List<FindResultSimplePair> FindByString(string strValue)
        {
            return new List<FindResultSimplePair>
            {
                new FindResultSimplePair { Id = 1, StrValue = "one"},
                new FindResultSimplePair { Id = 21, StrValue = "second"},
                new FindResultSimplePair { Id = 31, StrValue = "another world"},
            };
        }

        public string GetDetailsByKey(long keyValue)
        {
            return "test Entity Value";
        }
    }
}
