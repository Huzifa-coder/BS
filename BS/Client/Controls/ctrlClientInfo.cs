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
    public partial class ctrlClientInfo : UserControl
    {
        clsClient _Client;
        int _ClientID;

        public clsClient Client {  get { return _Client; } }
        public int ClientID { get { return _ClientID; } }


        public ctrlClientInfo()
        {
            InitializeComponent();
        }

        public void LoadClientInfo(int ClientID)
        {
            _Client = clsClient.Find(ClientID);

            if (Client == null)
            {
                MessageBox.Show($"There Are No Client With ID : {ClientID}");
                return;
            }

            _ClientID = ClientID;

            lbClientID.Text = ClientID.ToString();
            lbFullName.Text = _Client.PersonInfo.FullName;
            lbPhone.Text = _Client.Phone;


        }

        public void LoadClientInfo(string Phone)
        {
            _Client = clsClient.Find(Phone);

            if (Client == null)
            {
                MessageBox.Show($"There Are No Client With ID : {Phone}");
                return;
            }

            _ClientID = ClientID;

            lbClientID.Text = Client.ClientID.ToString();
            lbFullName.Text = _Client.PersonInfo.FullName;
            lbPhone.Text = _Client.Phone;

        }


    }
}
