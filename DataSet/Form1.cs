using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DataSet
{
    public partial class Form1 : Form
    {
        System.Data.DataSet ds;
        string ConnectionString = @"Data Source =ACER\SQLEXPRESS; Initial Catalog = userBd; Integrated Security = True";
        public Form1()
        {

            InitializeComponent();
            
            string sql = "Select * From Users";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                ds = new System.Data.DataSet();
                adapter.Fill(ds);
                DataTable dt = ds.Tables[0];
                dataGridView1.DataSource = ds.Tables[0];

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //#region first method
            //string sql = String.Format("Select * From Users"); //Insert INTO Users
            //using (SqlConnection connection = new SqlConnection(ConnectionString))
            //{
            //    connection.Open();
            //    SqlCommand command = new SqlCommand(sql, connection);

            //    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
            //    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
            //    // System.Data.DataSet ds = new System.Data.DataSet();
            //    DataTable dt = ds.Tables[0];
            //    DataRow Row = ds.Tables[0].NewRow();
            //    RandomName a = new RandomName();
            //    Row["Name"] = a.Name;
            //    Row["Age"] = a.Age;
            //    ds.Tables[0].Rows.Add(Row);
            //    adapter.Update(ds);
            //    ds.Clear();
            //    adapter.Fill(ds);
            //    dataGridView1.DataSource = ds.Tables[0];
            //}
            //#endregion first method

            #region second method
             string sql = String.Format("Select * From Users"); //Insert INTO Users
            //string sql = String.Format(" select * from Users.columns");
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                // System.Data.DataSet ds = new System.Data.DataSet();
                // устанавливаем команду на вставку
                adapter.InsertCommand = new SqlCommand("sp_CreateUser", connection);
                // это будет зранимая процедура
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                // добавляем параметр для name
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 50, "Name"));
                // добавляем параметр для age
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@age", SqlDbType.Int, 0, "Age"));
                // добавляем выходной параметр для id
                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");


                System.Type type = typeof(int);

                parameter.Direction = ParameterDirection.Output;
                DataTable dt = ds.Tables[0];
                dataGridView1.DataSource = ds.Tables[0];
                DataRow Row = ds.Tables[0].NewRow();
                RandomName a = new RandomName();
                Row["Name"] = a.Name;
                Row["Age"] = a.Age;
                ds.Tables[0].Rows.Add(Row);
                adapter.Update(ds);
                //    ds.AcceptChanges(); //И после обновления базы данных с помощью метода AcceptChanges()
                //объекта DataSet производится принятие всех изменений в DataSet ко всем измененным строкам.
                // Лично я вообще не понял зачем нужке, ведь Grid и так привзан и обновляется от Dataset
            }
            #endregion second method

            #region third method

            #endregion third method
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ds.AcceptChanges();
        }
    }
}
