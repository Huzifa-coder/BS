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

namespace BS.Category
{
    public partial class frmAddEditCategory : Form
    {
        enum enMode { Add, Edit }
        enMode _MODE = enMode.Add;

        int _CategoryID = -1;
        clsCategory _Category;

        public frmAddEditCategory()
        {
            InitializeComponent();

            _MODE = enMode.Add;
        }

        public frmAddEditCategory(int CategoryID)
        {
            InitializeComponent();

            _MODE = enMode.Edit;
            _CategoryID = CategoryID;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddEditCategory_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if(_MODE == enMode.Edit)
            {
                _LoadData();
            }
        }

        private void _LoadData()
        {
            _Category = clsCategory.Find(_CategoryID);

            if (_Category == null)
            {
                MessageBox.Show($"There Are No Category With ID : {_CategoryID}");
                return;
            }

            lbCategoryID.Text = _CategoryID.ToString();
            tbName.Text = _Category.CategoryName;

        }

        private void _ResetDefualtValues()
        {
            _Category = new clsCategory();

            lbCategoryID.Text = "N/A";
            tbName.Text =string.Empty;
            
            lbTitle.Text = this.Text = (_MODE == enMode.Add) ? "Add New Category" : "Update Category Info";
        }

        private void tbName_Validating(object sender, CancelEventArgs e)
        {
            if (tbName.Text.Equals(""))
            {
                e.Cancel = true;
                tbName.Focus();
                errorProvider1.SetError(tbName, "Please Enter A Name For The Category");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbName, "");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please Fill Empty Fields.");
                return;
            }

            _Category.CategoryName = tbName.Text.Trim();

            if (_Category.Save())
            {
                lbCategoryID.Text = _Category.CategoryId.ToString();
                MessageBox.Show("Category Info Saved Successfully.", "Complated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed To Save Category Info.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
