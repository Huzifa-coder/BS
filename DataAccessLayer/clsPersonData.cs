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
    
    public class clsPersonData
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

        public static bool GetPerson(int personId, ref string firstName, ref string lastName)
        {
            bool isFound = false;

            string firstNameString = string.Empty;
            string lastNameString = string.Empty;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetPersonByPersonID";
                command.Parameters.Add(new SqlParameter("@PersonID", SqlDbType.Int) { Value = personId });

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        firstNameString = reader["FirstName"] as string;
                        lastNameString = reader["LastName"] as string;
                        isFound = true;
                    }
                }
            });

            if (isFound)
            {
                firstName = firstNameString;
                lastName = lastNameString;
            }

            return isFound;
        }

        public static int AddNewPerson(string firstName, string lastName)
        {
            int id = -1;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_AddPerson";
                command.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar) { Value = firstName });
                command.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar) { Value = lastName });

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

        public static bool UpdatePersonInfo(int personId, string firstName, string lastName)
        {
            bool isUpdated = false;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_UpdatePerson";
                command.Parameters.Add(new SqlParameter("@PersonID", SqlDbType.Int) { Value = personId });
                command.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar) { Value = firstName });
                command.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar) { Value = lastName });

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

        public static bool DeletePerson(int personId)
        {
            bool isDeleted = false;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_DeletePerson";
                command.Parameters.Add(new SqlParameter("@PersonID", SqlDbType.Int) { Value = personId });

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

        public static bool IsExist(int personId)
        {
            bool isFound = false;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_CheckPersonExistByPersonID";
                command.Parameters.Add(new SqlParameter("@PersonID", SqlDbType.Int) { Value = personId });

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

        public static DataTable GetPeople()
        {
            DataTable dt = new DataTable();

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetAllPeople";

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
