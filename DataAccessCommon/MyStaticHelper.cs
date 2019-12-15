using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text.RegularExpressions;
namespace DataAccessCommon
{
    public static class MyStaticDBHelper
    {
        private static readonly string  parameterPattern = @"[@|:]\w+\b?";
        public static string providerName = "MySql.Data.MySqlClient"; //"System.Data.SqlClient","System.Data.OracleClient","System.Data.OleDb","MySql.Data.MySqlClient"
        private static DbProviderFactory dataFactory = DbProviderFactories.GetFactory(providerName);
        public static string CONNECTION_STRING = null;
        #region private methods
        private static DbCommand CreateCommand(object conn)
        {
            DbCommand command = null;
            if (conn is DbConnection)
            {
                command = ((DbConnection)conn).CreateCommand();
                if (command.Connection.State != ConnectionState.Open)
                    command.Connection.Open();
            }
            else //It is a transaction, then join the transaction
            {
                command = ((DbTransaction)conn).Connection.CreateCommand();
                command.Transaction = (DbTransaction)conn;
            }
            return command;
        }
        private static DbType GetDbType(Type runtimeType)
        {
            var nonNullableType = Nullable.GetUnderlyingType(runtimeType);
            if (nonNullableType != null)
            {
                runtimeType = nonNullableType;
            }
            var templateValue = (Object)null;
            if (runtimeType.IsClass == false)
            {
                templateValue = Activator.CreateInstance(runtimeType);
            }
            else if (runtimeType.IsClass == true && runtimeType.IsArray == true)
            {
                List<object> array = new List<object>();
                array.Add(Activator.CreateInstance(runtimeType.GetElementType()));
                array.Add(Activator.CreateInstance(runtimeType.GetElementType()));
                templateValue = array.ToArray();
            }
            var sqlParamter = dataFactory.CreateParameter();
            sqlParamter.ParameterName = string.Empty;
            sqlParamter.Value = templateValue;
            return sqlParamter.DbType;
        }
        private static List<String> GetParameterNames(string strSQLOrSPName)
        {
            Regex regex = new Regex(parameterPattern);
            MatchCollection matchCollection = regex.Matches(strSQLOrSPName);
            List<string> parameterNames = new List<string>();
            foreach (Match match in matchCollection)
                parameterNames.Add(match.Value);
            return parameterNames;
        }
        private static DbCommand SetupCommand(object conn, CommandType commandType, string strSQLOrSPName, object[] parameters, PropertyInfo[] properties = null)
        {
            List<string> parameterNames = GetParameterNames(strSQLOrSPName);
            DbCommand command = CreateCommand(conn);
            command.CommandText = strSQLOrSPName;
            command.CommandType = commandType;
            List<DbParameter> myDBParameters = new List<DbParameter>();
            int i = 0;
            if (parameters != null && parameters.Length > 0)
            {
                foreach (object parameter in parameters)
                {
                    Type parameterType = null;
                    if (parameter == null)
                    {
                        if (properties != null)
                        {
                            var propertyList = new List<PropertyInfo>(properties);
                            var property = propertyList.Find(x => x.Name == parameterNames[i].Substring(1));
                            parameterType = property.PropertyType;
                        }
                        else
                        {
                            parameterType = typeof(string);
                        }
                    }
                    else
                        parameterType = parameter.GetType();
                    myDBParameters.Add(CreateDBParameter(parameterNames[i], GetDbType(parameterType), parameter, ParameterDirection.Input));
                    i++;
                }
            }
            if (myDBParameters.Count > 0)
            {
                command.Parameters.AddRange(myDBParameters.ToArray());
            }
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
        private static DbConnection GetConnection()
        {
            DbConnection connection = dataFactory.CreateConnection();
            connection.ConnectionString = CONNECTION_STRING;
            return connection;
        }
        private static void BuildParameterList<T>(string sql, T entity, List<object> parameterList)
        {
            List<string> parameterNames = GetParameterNames(sql);
            var properties = new List<PropertyInfo>(entity.GetType().GetProperties());
            foreach (string parameterName in parameterNames)
            {
                var property = properties.Find(x => x.Name.ToLower() == parameterName.Substring(1).ToLower());
                parameterList.Add(property.GetValue(entity));
            }
        }
        private static T DoQuery<T>(Func<DbConnection, string, object[], T> function, string sql, object[] parameterList)
        {
            DbConnection dbConnection = null;
            dbConnection = GetConnection();
            dbConnection.Open();
            T returnResult = function(dbConnection, sql, parameterList);
            dbConnection.Close();
            return returnResult;
        }
        private static DataSet ExecuteDataset(string sql, params object[] parameterList)
        {
            return DoQuery<DataSet>((conn, SQL, parameters) =>
            {
                DbCommand command = SetupCommand(conn, CommandType.Text, SQL, parameters);
                DbDataAdapter dataAdaptor = dataFactory.CreateDataAdapter();
                DataSet ds = new DataSet();
                dataAdaptor.SelectCommand = command;
                dataAdaptor.Fill(ds);
                return ds;
            }, sql, parameterList);
        }
        private static DataSet ExecuteDatasetEntity<T>(string sql, T entity)
        {
            var parameterList = new List<object>();
            BuildParameterList(sql, entity, parameterList);
            return DoQuery<DataSet>((conn, SQL, parameters) =>
            {
                DbCommand command = SetupCommand(conn, CommandType.Text, SQL, parameters, entity.GetType().GetProperties());
                DbDataAdapter dataAdaptor = dataFactory.CreateDataAdapter();
                DataSet ds = new DataSet();
                dataAdaptor.SelectCommand = command;
                dataAdaptor.Fill(ds);
                return ds;
            }, sql, parameterList.ToArray());
        }
        #endregion
        public static int ExecuteNonQueryByParameters(string sql, params object[] parameterList)
        {
            return DoQuery<int>((conn, SQL, parameters) =>
            {
                DbCommand command = SetupCommand(conn, CommandType.Text, SQL, parameters);
                var returnCount = command.ExecuteNonQuery();
                return returnCount;
            }, sql, parameterList);
        }
        public static int ExecuteNonQueryByEntity<T>(string sql, T entity)
        {
            var parameterList = new List<object>();
            BuildParameterList(sql, entity, parameterList);
            return DoQuery<int>((conn, SQL, parameters) =>
            {
                DbCommand command = SetupCommand(conn, CommandType.Text, SQL, parameters, entity.GetType().GetProperties());
                var returnCount = command.ExecuteNonQuery();
                return returnCount;
            }, sql, parameterList.ToArray());
        }
        public static T GetEntityByParameters<T>(string sql, params object[] parameterList)
        {
            return ToEntityByEmit.GetEntity<T>(ExecuteDataset(sql, parameterList).Tables[0]);
        }
        public static T GetEntityByEntity<T>(string sql, T entity)
        {
            return ToEntityByEmit.GetEntity<T>(ExecuteDatasetEntity(sql, entity).Tables[0]);
        }
        public static List<T> GetListByParameters<T>(string sql, params object[] parameterList)
        {
            return ToEntityByEmit.GetList<T>(ExecuteDataset(sql, parameterList).Tables[0]);
        }
        public static List<T> GetListByEntity<T>(string sql, T entity)
        {
            return ToEntityByEmit.GetList<T>(ExecuteDatasetEntity(sql, entity).Tables[0]);
        }
    }
}
