using dbDataSearch.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbDataSearch.Repository
{
    public class SqlTestEntity : IEntity
    {
        SqlRunner sqlRunner;

        public SqlTestEntity(ConnectionDetails connectionDetails)
        {
            sqlRunner = new SqlRunner(connectionDetails);
        }

        public List<FindResultSimplePair> FindByString(string strValue)
        {
            SqlServerRepository repository = new SqlServerRepository(sqlRunner);
            DataTable testData = repository.GetTestData();

            List<FindResultSimplePair> result = new List<FindResultSimplePair>();

            for (int i=0; i < testData.DefaultView.Count; i++)
            {
                int key;
                Int32.TryParse(testData.DefaultView[i]["CityKey"].ToString(), out key);
                result.Add(
                    new FindResultSimplePair()
                    {
                        Id = key,
                        StrValue = testData.DefaultView[i]["City"].ToString()
                    });
            }

            return result;
        }

        public string GetDetailsByKey(long keyValue)
        {
            throw new NotImplementedException();
        }
    }
}
