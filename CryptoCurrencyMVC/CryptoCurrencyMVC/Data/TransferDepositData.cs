using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CryptoCurrencyMVC.Data
{
    public class TransferDepositData
    {
        private readonly Connection cn = new Connection();
        Random rnd = new Random();


        //Listo

        public bool TransferDeposit(double transfer, int userid, int id, string type)
        {
            bool respuesta;

            try
            {
                using (var connection = new SqlConnection(cn.getConnection()))
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand("TransferDeposit", connection);


                    //numero de transaccion

                    StringBuilder SB = new StringBuilder();
                    for (int i = 0; i < 22; i++)
                        SB.Append(rnd.Next(0, 9));

                    if(type == "Peso")
                    {
                        sqlCommand.Parameters.AddWithValue("@Money", "$");
                    }
                    else
                    {
                        if (type == "Dolar")
                        {
                            sqlCommand.Parameters.AddWithValue("@Money", "USD");
                        }
                        else
                        {
                            sqlCommand.Parameters.AddWithValue("@Money", "BTC");
                        }
                    }

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


        public bool EditCryptocurrency(char[] amount, int id)
        {
            bool respuesta;

            try
            {

                using (var conexion = new SqlConnection(cn.getConnection()))
                {
                    conexion.Open();

                    SqlCommand sqlCommand = new SqlCommand("EditCryptocurrency", conexion);

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
        //Listo

        public bool Buy(char[] transfer, int userid, string type, string moneda)
        {
            bool respuesta;

            try
            {
                using (var connection = new SqlConnection(cn.getConnection()))
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand("BuyCryptocurrency", connection);


                    //numero de transaccion

                    StringBuilder SB = new StringBuilder();
                    for (int i = 0; i < 22; i++)
                        SB.Append(rnd.Next(0, 9));

                    if (type == "Venta")
                    {
                        sqlCommand.Parameters.AddWithValue("@Title", "Vendiste Criptomonedas");
                    }
                    else
                    {
                        sqlCommand.Parameters.AddWithValue("@Title", "Compraste Criptomonedas");
                    }

                    if(moneda == "Peso")
                    {
                        sqlCommand.Parameters.AddWithValue("@Money", "$");
                    }
                    else if(moneda == "Dolar")
                    {
                        sqlCommand.Parameters.AddWithValue("@Money", "USD");
                    }
                    else
                    {
                        sqlCommand.Parameters.AddWithValue("@Money", "BTC");

                    }

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



    }
}
