using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
namespace CS129_ProjVer2._0
{
    public static class Database
    {
        public static SqlConnection GetConnection()
        {
            string connString = "Data Source=ANDRE;Initial Catalog=general;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connString);
            return conn;

            
        }
         
        public static void AddItem(string description, string brand, int ssize, string color, int quantity)
        {
           // Private Class Data (Design Pattern)
            string insStmt = "INSERT INTO Inventory (description, brand, shoesize, color, quantity) VALUES (@description, @brand, @sSize, @color, @quantity)";
            SqlConnection conn = GetConnection();
            SqlCommand insCmd = new SqlCommand(insStmt, conn);
            insCmd.Parameters.AddWithValue("@description", description);
            insCmd.Parameters.AddWithValue("@brand", brand);
            insCmd.Parameters.AddWithValue("@sSize", ssize);
            insCmd.Parameters.AddWithValue("@color", color);
            insCmd.Parameters.AddWithValue("@quantity", quantity);
            try
            {
                conn.Open(); insCmd.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

          
        }



        public static List<Inventory> GetInventory()
        {

            List<Inventory> inventoryList = new List<Inventory>();
            SqlConnection conn = GetConnection();
            string selStmt = "SELECT * FROM Inventory ORDER BY description, brand";
            SqlCommand selCmd = new SqlCommand(selStmt, conn);

            try
            {
                conn.Open();
                SqlDataReader reader = selCmd.ExecuteReader();
                while (reader.Read())
                {
                    Inventory inv = new Inventory();
                    inv.IdNo = Convert.ToInt32(reader["id"]);
                    inv.Description = reader["description"].ToString();
                    inv.Brand = reader["brand"].ToString();
                    inv.SSize = Convert.ToInt32(reader["shoe Size"]);
                    inv.Color = reader["color"].ToString();
                    inv.Quantity = Convert.ToInt32(reader["quantity"]);
                    inventoryList.Add(inv);


                }

                reader.Close();





            }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
            return inventoryList;

            }
 

                
                              
        }
    }

