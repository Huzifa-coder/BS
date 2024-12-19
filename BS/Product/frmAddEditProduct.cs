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
    public partial class frmAddEditProduct : Form
    {
        enum enMode { Add, Edit }
        enMode _MODE = enMode.Add;

        int _ProductID = -1;
        clsProduct _Product;

        public frmAddEditProduct()
        {
            InitializeComponent();

            _MODE = enMode.Add;
        }

        public frmAddEditProduct(int ProductID)
        {
            InitializeComponent();

            _ProductID = ProductID;
            _MODE = enMode.Edit;
        }

        private void tbProductName_Validating(object sender, CancelEventArgs e)
        {
            if (tbProductName.Text.Equals(""))
            {
                e.Cancel = true;
                tbProductName.Focus();
                errorProvider1.SetError(tbProductName, "Please Enter Your Prouduct Name. ");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbProductName, "");
            }
        }

        private void tbPrice_Validating(object sender, CancelEventArgs e)
        {
            if (tbPrice.Text.Equals(""))
            {
                e.Cancel = true;
                tbPrice.Focus();
                errorProvider1.SetError(tbPrice, "Please Enter Product Price.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbPrice, "");
            }
        }

        private void tbBrand_Validating(object sender, CancelEventArgs e)
        {
            if (tbBrand.Text.Equals(""))
            {
                e.Cancel = true;
                tbBrand.Focus();
                errorProvider1.SetError(tbBrand, "Please Enter Your Prouduct Brand.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbBrand, "");
            }
        }

        private void tbPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void frmAddEditProduct_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if(_MODE == enMode.Edit)
            {
                _LoadData();
            }
        }

        private void _LoadData()
        {
            _Product = clsProduct.Find(_ProductID);

            if(_Product == null)
            {
                MessageBox.Show($"There Are No Product With ID : {_ProductID}");
                return;
            }

            lbProductID.Text = _Product.ProductID.ToString();
            tbProductName.Text = _Product.ProductName;
            tbPrice.Text = _Product.Price.ToString();
            tbBrand.Text = _Product.Brand.ToString();

            cbCategory.SelectedValue = _Product.CategoryInfo.CategoryName;

        }

        private void _ResetDefualtValues()
        {
            _Product = new clsProduct();
            _Product.DateAdded = DateTime.Now;

            _LoadCategoriesToComaboBox();

            lbProductID.Text = "N/A";
            tbProductName.Text = string.Empty;
            tbPrice.Text = string.Empty;
            tbBrand.Text = string.Empty;    

            cbCategory.SelectedIndex = 0;
        }

        private void _LoadCategoriesToComaboBox()
        {
            DataTable dt = clsCategory.GetCategories(); 

            foreach (DataRow dr in dt.Rows)
            {
                cbCategory.Items.Add(dr["CategoryName"].ToString().Trim());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please Fill Empty Fields.");
                return;
            }

            _Product.ProductName = tbProductName.Text.Trim();
            _Product.Price = Convert.ToInt32(tbPrice.Text);
            _Product.Brand = tbBrand.Text.Trim();
            _Product.CategoryID = clsCategory.Find(cbCategory.SelectedItem.ToString()).CategoryId;

            if (_Product.Save())
            {
                lbProductID.Text = _Product.ProductID.ToString();
                MessageBox.Show("Product Info Saved Successfully.", "Complated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed To Save Product Info.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmAddEditProduct_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

    }
}
