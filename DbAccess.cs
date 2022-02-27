using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;
using System.Windows.Forms;

namespace C969Scheduler
{
    class DbAccess
    {

        MySqlConnection connection;

        public bool Open()
        {
            try
            {
                connection = new MySqlConnection(Properties.Settings.Default.client_scheduleConnectionString);
                connection.Open();
                return true;
            }
            catch (Exception er)
            {
                MessageBox.Show(Properties.Resources.ConnectionError + er.Message, "Information");
            }
            return false;
        }

        public void Close()
        {
            connection.Close();
            connection.Dispose();
        }

        public DataSet ExecuteData(string sql)
        {
            try
            {
                DataSet dataSet = new DataSet();
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, connection);
                dataAdapter.Fill(dataSet, "appointments");
                return dataSet;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public MySqlDataReader ExecuteReader(string sql)
        {
            try
            {
                MySqlDataReader dataReader;
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                dataReader = cmd.ExecuteReader();
                return dataReader;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public int ExecuteNonQuery(string sql)
        {
            try
            {
                int affected;
                MySqlTransaction myTransaction = connection.BeginTransaction();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                affected = cmd.ExecuteNonQuery();
                myTransaction.Commit();
                return affected;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return -1;
        }

    }
}
