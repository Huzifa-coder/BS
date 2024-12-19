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
    public static class clsClientData
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

        // Add a new client
        public static int AddClient(int PersonID, string Phone)
        {
            int id = -1;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_AddClient";
                command.Parameters.Add(new SqlParameter("@PersonID", SqlDbType.Int) { Value = PersonID });
                command.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar) { Value = Phone });

                var returnValue = new SqlParameter
                {
                    Direction = ParameterDirection.ReturnValue
                };
                command.Parameters.Add(returnValue);

                command.ExecuteNonQuery();

                if((int)returnValue.Value > 0)
                {
                    id = (int)returnValue.Value;
                }
            });

            return id;
        }

        // Get client by ClientID
        public static bool GetClient(int ClientID, ref int PersonID, ref string Phone)
        {
            bool isFound = false;

            int personIdInt = 0;
            string phoneString = string.Empty;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetClientByClientID";
                command.Parameters.Add(new SqlParameter("@ClientID", SqlDbType.Int) { Value = ClientID });

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        personIdInt = (int)reader["PersonID"];
                        phoneString = reader["Phone"] as string;
                        isFound = true;
                    }
                }
            });

            if (isFound)
            {
                PersonID = personIdInt;
                Phone = phoneString;
            }

            return isFound;
        }

        // Update client information
        public static bool UpdateClient(int ClientID, int PersonID, string Phone)
        {
            bool isUpdated = false;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_UpdateClient";
                command.Parameters.Add(new SqlParameter("@ClientID", SqlDbType.Int) { Value = ClientID });
                command.Parameters.Add(new SqlParameter("@PersonID", SqlDbType.Int) { Value = PersonID });
                command.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar) { Value = Phone });

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

        // Delete a client by ClientID
        public static bool DeleteClient(int ClientID)
        {
            bool isDeleted = false;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_DeleteClient";
                command.Parameters.Add(new SqlParameter("@ClientID", SqlDbType.Int) { Value = ClientID });

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

        // Check if a client exists by ClientID
        public static bool IsClientExist(int ClientID)
        {
            bool isFound = false;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_CheckClientExistByClientID";
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

        // Check if a client exists by PersonID
        public static bool IsClientExistByPersonID(int PersonID)
        {
            bool isFound = false;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_CheckClientExistByPersonID";
                command.Parameters.Add(new SqlParameter("@PersonID", SqlDbType.Int) { Value = PersonID });

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

        // Get all clients
        public static DataTable GetAllClients()
        {
            DataTable dt = new DataTable();

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetAllClients";

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

        public static bool GetClient(string Phone, ref int ClientID, ref int PersonID)
        {
            bool isFound = false;

            int clientIdInt = 0;
            int personIdInt = 0;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetClientByPhone"; // Update with the actual stored procedure name
                command.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar, 50) { Value = Phone });

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        clientIdInt = (int)reader["ClientID"];
                        personIdInt = (int)reader["PersonID"];
                        isFound = true;
                    }
                }
            });

            if (isFound)
            {
                ClientID = clientIdInt;
                PersonID = personIdInt;
            }

            return isFound;
        }

        public static bool IsClientExist(string Phone)
        {
            bool isFound = false;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_CheckClientExistByPhone";
                command.Parameters.Add(new SqlParameter("@Phone", SqlDbType.VarChar, 15) { Value = Phone });

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

    }
}
