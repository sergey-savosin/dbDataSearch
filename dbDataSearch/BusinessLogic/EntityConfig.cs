using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using dbDataSearch.Contracts;

namespace dbDataSearch.BusinessLogic
{
	public class EntityConfig : IEntityConfig
	{
		const string selectCityByKey = @"
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
      , DataFoundInColumn = '[City Key]'
  FROM [WideWorldImportersDW].[Dimension].[City]
  WHERE [City Key] = @KeyValue;
";

		const string selectCityByString = @"
SELECT [City Key] as [Key]
      ,[City] StrValue
      ,'[City]' FoundColumn
  FROM [WideWorldImportersDW].[Dimension].[City] c
  WHERE c.[City] = @StrValue
";

		public string GetEntityQuery(string entityName, EntityQueryType queryType)
		{
			if (entityName == "City")
			{
				switch (queryType)
				{
					case EntityQueryType.FindByKey:
						return selectCityByKey;
					case EntityQueryType.FindByString:
						return selectCityByString;
					default:
						throw new ArgumentException("No query for this EntityQueryType");
				}
			}
			else
			{
				throw new ArgumentException($"No query for this EntityName: {entityName}");
			}
		}

		public List<string> GetRootEntityNames()
		{
			List<string> res = new List<string>()
			{
				"City"
			};

			return res;
		}
	}
}
