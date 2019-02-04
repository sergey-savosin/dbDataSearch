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
        SqlServerRepository repository;
        readonly string entityName = "TestEntity";

        public SqlTestEntity(ConnectionDetails connectionDetails)
        {
            repository = new SqlServerRepository(connectionDetails);
        }

        public List<FindAllResult> FindByString(string strValue)
        {
            DataTable testData = repository.GetTestData();

            List<FindAllResult> result = new List<FindAllResult>();

            for (int i=0; i < testData.DefaultView.Count; i++)
            {
                int key;
                Int32.TryParse(testData.DefaultView[i]["CityKey"].ToString(), out key);
                result.Add(
                    new FindAllResult()
                    {
                        Id = key,
                        StrValue = testData.DefaultView[i]["City"].ToString(),
                        EntityName = entityName
                    });
            }

            return result;
        }

        public DataTable GetDetailsByKey(long keyValue)
        {
            DataTable testData = repository.GetTestDataByKey(keyValue);
            return testData;
        }
    }
}
