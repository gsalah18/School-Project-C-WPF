using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;

namespace School_Project
{
    class SqlConnectionDB
    {
        private static MySqlCommand command = null;
        public static MySqlConnection connection = null;
        public String myCurrent_Conn;
       


        public SqlConnectionDB()
        {
            try {
                connection = new MySqlConnection("server=localhost;user=root;database=school;password=;");
                myCurrent_Conn = connection.ConnectionString;
                connection.Open();
                command = new MySqlCommand("SET NAMES utf8;", connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (MySqlException)
            {
                MessageBox.Show("Check The Internet Connection");
            }
        }
        public void selectDB(ref System.Data.DataSet dataset, String SelectStatement)
        {
            try
            {
                command = new MySqlCommand();
                command.Connection = SqlConnectionDB.connection;
                command.CommandText = SelectStatement;
                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(dataset);
                //MessageBox.Show("Correct Selection");
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e+"");
                connection.Close();
            }
        }// end of selectDB method

        public Boolean insertDB(String InsertStatement)
        {
            try
            {
                command = new MySqlCommand();
                command.Connection = SqlConnectionDB.connection;
                connection.Open();
                command.CommandText = InsertStatement;
                command.ExecuteNonQuery();
                connection.Close();
                //MessageBox.Show("Correct Insertion");
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e+"");
                connection.Close();
                return false;
            }
        } // end of insertDB

        public Boolean updateDB(String UpdateStatement)
        {
            try
            {
                command = new MySqlCommand();
                command.Connection = SqlConnectionDB.connection;
                connection.Open();
                command.CommandText = UpdateStatement;
                command.ExecuteNonQuery();
                connection.Close();
                //MessageBox.Show("Correct Updation");
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e+"");
                connection.Close();
                return false;
            }
        } // end of updateDB

        public Boolean deleteDB(String DeleteStatement)
        {
            try
            {
                command = new MySqlCommand();
                command.Connection = SqlConnectionDB.connection;
                connection.Open();
                command.CommandText = DeleteStatement;
                command.ExecuteNonQuery();
                connection.Close();
                //MessageBox.Show("Correct Deletion");
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Not Deleted");
                connection.Close();
                return false;
            }
        } // end of deleteDB
        public bool Login(string query)
        {
            connection.Open();
            command = new MySqlCommand(query, connection);
            MySqlDataReader reader= command.ExecuteReader();
            bool test = reader.HasRows;
            connection.Close();
            return test;
        }
        public LinkedList<string>ListOF(string query)
        {
            LinkedList<string> data = new LinkedList<string>();
            connection.Open();
            command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                data.AddLast(reader.GetString(0));
            connection.Close();
            return data;
        }
        public int SelectID(string query)
        {
            connection.Open();
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            int id = 0;
            if (reader.Read())
                id = reader.GetInt32(0);
            connection.Close();
            return id;
        }
        public bool DoseExists(string query)
        {
            connection.Open();
            command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            bool test = reader.HasRows;
            connection.Close();
            return test;
        }
    }

    
}
