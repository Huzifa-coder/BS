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

namespace BS.Client
{
    public partial class frmClientsList : Form
    {
        DataTable _dtClients;

        public frmClientsList()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmClientsList_Load(object sender, EventArgs e)
        {
            _dtClients = clsClient.GetClients();

            if (_dtClients.Rows.Count > 0)
            {
                dgvClients.DataSource = _dtClients;
                lbRecords.Text = dgvClients.Rows.Count.ToString();
                cbFilterBy.SelectedIndex = 0;
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dgvClients.CurrentRow.Cells[0].Value;

            if (MessageBox.Show("Are Do You Want To Delete This Client : \n" + id, "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                if (clsClient.Delete(id))
                {
                    MessageBox.Show("Client Deleted Successfully.");
                    frmClientsList_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Faild To delete This Client", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClientsList_Load(null, null);
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddEditClient frm = new frmAddEditClient();
            frm.ShowDialog();
            

            frmClientsList_Load(null, null);
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

            if (dgvClients.RowCount <= 0)
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

                _dtClients.DefaultView.RowFilter = "";
                lbRecords.Text = dgvClients.Rows.Count.ToString();
                return;
            }

            _dtClients.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", filterColumn, tbFilterValue.Text.Trim());

            lbRecords.Text = dgvClients.Rows.Count.ToString();
        }

        private void frmClientsList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason == CloseReason.None)
            {
                e.Cancel = true;
            }
        }

        private void editUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dgvClients.CurrentRow.Cells[0].Value;

            frmAddEditClient frm = new frmAddEditClient(id);
            frm.ShowDialog();
        }
    }
}
