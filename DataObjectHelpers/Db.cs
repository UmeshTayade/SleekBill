using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Reflection;

namespace DataObjectHelpers
{
    public static class Db
    {
        private static readonly DbProviderFactory factory = ProviderFactory.DefProviderFactory.GetProviderFactory();

        public static DbCommand CreateCommand(string sql, CommandType CommandType, params DbParameter[] Parameters)
        {
            IDbConnection con = null;
            DbCommand command2;
            try
            {
                con = ConnectionFactory.DefConectionFactory.getNewConnection();
                DbCommand command = factory.CreateCommand();
                command.Connection = (DbConnection)con;
                command.CommandType = CommandType;
                command.CommandText = sql;
                command.Parameters.AddRange(Parameters);
                command2 = command;
            }
            finally
            {
                ConnectionFactory.safeClose(con);
                con = null;
            }
            return command2;
        }

        public static DbParameter CreateParameter(string ParameterName)
        {
            DbParameter parameter = factory.CreateParameter();
            parameter.ParameterName = ParameterName;
            return parameter;
        }

        public static DbParameter CreateParameter(string ParameterName, object ParameterValue)
        {
            DbParameter parameter = factory.CreateParameter();
            parameter.ParameterName = ParameterName;
            parameter.Value = ParameterValue;
            return parameter;
        }

        public static DbParameter CreateParameter(string ParameterName, DbType ParameterType, int ParameterSize)
        {
            DbParameter parameter = factory.CreateParameter();
            parameter.ParameterName = ParameterName;
            parameter.DbType = ParameterType;
            parameter.Size = ParameterSize;
            return parameter;
        }

        public static string Escape(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return "NULL";
            }
            return ("'" + s.Trim().Replace("'", "''") + "'");
        }

        public static string Escape(string s, int maxLength)
        {
            if (string.IsNullOrEmpty(s))
            {
                return "NULL";
            }
            s = s.Trim();
            if (s.Length > maxLength)
            {
                s = s.Substring(0, maxLength - 1);
            }
            return ("'" + s.Trim().Replace("'", "''") + "'");
        }

        public static int ExecuteNonQuery(string sql, CommandType CommandType, params DbParameter[] Parameters)
        {
            IDbConnection con = null;
            int num2;
            try
            {
                con = ConnectionFactory.DefConectionFactory.getNewConnection();
                DbCommand command = factory.CreateCommand();
                command.Connection = (DbConnection)con;
                command.CommandType = CommandType;
                command.CommandText = sql;
                command.Parameters.AddRange(Parameters);
                num2 = command.ExecuteNonQuery();
            }
            finally
            {
                ConnectionFactory.safeClose(con);
                con = null;
            }
            return num2;
        }

        public static IDataReader ExecuteReader(string sql, CommandType CommandType, params DbParameter[] Parameters)
        {
            IDbConnection connection = null;
            try
            {
                connection = ConnectionFactory.DefConectionFactory.getNewConnection();
                DbCommand command = factory.CreateCommand();
                command.Connection = (DbConnection)connection;
                command.CommandType = CommandType;
                command.CommandText = sql;
                command.Parameters.AddRange(Parameters);
                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            finally
            {
            }
        }

        public static object ExecuteScalar(string sql, CommandType CommandType, params DbParameter[] Parameters)
        {
            IDbConnection con = null;
            object obj3;
            try
            {
                con = ConnectionFactory.DefConectionFactory.getNewConnection();
                DbCommand command = factory.CreateCommand();
                command.Connection = (DbConnection)con;
                command.CommandType = CommandType;
                command.CommandText = sql;
                command.Parameters.AddRange(Parameters);
                obj3 = command.ExecuteScalar();
            }
            finally
            {
                ConnectionFactory.safeClose(con);
                con = null;
            }
            return obj3;
        }

        public static TTarget Map<TTarget>(string sql, CommandType CommandType, params DbParameter[] Parameters) where TTarget : new()
        {
            List<TTarget> list = MapReader<TTarget>(sql, CommandType.StoredProcedure, Parameters);
            if (list.Count > 0)
            {
                return list[0];
            }
            return new TTarget();
        }

        public static List<TTarget> MapReader<TTarget>(string sql, CommandType CommandType, params DbParameter[] Parameters) where TTarget : new()
        {
            IDataReader reader = ExecuteReader(sql, CommandType.StoredProcedure, Parameters);
            ValidateMappings<TTarget>(reader);
            List<TTarget> list = new List<TTarget>();
            while (reader.Read())
            {
                TTarget local2 = default(TTarget);
                TTarget target = (local2 == null) ? Activator.CreateInstance<TTarget>() : (local2 = default(TTarget));
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetValue(i) != DBNull.Value)
                    {
                        DataMapper.SetPropertyValue(target, reader.GetName(i), reader.GetValue(i));
                    }
                }
                list.Add(target);
            }
            reader.Close();
            return list;
        }

        private static void ValidateMappings<TTarget>(IDataRecord reader)
        {
            List<PropertyInfo> list = new List<PropertyInfo>(DataMapper.GetSourceProperties(typeof(TTarget)));
            Predicate<PropertyInfo> match = null;
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (match == null)
                {
                    match = pi => pi.Name == reader.GetName(i);
                }
                if (list.Find(match) == null)
                {
                    throw new Exception(string.Format("Property '{0}' of type '{1}' is missing from the type '{2}'", reader.GetName(i), reader.GetFieldType(i), typeof(TTarget).FullName));
                }
            }
        }
    }
}
