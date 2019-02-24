using System.Collections.Generic;

namespace dbDataSearch.Setup
{
    public class TSetupEntity
    {
        public string EntityName { get; set; }// = "New Name";

        public List<TSetupEntityQuery> EntityQueryCollection { get; set; }// = new List<TSetupEntityQuery>();

        public string EntityComment { get; set; }// = "Test Entity 2";

    }
}
