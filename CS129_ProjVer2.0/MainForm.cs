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
    

        public MainForm()
        {
            InitializeComponent();

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }


        //string conString = "Data Source=.\\SQLEXPRESS;AttachDbFilename=C:\\Users\\YOGI\\Documents\\Visual Studio 2010\\Projects\\CS129_ProjVer2.0\\CS129_ProjVer2.0\\Inventory.mdf;Integrated Security=True;User Instance=True";
        

        private void MainForm_Load(object sender, EventArgs e)
        {

            txtID.Enabled = false;
            SqlCommand cmdDataBase = new SqlCommand(" select * from Inventory ;", conDataBase);
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


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

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

            try
            {
                //Applied Design Pattern (Private Class Data)
                Database.AddItem(txtDesc.Text, txtBrand.Text, Int32.Parse(txtShoe.Text), txtColor.Text, Int32.Parse(txtQuantity.Text));
                
                txtDesc.Text = "";
                txtBrand.Text = "";
                txtShoe.Text = string.Empty;
                txtColor.Text = "";
                txtQuantity.Text = string.Empty;
                this.MainForm_Load(this, null);
                MessageBox.Show("Item successfully added!");
            }

            catch (Exception ex) { MessageBox.Show(ex.Message); }

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

            if (cell!=null)
            {
                DataGridViewRow row = cell.OwningRow;
                txtID.Text = row.Cells[0].Value.ToString();
                txtDesc.Text = row.Cells[1].Value.ToString();
                txtBrand.Text = row.Cells[2].Value.ToString();
                txtShoe.Text = row.Cells[3].Value.ToString();
                txtColor.Text = row.Cells[4].Value.ToString();
                txtQuantity.Text = row.Cells[5].Value.ToString();

            }

            ;
           
          
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
            try
            {
                conDataBase.Open();
                SqlDataAdapter SDA = new SqlDataAdapter("Update Inventory set description = '" + txtDesc.Text + "',brand = '" + txtBrand.Text + "',shoesize = '" + txtShoe.Text + "',color = '" + txtColor.Text + "',quantity = '" + txtQuantity.Text + "' WHERE id = "+txtID.Text+ "", conDataBase);
                SDA.SelectCommand.ExecuteNonQuery();
                conDataBase.Close();
                this.MainForm_Load(this, null);
                MessageBox.Show("Update successful!");

                
 
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            SqlCommand cmdDataBase = new SqlCommand(" select * from Inventory ;", conDataBase);
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


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                conDataBase.Open();
                SqlDataAdapter SDA = new SqlDataAdapter("Delete from Inventory Where id = '" + txtID.Text + "'", conDataBase);
                SDA.SelectCommand.ExecuteNonQuery();
                conDataBase.Close();
                this.MainForm_Load(this, null);
                MessageBox.Show("Delete successful!");



            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

     

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        /*private void textBox1_TextChanged(object sender, EventArgs e)
        {

            SqlConnection connect = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename=D:\\My Documents\\Visual Studio 2010\\Projects\\CS129_ProjVer2.0\\CS129_ProjVer2.0\\Inventory.mdf;Integrated Security=True;User Instance=True");

            if (cmbSearch.Text=="ID Number")
            {
                SqlDataAdapter SDA = new SqlDataAdapter("Select IDNumber, Description, Brand, [Shoe Size], Color, Quantity FROM Inventory WHERE IDNumber LIKE '" + txtSearch + "%'", connect);
                DataTable data = new DataTable();
                SDA.Fill(data);
                dataGridView1.DataSource = data;
            }
            else if (cmbSearch.Text == "Description")
            {
                SqlDataAdapter SDA = new SqlDataAdapter("Select IDNumber, Description, Brand, [Shoe Size], Color, Quantity FROM Inventory WHERE Description LIKE '" + txtSearch + "%'", connect);
                DataTable data = new DataTable();
                SDA.Fill(data);
                dataGridView1.DataSource = data;
 
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
            if (cmbSearch.Text == "ID Number")
            {

                SqlConnection connectu = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename=C:\\Users\\YOGI\\Documents\\Visual Studio 2010\\Projects\\CS129_ProjVer2.0\\CS129_ProjVer2.0\\Inventory.mdf;Integrated Security=True;User Instance=True");
                SqlDataAdapter SDA = new SqlDataAdapter("Select IDNumber, Description, Brand, [Shoe Size], Color, Quantity FROM Inventory WHERE IDNumber LIKE '" + txtSearch + "%'", connectu);
                DataTable data = new DataTable();
                SDA.Fill(data);
                dataGridView1.DataSource = data;
            }
            else if (cmbSearch.Text == "Description")
            {
                SqlConnection connectu = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename=C:\\Users\\YOGI\\Documents\\Visual Studio 2010\\Projects\\CS129_ProjVer2.0\\CS129_ProjVer2.0\\Inventory.mdf;Integrated Security=True;User Instance=True");
                SqlDataAdapter SDA = new SqlDataAdapter("Select IDNumber, Description, Brand, [Shoe Size], Color, Quantity FROM Inventory WHERE Description LIKE '" + txtSearch + "%'", connectu);
                DataTable data = new DataTable();
                SDA.Fill(data);
                dataGridView1.DataSource = data;

            }
        }*/

      

    }
}

