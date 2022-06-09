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
                            oUser.FirstName = dr["FirstName"].ToString();
                            oUser.LastName = dr["LastName"].ToString();
                            oUser.Phone = dr["Phone"].ToString();
                            oUser.Address = dr["Address"].ToString();
                            oUser.Gender = dr["Gender"].ToString();
                            oUser.LocationName = dr["LocationName"].ToString();
                            oUser.Birthdate = Convert.ToDateTime(dr["Birthdate"]);

                            oUser.accountPeso = new AccountPesoModel();
                            oUser.accountPeso.CBU = dr["CBU"].ToString();
                            oUser.accountPeso.Alias = dr["Alias"].ToString();
                            oUser.accountPeso.AccountNumber = dr["Alias"].ToString();
                            oUser.accountPeso.AccountBalance = Convert.ToDouble(dr["accountBalance"]);

                            oUser.accountDollar = new AccountDollarModel();
                            oUser.accountDollar.CBU = dr["CBUDolar"].ToString();
                            oUser.accountDollar.Alias = dr["AliasDolar"].ToString();
                            oUser.accountDollar.AccountNumber = dr["Alias"].ToString();
                            oUser.accountDollar.AccountBalance = Convert.ToDouble(dr["Balance"]);

                            oUser.AccountCryptocurrencyModel = new AccountCryptocurrencyModel();
                            oUser.AccountCryptocurrencyModel.UUID = Guid.Parse(dr["UUID"].ToString());
                            oUser.AccountCryptocurrencyModel.data =  dr["DATA"].ToString();
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



        public List<OperationModel> Movements(int id)
        {
            var oLista = new List<OperationModel>();
            bool respuesta;
            try
            {
                using (var connection = new SqlConnection(cn.getConnection()))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("Operations", connection);

                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new OperationModel()
                            {
                                ID = Convert.ToInt32(dr["ID"]),
                                Title = dr["INFO"].ToString(),
                                Icon = dr["Imagen"].ToString(),
                                Date =  Convert.ToDateTime(dr["Fecha"]),
                                Amount = Convert.ToDouble(dr["Cantidad"])
                            });
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                string error = ex.Message;
            }

            return oLista;
        }



        public bool Transfer(double transfer, int userid, int id)
        {
            bool respuesta;

            try
            {
                using (var connection = new SqlConnection(cn.getConnection()))
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand("Operacion", connection);

                     
                    //numero de transaccion

                    StringBuilder SB = new StringBuilder();
                    for (int i = 0; i < 22; i++)
                        SB.Append(rnd.Next(0, 9));


                    sqlCommand.Parameters.AddWithValue("@Title", "Nueva transferencia");
                    sqlCommand.Parameters.AddWithValue("@Date", DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("@Amount", transfer);
                    sqlCommand.Parameters.AddWithValue("@TransactionNumber", SB.ToString());
                    sqlCommand.Parameters.AddWithValue("@Icon", "https://i.imgur.com/lw6Cpl1.png");

                    sqlCommand.Parameters.AddWithValue("@Title2", "Nueva Deposito");
                    sqlCommand.Parameters.AddWithValue("@Icon2", "https://i.imgur.com/rP0ffiu.png");

                    sqlCommand.Parameters.AddWithValue("@ID", id);
                    sqlCommand.Parameters.AddWithValue("@userid", userid);


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


        public bool Editar(double amount, int id)
        {
            bool respuesta;

            try
            {

                using (var conexion = new SqlConnection(cn.getConnection()))
                {
                    conexion.Open();

                    //SqlCommand sqlCommand = new SqlCommand("editarcrypto", conexion);

                    SqlCommand sqlCommand = new SqlCommand("editar", conexion);


                    sqlCommand.Parameters.AddWithValue("Id", id);
                    sqlCommand.Parameters.AddWithValue("Amount", amount);

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
        public bool EditarDolar(double amount, int id)
        {
            bool respuesta;

            try
            {

                using (var conexion = new SqlConnection(cn.getConnection()))
                {
                    conexion.Open();

                    //SqlCommand sqlCommand = new SqlCommand("editarcrypto", conexion);

                    SqlCommand sqlCommand = new SqlCommand("editarDolar", conexion);


                    sqlCommand.Parameters.AddWithValue("Id", id);
                    sqlCommand.Parameters.AddWithValue("Amount", amount);

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




        public bool Editar(char[] amount, int id)
        {
            bool respuesta;

            try
            {

                using (var conexion = new SqlConnection(cn.getConnection()))
                {
                    conexion.Open();

                    SqlCommand sqlCommand = new SqlCommand("editarcrypto", conexion);

                    sqlCommand.Parameters.AddWithValue("Id", id);
                    sqlCommand.Parameters.AddWithValue("Amount", amount);

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

        public TransferModel VerificarCBU(string CBU)
        {

            var transferModel = new TransferModel();


            using (var conexion = new SqlConnection(cn.getConnection()))
            {
                conexion.Open();

                SqlCommand sqlCommand = new SqlCommand("VerificarCBU", conexion);
                sqlCommand.Parameters.AddWithValue("@CBU", CBU);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                using (var dr = sqlCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        transferModel.ID = Convert.ToInt32(dr["ID"]);
                        transferModel.CBU = dr["CBU"].ToString();
                    }
                }
            }
            return transferModel;

        }
       
        public TransferModel VerificarCBUDollar(string CBU)
        {

            var transferModel = new TransferModel();


            using (var conexion = new SqlConnection(cn.getConnection()))
            {
                conexion.Open();

                SqlCommand sqlCommand = new SqlCommand("verificarCBUDolar", conexion);
                sqlCommand.Parameters.AddWithValue("@CBU", CBU);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                using (var dr = sqlCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        transferModel.ID = Convert.ToInt32(dr["ID"]);
                        transferModel.CBU = dr["CBU"].ToString();
                    }
                }
            }
            return transferModel;

        }

        public AccountCryptocurrencyModel VerificarUUID(Guid UUID)
        {

            var accountCryptocurrency  = new AccountCryptocurrencyModel();


            using (var conexion = new SqlConnection(cn.getConnection()))
            {
                conexion.Open();

                SqlCommand sqlCommand = new SqlCommand("verificarUUID", conexion);
                sqlCommand.Parameters.AddWithValue("@UUID", UUID);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                using (var dr = sqlCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        accountCryptocurrency.ID = Convert.ToInt32(dr["ID"]);
                        accountCryptocurrency.UUID = Guid.Parse(dr["UUID"].ToString());
                    }
                }
            }
            return accountCryptocurrency;

        }

        public List<OperationModel> History(int id)
        {
            var oLista = new List<OperationModel>();
            bool respuesta;
            try
            {
                using (var connection = new SqlConnection(cn.getConnection()))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("Historial", connection);

                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new OperationModel()
                            {
                                ID = Convert.ToInt32(dr["ID"]),
                                Title = dr["INFO"].ToString(),
                                Icon = dr["Imagen"].ToString(),
                                Date = Convert.ToDateTime(dr["Fecha"]),
                                Amount = Convert.ToDouble(dr["Cantidad"])
                            });
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                string error = ex.Message;
            }

            return oLista;
        }


        public bool Comprar(char[] transfer, int userid)
        {
            bool respuesta;

            try
            {
                using (var connection = new SqlConnection(cn.getConnection()))
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand("crypto", connection);


                    //numero de transaccion

                    StringBuilder SB = new StringBuilder();
                    for (int i = 0; i < 22; i++)
                        SB.Append(rnd.Next(0, 9));


                    sqlCommand.Parameters.AddWithValue("@Title", "Nueva Compra");
                    sqlCommand.Parameters.AddWithValue("@Date", DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("@Amount", transfer);
                    sqlCommand.Parameters.AddWithValue("@Number", SB.ToString());
                    sqlCommand.Parameters.AddWithValue("@Icon", "https://i.imgur.com/GCOdUHy.png");

                    sqlCommand.Parameters.AddWithValue("@ID", userid);

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



        public AccountDollarModel DepositoDolar(int id)
        {

            var acountDolar = new AccountDollarModel();

            using (var conexion = new SqlConnection(cn.getConnection()))
            {
                conexion.Open();

                SqlCommand sqlCommand = new SqlCommand("SUMARDolar", conexion);
                sqlCommand.Parameters.AddWithValue("@Id", id);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                using (var dr = sqlCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        acountDolar.ID = Convert.ToInt32(dr["ID"]);
                        acountDolar.AccountBalance = Convert.ToDouble(dr["AccountBalance"]);
                    }
                }
            }
            return acountDolar;

        }
   
        
        public AccountCryptocurrencyModel DepositoCrypto(int id)
        {

            var accountCryptocurrency = new AccountCryptocurrencyModel();

            using (var conexion = new SqlConnection(cn.getConnection()))
            {
                conexion.Open();

                SqlCommand sqlCommand = new SqlCommand("SUMARCrypto", conexion);
                sqlCommand.Parameters.AddWithValue("@Id", id);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                using (var dr = sqlCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        accountCryptocurrency.ID = Convert.ToInt32(dr["ID"]);
                        accountCryptocurrency.AccountBalance = Convert.ToDouble(dr["AccountBalance"]);
                    }
                }
            }
            return accountCryptocurrency;

        }



        public AccountPesoModel Deposito(int id)
        {

            var accountPeso = new AccountPesoModel();


            using (var conexion = new SqlConnection(cn.getConnection()))
            {
                conexion.Open();

                SqlCommand sqlCommand = new SqlCommand("SUMARPESO", conexion);
                sqlCommand.Parameters.AddWithValue("@Id", id);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                using (var dr = sqlCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        accountPeso.ID = Convert.ToInt32(dr["ID"]);
                        accountPeso.AccountBalance = Convert.ToDouble(dr["AccountBalance"]);
                    }
                }
            }
            return accountPeso;

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
                    sqlCommand.Parameters.AddWithValue("Gender", user.Gender);
                    sqlCommand.Parameters.AddWithValue("Birthdate", user.Birthdate);
                    sqlCommand.Parameters.AddWithValue("LocationName", user.LocationName);

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

    }
}
