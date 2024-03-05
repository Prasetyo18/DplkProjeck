using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using MySql.Data.MySqlClient;
using System.Data;

namespace DPLKCORE.Framework
{
    public class DatabaseMySql
    {
//        public MySqlConnection Mcon;
//        public MySqlCommand Command;
//        public MySqlTransaction Transaction;
//        public MySqlDataReader reader;
//        private String query = "";

//        public DatabaseMySql(String conStrMysql)
//        {
//            this.Mcon = new MySqlConnection(conStrMysql);
//        }

//        public void Open()
//        {
//            if (this.Mcon.State == ConnectionState.Closed || this.Mcon.State == ConnectionState.Broken)
//            {
//                this.Mcon.Open();
//            }
//        }

//        public void Close()
//        {
//            if (this.Mcon.State == ConnectionState.Open)
//            {
//                this.Mcon.Close();
//            }
//        }
        
//        public void setQuery(String query)
//        {
//            Command = new MySqlCommand();
//            Command.Connection = this.Mcon;
//            Command.CommandText = query;
//        }

//        public void setQuery(String query, int timeout)
//        {
//            Command = new MySqlCommand();
//            Command.Connection = this.Mcon;
//            Command.CommandText = query;
//            Command.CommandTimeout = timeout;
//        }

//        public MySqlDataReader ExecuteReader()
//        {
//            try
//            {
//                this.Command.Transaction = Transaction;
//                this.reader = this.Command.ExecuteReader();
//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.Message);
//            }

//            return reader;
//        }

//        public Object ExecuteScalar()
//        {
//            object result;
//            try
//            {
//                this.Command.Transaction = Transaction;
//                result = this.Command.ExecuteScalar();
//            }
//            catch (Exception ex)
//            {
                
//                throw new Exception (ex.Message);
//            }
//            return result;
//        }

//        public int ExecuteNonQuery()
//        {
//            try
//            {
//                return this.ExecuteNonQuery(CommandType.Text);
//            }
//            catch (Exception ex)
//            {
                
//                throw new Exception (ex.Message);
//            }
//        }

//        public int ExecuteNonQuery(CommandType ctype)
//        {
//            try
//            {
//                this.Command.Transaction = Transaction;
//                this.Command.CommandType = ctype;
//                int result = this.Command.ExecuteNonQuery();
//                return result;
//            }
//            catch (Exception ex)
//            {
                
//                throw new Exception(ex.Message);
//            }
//        }

//        public DataTable ExecuteQuery()
//        {
//            DataTable dtResult = new DataTable();
//            dtResult.Load(this.ExecuteReader());

//            if (!this.reader.IsClosed)
//            {
//                reader.Close();
//            }

//            return dtResult;
//        }

//        public void AddParameter(String parameter, Object value)
//        {
//            this.Command.Parameters.AddWithValue(parameter, value);
//        }

//        public void BeginTransaction()
//        {
//            this.Transaction = this.Mcon.BeginTransaction(IsolationLevel.ReadUncommitted);
//        }

//        public void CommitTransaction()
//        {
//            this.Transaction.Commit();
//        }

//        public void RollbackTransaction()
//        {
//            this.Transaction.Rollback();
//        }
    }
}