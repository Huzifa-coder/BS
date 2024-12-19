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
    public static class clsUserData
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

        // Add a new user
        public static bool AddUser(string UserID, int PersonID, string Password, byte Permissions)
        {
            bool isAdded = false;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_AddUser";
                command.Parameters.Add(new SqlParameter("@UserID", SqlDbType.NVarChar) { Value = UserID });
                command.Parameters.Add(new SqlParameter("@PersonID", SqlDbType.Int) { Value = PersonID });
                command.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar) { Value = Password });
                command.Parameters.Add(new SqlParameter("@Permissions", SqlDbType.TinyInt) { Value = Permissions });

                var returnValue = new SqlParameter
                {
                    Direction = ParameterDirection.ReturnValue
                };
                command.Parameters.Add(returnValue);

                command.ExecuteNonQuery();

                isAdded = (int)returnValue.Value > 0;
            });

            return isAdded;
        }

        // Get user by UserID
        public static bool GetUser(string UserID, ref int PersonID, ref string Password, ref byte Permissions)
        {
            bool isFound = false;

            int personIdValue = 0;
            string passwordValue = string.Empty;
            byte permissionsValue = 0;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetUserByUserID";
                command.Parameters.Add(new SqlParameter("@UserID", SqlDbType.NVarChar) { Value = UserID });

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        personIdValue = (int)reader["PersonID"];
                        passwordValue = reader["Password"].ToString();
                        permissionsValue = (byte)reader["Permissions"];
                        isFound = true;
                    }
                }
            });

            if (isFound)
            {
                PersonID = personIdValue;
                Password = passwordValue;
                Permissions = permissionsValue;
            }

            return isFound;
        }

        // Update user information
        public static bool UpdateUser(string UserID, int PersonID, string Password, byte Permissions)
        {
            bool isUpdated = false;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_UpdateUser";
                command.Parameters.Add(new SqlParameter("@UserID", SqlDbType.NVarChar) { Value = UserID });
                command.Parameters.Add(new SqlParameter("@PersonID", SqlDbType.Int) { Value = PersonID });
                command.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar) { Value = Password });
                command.Parameters.Add(new SqlParameter("@Permissions", SqlDbType.TinyInt) { Value = Permissions });

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

        // Delete user by UserID
        public static bool DeleteUser(string UserID)
        {
            bool isDeleted = false;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_DeleteUser";
                command.Parameters.Add(new SqlParameter("@UserID", SqlDbType.NVarChar) { Value = UserID });

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

        // Check if a user exists by UserID
        public static bool IsUserExist(string UserID)
        {
            bool isFound = false;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_CheckUserExistByUserID";
                command.Parameters.Add(new SqlParameter("@UserID", SqlDbType.NVarChar) { Value = UserID });

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

        // Get all users
        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetAllUsers";

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
