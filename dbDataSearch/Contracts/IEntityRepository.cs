using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbDataSearch.Contracts
{
    interface IEntityRepository
    {
        /// <summary>
        /// Поиск среди всех сущностей по строке
        /// </summary>
        /// <param name="strValue">Строка поиска</param>
        /// <returns>Список результатов поиска</returns>
        List<TSearchEntityResult> SearchEntitiesByString(string strValue);

        /// <summary>
        /// Получение подробностей по названию и ключу сущности
        /// </summary>
        /// <param name="entityName">Название сущности</param>
        /// <param name="key">Ключ</param>
        /// <returns>Результат запроса</returns>
        DataTable GetEntityDetailsByKey(string entityName, long key);

        /// <summary>
        /// Поиск дочерних сущностей по ключу текущей сущности
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns>Список результатов поиска</returns>
        List<TSearchEntityResult> SearchChildEntitiesByParentKey(string parentEntityName, long parentKeyValue);
    }
}
