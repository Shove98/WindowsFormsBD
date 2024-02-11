using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;

namespace WindowsFormsBD
{
    internal class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string username;
        private string password;
        private string database;
        private string port;

        public DBConnect() 
        {
            Initialize();
        }

        private void Initialize()
        {
            server = "192.168.1.8";
            username = "programador";
            password = "Qwerty12";
            database = "gestaoformandos";
            port = "3306";
            string connectionString = "Server = " + server + "; Port = " + port + " ; Database = " + database + "; Uid = " + username + "; Pwd = " + password + ";";
            connection = new MySqlConnection(connectionString);
        }


        private bool OpenConnection()
        {
            try
            {
                 connection.Open();
                 return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
         private bool CloseConnection()
         {
             try
             {
                 connection.Close();
                return true;
             }
                 catch (MySqlException ex)
                 {
                    MessageBox.Show(ex.Message);
                    return false;
                 }
         }

        public string StatusConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                else
                {
                    connection.Close();
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return connection.State.ToString();
        }

        public int Count()
        {
                    string query = "select count(*) from formando;";
                    int count = -1;
                    try
                    {
                        if (OpenConnection())
                        {
                            MySqlCommand cmd = new MySqlCommand(query, connection);
                            count = int.Parse(cmd.ExecuteScalar().ToString());
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                    return count;
        }
    }
 }