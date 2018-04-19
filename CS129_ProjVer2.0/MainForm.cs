using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CS129_ProjVer2._0
{
    public partial class MainForm : Form
    {

        SqlConnection conDataBase = new SqlConnection("Data Source=ANDRE;Initial Catalog=general;Integrated Security=True");
        Database db = new Database();

        public MainForm()
        {
            InitializeComponent();

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }


        
        

        private void MainForm_Load(object sender, EventArgs e)
        {


            addColumns();   
            populateDataGridView();
            

            txtID.Enabled = false;
            txtID.Text = string.Empty;
            txtDesc.Text = string.Empty;
            txtBrand.Text = string.Empty;
            txtShoe.Text = string.Empty;
            txtColor.Text = string.Empty;
            txtQuantity.Text = string.Empty; 

        }

        private void button4_Click(object sender, EventArgs e)
        {


        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }



        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewCell cell = null;

            foreach (DataGridViewCell selectedCell in dataGridView1.SelectedCells)
            {
                cell = selectedCell;
                break;

            }
            try
            {
                if (cell != null)
                {

                    DataGridViewRow row = cell.OwningRow;

                    txtID.Text = row.Cells[0].Value.ToString();
                    txtDesc.Text = row.Cells[1].Value.ToString();
                    txtBrand.Text = row.Cells[2].Value.ToString();
                    txtShoe.Text = row.Cells[3].Value.ToString();
                    txtColor.Text = row.Cells[4].Value.ToString();
                    txtQuantity.Text = row.Cells[5].Value.ToString();

                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            
           
          
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtID.Text = string.Empty;
            txtDesc.Text = string.Empty;
            txtBrand.Text = string.Empty;
            txtShoe.Text = string.Empty;
            txtColor.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtID.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(txtID.Text);
            int sSize = Int32.Parse(txtShoe.Text);
            int quantity = Int32.Parse(txtQuantity.Text);
            string description = txtDesc.Text;
            string brand = txtBrand.Text;
            string color = txtColor.Text;
            Inventory itemForUpdate = new Inventory(id, sSize,quantity,description,brand,color);
            MessageBox.Show(db.updateItem(itemForUpdate));
            populateDataGridView();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            List<Inventory> inventoryList = new List<Inventory>();
            foreach(Inventory inv in inventoryList)
            {
                dataGridView1.Rows.Add(new object[] { inv.IdNo, inv.Description, inv.Brand, inv.SSize, inv.Color, inv.Quantity});
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            MessageBox.Show(db.deleteItem(Int32.Parse(txtID.Text)));
            populateDataGridView();
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void searchInventory(object sender, KeyPressEventArgs e)
        {

        }

        private string parseColumnText(string column)
        {
            if(column == null)
            {
                return null;
            }
            switch (column)
            {
                case "ID": return "id";
                case "Description": return "description";
                case "Brand": return "brand";
                case "Shoe Size": return "shoesize";
                case "Color": return "color";
                case "Quantity": return "quantity";
                
            }
            return null;
        }

        private void addColumns()
        {
            dataGridView1.Columns.Add("1", "id");
            dataGridView1.Columns.Add("2", "description");
            dataGridView1.Columns.Add("3", "brand");
            dataGridView1.Columns.Add("4", "shoesize");
            dataGridView1.Columns.Add("5", "color");
            dataGridView1.Columns.Add("6", "quantity");
        }

        private void search(object sender, EventArgs e)
        {
            //keypress event
            dataGridView1.Rows.Clear();
            string column = "";
            if (txtSearch.Text == "")
            {
                populateDataGridView();
                return;
            }
            /*
            if(cmbColumn.SelectedItem == null)
            {
               
                MessageBox.Show("Specify search by field");
                return;
            }
            */
            
            if(cmbColumn.SelectedItem == null)
            {
                column = parseColumnText(null);
            }
            else
            {
                column = parseColumnText(cmbColumn.SelectedItem.ToString());
            }
            
            string pattern = txtSearch.Text;
            Console.WriteLine("Search text: " + pattern);
            List<Inventory> searchResults = db.searchDatabase(column, pattern);
            

            foreach(Inventory inv in searchResults)
            {
                dataGridView1.Rows.Add(new object[] {inv.IdNo, inv.Description, inv.Brand, inv.SSize, inv.Color, inv.Quantity});
            }

        }

        private void populateDataGridView()
        {
            dataGridView1.Rows.Clear();
            List<Inventory> inventoryList = db.GetInventory();
            foreach(Inventory inv in inventoryList)
            {
                dataGridView1.Rows.Add(new object[] {inv.IdNo, inv.Description, inv.Brand, inv.SSize, inv.Color, inv.Quantity});
            }
        }
        //other more straightforward approach which is to bind a datasource:
        private void populateDataGridViewVer2()
        {

            SqlCommand cmdDataBase = new SqlCommand(" select * from Inventory order by id", conDataBase);
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSource = new BindingSource();
                bSource.DataSource = dbdataset;
                dataGridView1.DataSource = bSource;
                sda.Update(dbdataset);
                dataGridView1.ClearSelection();


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
 
        private void clearFields()
        {
            txtDesc.Text = string.Empty;
            txtBrand.Text = string.Empty;
            txtShoe.Text = string.Empty;
            txtColor.Text = string.Empty;
            txtQuantity.Text = string.Empty;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {

            try
            {

                MessageBox.Show(db.AddItem(txtDesc.Text, txtBrand.Text, Int32.Parse(txtShoe.Text), txtColor.Text, Int32.Parse(txtQuantity.Text)));
                clearFields();
                populateDataGridView();

            }

            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }





    }
}

