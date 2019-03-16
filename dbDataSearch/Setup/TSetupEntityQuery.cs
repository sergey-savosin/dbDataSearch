using dbDataSearch.Contracts;

namespace dbDataSearch.Setup
{
    public class TSetupEntityQuery
    {
        public EntityQueryType QueryType { get; set; }// = EntityQueryType.FindByKey;

        public string ParentEntityName { get; set; }// = "ParentEntity Name";

        public string QuerySql { get; set; } = "select * from a table;";

        public string QueryComment { get; set; }

        public int SortOrder { get; set; }
    }
}
