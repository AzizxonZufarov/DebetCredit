using DebetCredit.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DebetCredit.Services
{
    public class BalanceService
    {
        private readonly string connectionString = @"Data Source = ACER\SQLEXPRESS; Initial Catalog = DebetCreditdb; Integrated Security=True";

        public void Add(BalanceModel model)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var query = "INSERT INTO [dbo].[Table_2] VALUES (@Date, @Inout, @Category, @Summ, @Comment);";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Date", model.Date);
                    command.Parameters.AddWithValue("@Inout", model.Inout);
                    command.Parameters.AddWithValue("@Category", model.Category);
                    command.Parameters.AddWithValue("@Summ", model.Summ);
                    command.Parameters.AddWithValue("@Comment", model.Comment);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public BalanceModel GetById(int id)
        {
            return GetAll(id: id).FirstOrDefault();
        }

        public void Delete(int id)
        {
            var query = "DELETE FROM Table_2 WHere ID = @ID";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public void Update(int id, BalanceModel model)
        {
            var query = "UPDATE [dbo].[Table_2] SET Date = @Date, Inout = @Inout, Category = @Category, Summ = @Summ, Comment = @Comment WHERE ID = @ID";

            using (var  connection = new SqlConnection(connectionString))
            {
                connection.Open();
                
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@Date", model.Date);
                    command.Parameters.AddWithValue("@Inout", model.Inout);
                    command.Parameters.AddWithValue("@Category", model.Category);
                    command.Parameters.AddWithValue("@Summ", model.Summ);
                    command.Parameters.AddWithValue("@Comment", model.Comment);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }



        public BalanceModel[] GetAll(string category = null, int? id = null)
        {
            var table = new DataTable();

            var sql = "SELECT * FROM [dbo].[Table_2]";

            if (category != null)
            {
                sql += $" WHERE Category = '{category}';";
            }

            if (id != null)
            {
                sql += $" WHERE ID = {id};";
            }

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // todo: 
                using (var adapter = new SqlDataAdapter(sql, connection))
                {
                    adapter.Fill(table);
                }
            }

            var i = 0;
            var array = new BalanceModel[table.Rows.Count];

            foreach (var t in table.Rows)
            {
                var row = t as DataRow;

                array[i] = new BalanceModel
                {
                    ID = row.Field<int>("ID"),
                    Date = row.Field<DateTime>("Date"),
                    Inout = row.Field<string>("Inout"),
                    Category = row.Field<string>("Category"),
                    Summ = row.Field<int>("Summ"),
                    Comment = row.Field<string>("Comment")
                };

                i++;
            }

            return array;
        }
    }
}