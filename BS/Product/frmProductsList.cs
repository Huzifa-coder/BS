using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BS.Product
{
    public partial class frmProductsList : Form
    {
        DataTable _dtProductsList;

        public frmProductsList()
        {
            InitializeComponent();
        }

        private void frmProductsList_Load(object sender, EventArgs e)
        {
            _dtProductsList = clsProduct.GetProducts();

            if( _dtProductsList.Rows.Count > 0 )
            {
                cbFilterBy.SelectedIndex = 0;
                dgvProducts.DataSource = _dtProductsList;
                lbRecords.Text = dgvProducts.Rows.Count.ToString(); 
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbFilterValue.Visible = ((string)cbFilterBy.SelectedItem != "None");

            if (tbFilterValue.Visible)
            {
                tbFilterValue.Text = "";
                tbFilterValue.Focus();
            }
        }

        private void tbFilterValue_TextChanged(object sender, EventArgs e)
        {

            if (dgvProducts.RowCount <= 0)
                return;

            string filterColumn = "";

            switch ((string)cbFilterBy.SelectedItem)
            {
                case "Product Name":
                    filterColumn = "ProductName";
                    break;

                case "Category Name":
                    filterColumn = "CategoryName";

                    break;

                case "Brand":
                    filterColumn = "Brand";

                    break;

                default:
                    filterColumn = "None";
                    break;
            }

            if (filterColumn == "None" || tbFilterValue.Text.Trim() == "")
            {

                _dtProductsList.DefaultView.RowFilter = "";
                lbRecords.Text = dgvProducts.Rows.Count.ToString();
                return;
            }

            _dtProductsList.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", filterColumn, tbFilterValue.Text.Trim());

            lbRecords.Text = dgvProducts.Rows.Count.ToString();
        }

        private void frmProductsList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason == CloseReason.None)
                e.Cancel = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dgvProducts.CurrentRow.Cells[0].Value;

            frmAddEditProduct frm = new frmAddEditProduct(id);
            frm.ShowDialog();

            frmProductsList_Load(null, null);

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dgvProducts.CurrentRow.Cells[0].Value;

            if (MessageBox.Show("Are Do You Want To Delete This Product : \n" + id, "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                if (clsProduct.Delete(id))
                {
                    MessageBox.Show("Product Deleted Successfully.");
                    frmProductsList_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Faild To delete This Product", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProductsList_Load(null, null);
        }

        private void btnAddNewProduct_Click(object sender, EventArgs e)
        {
            frmAddEditProduct frm = new frmAddEditProduct();    
            frm.ShowDialog();

            frmProductsList_Load(null, null);
        }
    }
}
