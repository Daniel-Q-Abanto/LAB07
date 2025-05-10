using System.Collections.Generic;
using System.Linq;
using LAB07.Entidad;
using LAB07.Datos;

namespace LAB07.Negocio
{
    public class ProductBO
    {
        private readonly ProductDAO dao = new();

        public List<Product> ObtenerProductos(string nombre) =>
            dao.ListarProductos()
               .Where(p => p.Active) 
               .Where(p => p.Name.ToLower().Contains(nombre.ToLower().Trim()))
               .ToList();

        public bool AgregarProducto(Product p) => dao.InsertarProducto(p);

        public bool EditarProducto(Product p) => dao.ActualizarProducto(p);

        public bool EliminarProducto(int id) => dao.EliminarProducto(id);
    }
}
