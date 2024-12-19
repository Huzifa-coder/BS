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
    public static class clsProductData
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

        // Add a new product
        public static int AddProduct(string ProductName, int CategoryID, int Price, string Brand, DateTime DateAdded)
        {
            int id = -1;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_AddProduct";
                command.Parameters.Add(new SqlParameter("@ProductName", SqlDbType.NVarChar) { Value = ProductName });
                command.Parameters.Add(new SqlParameter("@CategoryID", SqlDbType.Int) { Value = CategoryID });
                command.Parameters.Add(new SqlParameter("@Price", SqlDbType.Int) { Value = Price });
                command.Parameters.Add(new SqlParameter("@Brand", SqlDbType.NVarChar) { Value = Brand });
                command.Parameters.Add(new SqlParameter("@DateAdded", SqlDbType.Date) { Value = DateAdded });
                
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

        // Get a product by ProductID
        public static bool GetProduct(int ProductID, ref string ProductName, ref int CategoryID, ref int Price, ref string Brand, ref DateTime DateAdded)
        {
            bool isFound = false;

            string productName = string.Empty;
            int categoryId = 0;
            int price = 0;
            string brand = string.Empty;
            DateTime dateAdded = DateTime.MinValue;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetProductByProductID";
                command.Parameters.Add(new SqlParameter("@ProductID", SqlDbType.Int) { Value = ProductID });

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        productName = reader["ProductName"] as string;
                        categoryId = (int)reader["CategoryID"];
                        price = (int)reader["Price"];
                        brand = reader["Brand"] as string;
                        dateAdded = (DateTime)reader["DateAdded"];
                        isFound = true;
                    }
                }
            });

            if (isFound)
            {
                ProductName = productName;
                CategoryID = categoryId;
                Price = price;
                Brand = brand;
                DateAdded = dateAdded;
            }

            return isFound;
        }

        // Get a product by ProductName
        public static bool GetProduct(string ProductName, ref int ProductID, ref int CategoryID, ref int Price, ref string Brand, ref DateTime DateAdded)
        {
            bool isFound = false;

            int productId = 0;
            int categoryId = 0;
            int price = 0;
            string brand = string.Empty;
            DateTime dateAdded = DateTime.MinValue;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetProductByProductName";
                command.Parameters.Add(new SqlParameter("@ProductName", SqlDbType.NVarChar) { Value = ProductName });

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        productId = (int)reader["ProductID"];
                        categoryId = (int)reader["CategoryID"];
                        price = (int)reader["Price"];
                        brand = reader["Brand"] as string;
                        dateAdded = (DateTime)reader["DateAdded"];
                        isFound = true;
                    }
                }
            });

            if (isFound)
            {
                ProductID = productId;
                CategoryID = categoryId;
                Price = price;
                Brand = brand;
                DateAdded = dateAdded;
            }

            return isFound;
        }

        // Update product information
        public static bool UpdateProduct(int ProductID, string ProductName, int CategoryID, int Price, string Brand, DateTime DateAdded)
        {
            bool isUpdated = false;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_UpdateProduct";
                command.Parameters.Add(new SqlParameter("@ProductID", SqlDbType.Int) { Value = ProductID });
                command.Parameters.Add(new SqlParameter("@ProductName", SqlDbType.NVarChar) { Value = ProductName });
                command.Parameters.Add(new SqlParameter("@CategoryID", SqlDbType.Int) { Value = CategoryID });
                command.Parameters.Add(new SqlParameter("@Price", SqlDbType.Int) { Value = Price });
                command.Parameters.Add(new SqlParameter("@Brand", SqlDbType.NVarChar) { Value = Brand });
                command.Parameters.Add(new SqlParameter("@DateAdded", SqlDbType.Date) { Value = DateAdded });

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

        // Delete a product by ProductID
        public static bool DeleteProduct(int ProductID)
        {
            bool isDeleted = false;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_DeleteProduct";
                command.Parameters.Add(new SqlParameter("@ProductID", SqlDbType.Int) { Value = ProductID });

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

        // Check if a product exists by ProductID
        public static bool IsProductExist(int ProductID)
        {
            bool isFound = false;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_CheckProductExistByProductID";
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

        // Check if a product exists by ProductName
        public static bool IsProductExistByName(string ProductName)
        {
            bool isFound = false;

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_CheckProductExistByProductName";
                command.Parameters.Add(new SqlParameter("@ProductName", SqlDbType.NVarChar) { Value = ProductName });

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

        // Get all products
        public static DataTable GetAllProducts()
        {
            DataTable dt = new DataTable();

            ExecuteDatabaseOperation(command =>
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetAllProducts";

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
