using CryptoCurrencyMVC.Models;
using System.Data;
using System.Data.SqlClient;

namespace CryptoCurrencyMVC.Data
{
    public class UserData
    {

        private readonly Connection cn = new Connection();

        public bool RegisterUser(UserModel oUser)
        {
            bool respuesta;


            try
            {
                using (var connection = new SqlConnection(cn.getConnection()))
               {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand("Registrar", connection);

                    sqlCommand.Parameters.AddWithValue("Email", oUser.Email);
                    sqlCommand.Parameters.AddWithValue("Password", oUser.Password);

                    sqlCommand.Parameters.AddWithValue("FirstName", oUser.FirstName);
                    sqlCommand.Parameters.AddWithValue("LastName", oUser.LastName);
                    sqlCommand.Parameters.AddWithValue("Phone", oUser.Phone);
                    sqlCommand.Parameters.AddWithValue("Address", oUser.Address);
                    sqlCommand.Parameters.AddWithValue("Gender", oUser.Gender);
                    sqlCommand.Parameters.AddWithValue("LocationName", oUser.LocationName);

                    sqlCommand.ExecuteNonQuery();
                }

                respuesta = true;
            }
            catch (Exception ex)
            {

                string error = ex.Message;
                respuesta = false;
            }
            
            return respuesta;

        }


        public bool RegisterPerson(UserModel oUser)
        {
            bool respuesta;

            try
            {
                using (var connection = new SqlConnection(cn.getConnection()))
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand("RegisterPerson", connection);

                    //Persons
                    sqlCommand.Parameters.AddWithValue("ID", oUser.PersonID);
                    sqlCommand.Parameters.AddWithValue("FirstName", oUser.FirstName);
                    sqlCommand.Parameters.AddWithValue("LastName", oUser.LastName);
                    sqlCommand.Parameters.AddWithValue("Phone", oUser.Phone);
                    sqlCommand.Parameters.AddWithValue("Address", oUser.Address);
                    sqlCommand.Parameters.AddWithValue("Gender", oUser.Gender);
                    sqlCommand.Parameters.AddWithValue("Birthdate", oUser.Birthdate);
                    sqlCommand.Parameters.AddWithValue("LocationName", oUser.LocationName);

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.ExecuteNonQuery();
                    connection.Close();

                }

                respuesta = true;
            }
            catch (Exception ex)
            {

                string error = ex.Message;
                respuesta = false;
            }

            return respuesta;

        }

        public UserModel ObtenerUsuario(string? email, string? password)
        {
            var oUser = new UserModel();
            bool respuesta;
            try
            {
                using (var connection = new SqlConnection(cn.getConnection()))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("Login", connection);


                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oUser.ID = Convert.ToInt32(dr["ID"]);
                            oUser.Email = dr["Email"].ToString();
                            oUser.Password = dr["Password"].ToString();
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                string error = ex.Message;
            }

            return oUser;
        }

    }
}
