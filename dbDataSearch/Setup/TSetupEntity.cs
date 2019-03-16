using System.Collections.Generic;

namespace dbDataSearch.Setup
{
    public class TSetupEntity
    {
        public string EntityName { get; set; } = "New Name";

        // Необходимо создать данную коллекцию сразу,
        // чтобы при добавлении строки в BS для SetupEntityCollection
        // можно было работать с дочерней BS для EntityQueryCollection
        public List<TSetupEntityQuery> EntityQueryCollection { get; set; } = new List<TSetupEntityQuery>();

        public string EntityComment { get; set; }// = "Test Entity 2";

    }
}
