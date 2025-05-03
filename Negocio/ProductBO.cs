using System.Collections.Generic;
using LAB07.Entidad;
using LAB07.Datos;

namespace LAB07.Negocio
{
    public class ProductBO
    {
        private ProductDAO dao = new ProductDAO();

        
        public List<Product> ObtenerProductos(string nombre)
        {
            var productos = dao.ListarProductos();

            nombre = nombre.ToLower().Trim();
            productos = productos
                .Where(p => p.Name.ToLower().Contains(nombre))
                .ToList();

            return productos;
        }
    }
}