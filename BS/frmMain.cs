using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BS.Category;
using BS.Client;
using BS.Invoice;
using BS.Product;
using BS.Repository;
using BS.User;
using BusinessLayer;

namespace BS
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            clsPerson   p = new clsPerson();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsersList frm = new frmUsersList();
            frm.ShowDialog();
        }

        private void clientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClientsList frm = new frmClientsList();
            frm.ShowDialog();
        }

        private void categoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCategoriesList frm = new frmCategoriesList();
            frm.ShowDialog();
        }

        private void prosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProductsList frm = new frmProductsList();
            frm.ShowDialog();
        }

        private void repositoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRepositoriesList frm = new frmRepositoriesList();
            frm.ShowDialog();
        }

        private void invoicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInvoicesList frm = new frmInvoicesList();
            frm.ShowDialog();
        }

    }
}
