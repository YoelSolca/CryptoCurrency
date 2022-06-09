using CryptoCurrencyMVC.Helpers;
using CryptoCurrencyMVC.Models;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CryptoCurrencyMVC.Data
{
    public class UserData
    {

        private readonly Connection cn = new Connection();
        Random rnd = new Random();
        public bool RegisterUser(UserModel oUser)
        {
            bool respuesta;


            try
            {
                using (var connection = new SqlConnection(cn.getConnection()))
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand("RegisterUser", connection);

                    sqlCommand.Parameters.AddWithValue("@Email", oUser.Email);

                    //Encriptar contraseña
                    oUser.Password = Encrypt.GetSHA256(oUser.Password);
                    sqlCommand.Parameters.AddWithValue("@Password", oUser.Password);

                    sqlCommand.Parameters.AddWithValue("@FirstName", oUser.FirstName);
                    sqlCommand.Parameters.AddWithValue("@LastName", oUser.LastName);
                    sqlCommand.Parameters.AddWithValue("@Phone", oUser.Phone);
                    sqlCommand.Parameters.AddWithValue("@Address", oUser.Address);
                    sqlCommand.Parameters.AddWithValue("@Dni", oUser.Dni);
                    sqlCommand.Parameters.AddWithValue("@Birthdate", oUser.Birthdate);

                    //Cuentas 
                    //1- C-Peso
                    oUser.accountPeso = new AccountPesoModel();

                        StringBuilder SB = new StringBuilder();
                        for (int i = 0; i < 22; i++)
                            SB.Append(rnd.Next(0, 9));


                        oUser.accountPeso.CBU = SB.ToString();
                        oUser.accountPeso.Alias = oUser.FirstName + "" + oUser.LastName;

                        sqlCommand.Parameters.AddWithValue("@CBU", oUser.accountPeso.CBU);

                        sqlCommand.Parameters.AddWithValue("@Alias", oUser.accountPeso.Alias);
                        sqlCommand.Parameters.AddWithValue("@AccountBalance", 0);


                    //2 - C-Dolar

                    oUser.accountDollar = new AccountDollarModel();
                    oUser.accountDollar.Alias = oUser.accountPeso.Alias + ".usd";

                    StringBuilder SB2 = new StringBuilder();
                    for (int i = 0; i < 22; i++)
                        SB2.Append(rnd.Next(0, 9));

                    oUser.accountDollar.CBU = SB2.ToString();

                    sqlCommand.Parameters.AddWithValue("@CBUDollar", oUser.accountDollar.CBU);
                        sqlCommand.Parameters.AddWithValue("@AliasDollar", oUser.accountDollar.Alias);

                    //3 - C-Crypto
                    oUser.AccountCryptocurrencyModel = new AccountCryptocurrencyModel();
                    oUser.AccountCryptocurrencyModel.UUID = Guid.NewGuid(); 

                    sqlCommand.Parameters.AddWithValue("@UUID", oUser.AccountCryptocurrencyModel.UUID);


                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.ExecuteNonQuery();

                    respuesta = true;
                }
            }
            catch (Exception ex)
            {

                string error = ex.Message;
                respuesta = false;
            }
            
            return respuesta;

        }


        public UserModel GetUser(int id)
        {
            var oUser = new UserModel();
            try
            {
                using (var connection = new SqlConnection(cn.getConnection()))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("GetUser", connection);

                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oUser.ID = Convert.ToInt32(dr["ID"]);
                            oUser.FirstName = dr["FirstName"].ToString();
                            oUser.LastName = dr["LastName"].ToString();
                            oUser.Phone = dr["Phone"].ToString();
                            oUser.Address = dr["Address"].ToString();
                            oUser.Dni = dr["Dni"].ToString();
                            oUser.Birthdate = Convert.ToDateTime(dr["Birthdate"]);

                            oUser.accountPeso = new AccountPesoModel();
                            oUser.accountPeso.CBU = dr["CBU"].ToString();
                            oUser.accountPeso.Alias = dr["Alias"].ToString();
                            //oUser.accountPeso.AccountNumber = dr["Alias"].ToString();
                            oUser.accountPeso.AccountBalance = Convert.ToDouble(dr["accountBalance"]);

                            oUser.accountDollar = new AccountDollarModel();
                            oUser.accountDollar.CBU = dr["CBUDollar"].ToString();
                            oUser.accountDollar.Alias = dr["AliasDollar"].ToString();
                            //oUser.accountDollar.AccountNumber = dr["Alias"].ToString();
                            oUser.accountDollar.AccountBalance = Convert.ToDouble(dr["BalanceDollar"]);

                            oUser.AccountCryptocurrencyModel = new AccountCryptocurrencyModel();
                            oUser.AccountCryptocurrencyModel.UUID = Guid.Parse(dr["UUID"].ToString());
                            oUser.AccountCryptocurrencyModel.Data = dr["BalanceCryptocurrency"].ToString();
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

        public UserModel Login(string? email, string? password)
        {
            var oUser = new UserModel();
            try
            {
                using (var connection = new SqlConnection(cn.getConnection()))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("Login", connection);


                    cmd.Parameters.AddWithValue("@Email", email);

                    //Encriptar contraseña
                    password = Encrypt.GetSHA256(password);

                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oUser.ID = Convert.ToInt32(dr["ID"]);
                            oUser.Email = dr["Email"].ToString();
                            oUser.Password = dr["Password"].ToString();
                            oUser.FirstName = dr["FirstName"].ToString();
                            oUser.LastName = dr["LastName"].ToString();
                            oUser.Phone = dr["Phone"].ToString();
                            oUser.Address = dr["Address"].ToString();
                            oUser.Dni = dr["Dni"].ToString();
                            oUser.Birthdate = Convert.ToDateTime(dr["Birthdate"]);

                            oUser.accountPeso = new AccountPesoModel();
                            oUser.accountPeso.CBU = dr["CBU"].ToString();
                            oUser.accountPeso.Alias = dr["Alias"].ToString();
                            oUser.accountPeso.AccountBalance = Convert.ToDouble(dr["accountBalance"]);

                            oUser.accountDollar = new AccountDollarModel();

                            oUser.accountDollar.CBU = dr["CBUDollar"].ToString();
                            oUser.accountDollar.Alias = dr["AliasDollar"].ToString();
                            oUser.accountDollar.AccountBalance = Convert.ToDouble(dr["BalanceDollar"]);

                            oUser.AccountCryptocurrencyModel = new AccountCryptocurrencyModel();
                            oUser.AccountCryptocurrencyModel.UUID = Guid.Parse(dr["UUID"].ToString());
                            oUser.AccountCryptocurrencyModel.Data =  dr["BalanceCryptocurrency"].ToString();
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


        public bool EditUser(PersonModel user, int id)
        {
            bool respuesta;

            try
            {

                using (var conexion = new SqlConnection(cn.getConnection()))
                {
                    conexion.Open();

                    SqlCommand sqlCommand = new SqlCommand("EditUser", conexion);


                    sqlCommand.Parameters.AddWithValue("Id", id);
                    sqlCommand.Parameters.AddWithValue("FirstName", user.FirstName);
                    sqlCommand.Parameters.AddWithValue("LastName", user.LastName);
                    sqlCommand.Parameters.AddWithValue("Phone", user.Phone);
                    sqlCommand.Parameters.AddWithValue("Address", user.Address);
                    sqlCommand.Parameters.AddWithValue("Dni", user.Dni);
                    sqlCommand.Parameters.AddWithValue("Birthdate", user.Birthdate);

                    sqlCommand.CommandType = CommandType.StoredProcedure;
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


        public UserModel VerifyEmail(string Email)
        {

            var userModel = new UserModel();


            using (var conexion = new SqlConnection(cn.getConnection()))
            {
                conexion.Open();

                SqlCommand sqlCommand = new SqlCommand("VerifyEmail", conexion);

                sqlCommand.Parameters.AddWithValue("@Email", Email);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                using (var dr = sqlCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        userModel.ID = Convert.ToInt32(dr["ID"]);
                        userModel.Email = dr["Email"].ToString();
                    }
                }
            }
            return userModel;

        }

    }
}
