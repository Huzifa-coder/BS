using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsInvoice
    {
        private enum enMode { Add, Update }
        private enMode _Mode = enMode.Add;

        public int InvoiceID { get; set; }
        public DateTime CreateDate { get; set; }
        public int ClientID { get; set; }
        public clsClient ClientInfo { get; set; }
        public int? Total { get; set; }
        public string CreatedBy { get; set; }

        public clsInvoice()
        {
            InvoiceID = 0;
            CreateDate = DateTime.Now;
            ClientID = 0;
            Total = null;
            CreatedBy = string.Empty;

            ClientInfo = new clsClient();
            _Mode = enMode.Add;
        }

        public clsInvoice(int invoiceID, DateTime createDate, int clientID, int? total, string createdBy)
        {
            InvoiceID = invoiceID;
            CreateDate = createDate;
            ClientID = clientID;
            Total = total;
            CreatedBy = createdBy;

            ClientInfo = clsClient.Find(clientID);
            _Mode = enMode.Update;
        }

        public static clsInvoice Find(int invoiceID)
        {
            DateTime createDate = DateTime.MinValue;
            int clientID = 0;
            int? total = null;
            string createdBy = string.Empty;

            if (clsInvoiceData.GetInvoice(invoiceID, ref createDate, ref clientID, ref total, ref createdBy))
            {
                return new clsInvoice(invoiceID, createDate, clientID, total, createdBy);
            }

            return null;
        }

        public static bool IsExistByInvoiceID(int invoiceID)
        {
            return clsInvoiceData.IsInvoiceExistByInoiceID(invoiceID);
        }

        public static bool IsExistByClientID(int ClientID)
        {
            return clsInvoiceData.IsInvoiceExistByClientID(ClientID);
        }

        public static bool Delete(int invoiceID)
        {
            return clsInvoiceData.DeleteInvoice(invoiceID);
        }

        public static DataTable GetInvoices()
        {
            return clsInvoiceData.GetAllInvoices();
        }

        public DataTable GetProducts()
        {
            return clsInvoiceProductData.GetAllInvoiceProducts(InvoiceID);
        }
        private bool _Add()
        {
            int invoiceID = clsInvoiceData.AddInvoice(CreateDate, ClientID, Total, CreatedBy);

            if (invoiceID > 0)
            {
                this.InvoiceID = invoiceID;
                return true;
            }

            return false;
        }

        private bool _Update()
        {
            return clsInvoiceData.UpdateInvoice(InvoiceID, CreateDate, ClientID, Total, CreatedBy);
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
