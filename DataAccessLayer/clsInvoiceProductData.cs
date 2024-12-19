using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccessLayer.clsGlobal;

namespace DataAccessLayer
{
    public static class clsInvoiceProductData
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["LocalDBConnection"].ConnectionString;

        private static void ExecuteDatabaseOperation(Action<SqlCommand> action)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        action(command);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteEventLogEntry(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
        }

        // Add a new invoice product
        public static int AddInvoiceProduct(int ProductID, int InvoiceID, int Quantity, int Total)
        {
            int id = -1;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_AddInvoiceProduct";
                command.Parameters.Add(new SqlParameter("@ProductID", SqlDbType.Int) { Value = ProductID });
                command.Parameters.Add(new SqlParameter("@InvoiceID", SqlDbType.Int) { Value = InvoiceID });
                command.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.Int) { Value = Quantity });
                command.Parameters.Add(new SqlParameter("@Total", SqlDbType.Int) { Value = Total });

                var returnValue = new SqlParameter
                {
                    Direction = ParameterDirection.ReturnValue
                };

                command.Parameters.Add(returnValue);

                command.ExecuteNonQuery();

                if( (int)returnValue.Value > 0)
                {
                    id = (int)returnValue.Value;
                }
            });

            return id;
        }

        // Get an invoice product by InvoiceProductID
        public static bool GetInvoiceProduct(int InvoiceProductID, ref int ProductID, ref int InvoiceID, ref int Quantity, ref int Total)
        {
            bool isFound = false;

            int productIdInt = 0;
            int invoiceIdInt = 0;
            int quantityInt = 0;
            int totalInt = 0;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetInvoiceProductByInvoiceProductID";
                command.Parameters.Add(new SqlParameter("@InvoiceProductID", SqlDbType.Int) { Value = InvoiceProductID });

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        productIdInt = (int)reader["ProductID"];
                        invoiceIdInt = (int)reader["InvoiceID"];
                        quantityInt = (int)reader["Quantity"];
                        totalInt = (int)reader["Total"];
                        isFound = true;
                    }
                }
            });

            if (isFound)
            {
                ProductID = productIdInt;
                InvoiceID = invoiceIdInt;
                Quantity = quantityInt;
                Total = totalInt;
            }

            return isFound;
        }

        // Update an invoice product
        public static bool UpdateInvoiceProduct(int InvoiceProductID, int ProductID, int InvoiceID, int Quantity, int Total)
        {
            bool isUpdated = false;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_UpdateInvoiceProduct";
                command.Parameters.Add(new SqlParameter("@InvoiceProductID", SqlDbType.Int) { Value = InvoiceProductID });
                command.Parameters.Add(new SqlParameter("@ProductID", SqlDbType.Int) { Value = ProductID });
                command.Parameters.Add(new SqlParameter("@InvoiceID", SqlDbType.Int) { Value = InvoiceID });
                command.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.Int) { Value = Quantity });
                command.Parameters.Add(new SqlParameter("@Total", SqlDbType.Int) { Value = Total });

                var returnValue = new SqlParameter
                {
                    Direction = ParameterDirection.ReturnValue
                };
                command.Parameters.Add(returnValue);

                command.ExecuteNonQuery();

                isUpdated = (int)returnValue.Value > 0;
            });

            return isUpdated;
        }

        // Delete an invoice product by InvoiceProductID
        public static bool DeleteInvoiceProduct(int InvoiceProductID)
        {
            bool isDeleted = false;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_DeleteInvoiceProduct";
                command.Parameters.Add(new SqlParameter("@InvoiceProductID", SqlDbType.Int) { Value = InvoiceProductID });

                var returnValue = new SqlParameter
                {
                    Direction = ParameterDirection.ReturnValue
                };
                command.Parameters.Add(returnValue);

                command.ExecuteNonQuery();

                isDeleted = (int)returnValue.Value > 0;
            });

            return isDeleted;
        }

        // Check if an invoice product exists by InvoiceProductID
        public static bool IsInvoiceProductExist(int InvoiceProductID)
        {
            bool isFound = false;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_CheckInvoiceProductExistByInvoiceProductID";
                command.Parameters.Add(new SqlParameter("@InvoiceProductID", SqlDbType.Int) { Value = InvoiceProductID });

                var returnValue = new SqlParameter
                {
                    Direction = ParameterDirection.ReturnValue
                };
                command.Parameters.Add(returnValue);

                command.ExecuteNonQuery();

                isFound = (int)returnValue.Value > 0;
            });

            return isFound;
        }

        // Check if an invoice product exists by InvoiceID
        public static bool IsInvoiceProductExistByInvoiceID(int InvoiceID)
        {
            bool isFound = false;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_CheckInvoiceProductExistByInvoiceID";
                command.Parameters.Add(new SqlParameter("@InvoiceID", SqlDbType.Int) { Value = InvoiceID });

                var returnValue = new SqlParameter
                {
                    Direction = ParameterDirection.ReturnValue
                };
                command.Parameters.Add(returnValue);

                command.ExecuteNonQuery();

                isFound = (int)returnValue.Value > 0;
            });

            return isFound;
        }

        // Get all invoice products
        public static DataTable GetAllInvoiceProducts()
        {
            DataTable dt = new DataTable();

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetAllInvoiceProducts";

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        dt.Load(reader);
                    }
                }
            });

            return dt;
        }

        public static DataTable GetAllInvoiceProducts(int invoiceID)
        {
            DataTable dt = new DataTable();

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetAllInvoiceProductsByInvoiceID";
                command.Parameters.Add(new SqlParameter("@InvoiceID", SqlDbType.Int) { Value = invoiceID });

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        dt.Load(reader);
                    }
                }
            });

            return dt;
        }
    }

}
