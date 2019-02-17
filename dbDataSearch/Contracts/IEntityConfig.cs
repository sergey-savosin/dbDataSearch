using System.Collections.Generic;

namespace dbDataSearch.Contracts
{
	public interface IEntityConfig
	{
		List<string> GetRootEntityNames();

		string GetEntityQuery(string EntityName, EntityQueryType queryType, string parentEntityName = "");

        List<string> GetChildEntityNames(string entityName);

    }
}
