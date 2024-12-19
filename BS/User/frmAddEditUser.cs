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
    public partial class frmAddEditUser : Form
    {
        enum enMode { Add, Edit }
        enMode _MODE = enMode.Add;

        clsUser _User;
        string _UserID = string.Empty;

        public frmAddEditUser()
        {
            InitializeComponent();

            _MODE = enMode.Add;
        }

        public frmAddEditUser(string UserID)
        {
            InitializeComponent();

            _UserID = UserID;

            _MODE = enMode.Edit;
        }

        private void tbUserID_Validating(object sender, CancelEventArgs e)
        {
            if (tbUserID.Text.Equals(""))
            {
                e.Cancel = true;
                tbUserID.Focus();
                errorProvider1.SetError(tbUserID, "Please Enter Your User ID.");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbUserID, "");
            }

            if (clsUser.IsExist(tbUserID.Text) && _MODE == enMode.Add) {

                e.Cancel = true; 
                tbUserID.Focus();
                errorProvider1.SetError(tbUserID, "This ID Is Already Used, Please Choice Another One.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbUserID, "");
            }

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
                errorProvider1.SetError(tbLastName, "Please Enter Your Last Name.");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbLastName, "");
            }
        }

        private void tbPassword_Validating(object sender, CancelEventArgs e)
        {
            if (tbPassword.Text.Equals(""))
            {
                e.Cancel = true;
                tbPassword.Focus();
                errorProvider1.SetError(tbPassword, "Please  Enter Your Password.");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbPassword, "");
            }
        }

        private void tbConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (tbConfirmPassword.Text.Equals(""))
            {
                e.Cancel = true;
                tbConfirmPassword.Focus();
                errorProvider1.SetError(tbConfirmPassword, "Please  Confirm Your Password.");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbConfirmPassword, "");
            }

            if (tbConfirmPassword.Text.Equals(tbPassword.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(tbConfirmPassword, "");
            }
            else
            {
                e.Cancel = true;
                tbConfirmPassword.Focus();
                errorProvider1.SetError(tbConfirmPassword, "The Password Not Matchs.");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please Fill Empty Fields.");
                return;
            }

            _User.UserID = tbUserID.Text.Trim();
            _User.PersonInfo.FirstName = tbFirstName.Text.Trim();
            _User.PersonInfo.LastName = tbLastName.Text.Trim();
            _User.Password = tbPassword.Text.Trim();
            _User.Permissions = 0; //Untill I came to permissions

            if (!_User.PersonInfo.Save())
            {
                MessageBox.Show("Failed To Save Person Info.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _User.PersonID = _User.PersonInfo.PersonID;

            if (_User.Save())
            {
                MessageBox.Show("Failed To Save User Info.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("User Info Saved Successfully.", "Complated", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if(_MODE == enMode.Edit)
            {
                _LoadData();
            }
        }

        private void _LoadData()
        {
            _User = clsUser.Find(_UserID);

            if(_User == null )
            {
                MessageBox.Show($"There Are No User With ID : {_UserID}");
                return;
            }

            tbUserID.Text = _User.UserID;
            tbFirstName .Text = _User.PersonInfo.FirstName;
            tbLastName .Text = _User.PersonInfo.LastName;
            tbPassword.Text = _User.Password;

            tbUserID.Enabled = false;
        }

        private void _ResetDefualtValues()
        {
            _User = new clsUser();

            tbUserID.Text = string.Empty;
            tbFirstName.Text = string.Empty;    
            tbLastName.Text = string.Empty;
            tbPassword.Text = string.Empty;
            tbConfirmPassword .Text = string.Empty;

            lbTitle.Text = (_MODE == enMode.Add) ? "Add New User" : "Update User Info";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    }
}
