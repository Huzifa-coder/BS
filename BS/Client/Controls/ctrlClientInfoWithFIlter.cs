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

namespace BS.Client.Controls
{
    public partial class ctrlClientInfoWithFIlter : UserControl
    {
        // Define a custom event handler delegate with parameters
        public event Action<int> OnClientSelected;
        // Create a protected method to raise the event with a parameter
        protected virtual void ClientSelected(int ClientID)
        {
            Action<int> handler = OnClientSelected;
            if (handler != null)
            {
                handler(ClientID); // Raise the event with the parameter
            }
        }

        public clsClient Client { get { return ctrlClientInfo1.Client; } }
        public int ClinetID { get { return ctrlClientInfo1.ClientID; } }


        public ctrlClientInfoWithFIlter()
        {
            InitializeComponent();

            cbFilterBy.SelectedIndex = 0;
        }

        private void tbFilterValue_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please Fill The Empty Fields");
                return; 
            }

            switch(cbFilterBy.SelectedIndex)
            {
                case 0:

                    if (!clsClient.IsExist(Convert.ToInt32(tbFilterValue.Text)))
                    {
                        MessageBox.Show($"There Are No Client With ID : {Convert.ToInt32(tbFilterValue.Text)}");
                        return; 
                    }

                    ctrlClientInfo1.LoadClientInfo(Convert.ToInt32(tbFilterValue.Text));

                    if (OnClientSelected != null)
                        // Raise the event with a parameter
                        OnClientSelected(Convert.ToInt32(tbFilterValue.Text));

                    break;

                case 1:

                    if (!clsClient.IsExist(tbFilterValue.Text.ToString()))
                    {
                        MessageBox.Show($"There Are No Client With Phone Number : {Convert.ToInt32(tbFilterValue.Text)}");
                        return;
                    }

                    ctrlClientInfo1.LoadClientInfo(tbFilterValue.Text.ToString());

                    if (OnClientSelected != null)
                        // Raise the event with a parameter
                        OnClientSelected(Convert.ToInt32(tbFilterValue.Text));

                    break;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddEditClient frm = new frmAddEditClient();
            frm.DataBack += ClientAdded;
            frm.ShowDialog();
        }

        private void ClientAdded(object sender, int ClientID)
        {
            
            ctrlClientInfo1.LoadClientInfo((int)ClientID);

            if (OnClientSelected != null)
                // Raise the event with a parameter
                OnClientSelected(ClientID);
        }

        private void tbFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if (tbFilterValue.Text.Equals(""))
            {
                e.Cancel = true;
                tbFilterValue.Focus();
                errorProvider1.SetError(tbFilterValue, "Please Write Your Value.");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbFilterValue, "");
            }
        }

    }
}
