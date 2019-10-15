using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace DataAccessCommon
{
    /// <summary>
    /// The MyDBHelper class is intended to encapsulate high performance, scalable best practices for 
    /// common uses of SqlClient, OracleClient, OleDb, and others
    /// </summary>
    public static class MyStaticDBHelper
    {
        public struct MyDBParameter
        {
            public string strParameterName;
            public DbType dbType;
            public object value;
            public ParameterDirection parameterDirection;

            public MyDBParameter(string parameterName, DbType type, object theValue, ParameterDirection direction = ParameterDirection.Input)
            {
                strParameterName = parameterName;
                dbType = type;
                value = theValue;
                parameterDirection = direction;
            }
        }
        public static string DatabaseType = "MySql";
        private static Dictionary<string, string> providers = new Dictionary<string, string>() {
                { "SqlServer", "System.Data.SqlClient" }
                , { "Oracle", "System.Data.OracleClient" }
                , { "OleDb", "System.Data.OleDb" }
                , { "MySql", "MySql.Data.MySqlClient" }
                };
        private static DbProviderFactory dataFactory = DbProviderFactories.GetFactory(providers[DatabaseType]);
        public static string CONNECTION_STRING = null;

        #region private methods
        private static void AttachParameters(DbCommand command, DbParameter[] parameters)
        {
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
        }

        private static DbCommand CreateCommand(object conn)
        {
            DbCommand command = null;
            //If it is just a connection(not a transaction)
            if (conn is DbConnection)
            {
                command = ((DbConnection)conn).CreateCommand();
                if (command.Connection.State != ConnectionState.Open)
                {
                    command.Connection.Open();
                }
            }
            else //It is a transaction, then join the transaction
            {
                command = ((DbTransaction)conn).Connection.CreateCommand();
                command.Transaction = (DbTransaction)conn;
            }
            return command;
        }

        private static DbCommand SetupCommand(object conn, CommandType commandType, string strSQLOrSPName, List<MyDBParameter> myDBParameters)
        {
            DbParameter[] parameters = myDBParameters != null ? CreateDBParameters(myDBParameters).ToArray() : null;
            DbCommand command = CreateCommand(conn);
            command.CommandText = strSQLOrSPName;
            command.CommandType = commandType;
            AttachParameters(command, parameters);
            return command;
        }

        private static DbParameter CreateDBParameter(string strParameterName, DbType dbType, object value, ParameterDirection direction)
        {
            DbParameter parameter = dataFactory.CreateParameter();
            parameter.ParameterName = strParameterName;
            parameter.DbType = dbType;
            parameter.Value = value;
            parameter.Direction = direction;
            return parameter;
        }

        private static List<DbParameter> CreateDBParameters(List<MyDBParameter> myDBParameters)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            foreach (MyDBParameter myDBParameter in myDBParameters)
            {
                parameters.Add(CreateDBParameter(myDBParameter.strParameterName, myDBParameter.dbType, myDBParameter.value, myDBParameter.parameterDirection));
            }
            return parameters;
        }

        private static DbConnection GetConnection()
        {
            DbConnection connection = dataFactory.CreateConnection();
            connection.ConnectionString = CONNECTION_STRING;
            return connection;
        }

        private static int ExecuteNonQuery(object conn, CommandType commandType, string strSQLOrSPName, List<MyDBParameter> myDBParameters = null)
        {
            DbCommand command = SetupCommand(conn, commandType, strSQLOrSPName, myDBParameters);
            return command.ExecuteNonQuery();
        }

        private static DataSet ExecuteDataset(object conn, CommandType commandType, string strSQLOrSPName, List<MyDBParameter> myDBParameters = null)
        {
            DbCommand command = SetupCommand(conn, commandType, strSQLOrSPName, myDBParameters);
            DbDataAdapter dataAdaptor = dataFactory.CreateDataAdapter();
            DataSet ds = new DataSet();
            dataAdaptor.SelectCommand = command;
            dataAdaptor.Fill(ds);
            return ds;
        }

        private static DbDataReader ExecuteReader(object conn, CommandType commandType, string strSQLOrSPName, List<MyDBParameter> myDBParameters = null)
        {
            DbCommand command = SetupCommand(conn, commandType, strSQLOrSPName, myDBParameters);
            return command.ExecuteReader();
        }

        private static object ExecuteScalar(object conn, CommandType commandType, string strSQLOrSPName, List<MyDBParameter> myDBParameters = null)
        {
            DbCommand command = SetupCommand(conn, commandType, strSQLOrSPName, myDBParameters);
            return command.ExecuteScalar();
        }
        #endregion

        public static int ExecuteNonQuery(List<MyDBParameter> parameterList, string sql, ILogger logger)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = GetConnection();
                dbConnection.Open();
                var returnCount = ExecuteNonQuery(dbConnection, CommandType.Text, sql, parameterList);
                dbConnection.Close();
                return returnCount;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public static DataSet ExecuteDataset(List<MyDBParameter> parameterList, string sql, ILogger logger)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = GetConnection();
                dbConnection.Open();
                var dataSet = ExecuteDataset(dbConnection, CommandType.Text, sql, parameterList);
                dbConnection.Close();
                return dataSet;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}