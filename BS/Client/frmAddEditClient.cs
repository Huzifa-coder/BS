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
    public partial class frmAddEditClient : Form
    {
        public delegate void DatabackEventHandler(object sender, int ClientID);

        public event DatabackEventHandler DataBack;

        enum enMode { Add, Edit }
        enMode _MODE = enMode.Add;

        int _ClientID = -1;
        clsClient _Client;

        public frmAddEditClient()
        {
            InitializeComponent();

            _MODE = enMode.Add;
        }

        public frmAddEditClient(int ClientID)
        {
            InitializeComponent();

            _ClientID = ClientID;
            _MODE = enMode.Add;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (tbFirstName.Text.Equals(""))
            {
                e.Cancel = true;
                tbFirstName.Focus();
                errorProvider1.SetError(tbFirstName, "Please Enter Your First Name");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbFirstName, "");
            }
        }

        private void tbLastName_Validating(object sender, CancelEventArgs e)
        {
            if (tbLastName.Text.Equals(""))
            {
                e.Cancel = true;
                tbLastName.Focus();
                errorProvider1.SetError(tbLastName, "Please Enter Your Last Name");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbLastName, "");
            }
        }

        private void tbPhone_Validating(object sender, CancelEventArgs e)
        {
            if (tbPhone.Text.Equals(""))
            {
                e.Cancel = true;
                tbPhone.Focus();
                errorProvider1.SetError(tbPhone, "Please Enter Your Phone Number");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbPhone, "");
            }

            if (clsClient.IsExist(tbPhone.Text))
            {
                e.Cancel = true;
                tbPhone.Focus();
                errorProvider1.SetError(tbPhone, "This Number Is Already Regiseter in The System");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbPhone, "");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please Fill Empty Fields.");
                return;
            }

            _Client.PersonInfo.FirstName = tbFirstName.Text.Trim();
            _Client.PersonInfo.LastName = tbLastName.Text.Trim();
            _Client.Phone = tbPhone.Text.Trim();

            if (!_Client.PersonInfo.Save())
            {
                MessageBox.Show("Failed To Save Person Info.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _Client.PersonID = _Client.PersonInfo.PersonID;
            lbClientID.Text = _Client.PersonInfo.PersonID.ToString();

            if (!_Client.Save())
            {
                MessageBox.Show("Failed To Save Client Info.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Client Info Saved Successfully.", "Complated", MessageBoxButtons.OK, MessageBoxIcon.Information);

            DataBack?.Invoke(this, _Client.ClientID);

            this.Close();
        }

        private void frmAddEditClient_Load(object sender, EventArgs e)
        {
            ResetDefualtValues();

            if(_MODE == enMode.Edit)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            _Client = clsClient.Find(_ClientID);

            if (_Client == null)
            {
                MessageBox.Show($"There Are No Client With ID : {_ClientID}");
                return;
            }

            lbClientID.Text = _Client.ClientID.ToString();
            tbFirstName.Text = _Client.PersonInfo.FirstName;
            tbLastName.Text = _Client.PersonInfo.LastName;
            tbPhone.Text = _Client.Phone;

        }

        private void ResetDefualtValues()
        {
            _Client = new clsClient();

            lbClientID.Text = "N/A";
            tbFirstName.Text = string.Empty;
            tbLastName.Text = string.Empty;
            tbPhone.Text = string.Empty;

            lbTitle.Text = this.Text = (_MODE == enMode.Add) ? "Add New Client" : "Update Client Info";
        }

        private void tbPhone_KeyPress(object sender, KeyPressEventArgs e)
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

    }
}
