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
    public static class clsRepositoryData
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

        // Add a new repository entry
        public static int AddRepository(int ProductID, int Quantity)
        {
            int id = -1;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_AddRepository";
                command.Parameters.Add(new SqlParameter("@ProductID", SqlDbType.Int) { Value = ProductID });
                command.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.Int) { Value = Quantity });

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

        // Get repository entry by RepositoryID
        public static bool GetRepository(int RepositoryID, ref int ProductID, ref int Quantity)
        {
            bool isFound = false;

            int productIdValue = 0;
            int quantityValue = 0;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetRepositoryByRepositoryID";
                command.Parameters.Add(new SqlParameter("@RepositoryID", SqlDbType.Int) { Value = RepositoryID });

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        productIdValue = (int)reader["ProductID"];
                        quantityValue = (int)reader["Quantity"];
                        isFound = true;
                    }
                }
            });

            if (isFound)
            {
                ProductID = productIdValue;
                Quantity = quantityValue;
            }

            return isFound;
        }

        // Update repository entry
        public static bool UpdateRepository(int RepositoryID, int ProductID, int Quantity)
        {
            bool isUpdated = false;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_UpdateRepository";
                command.Parameters.Add(new SqlParameter("@RepositoryID", SqlDbType.Int) { Value = RepositoryID });
                command.Parameters.Add(new SqlParameter("@ProductID", SqlDbType.Int) { Value = ProductID });
                command.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.Int) { Value = Quantity });

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

        // Delete repository entry by RepositoryID
        public static bool DeleteRepository(int RepositoryID)
        {
            bool isDeleted = false;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_DeleteRepository";
                command.Parameters.Add(new SqlParameter("@RepositoryID", SqlDbType.Int) { Value = RepositoryID });

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

        // Check if a repository entry exists by RepositoryID
        public static bool IsRepositoryExist(int RepositoryID)
        {
            bool isFound = false;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_CheckRepositoryExistByRepositoryID";
                command.Parameters.Add(new SqlParameter("@RepositoryID", SqlDbType.Int) { Value = RepositoryID });

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

        // Check if a repository entry exists by ProductID
        public static bool IsRepositoryExistByProductID(int ProductID)
        {
            bool isFound = false;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_CheckRepositoryExistByProductID";
                command.Parameters.Add(new SqlParameter("@ProductID", SqlDbType.Int) { Value = ProductID });

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

        // Get all repository entries
        public static DataTable GetAllRepositories()
        {
            DataTable dt = new DataTable();

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetAllRepositories";

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
