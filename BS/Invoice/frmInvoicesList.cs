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

namespace BS.Invoice
{
    public partial class frmInvoicesList : Form
    {
        DataTable _dtInvoicesList;

        public frmInvoicesList()
        {
            InitializeComponent();
        }

        private void frmInvoicesList_Load(object sender, EventArgs e)
        {
            _dtInvoicesList = clsInvoice.GetInvoices();

            if( _dtInvoicesList.Rows.Count > 0 )
            {
                cbFilterBy.SelectedIndex = 0;
                dgvInvoices.DataSource = _dtInvoicesList;
                lbRecords.Text = dgvInvoices.Rows.Count.ToString();
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
            if (dgvInvoices.RowCount <= 0)
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

                _dtInvoicesList.DefaultView.RowFilter = "";
                lbRecords.Text = dgvInvoices.Rows.Count.ToString();
                return;
            }

            _dtInvoicesList.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", filterColumn, tbFilterValue.Text.Trim());

            lbRecords.Text = dgvInvoices.Rows.Count.ToString();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dgvInvoices.CurrentRow.Cells["InvoiceID"].Value;

            if (MessageBox.Show("Are Do You Want To Delete This Invoice : \n" + id, "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                if (clsInvoice.Delete(id))
                {
                    MessageBox.Show("Invoice Deleted Successfully.");
                    frmInvoicesList_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Faild To delete This Invoice", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInvoicesList_Load(null, null);
        }

        private void frmInvoicesList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason == CloseReason.None)
            {
                e.Cancel = true;
            }
        }

        private void btnAddNewInvoice_Click(object sender, EventArgs e)
        {
            frmAddEditInvoice frm = new frmAddEditInvoice();
            frm.ShowDialog();

            frmInvoicesList_Load(null, null);
        }

        private void editInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dgvInvoices.CurrentRow.Cells["InvoiceID"].Value;

            frmAddEditInvoice frm = new frmAddEditInvoice(id);
            frm.ShowDialog();

            frmInvoicesList_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
