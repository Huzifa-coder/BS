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
    public class clsCategoryData
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

        // Get a Category by CategoryID
        public static bool GetCategory(int CategoryId, ref string CategoryName)
        {
            bool isFound = false;

            string CategoryNameString = string.Empty;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetCategoryByCategoryID";
                command.Parameters.Add(new SqlParameter("@CategoryID", SqlDbType.Int) { Value = CategoryId });

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        CategoryNameString = reader["CategoryName"] as string;
                        isFound = true;
                    }
                }
            });

            if (isFound)
            {
                CategoryName = CategoryNameString;
            }

            return isFound;
        }

        // Add a new Category
        public static int AddNewCategory(string CategoryName)
        {
            int id = -1;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_AddCategory";
                command.Parameters.Add(new SqlParameter("@CategoryName", SqlDbType.NVarChar) { Value = CategoryName });

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

        // Update Category Info
        public static bool UpdateCategoryInfo(int CategoryId, string CategoryName)
        {
            bool isUpdated = false;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_UpdateCategory";
                command.Parameters.Add(new SqlParameter("@CategoryID", SqlDbType.Int) { Value = CategoryId });
                command.Parameters.Add(new SqlParameter("@CategoryName", SqlDbType.NVarChar) { Value = CategoryName });

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

        // Delete Category by CategoryID
        public static bool DeleteCategory(int CategoryId)
        {
            bool isDeleted = false;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_DeleteCategory";
                command.Parameters.Add(new SqlParameter("@CategoryID", SqlDbType.Int) { Value = CategoryId });

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

        // Check if Category exists by CategoryID
        public static bool IsCategoryExist(int CategoryId)
        {
            bool isFound = false;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_CheckCategoryExistByID";
                command.Parameters.Add(new SqlParameter("@CategoryID", SqlDbType.Int) { Value = CategoryId });

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

        // Get all Categories
        public static DataTable GetAllCategories()
        {
            DataTable dt = new DataTable();

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetAllCategories";

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

        public static bool GetCategory(string categoryName, ref int id)
        {
            bool isFound = false;

            int ID = -1;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetCategoryByCategoryName";
                command.Parameters.Add(new SqlParameter("@CategoryName", SqlDbType.NVarChar) { Value = categoryName });

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ID = (int)reader["CategoryID"];
                        isFound = true;
                    }
                }
            });

            if (isFound)
            {
                id = ID;
            }

            return isFound;
        }
    }


}
