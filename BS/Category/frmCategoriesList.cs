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
    public partial class frmCategoriesList : Form
    {
        DataTable _dtCategoriesList;

        public frmCategoriesList()
        {
            InitializeComponent();
        }

        private void frmCategoriesList_Load(object sender, EventArgs e)
        {
            _dtCategoriesList = clsCategory.GetCategories();

            if (_dtCategoriesList.Rows.Count > 0)
            {
                cbFilterBy.SelectedIndex = 0;
                dgvUsers.DataSource = _dtCategoriesList;
                lbRecords.Text = dgvUsers.Rows.Count.ToString();

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
            if (dgvUsers.RowCount <= 0)
                return;

            string filterColumn = "";

            switch ((string)cbFilterBy.SelectedItem)
            {
                case "ID":
                    filterColumn = "UserID";
                    break;

                case "Name":
                    filterColumn = "CategoryName";

                    break;

                default:
                    filterColumn = "None";
                    break;
            }

            if (filterColumn == "None" || tbFilterValue.Text.Trim() == "")
            {

                _dtCategoriesList.DefaultView.RowFilter = "";
                lbRecords.Text = dgvUsers.Rows.Count.ToString();
                return;
            }

            _dtCategoriesList.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", filterColumn, tbFilterValue.Text.Trim());

            lbRecords.Text = dgvUsers.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCategoriesList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason == CloseReason.None)
            {
                e.Cancel = true;
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dgvUsers.CurrentRow.Cells[0].Value;

            if (MessageBox.Show("Are Do You Want To Delete This Category : \n" + id, "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                if (clsCategory.Delete(id))
                {
                    MessageBox.Show("Category Deleted Successfully.");
                    frmCategoriesList_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Category To delete This Person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCategoriesList_Load(null, null);
        }

        private void btnAddNewCategory_Click(object sender, EventArgs e)
        {
            frmAddEditCategory frm = new frmAddEditCategory();
            frm.ShowDialog();

            frmCategoriesList_Load(null, null);
        }

        private void editInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dgvUsers.CurrentRow.Cells[0].Value;

            frmAddEditCategory frm = new frmAddEditCategory(id);
            frm.ShowDialog();

            frmCategoriesList_Load(null, null);
        }

    }
}
