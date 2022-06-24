using CryptoCurrencyMVC.Models;
using System.Data;
using System.Data.SqlClient;

namespace CryptoCurrencyMVC.Data
{
    public class MoneyData
    {
        private readonly Connection cn = new Connection();

        public bool EditPeso(double amount, int id)
        {
            bool respuesta;

            try
            {

                using (var conexion = new SqlConnection(cn.getConnection()))
                {
                    conexion.Open();

                    SqlCommand sqlCommand = new SqlCommand("EditPeso", conexion);


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
        public bool EditDollar(double amount, int id)
        {
            bool respuesta;

            try
            {

                using (var conexion = new SqlConnection(cn.getConnection()))
                {
                    conexion.Open();

                    //SqlCommand sqlCommand = new SqlCommand("editarcrypto", conexion);

                    SqlCommand sqlCommand = new SqlCommand("EditDollar", conexion);


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


        public TransferModel VerifyCBUPeso(string CBU)
        {

            var transferModel = new TransferModel();


            using (var conexion = new SqlConnection(cn.getConnection()))
            {
                conexion.Open();

                SqlCommand sqlCommand = new SqlCommand("VerifyCBUPeso", conexion);
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

        public TransferModel VerifyCBUDollar(string CBU)
        {

            var transferModel = new TransferModel();


            using (var conexion = new SqlConnection(cn.getConnection()))
            {
                conexion.Open();

                SqlCommand sqlCommand = new SqlCommand("VerifyCBUDollar", conexion);
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

        public AccountCryptocurrencyModel VerifyUUID(Guid UUID)
        {

            var accountCryptocurrency = new AccountCryptocurrencyModel();


            using (var conexion = new SqlConnection(cn.getConnection()))
            {
                conexion.Open();

                SqlCommand sqlCommand = new SqlCommand("VerifyUUID", conexion);
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


        public AccountDollarModel GetDollar(int id)
        {

            var acountDolar = new AccountDollarModel();

            using (var conexion = new SqlConnection(cn.getConnection()))
            {
                conexion.Open();

                SqlCommand sqlCommand = new SqlCommand("GetDollar", conexion);
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


        public AccountCryptocurrencyModel GetCryptocurrency(int id)
        {

            var accountCryptocurrency = new AccountCryptocurrencyModel();

            using (var conexion = new SqlConnection(cn.getConnection()))
            {
                conexion.Open();

                SqlCommand sqlCommand = new SqlCommand("GetCryptocurrency", conexion);
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

        public AccountPesoModel GetPeso(int id)
        {

            var accountPeso = new AccountPesoModel();


            using (var conexion = new SqlConnection(cn.getConnection()))
            {
                conexion.Open();

                SqlCommand sqlCommand = new SqlCommand("GetPeso", conexion);
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


    }
}
