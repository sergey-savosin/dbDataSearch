using dbDataSearch.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbDataSearch.Repository
{
    public class SqlServerRepository : IRepository
    {
        ISqlRunner sqlRunner;
        TConnectionDetails connectionDetails;

        public SqlServerRepository()
        {
            connectionDetails = GetConnectionDetails("test");
            sqlRunner = new SqlRunner(connectionDetails);
        }

        public SqlServerRepository(TConnectionDetails _connectionDetails)
        {
            connectionDetails = _connectionDetails;
            sqlRunner = new SqlRunner(connectionDetails);
        }

        public List<IEntity> GetAllRootEntities()
        {
            List<IEntity> lst = new List<IEntity>();
            lst.Add(new SqlTestEntity(connectionDetails));

            return lst;
        }

        public DataTable GetEntityDetails(string entityName, long entityKey)
        {
            switch (entityName)
            {
                case "TestEntity":
                    SqlTestEntity ent = new SqlTestEntity(connectionDetails);
                    return ent.GetDetailsByKey(entityKey);
                default:
                    throw new NotSupportedException($"Invalid entity: {entityName}");
            }
        }

        public DataTable GetTestData()
        {
            string sqlQuery = @"
SELECT TOP(50) [City Key] CityKey
      ,[WWI City ID]
      ,[City]
      ,[State Province]
      ,[Country]
      ,[Continent]
      ,[Sales Territory]
      ,[Region]
      ,[Subregion]
      ,[Latest Recorded Population]
  FROM [WideWorldImportersDW].[Dimension].[City]";
            return sqlRunner.GetTableValue(sqlQuery);
        }

        public DataTable GetTestDataByKey(long keyValue)
        {
            string sqlQuery = @"
SELECT [City Key] CityKey
      ,[WWI City ID]
      ,[City]
      ,[State Province]
      ,[Country]
      ,[Continent]
      ,[Sales Territory]
      ,[Region]
      ,[Subregion]
      ,[Latest Recorded Population]
  FROM [WideWorldImportersDW].[Dimension].[City]
  WHERE [City Key] = @KeyValue";
            return sqlRunner.GetTableValueByKey(sqlQuery, keyValue);

        }
    }
}
