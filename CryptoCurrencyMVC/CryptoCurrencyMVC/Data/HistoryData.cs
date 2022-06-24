using CryptoCurrencyMVC.Models;
using System.Data;
using System.Data.SqlClient;

namespace CryptoCurrencyMVC.Data
{
    public class HistoryData
    {
        private readonly Connection cn = new Connection();


        //Listo
        public List<OperationModel> Movements(int id)
        {
            var oLista = new List<OperationModel>();
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
                                Title = dr["INFO"].ToString(),
                                Icon = dr["Imagen"].ToString(),
                                Date = Convert.ToDateTime(dr["Fecha"]),
                                Amount = Convert.ToDouble(dr["Cantidad"]),
                                Money = (dr["Money"].ToString())
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


        public List<OperationModel> History(int id)
        {
            var oLista = new List<OperationModel>();
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
                                Title = dr["INFO"].ToString(),
                                Icon = dr["Imagen"].ToString(),
                                Money = dr["Money"].ToString(),
                                Date = Convert.ToDateTime(dr["Fecha"]),
                                Amount = Convert.ToDouble(dr["Cantidad"]
                                )
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

    }
}
