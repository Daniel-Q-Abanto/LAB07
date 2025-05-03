using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LAB07.Entidad;

namespace LAB07.Datos
{
    public class ProductDAO
    {
        private string connectionString = "Server=localhost,1433;Database=FacturaDB;User Id=Daniel;Password=123456;TrustServerCertificate=True;";

        public List<Product> ListarProductos()
        {
            List<Product> lista = new List<Product>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_ListarProductos", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Product
                            {
                                ProductId = (int)dr["product_id"],
                                Name = dr["name"].ToString(),
                                Price = (decimal)dr["price"],
                                Stock = (int)dr["stock"],
                                Active = (bool)dr["active"]
                            });
                        }
                    }
                }
            }

            return lista;
        }
    }
}