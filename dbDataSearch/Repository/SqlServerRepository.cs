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
        ConnectionDetails connectionDetails;

        public SqlServerRepository()
        {
            connectionDetails = GetConnectionDetails("test");
            sqlRunner = new SqlRunner(connectionDetails);
        }

        public SqlServerRepository(ConnectionDetails _connectionDetails)
        {
            connectionDetails = _connectionDetails;
            sqlRunner = new SqlRunner(connectionDetails);
        }

        /// <summary>
        /// Список всех соединений к SqlServer
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllConnectionNames()
        {
            List<string> list = new List<string>()
            {
                "talent_manager","test"
            };
            return list;
        }

        public List<IEntity> GetAllRootEntities()
        {
            List<IEntity> lst = new List<IEntity>();
            lst.Add(new TestEntity());
            lst.Add(new SqlTestEntity(connectionDetails));

            return lst;
        }

        /// <summary>
        /// Данные одного соединения SqlServer
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public ConnectionDetails GetConnectionDetails(string connectionName)
        {
            return new ConnectionDetails()
            {
                ServerName = @"SergeyHome\SQLExpress",
                ConnectionName = connectionName,
                DatabaseName = "WideWorldImportersDW"
            };

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
