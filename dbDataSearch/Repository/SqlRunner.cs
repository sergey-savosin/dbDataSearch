using dbDataSearch.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbDataSearch.Repository
{
    public class SqlRunner : ISqlRunner
    {
        ConnectionDetails connectionDetails;

        public SqlRunner(ConnectionDetails _connectionDetails)
        {
            connectionDetails = _connectionDetails;
        }

        public void Execute(string sqlQuery)
        {
            throw new NotImplementedException();
        }

        public string GetScalarValue(string sqlQuery)
        {
            throw new NotImplementedException();
        }

        public DataTable GetTableValue(string sqlQuery)
        {
            string sqlConnectionString = GetSqlConnectionString(connectionDetails);
            DataTable table = new DataTable("ExampleTable");

            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, sqlConnectionString))
            {
                adapter.Fill(table);
            }

            return table;

        }

        public DataTable GetTableValue2(string sqlQuery)
        {
            string sqlConnectionString = GetSqlConnectionString(connectionDetails);
            DataTable table = new DataTable("ExampleTable2");

            using (var conn = new SqlConnection(sqlConnectionString))
            {
                SqlCommand selectCmd = new SqlCommand(sqlQuery);
                conn.Open();
                SqlDataReader reader = selectCmd.ExecuteReader();
                table.Load(reader);
            }

            return table;

        }

        public DataTable GetTableValueByKey2(string sqlQuery, long keyValue)
        {
            string sqlConnectionString = GetSqlConnectionString(connectionDetails);
            DataTable table = new DataTable("ExampleTableByKey");

            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery))
                {
                    SqlParameter prm = new SqlParameter("KeyValue", SqlDbType.BigInt);
                    prm.Value = keyValue;
                    cmd.Parameters.Add(prm);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }
            }

            return table;
        }

        public DataTable GetTableValueByKey(string sqlQuery, long keyValue)
        {
            string sqlConnectionString = GetSqlConnectionString(connectionDetails);
            DataTable table = new DataTable("ExampleTableByKey");

            using (var conn = new SqlConnection(sqlConnectionString))
            {
                SqlCommand selectCmd = new SqlCommand(sqlQuery, conn);
                selectCmd.Parameters.Add("KeyValue", SqlDbType.BigInt);
                selectCmd.Parameters["KeyValue"].Value = keyValue;

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
        public string GetSqlConnectionString(ConnectionDetails connectionDetails)
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
        SqlConnection GetSqlConnection(ConnectionDetails connectionDetails)
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
