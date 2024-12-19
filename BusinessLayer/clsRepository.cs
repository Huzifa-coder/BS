using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsRepository
    {
        enum enMode { Add, Update }
        enMode _Mode = enMode.Add;

        public int RepositoryID { get; set; }
        public int ProductID { get; set; }
        public clsProduct ProductInfo { get; set; }
        public int Quantity { get; set; }

        public clsRepository()
        {
            RepositoryID = 0;
            ProductID = 0;
            Quantity = 0;

            ProductInfo = new clsProduct();
            _Mode = enMode.Add;
        }

        public clsRepository(int repositoryID, int productID, int quantity)
        {
            RepositoryID = repositoryID;
            ProductID = productID;
            Quantity = quantity;

            ProductInfo = clsProduct.Find(productID);    
            _Mode = enMode.Update;
        }

        static public clsRepository Find(int RepositoryID)
        {
            int productID = 0;
            int quantity = 0;

            if (clsRepositoryData.GetRepository(RepositoryID, ref productID, ref quantity))
            {
                return new clsRepository(RepositoryID, productID, quantity);
            }

            return null;
        }

        static public bool IsExist(int RepositoryID)
        {
            return clsRepositoryData.IsRepositoryExist(RepositoryID);
        }

        static public bool Delete(int RepositoryID)
        {
            return clsRepositoryData.DeleteRepository(RepositoryID);
        }

        static public DataTable GetRepositories()
        {
            return clsRepositoryData.GetAllRepositories();
        }

        private bool _Add()
        {
            int repositoryID = clsRepositoryData.AddRepository(ProductID, Quantity);

            if (repositoryID > 0)
            {
                this.RepositoryID = repositoryID;
                return true;
            }

            return false;
        }

        private bool _Update()
        {
            return clsRepositoryData.UpdateRepository(RepositoryID, ProductID, Quantity);
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
