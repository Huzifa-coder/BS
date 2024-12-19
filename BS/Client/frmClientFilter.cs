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
    public partial class frmClientFilter : Form
    {
        public delegate void DatabackEventHandler(object sender, int ClientID);

        public event DatabackEventHandler DataBack;

        public frmClientFilter()
        {
            InitializeComponent();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {

            if( ctrlClientInfoWithFIlter1.Client != null)
            {
                DataBack?.Invoke(this, ctrlClientInfoWithFIlter1.Client.ClientID);
                this.Close();
            }
            else
            {
                MessageBox.Show("Please Select A Client To Save To The Invoice");
            }
   
        }


    }
}
