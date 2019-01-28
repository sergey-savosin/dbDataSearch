using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbDataSearch.Contracts
{
    public interface IEntity
    {
        /// <summary>
        /// Поиск сущности по строке.
        /// Возвращает Key найденных строк
        /// </summary>
        /// <param name="strValue">Строка для поиска</param>
        /// <returns>Список Key найденных сущностей</returns>
        List<FindResultSimplePair> FindByString(string strValue);
        
        /// <summary>
        /// Получение детальной информации сущности по ключу
        /// </summary>
        /// <param name="keyValue">Значение ключа сущности</param>
        string GetDetailsByKey(long keyValue);
    }
}
