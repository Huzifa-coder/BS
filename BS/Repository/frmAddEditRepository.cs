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

namespace BS.Repository
{
    public partial class frmAddEditRepository : Form
    {
        enum enMode { Add, Edit }
        enMode _MODE = enMode.Add;

        int _RepositoryID;
        clsRepository _Repository;

        public frmAddEditRepository()
        {
            InitializeComponent();

            _MODE = enMode.Add;
        }

        public frmAddEditRepository(int RepositoryID)
        {
            InitializeComponent();

            _RepositoryID = RepositoryID;
            _MODE = enMode.Add;
        }

        private void tbQuantity_KeyPress(object sender, KeyPressEventArgs e)
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

        private void tbQuantity_Validating(object sender, CancelEventArgs e)
        {
            if (tbQuantity.Text.Equals(""))
            {
                e.Cancel = true;
                tbQuantity.Focus();
                errorProvider1.SetError(tbQuantity, "Please Enter Your Prouduct Quantity.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbQuantity, "");
            }
        }

        private void frmAddEditRepository_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if(_MODE == enMode.Edit)
            {
                _LoadData();
            }
        }

        private void _LoadData()
        {
            _Repository = clsRepository.Find(_RepositoryID);

            if (_Repository == null)
            {
                MessageBox.Show($"There Are No Repository With ID : {_RepositoryID}");
                return;
            }

            lbRepositoryID.Text = _RepositoryID.ToString();
            tbQuantity.Text = _Repository.Quantity.ToString();
            cbProduct.SelectedValue = _Repository.ProductInfo.ProductName;

        }

        private void _ResetDefualtValues()
        {
            _Repository = new clsRepository();

            _LoadProducts();

            cbProduct.SelectedIndex = 0;
            tbQuantity.Text = string.Empty;
            lbRepositoryID.Text = "N/A";

        }

        private void _LoadProducts()
        {
            DataTable dt = clsProduct.GetProducts();

            foreach (DataRow dr in dt.Rows)
            {
                cbProduct.Items.Add(dr["ProductName"]);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Please Fill Empty Fields.");
                return;
            }

            _Repository.Quantity = Convert.ToInt32(tbQuantity.Text);
            _Repository.ProductID = clsProduct.Find(cbProduct.SelectedItem.ToString()).ProductID;

            if (_Repository.Save())
            {
                lbRepositoryID.Text = _Repository.RepositoryID.ToString();
                MessageBox.Show("Repository Info Saved Successfully.", "Complated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed To Save Repository Info.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
