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
    public partial class frmRepositoriesList : Form
    {
        DataTable _dtRepostoriesList;

        public frmRepositoriesList()
        {
            InitializeComponent();
        }

        private void frmRepositoriesList_Load(object sender, EventArgs e)
        {
            _dtRepostoriesList = clsRepository.GetRepositories();

            if( _dtRepostoriesList.Rows.Count > 0 )
            {
                cbFilterBy.SelectedIndex = 0;
                dgvRepositories.DataSource = _dtRepostoriesList;
                lbRecords.Text = dgvRepositories.Rows.Count.ToString();
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
            if (dgvRepositories.RowCount <= 0)
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

                _dtRepostoriesList.DefaultView.RowFilter = "";
                lbRecords.Text = dgvRepositories.Rows.Count.ToString();
                return;
            }

            _dtRepostoriesList.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", filterColumn, tbFilterValue.Text.Trim());

            lbRecords.Text = dgvRepositories.Rows.Count.ToString();
        }

        private void editInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dgvRepositories.CurrentRow.Cells[0].Value;

            if (MessageBox.Show("Are Do You Want To Delete This Repository : \n" + id, "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                if (clsRepository.Delete(id))
                {
                    MessageBox.Show("Repository Deleted Successfully.");
                    frmRepositoriesList_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Faild To delete This Repository", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRepositoriesList_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRepositoriesList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason == CloseReason.None)
            {
                e.Cancel = true;
            }
        }

        private void btnAddNewRepository_Click(object sender, EventArgs e)
        {
            frmAddEditRepository frm = new frmAddEditRepository();
            frm.ShowDialog();

            frmRepositoriesList_Load(null, null);
        }
    }
}
