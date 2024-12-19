using BS.Client;
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
    public partial class frmAddEditInvoice : Form
    {
        DataTable _dtProductsList;

        enum enMode { Add, Edit }
        enMode _MODE = enMode.Add;

        int _InvoiceID = -1;
        clsInvoice _Invoice;

        public frmAddEditInvoice()
        {
            InitializeComponent();

            _MODE = enMode.Add;
        }

        public frmAddEditInvoice(int InvoiceID)
        {
            InitializeComponent();

            _InvoiceID = InvoiceID;
            _MODE = enMode.Edit;
        }

        private void frmAddEditInvoice_Load(object sender, EventArgs e)
        {
           
            if (_MODE == enMode.Add)
            {
                _CreateNewInvoice();
            }
            else
            {
                _LoadData();
            }

            lbTotal.Text = GetTotalOfProduct().ToString();  
        }

        private void _CreateNewInvoice()
        {
            _Invoice = new clsInvoice();

            _Invoice.CreateDate = DateTime.Now;
            _Invoice.CreatedBy = "User1";//Add it as global 

            if (!_Invoice.Save())
            {
                MessageBox.Show("Error : Not Able To Create New Invoice.", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _InvoiceID = _Invoice.InvoiceID;
            lbInvoiceID.Text = _Invoice.InvoiceID.ToString();
        }

        private void _LoadData()
        {
            _Invoice = clsInvoice.Find(_InvoiceID);

            if (_Invoice == null)
            {
                MessageBox.Show($"There Are No Invoice With ID : {_InvoiceID}");
                return;
            }

            lbInvoiceID.Text = _InvoiceID.ToString();
            
            _dtProductsList = _Invoice.GetProducts();
            if (_dtProductsList.Rows.Count > 0)
            {
                dgvProducts.DataSource = _dtProductsList;
            }

            btnSave.Enabled = true;
            btnClientInfo.Enabled = false;
        }

        private void btnAddNewProduct_Click(object sender, EventArgs e)
        {
            frmAddNewProductToInvoice frm = new frmAddNewProductToInvoice(_InvoiceID);
            frm.ShowDialog();

            _RefreshDataGridView();
        }

        private void _RefreshDataGridView()
        {
            _dtProductsList = _Invoice.GetProducts();

            if (_dtProductsList.Rows.Count > 0)
            {
                dgvProducts.DataSource = _dtProductsList;
            }
            else
            {
                dgvProducts.DataSource = null;
            }

            lbTotal.Text = GetTotalOfProduct().ToString();

        }

        private void frmAddEditInvoice_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason == CloseReason.None)
            {
                e.Cancel = true;
            }
        }

        private int GetTotalOfProduct()
        {
            if (_dtProductsList == null)
                return 0;

            int total = 0;  

            foreach (DataRow dr in _dtProductsList.Rows)
            {
                total += Convert.ToInt32(dr["Total"]);
            }

            return total;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _Invoice.Total = GetTotalOfProduct();

            if (_Invoice.Save())
            {
                MessageBox.Show("Invoice Info Saved Successfully.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Error Invoice Info Not Saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClientInfo(object sender, int ClientID)
        {
            _Invoice.ClientID = ClientID;           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClientInfo_Click(object sender, EventArgs e)
        {
            //get the client info to save to the invoice
            frmClientFilter frm = new frmClientFilter();
            frm.DataBack += ClientInfo;
            frm.ShowDialog();

            btnSave.Enabled = true;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dgvProducts.CurrentRow.Cells["InvoiceProductID"].Value;

            if (MessageBox.Show("Are Do You Want To Delete This Product : \n" + id, "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                if (clsInvoiceProduct.Delete(id))
                {
                    _RefreshDataGridView();

                    _Invoice.Total = GetTotalOfProduct();
                    _Invoice.Save();

                    MessageBox.Show("Product Deleted Successfully.");
                }
                else
                {
                    MessageBox.Show("Faild To delete This Product", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void frmAddEditInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnAddNewProduct.PerformClick();
            }
        }
    }
}
