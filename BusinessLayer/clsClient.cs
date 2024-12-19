using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsClient
    {
        enum enMode { Add, Update }
        enMode _Mode = enMode.Add;

        public int ClientID { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo { get; set; }
        public string Phone { get; set; }

        public clsClient()
        {
            ClientID = 0;
            PersonID = 0;
            Phone = string.Empty;

            PersonInfo = new clsPerson();
            _Mode = enMode.Add;
        }

        public clsClient(int clientID, int personID, string phone)
        {
            ClientID = clientID;
            PersonID = personID;
            Phone = phone;

            PersonInfo = clsPerson.Find(personID);
            _Mode = enMode.Update;
        }

        static public clsClient Find(int ClientID)
        {
            int personID = 0;
            string phone = string.Empty;

            if (clsClientData.GetClient(ClientID, ref personID, ref phone))
            {
                return new clsClient(ClientID, personID, phone);
            }

            return null;
        }

        static public clsClient Find(string phone)
        {
            int personID = 0;
            int clientID = 0;

            if (clsClientData.GetClient(phone, ref clientID, ref personID))
            {
                return new clsClient(clientID, personID, phone);
            }

            return null;
        }

        static public bool IsExist(int ClientID)
        {
            return clsClientData.IsClientExist(ClientID);
        }

        static public bool IsExistByPersonID(int PersonID)
        {
            return clsClientData.IsClientExistByPersonID(PersonID);
        }

        static public bool Delete(int ClientID)
        {
            return clsClientData.DeleteClient(ClientID);
        }

        static public DataTable GetClients()
        {
            return clsClientData.GetAllClients();
        }

        private bool _Add()
        {
            int clientID = clsClientData.AddClient(PersonID, Phone);

            if (clientID > 0)
            {
                this.ClientID = clientID;
                return true;
            }

            return false;
        }

        private bool _Update()
        {
            return clsClientData.UpdateClient(ClientID, PersonID, Phone);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.Add:
                    if (_Add())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _Update();
            }

            return false;
        }

        public static bool IsExist(string Phone)
        {
            return clsClientData.IsClientExist(Phone);
        }
    }

}
