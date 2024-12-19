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

namespace BS.User
{
    public partial class frmUsersList : Form
    {
        DataTable _dtUsersList;

        public frmUsersList()
        {
            InitializeComponent();
        }

        private void frmUsersList_Load(object sender, EventArgs e)
        {
            try
            {
                _dtUsersList = clsUser.GetUsers();

                if (_dtUsersList.Rows.Count > 0)
                {
                    dgvUsers.DataSource = _dtUsersList;
                    lbRecords.Text = dgvUsers.Rows.Count.ToString();
                    cbFilterBy.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string id = dgvUsers.CurrentRow.Cells[0].Value.ToString().Trim();

            frmAddEditUser frmAddEditUser = new frmAddEditUser(id);
            frmAddEditUser.ShowDialog();

            frmUsersList_Load(null, null);
        }

        private void deleteUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string id = dgvUsers.CurrentRow.Cells[0].Value.ToString().Trim();

            if (MessageBox.Show("Are Do You Want To Delete This User : \n" + id, "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) 
            {

                if (clsUser.Delete(id))
                {
                    MessageBox.Show("User Deleted Successfully.");
                    frmUsersList_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Faild To delete This Person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsersList_Load(null, null);
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {

            frmAddEditUser frm = new frmAddEditUser();
            frm.ShowDialog();

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

                case "First Name":
                    filterColumn = "FirstName";

                    break;

                case "Last Name":
                    filterColumn = "LastName";

                    break;

                default:
                    filterColumn = "None";
                    break;
            }

            if (filterColumn == "None" || tbFilterValue.Text.Trim() == "")
            {

                _dtUsersList.DefaultView.RowFilter = "";
                lbRecords.Text = dgvUsers.Rows.Count.ToString();
                return;
            }

            _dtUsersList.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", filterColumn, tbFilterValue.Text.Trim());

            lbRecords.Text = dgvUsers.Rows.Count.ToString();
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

        private void frmUsersList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason == CloseReason.None)
            {
                e.Cancel = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
