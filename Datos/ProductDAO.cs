using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using LAB07.Entidad;

namespace LAB07.Datos
{
    public class ProductDAO
    {
        private readonly string connectionString =
            "Server=localhost,1433;Database=FacturaDB;User Id=Daniel;Password=123456;TrustServerCertificate=True;";

        public List<Product> ListarProductos()
        {
            var lista = new List<Product>();
            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand("sp_ListarProductos", conn) { CommandType = CommandType.StoredProcedure };
            conn.Open();
            using var dr = cmd.ExecuteReader();
            while (dr.Read())
                lista.Add(new Product
                {
                    ProductId = (int)dr["product_id"],
                    Name = dr["name"].ToString()!,
                    Price = (decimal)dr["price"],
                    Stock = (int)dr["stock"],
                    Active = (bool)dr["active"]
                });
            return lista;
        }

        public bool InsertarProducto(Product p) => Ejecutar("sp_InsertarProducto", p);

        public bool ActualizarProducto(Product p) => Ejecutar("sp_ActualizarProducto", p);

        public bool EliminarProducto(int id) =>
            Ejecutar("sp_EliminarProducto", new Product { ProductId = id });

        private bool Ejecutar(string sp, Product p)
        {
            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand(sp, conn) { CommandType = CommandType.StoredProcedure };
            conn.Open();

            if (sp != "sp_InsertarProducto") cmd.Parameters.AddWithValue("@product_id", p.ProductId);
            if (sp != "sp_EliminarProducto")
            {
                cmd.Parameters.AddWithValue("@name", p.Name);
                cmd.Parameters.AddWithValue("@price", p.Price);
                cmd.Parameters.AddWithValue("@stock", p.Stock);
                cmd.Parameters.AddWithValue("@active", p.Active);
            }

            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
