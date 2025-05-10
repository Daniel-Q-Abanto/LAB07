using System.Windows;
using System.Windows.Controls;
using LAB07.Entidad;
using LAB07.Negocio;

namespace LAB07
{
    public partial class MainWindow : Window
    {
        private readonly ProductBO logica = new();
        private Product? productoSeleccionado;

        public MainWindow()
        {
            InitializeComponent();
            Refrescar();
        }

        private void Refrescar() =>
            dgProductos.ItemsSource = logica.ObtenerProductos("");

        private void Buscar_Click(object sender, RoutedEventArgs e) =>
            dgProductos.ItemsSource = logica.ObtenerProductos(txtBuscar.Text.Trim());

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            var nuevo = new Product
            {
                Name = txtNombre.Text.Trim(),
                Price = decimal.Parse(txtPrecio.Text),
                Stock = int.Parse(txtStock.Text),
                Active = chkActivo.IsChecked == true
            };

            var mensaje = logica.AgregarProducto(nuevo)
                ? "Producto agregado" : "Error al agregar";

            MessageBox.Show(mensaje);
            Limpiar();
        }

        private void Actualizar_Click(object sender, RoutedEventArgs e)
        {
            if (productoSeleccionado == null) return;

            productoSeleccionado.Name = txtNombre.Text.Trim();
            productoSeleccionado.Price = decimal.Parse(txtPrecio.Text);
            productoSeleccionado.Stock = int.Parse(txtStock.Text);
            productoSeleccionado.Active = chkActivo.IsChecked == true;

            var mensaje = logica.EditarProducto(productoSeleccionado)
                ? "Producto actualizado" : "Error al actualizar";

            MessageBox.Show(mensaje);
            Limpiar();
        }

        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (productoSeleccionado == null) return;

            var mensaje = logica.EliminarProducto(productoSeleccionado.ProductId)
                ? "Producto desactivado" : "Error al desactivar";

            MessageBox.Show(mensaje);
            Limpiar();
        }

        private void dgProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            productoSeleccionado = dgProductos.SelectedItem as Product;
            if (productoSeleccionado == null) return;

            txtNombre.Text = productoSeleccionado.Name;
            txtPrecio.Text = productoSeleccionado.Price.ToString();
            txtStock.Text = productoSeleccionado.Stock.ToString();
            chkActivo.IsChecked = productoSeleccionado.Active;
        }

        private void Limpiar()
        {
            txtNombre.Text = txtPrecio.Text = txtStock.Text = "";
            chkActivo.IsChecked = true;
            productoSeleccionado = null;
            Refrescar();
        }
    }
}
