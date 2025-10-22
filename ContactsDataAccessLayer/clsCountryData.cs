using System;
using System.Data;
using System.Data.SqlClient;


namespace ContactsDataAccessLayer
{
    public class clsCountryData
    {
        private static string _GetStringOrNull(SqlDataReader reader, string column)
        {
            return reader[column] == DBNull.Value ? "Null" : (string)reader[column];
        }

        private static void _AddParameterOrDbNull(ref SqlCommand command, string parameterName, object value)
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

        public static bool GetCountryInfoByID(int ID, ref string countryName, ref string code, ref string phoneCode)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsContactsDataAccessSettings.connectionString);

            string query = "select * from Countries where CountryID = @CountryID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryID", ID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    countryName = (string)reader["CountryName"];
                    code = _GetStringOrNull(reader, "Code");
                    phoneCode = _GetStringOrNull(reader, "PhoneCode");
                }
                else
                {
                    isFound = false;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool GetCountryInfoByName(string countryName, ref int ID, ref string code, ref string phoneCode)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsContactsDataAccessSettings.connectionString);

            string query = "select * from Countries where CountryName = @CountryName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", countryName);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    ID = (int)reader["CountryID"];
                    countryName = (string)reader["CountryName"];
                    code = _GetStringOrNull(reader, "Code");
                    phoneCode = _GetStringOrNull(reader, "PhoneCode");
                }
                else
                {
                    isFound = false;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static int AddNewCountry(string countryName, string code, string phoneCode)
        {
            int countryID = -1;
            SqlConnection connection = new SqlConnection(clsContactsDataAccessSettings.connectionString);

            string query = @"insert into Countries (CountryName, Code, PhoneCode)
                             values (@CountryName, @Code, @PhoneCode);
                             select SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", countryName);
            _AddParameterOrDbNull(ref command, "@Code", code);
            _AddParameterOrDbNull(ref command, "@PhoneCode", phoneCode);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    countryID = insertedID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return countryID;
        }

        public static bool UpdateCountry(int ID, string countryName, string code, string phoneCode)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsContactsDataAccessSettings.connectionString);

            string query = @"update Countries
                             set CountryName = @CountryName,
                                 Code = @Code,
                                 PhoneCode = @PhoneCode
                             where CountryID = @CountryID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryID", ID);
            command.Parameters.AddWithValue("@CountryName", countryName);
            _AddParameterOrDbNull(ref command, "@Code", code);
            _AddParameterOrDbNull(ref command, "@PhoneCode", phoneCode);


            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return rowsAffected > 0;
        }

        public static bool DelteCountry(int ID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsContactsDataAccessSettings.connectionString);

            string query = @"Delete from Countries
                             where CountryID = @CountryID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryID", ID);

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

        public static DataTable GetAllCountries()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsContactsDataAccessSettings.connectionString);

            string query = "select * from Countries";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static bool IsCountryExist(int ID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsContactsDataAccessSettings.connectionString);

            string query = "select Found = 1 from Countries where CountryID = @CountryID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryID", ID);

            try
            {
                connection.Open();

                Object result = command.ExecuteScalar();

                if (result != null)
                {
                    isFound = true;
                }
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool IsCountryExist(string countryName)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsContactsDataAccessSettings.connectionString);

            string query = "select Found = 1 from Countries where CountryName = @CountryName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", countryName);

            try
            {
                connection.Open();

                Object result = command.ExecuteScalar();

                if (result != null)
                {
                    isFound = true;
                }
            }
            catch (Exception ex)
            {
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
