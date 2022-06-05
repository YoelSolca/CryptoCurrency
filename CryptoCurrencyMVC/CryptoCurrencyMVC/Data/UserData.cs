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

                    SqlCommand sqlCommand = new SqlCommand("Registrar", connection);


                    sqlCommand.Parameters.AddWithValue("@Email", oUser.Email);

                    //Encriptar contraseña
                    oUser.Password = Encrypt.GetSHA256(oUser.Password);
                    sqlCommand.Parameters.AddWithValue("@Password", oUser.Password);

                    sqlCommand.Parameters.AddWithValue("@FirstName", oUser.FirstName);
                    sqlCommand.Parameters.AddWithValue("@LastName", oUser.LastName);
                    sqlCommand.Parameters.AddWithValue("@Phone", oUser.Phone);
                    sqlCommand.Parameters.AddWithValue("@Address", oUser.Address);
                    sqlCommand.Parameters.AddWithValue("@Gender", oUser.Gender);
                    sqlCommand.Parameters.AddWithValue("@LocationName", oUser.LocationName);


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
                        sqlCommand.Parameters.AddWithValue("@AccountNumber", 123);
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
                        sqlCommand.Parameters.AddWithValue("@AccountNumberDollar", 123);

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
