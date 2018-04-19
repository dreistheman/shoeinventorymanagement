using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Sql;

namespace CS129_ProjVer2._0
{
    public class Database
    {
        SqlConnection conn;

        public Database()
        {
            conn = new SqlConnection("Data Source=ANDRE;Initial Catalog=general;Integrated Security=True");
        }

        public SqlConnection GetConnection()
        {
            string connString = "Data Source=ANDRE;Initial Catalog=general;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connString);
            
            return conn;
        }
        

        public  List<Inventory> searchDatabase(string column, string pattern)
        {
            //List<Dictionary<string,string>> inventoryMap = new List<Dictionary<string,string>>();
            List<Inventory> searchResults = new List<Inventory>();
            List<String> columns = getColumns();
            
            string selectStmt = "";
            if(column == null)
            {
                selectStmt += "SELECT * FROM Inventory WHERE ";
                for(int i=0; i<columns.Count; i++)
                {
                    if (i == columns.Count - 1)
                    {
                        selectStmt += "(" + columns[i] + " LIKE '" + pattern + "%')";
                        break;
                    }
                    selectStmt += "(" + columns[i] + " LIKE '" + pattern + "%') OR "; 
                }
                Console.WriteLine(selectStmt);
            }
            else
            {
                selectStmt = "SELECT * FROM Inventory WHERE " + column + " LIKE '" + pattern + "%'";
            }
            
            SqlCommand selectCommand = new SqlCommand(selectStmt, conn);
            

            try
            {
                conn.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                while (reader.Read())
                {

                    int idNo = Convert.ToInt32(reader["id"]);
                    int sSize = Convert.ToInt32(reader["shoesize"]);
                    int quantity = Convert.ToInt32(reader["quantity"]);
                    string description = reader["description"].ToString();
                    string brand = reader["brand"].ToString();
                    string color = reader["color"].ToString();

                    searchResults.Add(new Inventory(idNo,sSize,quantity,description,brand,color));
                    /*
                    Dictionary<string, string> inventory = new Dictionary<string, string>();
                    inventory.Add("id", idNo.ToString());
                    inventory.Add("shoesize", sSize.ToString());
                    inventory.Add("quantity", quantity.ToString());
                    inventory.Add("description", description);
                    inventory.Add("brand", brand);
                    inventory.Add("color", color);
                    inventoryMap.Add(inventory);
                    */
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return searchResults;
        }


         
        public String AddItem(string description, string brand, int ssize, string color, int quantity)
        {
           
            string insStmt = "INSERT INTO Inventory (description, brand, shoesize, color, quantity) VALUES (@description, @brand, @sSize, @color, @quantity)";
            
            SqlCommand insCmd = new SqlCommand(insStmt, conn);
            insCmd.Parameters.AddWithValue("@description", description);
            insCmd.Parameters.AddWithValue("@brand", brand);
            insCmd.Parameters.AddWithValue("@sSize", ssize);
            insCmd.Parameters.AddWithValue("@color", color);
            insCmd.Parameters.AddWithValue("@quantity", quantity);
            try
            {
                conn.Open();
                insCmd.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                conn.Close();
                return ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return "Shoe added!";

          
        }



        public List<Inventory> GetInventory()
        {

            List<Inventory> inventoryList = new List<Inventory>();
            string selStmt = "SELECT * FROM Inventory ORDER BY id";
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
                    inv.SSize = Convert.ToInt32(reader["shoesize"]);
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
 


        public String deleteItem(int id)
        {
            string deleteStmt = "DELETE FROM Inventory WHERE id = " + id;
            SqlCommand deleteCommand = new SqlCommand(deleteStmt, conn);
            try
            {
                conn.Open();
                deleteCommand.ExecuteNonQuery();
            }catch(Exception ex)
            {
                conn.Close();
                return ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return "Delete successful!";
        }   

        public String updateItem(Inventory item)
        {
            string updateStmt = "UPDATE Inventory SET description=@desc, brand=@br, shoesize=@ssize, color=@c, quantity=@q WHERE id=@id";
            SqlCommand updateCommand = new SqlCommand(updateStmt, conn);
            updateCommand.Parameters.AddWithValue("@id", item.IdNo);
            updateCommand.Parameters.AddWithValue("@desc", item.Description);
            updateCommand.Parameters.AddWithValue("@br", item.Brand);
            updateCommand.Parameters.AddWithValue("@ssize", item.SSize);
            updateCommand.Parameters.AddWithValue("@c", item.Color);
            updateCommand.Parameters.AddWithValue("@q", item.Quantity);
            try
            {
                conn.Open();
                updateCommand.ExecuteNonQuery();
                
            }catch(Exception ex)
            {
                conn.Close();
                return ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return "Update successful!";
        }
        
        public List<String> getColumns()
        {
            List<String> columns = new List<String>();
            DataTable columnsTable;
            SqlDataReader reader;
            String selectStmt = "SELECT c.name from sys.columns c inner join sys.tables t on t.object_id = c.object_id and t.name = 'Inventory' and t.type = 'U'";
            SqlCommand cmd = new SqlCommand(selectStmt, conn);
            try
            {
                conn.Open();
                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        columns.Add(reader.GetString(0));
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            Console.WriteLine(String.Join("|", columns.ToArray()));
            return columns;
        }


                              
        }
    }

