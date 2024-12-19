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
    public static class clsInvoiceData
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

        // Add a new invoice
        public static int AddInvoice(DateTime CreateDate, int ClientID, int? Total, string CreatedBy)
        {
            int id = -1;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_AddInvoice";
                command.Parameters.Add(new SqlParameter("@CreateDate", SqlDbType.Date) { Value = CreateDate });
                if (ClientID > 0)
                {
                    command.Parameters.Add(new SqlParameter("@ClientID", SqlDbType.Int) { Value = ClientID });
                }
                else
                {
                    command.Parameters.Add(new SqlParameter("@ClientID", SqlDbType.Int) { Value = DBNull.Value });
                }

                if (Total > 0)
                {
                    command.Parameters.Add(new SqlParameter("@Total", SqlDbType.Int) { Value = Total });
                }
                else
                {
                    command.Parameters.Add(new SqlParameter("@Total", SqlDbType.Int) { Value = DBNull.Value });
                }

                command.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.NVarChar) { Value = CreatedBy });

                var returnValue = new SqlParameter
                {
                    Direction = ParameterDirection.ReturnValue
                };
                command.Parameters.Add(returnValue);

                command.ExecuteNonQuery();

                if ((int)returnValue.Value > 0)
                {
                    id = (int)returnValue.Value;
                }
            });

            return id;
        }

        // Get invoice by InvoiceID
        public static bool GetInvoice(int InvoiceID, ref DateTime CreateDate, ref int ClientID, ref int? Total, ref string CreatedBy)
        {
            bool isFound = false;

            DateTime createDateLocal = DateTime.MinValue;
            int clientIdLocal = 0;
            int? totalLocal = null;
            string createdByLocal = string.Empty;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetInvoiceByInvoiceID";
                command.Parameters.Add(new SqlParameter("@InvoiceID", SqlDbType.Int) { Value = InvoiceID });

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        createDateLocal = (DateTime)reader["CreateDate"];
                        clientIdLocal = (int)reader["ClientID"];
                        totalLocal = reader["Total"] as int?;
                        createdByLocal = reader["CreatedBy"] as string;
                        isFound = true;
                    }
                }
            });

            if (isFound)
            {
                CreateDate = createDateLocal;
                ClientID = clientIdLocal;
                Total = totalLocal;
                CreatedBy = createdByLocal;
            }

            return isFound;
        }

        // Update invoice information
        public static bool UpdateInvoice(int InvoiceID, DateTime CreateDate, int ClientID, int? Total, string CreatedBy)
        {
            bool isUpdated = false;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_UpdateInvoice";
                command.Parameters.Add(new SqlParameter("@InvoiceID", SqlDbType.Int) { Value = InvoiceID });
                command.Parameters.Add(new SqlParameter("@CreateDate", SqlDbType.Date) { Value = CreateDate });
                command.Parameters.Add(new SqlParameter("@ClientID", SqlDbType.Int) { Value = ClientID });
                command.Parameters.Add(new SqlParameter("@Total", SqlDbType.Int) { Value = (object)Total ?? DBNull.Value });
                command.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.NVarChar) { Value = CreatedBy });

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

        // Delete an invoice by InvoiceID
        public static bool DeleteInvoice(int InvoiceID)
        {
            bool isDeleted = false;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_DeleteInvoice";
                command.Parameters.Add(new SqlParameter("@InvoiceID", SqlDbType.Int) { Value = InvoiceID });

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

        public static bool IsInvoiceExistByInoiceID(int InvoiceID)
        {
            bool isFound = false;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_CheckInvoiceExistByInvoiceID"; // Ensure this stored procedure exists
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

        public static bool IsInvoiceExistByClientID(int ClientID)
        {
            bool isFound = false;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_CheckInvoiceExistByClientID"; // Ensure this stored procedure exists
                command.Parameters.Add(new SqlParameter("@ClientID", SqlDbType.Int) { Value = ClientID });

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

        // Get all invoices
        public static DataTable GetAllInvoices()
        {
            DataTable dt = new DataTable();

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetAllInvoices";

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
