using System;
using System.Data;
using System.Data.SqlClient;

namespace ContactsDataAccessLayer
{
    public class clsContactDataAccess
    {
        //private static string GetStringOrNull(SqlDataReader reader, string column)
        //{
        //    return reader[column] == DBNull.Value ? "Null" : (string)reader[column];
        //}

        private static void AddParameterOrDbNull(ref SqlCommand command, string parameterName, object value)
        {
            if (value != null & value.ToString() != "")
            {
                command.Parameters.AddWithValue(parameterName, value);
            }
            else
            {
                command.Parameters.AddWithValue(parameterName, DBNull.Value);
            }
        }

        public static bool GetContactInfoByID(int ID, ref string FirstName, ref string LastName, ref string Email, ref string Phone, ref string Address,
                                                ref DateTime DateOfBirth, ref int CountryID, ref string ImagePath)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsContactsDataAccessSettings.connectionString);

            string query = "select * from Contacts where ContactID = @ContactID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ContactID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["LastName"];
                    Email = (string)reader["Email"];
                    Phone = (string)reader["Phone"];
                    Address = (string)reader["Address"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    CountryID = (int)reader["CountryID"];

                    /* 
                    * ImagePath = (string)reader["ImagePath"]; 
                       * If the database field ImagePath is NULL, then reader["ImagePath"] returns DBNull.Value, not null.
                       * When you try to cast that to a string 
                       * (string)DBNull.Value
                       * it throws an InvalidCastException.
                   */

                    // Correct Solutions
                    // Option 1: Use reader.IsDBNull()
                    // This retrieves the zero-based column index of the column named "ImagePath" in the result set.
                    // It's more efficient than repeatedly using the column name, especially in loops

                    //ImagePath = reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? null : reader.GetString(reader.GetOrdinal("ImagePath"));

                    // Option 2: Use the as operator with DBNull check
                    ImagePath = reader["ImagePath"] != DBNull.Value ? (string)reader["ImagePath"] : "NULL";

                    // Option 3: Using helper method
                    //ImagePath = GetStringOrNull(reader, "ImagePath");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                // You put code of logging errors
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        public static int AddNewContact(string FirstName, string LastName, string Email, string Phone, string Address, DateTime DateOfBirth, 
                                        int CountryID, string ImagePath)
        {
            int contactID = -1;
            SqlConnection connection = new SqlConnection(clsContactsDataAccessSettings.connectionString);

            string query = @"insert into Contacts(FirstName, LastName, Email, Phone, Address, DateOfBirth ,CountryID, ImagePath)
                             values (@FirstName, @LastName, @Email, @Phone, @Address, @DataOfBirth, @CountryID, @ImagePath);
                             select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@DataOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@CountryID", CountryID);

            AddParameterOrDbNull(ref command, "ImagePath", ImagePath);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    contactID = InsertedID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return contactID;
        }

        public static bool UpdateContact(int ID, string FirstName, string LastName, string Email, string Phone, string Address, DateTime DateOfBirth,
                                        int CountryID, string ImagePath)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsContactsDataAccessSettings.connectionString);

            string query = @"update Contacts
                             set FirstName   = @FirstName,
                                 LastName    = @LastName,
                                 Email       = @Email,
                                 Phone       = @Phone,
                                 Address     = @Address,
                                 DateOfBirth = @DateOfBirth,
                                 CountryID   = @CountryID,
                                 ImagePath   = @ImagePath
                             where ContactID = @ContactID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ContactID", ID);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            AddParameterOrDbNull(ref command, "ImagePath", ImagePath);


            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return rowsAffected > 0;
        }
    }
}
