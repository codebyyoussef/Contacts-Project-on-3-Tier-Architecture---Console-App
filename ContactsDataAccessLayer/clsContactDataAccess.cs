using System;
using System.Data.SqlClient;

namespace ContactsDataAccessLayer
{
    public class clsContactDataAccess
    {
        private static string GetStringOrNull(SqlDataReader reader, string column)
        {
            return reader[column] == DBNull.Value ? "Null" : (string)reader[column];
        }
        public static bool GetContactInfoByID (int ID, ref string FirstName, ref string LastName, ref string Email, ref string Phone, ref string Address, 
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
                    //ImagePath = reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? null : reader.GetString(reader.GetOrdinal("ImagePath"));

                    // Option 2: Use the as operator with DBNull check
                    //ImagePath = reader["ImagePath"] != DBNull.Value ? (string)reader["ImagePath"] : "NULL";

                    // Option 3: Using helper method
                    ImagePath = GetStringOrNull(reader, "ImagePath");
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
    }
}
