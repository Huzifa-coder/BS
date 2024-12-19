using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer
{
    public class clsPerson
    {
        enum enMode { Add, Update }
        enMode _Mode = enMode.Add;

        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName { get { return FirstName + " " + LastName; } }

        public clsPerson()
        {
            PersonID = 0;
            FirstName = string.Empty;
            LastName = string.Empty;

            _Mode = enMode.Add;
        }

        public clsPerson(int personID, string firstName, string lastName)
        {
            PersonID = personID;
            FirstName = firstName;
            LastName = lastName;

            _Mode = enMode.Update;
        }

        static public clsPerson Find(int PersonID)
        {
            string FirstName = string.Empty;
            string LastName = string.Empty;

            if (clsPersonData.GetPerson(PersonID, ref FirstName, ref LastName))
            {
                return new clsPerson(PersonID, FirstName, LastName);
            }

            return null;
        }

        static public bool IsExist(int PersonID)
        {
            return clsPersonData.IsExist(PersonID);
        }

        static public bool Delete(int PersonID)
        {

            return clsPersonData.DeletePerson(PersonID);
        }

        static public DataTable GetPeople()
        {
            return clsPersonData.GetPeople();
        }

        private bool _Add()
        {
            int PersonID = clsPersonData.AddNewPerson(FirstName, LastName);

            if (PersonID > 0)
            {
                this.PersonID = PersonID;
                return true;
            }

            return false;
        }

        private bool _Update()
        {
            return clsPersonData.UpdatePersonInfo(PersonID, FirstName, LastName);
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
