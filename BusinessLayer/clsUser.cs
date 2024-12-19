using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsUser
    {
        enum enMode { Add, Update }
        enMode _Mode = enMode.Add;

        public string UserID { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo { get; set; }

        public string Password { get; set; }
        public byte Permissions { get; set; }

        public clsUser()
        {
            UserID = string.Empty;
            PersonID = 0;
            Password = string.Empty;
            Permissions = 0;

            PersonInfo = new clsPerson();

            _Mode = enMode.Add;
        }

        public clsUser(string userID, int personID, string password, byte permissions)
        {
            UserID = userID;
            PersonID = personID;
            Password = password;
            Permissions = permissions;

            PersonInfo = clsPerson.Find(personID);

            _Mode = enMode.Update;
        }

        static public clsUser Find(string UserID)
        {
            int personID = 0;
            string password = string.Empty;
            byte permissions = 0;

            if (clsUserData.GetUser(UserID, ref personID, ref password, ref permissions))
            {
                return new clsUser(UserID, personID, password, permissions);
            }

            return null;
        }

        static public bool IsExist(string UserID)
        {
            return clsUserData.IsUserExist(UserID);
        }

        static public bool Delete(string UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }

        static public DataTable GetUsers()
        {
            return clsUserData.GetAllUsers();
        }

        private bool _Add()
        {
            bool isAdded = clsUserData.AddUser(UserID, PersonID, Password, Permissions);

            if (isAdded)
            {
                return true;
            }

            return false;
        }

        private bool _Update()
        {
            return clsUserData.UpdateUser(UserID, PersonID, Password, Permissions);
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
    }

}
