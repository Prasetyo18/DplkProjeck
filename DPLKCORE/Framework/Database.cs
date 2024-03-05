using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
//using MySql.Data;
using System.Data;

namespace DPLKCORE.Framework
{
    public class Database
    {
        public SqlConnection Conn;
        public SqlCommand Command;
        public SqlTransaction Transaction;
        public SqlDataReader reader;
        private String query = "";

        public Database(String connString) {
            this.Conn = new SqlConnection(connString);
        }


        public void Open() 
        {
            if (this.Conn.State == ConnectionState.Closed ||
                this.Conn.State == ConnectionState.Broken) 
            {
                this.Conn.Open();    
            }
        }

        public void Close() 
        {
            if (this.Conn.State == ConnectionState.Open) 
            {
                this.Conn.Close();
            }
        }

        public void setQuery(String query) 
        {
            Command = new SqlCommand();
            Command.Connection = this.Conn;
            Command.CommandText = query;
        }

        public void setQuery(String query, int timeout)
        {
            Command = new SqlCommand();
            Command.Connection = this.Conn;
            Command.CommandText = query;
            Command.CommandTimeout = timeout;
        }


        public SqlDataReader ExecuteReader() 
        {
            try
            {
                this.Command.Transaction = Transaction;
                this.reader = this.Command.ExecuteReader();
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }

            return reader;
        }

        public Object ExecuteScalar() 
        {
            object result;

            try
            {
                this.Command.Transaction = Transaction;
                result = this.Command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }

            return result;
        }

        public int ExecuteNonQuery()
        {
            try
            {
                return this.ExecuteNonQuery(CommandType.Text);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public int ExecuteNonQuery(CommandType ctype)
        {
            try
            {
                this.Command.Transaction = Transaction;
                this.Command.CommandType = ctype;
                int result = this.Command.ExecuteNonQuery();
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public DataTable ExecuteQuery()
        {
            DataTable dtResult = new DataTable();
            dtResult.Load(this.ExecuteReader());

            if (!this.reader.IsClosed)
            {

                reader.Close();
            }

            return dtResult;
        }

        public void AddParameter(String parameter, Object value) 
        {
            this.Command.Parameters.AddWithValue(parameter, value);
        }

        public void AddOutputParam(SqlParameter outputPar){
            this.Command.Parameters.Add(outputPar);
        }

        public void BeginTransaction() 
        {
            this.Transaction = this.Conn.BeginTransaction(IsolationLevel.ReadUncommitted);
            //this.Transaction.IsolationLevel = System.Data.IsolationLevel.ReadUncommitted; 
        }
        public void CommitTransaction() 
        {
            this.Transaction.Commit();
        }

        public void RollbackTransaction()
        {
            this.Transaction.Rollback();
        }
    }
}