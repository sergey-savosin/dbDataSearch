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
        const string selectOrderByKey = @"
SELECT [Order Key]
      ,ct.City + ' - ' + ct.Country City
      ,c.Customer
      ,si.[Stock Item]
      ,[Order Date Key] OrderDate
      ,[Picked Date Key] PickedDate
      ,es.Employee Salesperson
      ,ep.Employee Picker
      ,[WWI Order ID]
      ,[WWI Backorder ID]
      ,[Description]
      ,[Package]
      ,[Quantity]
      ,o.[Unit Price]
      ,o.[Tax Rate]
      ,[Total Excluding Tax]
      ,[Tax Amount]
      ,[Total Including Tax]
      ,o.[Lineage Key]
  FROM [WideWorldImportersDW].[Fact].[Order] o
  LEFT JOIN [WideWorldImportersDW].Dimension.Employee es on es.[Employee Key] = o.[Salesperson Key]
  LEFT JOIN [WideWorldImportersDW].Dimension.Employee ep on ep.[Employee Key] = o.[Picker Key]
  LEFT JOIN WideWorldImportersDW.Dimension.[Stock Item] si on si.[Stock Item Key] = o.[Stock Item Key]
  LEFT JOIN WideWorldImportersDW.Dimension.Customer c on c.[Customer Key] = o.[Customer Key]
  left join WideWorldImportersDW.Dimension.City ct on ct.[City Key] = o.[City Key]
  WHERE o.[Order Key] = @KeyValue
";

		const string selectCityByString = @"
SELECT [City Key] as KeyValue
      ,[City] StrValue
      ,'[City]' FoundColumn
  FROM [WideWorldImportersDW].[Dimension].[City] c
  WHERE c.[City] = @StrValue
";

        const string selectOrderByCityKey = @"
SELECT TOP (20)
  [Order Key] KeyValue
, o.[Order Key] StrValue
, '[Order Key]' FoundColumn
FROM [WideWorldImportersDW].[Fact].[Order] o
WHERE o.[City Key] = @KeyValue
";


        public string GetEntityQuery(string entityName, EntityQueryType queryType, string parentEntityName = "")
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
            else if (entityName == "Order")
            {
                switch (queryType)
                {
                    case EntityQueryType.FindByKey:
                        return selectOrderByKey;
                    case EntityQueryType.FindByString:
                        return "";
                    case EntityQueryType.FindByParentKey:
                        if (parentEntityName == "City")
                            return selectOrderByCityKey;
                        else
                            return "";
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
				"City",
                "Order"
			};

			return res;
		}

        /// <summary>
        /// Вернуть список дочерних сущностей
        /// </summary>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public List<string> GetChildEntityNames(string entityName)
        {
            var parentChildRelation = new Dictionary<string, List<string>>();

            addToDictionaryList(ref parentChildRelation, "City", "Order");

            return parentChildRelation
                .Where(p => p.Key == entityName)
                .Select(o => o.Value)
                .FirstOrDefault();
        }

        private void addToDictionaryList(ref Dictionary<string, List<string>> dict, string key, string value)
        {
            if (dict.ContainsKey(key))
            {
                dict[key].Add(value);
            }
            else
            {
                dict[key] = new List<string>() { value };
            };
        }
	}
}
