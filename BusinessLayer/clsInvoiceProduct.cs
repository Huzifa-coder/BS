using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsInvoiceProduct
    {
        enum enMode { Add, Update }
        enMode _Mode = enMode.Add;

        public int InvoiceProductID { get; set; }
        public int ProductID { get; set; }
        public int InvoiceID { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }

        public clsInvoiceProduct()
        {
            InvoiceProductID = 0;
            ProductID = 0;
            InvoiceID = 0;
            Quantity = 0;
            Total = 0;

            _Mode = enMode.Add;
        }

        public clsInvoiceProduct(int invoiceProductID, int productID, int invoiceID, int quantity, int total)
        {
            InvoiceProductID = invoiceProductID;
            ProductID = productID;
            InvoiceID = invoiceID;
            Quantity = quantity;
            Total = total;

            _Mode = enMode.Update;
        }

        static public clsInvoiceProduct Find(int InvoiceProductID)
        {
            int productID = 0;
            int invoiceID = 0;
            int quantity = 0;
            int total = 0;

            if (clsInvoiceProductData.GetInvoiceProduct(InvoiceProductID, ref productID, ref invoiceID, ref quantity, ref total))
            {
                return new clsInvoiceProduct(InvoiceProductID, productID, invoiceID, quantity, total);
            }

            return null;
        }

        static public bool IsExist(int InvoiceProductID)
        {
            return clsInvoiceProductData.IsInvoiceProductExist(InvoiceProductID);
        }

        static public bool IsExistByInvoiceID(int InvoiceID)
        {
            return clsInvoiceProductData.IsInvoiceProductExistByInvoiceID(InvoiceID);
        }

        static public bool Delete(int InvoiceProductID)
        {
            return clsInvoiceProductData.DeleteInvoiceProduct(InvoiceProductID);
        }

        static public DataTable GetInvoiceProducts()
        {
            return clsInvoiceProductData.GetAllInvoiceProducts();
        }

        private bool _Add()
        {
            int invoiceProductID = clsInvoiceProductData.AddInvoiceProduct(ProductID, InvoiceID, Quantity, Total);

            if (invoiceProductID > 0)
            {
                this.InvoiceProductID = invoiceProductID;
                return true;
            }

            return false;
        }

        private bool _Update()
        {
            return clsInvoiceProductData.UpdateInvoiceProduct(InvoiceProductID, ProductID, InvoiceID, Quantity, Total);
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
