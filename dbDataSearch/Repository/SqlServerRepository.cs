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

        public SqlServerRepository(ISqlRunner _sqlRunner)
        {
            sqlRunner = _sqlRunner;
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

        public DataTable GetTestData()
        {
            string sqlQuery = @"SELECT TOP(5) [City Key] CityKey
      ,[WWI City ID]
      ,[City]
      ,[State Province]
      ,[Country]
      ,[Continent]
      ,[Sales Territory]
      ,[Region]
      ,[Subregion]
      ,[Location]
      ,[Latest Recorded Population]
  FROM [WideWorldImportersDW].[Dimension].[City]";
            return sqlRunner.GetTableValue(sqlQuery, GetConnectionDetails("WideWorldImportersDW"));
        }
    }
}
