using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsProduct
    {
        enum enMode { Add, Update }
        enMode _Mode = enMode.Add;

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public clsCategory CategoryInfo { get; set; }   
        public int Price { get; set; }
        public string Brand { get; set; }
        public DateTime DateAdded { get; set; }

        // Default constructor
        public clsProduct()
        {
            ProductID = 0;
            ProductName = string.Empty;
            CategoryID = 0;
            Price = 0;
            Brand = string.Empty;
            DateAdded = DateTime.MinValue;

            CategoryInfo = new clsCategory();
            _Mode = enMode.Add;
        }

        // Parameterized constructor
        public clsProduct(int productID, string productName, int categoryID, int price, string brand, DateTime dateAdded)
        {
            ProductID = productID;
            ProductName = productName;
            CategoryID = categoryID;
            Price = price;
            Brand = brand;
            DateAdded = dateAdded;

            CategoryInfo = clsCategory.Find(CategoryID);
            _Mode = enMode.Update;
        }

        // Find by ProductID
        public static clsProduct Find(int ProductID)
        {
            string productName = string.Empty;
            int categoryID = 0;
            int price = 0;
            string brand = string.Empty;
            DateTime dateAdded = DateTime.MinValue;

            if (clsProductData.GetProduct(ProductID, ref productName, ref categoryID, ref price, ref brand, ref dateAdded))
            {
                return new clsProduct(ProductID, productName, categoryID, price, brand, dateAdded);
            }

            return null;
        }

        // Find by ProductName
        public static clsProduct Find(string ProductName)
        {
            int productID = 0;
            int categoryID = 0;
            int price = 0;
            string brand = string.Empty;
            DateTime dateAdded = DateTime.MinValue;

            if (clsProductData.GetProduct(ProductName, ref productID, ref categoryID, ref price, ref brand, ref dateAdded))
            {
                return new clsProduct(productID, ProductName, categoryID, price, brand, dateAdded);
            }

            return null;
        }


        static public bool IsExist(int ProductID)
        {
            return clsProductData.IsProductExist(ProductID);
        }

        static public bool IsExistByName(string ProductName)
        {
            return clsProductData.IsProductExistByName(ProductName);
        }

        static public bool Delete(int ProductID)
        {
            return clsProductData.DeleteProduct(ProductID);
        }

        static public DataTable GetProducts()
        {
            return clsProductData.GetAllProducts();
        }

        // Add a new product
        private bool _Add()
        {
            int productID = clsProductData.AddProduct(ProductName, CategoryID, Price, Brand, DateAdded);

            if (productID > 0)
            {
                this.ProductID = productID;
                return true;
            }

            return false;
        }

        // Update product information
        private bool _Update()
        {
            return clsProductData.UpdateProduct(ProductID, ProductName, CategoryID, Price, Brand, DateAdded);
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
