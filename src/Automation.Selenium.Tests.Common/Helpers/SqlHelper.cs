using System;
using System.Data;
using System.Data.SqlClient;

namespace Automation.Selenium.Tests.Common.Helpers
{
    public class SqlHelper : IDisposable
    {
        // Internal members
        protected SqlConnection Conn;
        protected string ConnString;
        protected bool Disposed;
        protected SqlTransaction Trans;

        /// <summary>
        ///     Constructor using global connection string.
        /// </summary>
        public SqlHelper()
        {
            ConnString = ConnectionString;
            Connect();
        }

        /// <summary>
        ///     Constructure using connection string override
        /// </summary>
        /// <param name="connString">Connection string for this instance</param>
        public SqlHelper(string connString)
        {
            ConnString = connString;
            Connect();
        }

        /// <summary>
        ///     Sets or returns the connection string use by all instances of this class.
        /// </summary>
        public static string ConnectionString { get; set; }

        /// <summary>
        ///     Returns the current SqlTransaction object or null if no transaction
        ///     is in effect.
        /// </summary>
        public SqlTransaction Transaction
        {
            get { return Trans; }
        }

        // Creates a SqlConnection using the current connection string
        protected void Connect()
        {
            Conn = new SqlConnection(ConnString);
            Conn.Open();
        }

        /// <summary>
        ///     Constructs a SqlCommand with the given parameters. This method is normally called
        ///     from the other methods and not called directly. But here it is if you need access
        ///     to it.
        /// </summary>
        /// <param name="qry">SQL query or stored procedure name</param>
        /// <param name="type">Type of SQL command</param>
        /// <param name="args">
        ///     Query arguments. Arguments should be in pairs where one is the
        ///     name of the parameter and the second is the value. The very last argument can
        ///     optionally be a SqlParameter object for specifying a custom argument type
        /// </param>
        /// <returns></returns>
        public SqlCommand CreateCommand(string qry, CommandType type, params object[] args)
        {
            var cmd = new SqlCommand(qry, Conn);

            // Associate with current transaction, if any
            if (Trans != null)
                cmd.Transaction = Trans;

            // Set command type
            cmd.CommandType = type;

            // Construct SQL parameters
            for (var i = 0; i < args.Length; i++)
            {
                if (args[i] is string && i < (args.Length - 1))
                {
                    var parm = new SqlParameter();
                    parm.ParameterName = (string) args[i];
                    parm.Value = args[++i];
                    cmd.Parameters.Add(parm);
                }
                else if (args[i] is SqlParameter)
                {
                    cmd.Parameters.Add((SqlParameter) args[i]);
                }
                else throw new ArgumentException("Invalid number or type of arguments supplied");
            }
            return cmd;
        }

        #region Exec Members

        /// <summary>
        ///     Executes a query that returns no results
        /// </summary>
        /// <param name="qry">Query text</param>
        /// <param name="args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
        /// <returns>The number of rows affected</returns>
        public int ExecNonQuery(string qry, params object[] args)
        {
            using (var cmd = CreateCommand(qry, CommandType.Text, args))
            {
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        ///     Executes a stored procedure that returns no results
        /// </summary>
        /// <param name="proc">Name of stored proceduret</param>
        /// <param name="args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
        /// <returns>The number of rows affected</returns>
        public int ExecNonQueryProc(string proc, params object[] args)
        {
            using (var cmd = CreateCommand(proc, CommandType.StoredProcedure, args))
            {
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        ///     Executes a query that returns a single value
        /// </summary>
        /// <param name="qry">Query text</param>
        /// <param name="args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
        /// <returns>Value of first column and first row of the results</returns>
        public object ExecScalar(string qry, params object[] args)
        {
            using (var cmd = CreateCommand(qry, CommandType.Text, args))
            {
                return cmd.ExecuteScalar();
            }
        }

        /// <summary>
        ///     Executes a query that returns a single value
        /// </summary>
        /// <param name="qry">Name of stored proceduret</param>
        /// <param name="args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
        /// <returns>Value of first column and first row of the results</returns>
        public object ExecScalarProc(string qry, params object[] args)
        {
            using (var cmd = CreateCommand(qry, CommandType.StoredProcedure, args))
            {
                return cmd.ExecuteScalar();
            }
        }

        /// <summary>
        ///     Executes a query and returns the results as a SqlDataReader
        /// </summary>
        /// <param name="qry">Query text</param>
        /// <param name="args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
        /// <returns>Results as a SqlDataReader</returns>
        public SqlDataReader ExecDataReader(string qry, params object[] args)
        {
            using (var cmd = CreateCommand(qry, CommandType.Text, args))
            {
                return cmd.ExecuteReader();
            }
        }

        /// <summary>
        ///     Executes a stored procedure and returns the results as a SqlDataReader
        /// </summary>
        /// <param name="qry">Name of stored proceduret</param>
        /// <param name="args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
        /// <returns>Results as a SqlDataReader</returns>
        public SqlDataReader ExecDataReaderProc(string qry, params object[] args)
        {
            using (var cmd = CreateCommand(qry, CommandType.StoredProcedure, args))
            {
                return cmd.ExecuteReader();
            }
        }

        /// <summary>
        ///     Executes a query and returns the results as a DataSet
        /// </summary>
        /// <param name="qry">Query text</param>
        /// <param name="args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
        /// <returns>Results as a DataSet</returns>
        public DataSet ExecDataSet(string qry, params object[] args)
        {
            using (var cmd = CreateCommand(qry, CommandType.Text, args))
            {
                var adapt = new SqlDataAdapter(cmd);
                var ds = new DataSet();
                adapt.Fill(ds);
                return ds;
            }
        }

        /// <summary>
        ///     Executes a stored procedure and returns the results as a Data Set
        /// </summary>
        /// <param name="qry">Name of stored proceduret</param>
        /// <param name="args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
        /// <returns>Results as a DataSet</returns>
        public DataSet ExecDataSetProc(string qry, params object[] args)
        {
            using (var cmd = CreateCommand(qry, CommandType.StoredProcedure, args))
            {
                var adapt = new SqlDataAdapter(cmd);
                var ds = new DataSet();
                adapt.Fill(ds);
                return ds;
            }
        }

        #endregion

        #region Transaction Members

        /// <summary>
        ///     Begins a transaction
        /// </summary>
        /// <returns>The new SqlTransaction object</returns>
        public SqlTransaction BeginTransaction()
        {
            Rollback();
            Trans = Conn.BeginTransaction();
            return Transaction;
        }

        /// <summary>
        ///     Commits any transaction in effect.
        /// </summary>
        public void Commit()
        {
            if (Trans != null)
            {
                Trans.Commit();
                Trans = null;
            }
        }

        /// <summary>
        ///     Rolls back any transaction in effect.
        /// </summary>
        public void Rollback()
        {
            if (Trans != null)
            {
                Trans.Rollback();
                Trans = null;
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                // Need to dispose managed resources if being called manually
                if (disposing)
                {
                    if (Conn != null)
                    {
                        Rollback();
                        Conn.Dispose();
                        Conn = null;
                    }
                }
                Disposed = true;
            }
        }

        #endregion
    }
}