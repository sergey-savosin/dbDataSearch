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
        /// <param name="connectionDetails">Параметры соединения с БД</param>
        /// <returns>Список результатов поиска</returns>
        List<TSearchEntityResult> SearchEntitiesByString(string strValue, TConnectionDetails connectionDetails);

        /// <summary>
        /// Получение подробностей по названию и ключу сущности
        /// </summary>
        /// <param name="EntityName">Название сущности</param>
        /// <param name="key">Ключ</param>
        /// <param name="connectionDetails">Параметры соединения с БД</param>
        /// <returns>Результат запроса</returns>
        DataTable GetEntityDetailsByKey(string EntityName, long key, TConnectionDetails connectionDetails);
    }
}
