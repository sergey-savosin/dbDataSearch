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
		IEntityConfig entityConfig;
		ISqlRunner sqlRunner;
        TConnectionDetails connectionDetails;

		public EntityRepository(TConnectionDetails connectionDetails)
		{
			entityConfig = new EntityConfig();
            this.connectionDetails = connectionDetails;
            sqlRunner = new SqlRunner(connectionDetails);
		}

        public DataTable GetEntityDetailsByKey(string entityName, long key)
        {
			string selectQuery = entityConfig.GetEntityQuery(entityName, EntityQueryType.FindByKey);
            return sqlRunner.GetTableResultWithParam(selectQuery, key);
        }

        public List<TSearchEntityResult> SearchEntitiesByString(string strValue)
        {
            List<TSearchEntityResult> searchResult = new List<TSearchEntityResult>();

            List<string> entityNames = entityConfig.GetRootEntityNames();
            foreach (string entityName in entityNames)
            {
                string selectQuery = entityConfig.GetEntityQuery(entityName, EntityQueryType.FindByString);
                DataTable entityResult = sqlRunner.GetTableResultWithParam(selectQuery, strValue);

                for (int i = 0; i < entityResult.DefaultView.Count; i++)
                {
                    int key;
                    string foundStrValue = "n\a";
                    string foundColumn = "";
                    bool parseResult = Int32.TryParse(entityResult.DefaultView[i]["Key"].ToString(), out key);
                    if (parseResult)
                    {
                        foundStrValue = entityResult.DefaultView[i]["StrValue"].ToString();
                        foundColumn = entityResult.DefaultView[i]["FoundColumn"].ToString();
                    }
                    searchResult.Add(
                        new TSearchEntityResult()
                        {
                            Key = key,
                            StrValue = foundStrValue,
                            FoundColumn = foundColumn,
                            EntityName = entityName
                        });
                }
            }

            return searchResult;
        }
    }
}
