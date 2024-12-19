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
    public partial class frmAddNewProductToInvoice : Form
    {
        clsInvoiceProduct _InoiveProduct ;

        DataTable _dtProdcuts;

        int _InvoiceID = -1;

        public frmAddNewProductToInvoice(int InvoiceID)
        {
            InitializeComponent();      

            _InvoiceID = InvoiceID;
        }

        private void frmAddNewProductToInvoice_Load(object sender, EventArgs e)
        {
            _dtProdcuts = clsProduct.GetProducts();

            if( _dtProdcuts.Rows.Count > 0 )
            {
                _InoiveProduct = new clsInvoiceProduct();
                dgvProducts.DataSource = _dtProdcuts;
            }
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

        private void dgvProducts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please Choice A Quantity For The Product.");
                return;
            }

            int ProductID = (int)dgvProducts.CurrentRow.Cells[0].Value;

            _InoiveProduct.ProductID = ProductID;
            _InoiveProduct.InvoiceID = _InvoiceID;
            _InoiveProduct.Quantity = Convert.ToInt32(tbQuantity.Text.ToString());
            _InoiveProduct.Total = clsProduct.Find(ProductID).Price * _InoiveProduct.Quantity;

            if (!_InoiveProduct.Save())
            {
                MessageBox.Show("Error : Faild To Add Product To The Invoice");
            }

            this.Close();
        }

        private void tbQuantity_Validating(object sender, CancelEventArgs e)
        {
            if (tbQuantity.Text.Equals(""))
            {
                e.Cancel = true;
                tbQuantity.Focus();
                errorProvider1.SetError(tbQuantity, "Please Enter Your Phone Number");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbQuantity, "");
            }
        }

        private void frmAddNewProductToInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) 
            { 
                return;
            }

            if (!this.ValidateChildren())
            {
                tbQuantity.Focus();
                return;
            }

            int ProductID = (int)dgvProducts.CurrentRow.Cells[0].Value;

            _InoiveProduct.ProductID = ProductID;
            _InoiveProduct.InvoiceID = _InvoiceID;
            _InoiveProduct.Quantity = Convert.ToInt32(tbQuantity.Text.ToString());
            _InoiveProduct.Total = clsProduct.Find(ProductID).Price * _InoiveProduct.Quantity;

            if (!_InoiveProduct.Save())
            {
                MessageBox.Show("Error : Faild To Add Product To The Invoice");
            }

            this.Close();
        }
    }
}
