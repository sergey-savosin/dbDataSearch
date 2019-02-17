namespace dbDataSearch.Contracts
{
    public class TSearchEntityResult
    {
        public string EntityName { get; set; }

        public long Key { get; set; }

        /// <summary>
        /// Точное значение найденной строки, если поиск производился с шаблоном
        /// </summary>
        public string StrValue { get; set; }

        /// <summary>
        /// Подробности - в каком атрибуте найдена искомая строка
        /// </summary>
        public string FoundColumn { get; set; }
    }
}
