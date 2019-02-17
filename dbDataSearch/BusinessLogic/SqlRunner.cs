using dbDataSearch.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbDataSearch.BusinessLogic
{
    public class SqlRunner : ISqlRunner
    {
        TConnectionDetails connectionDetails;

        public SqlRunner(TConnectionDetails _connectionDetails)
        {
            connectionDetails = _connectionDetails;
        }

        public DataTable GetTableResult(string sqlQuery)
        {
            string sqlConnectionString = GetSqlConnectionString(connectionDetails);
            DataTable table = new DataTable("ExampleTable");

            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, sqlConnectionString))
            {
                adapter.Fill(table);
            }

            return table;

        }

        public DataTable GetTableResultWithParam(string sqlQuery, object paramValue)
        {
            string sqlConnectionString = GetSqlConnectionString(connectionDetails);
            DataTable table = new DataTable("TableName");

            using (var conn = new SqlConnection(sqlConnectionString))
            {
                SqlCommand selectCmd = new SqlCommand(sqlQuery, conn);

				switch (paramValue)
				{
					case long keyValue:
						selectCmd.Parameters.Add("KeyValue", SqlDbType.BigInt);
						selectCmd.Parameters["KeyValue"].Value = keyValue;
						break;
					case string strValue:
						selectCmd.Parameters.Add("StrValue", SqlDbType.VarChar, 500);
						selectCmd.Parameters["StrValue"].Value = strValue;
						break;

					default:
						throw new ArgumentException(
							message: "paramValue is not recognised type",
							paramName: nameof(paramValue));

				}

				conn.Open();
                SqlDataReader reader = selectCmd.ExecuteReader();
                table.Load(reader);
            }

            return table;
        }

        #region usingSqlConnection

        private SqlConnection sqlConnection;

        /// <summary>
        /// Получить строку подключения к БД
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public string GetSqlConnectionString(TConnectionDetails connectionDetails)
        {
            var connectionBuilder = new SqlConnectionStringBuilder();
            connectionBuilder.IntegratedSecurity = true;
            connectionBuilder.DataSource = connectionDetails.ServerName;
            connectionBuilder.InitialCatalog = connectionDetails.DatabaseName;
            return connectionBuilder.ToString();
        }

        /// <summary>
        /// Получить соединение к БД
        /// </summary>
        /// <param name="connectionDetails"></param>
        /// <returns></returns>
        SqlConnection GetSqlConnection(TConnectionDetails connectionDetails)
        {
            if (sqlConnection == null)
            {
                var connectionBuilder = new SqlConnectionStringBuilder();
                connectionBuilder.IntegratedSecurity = true;
                connectionBuilder.DataSource = connectionDetails.ServerName;
                connectionBuilder.InitialCatalog = connectionDetails.DatabaseName;
                sqlConnection = new SqlConnection(connectionBuilder.ToString());
                Debug.Assert(sqlConnection != null, "sqlConnection should be not null");
            }

            return sqlConnection;
        }

        /// <summary>
        /// Закрыть соединение
        /// </summary>
        void Disconnect()
        {
            if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            sqlConnection = null;
        }

        #endregion usingSqlConnection
    }
}
