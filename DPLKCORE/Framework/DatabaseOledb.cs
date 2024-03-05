using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

namespace DPLKCORE.Framework
{
    public class DatabaseOledb
    {
        public OleDbConnection Conn;
        public OleDbCommand Command;
        public OleDbTransaction Transaction;
        public OleDbDataReader reader;
        private String query = "";

        public DatabaseOledb(String connString)
        {
            this.Conn = new OleDbConnection(connString);
        }

        public void Open() {
        if (this.Conn.State == ConnectionState.Closed ||
            this.Conn.State == ConnectionState.Broken) {
        this.Conn.Open();    
             }
        }

        public void Close() {
            if (this.Conn.State == ConnectionState.Open) {
                this.Conn.Close();
            }
        }

        public void setQuery(String query) {
            Command = new OleDbCommand();
            Command.Connection = this.Conn;
            Command.CommandText = query;
        }

        public OleDbDataReader ExecuteReader()
        {
            try
            {
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
                this.Command.Transaction = Transaction;
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

        public void BeginTransaction() 
        {
            this.Transaction = this.Conn.BeginTransaction();
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