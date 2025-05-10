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
        private Product? productoDesactivado;

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

            MessageBox.Show(logica.AgregarProducto(nuevo) ? "Producto agregado" : "Error al agregar");
            Limpiar();
        }

        private void Actualizar_Click(object sender, RoutedEventArgs e)
        {
            if (productoSeleccionado == null) return;

            productoSeleccionado.Name = txtNombre.Text.Trim();
            productoSeleccionado.Price = decimal.Parse(txtPrecio.Text);
            productoSeleccionado.Stock = int.Parse(txtStock.Text);
            productoSeleccionado.Active = chkActivo.IsChecked == true;

            MessageBox.Show(logica.EditarProducto(productoSeleccionado) ? "Producto actualizado" : "Error al actualizar");
            Limpiar();
        }

        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (productoSeleccionado == null) return;

            MessageBox.Show(logica.EliminarProducto(productoSeleccionado.ProductId) ? "Producto desactivado" : "Error al desactivar");
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
            productoDesactivado = null;
            Refrescar();
            CargarDesactivados_Click(null!, null!);
        }

        private void CargarDesactivados_Click(object sender, RoutedEventArgs e) =>
            dgDesactivados.ItemsSource = logica.ObtenerDesactivados();

        private void dgDesactivados_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            productoDesactivado = dgDesactivados.SelectedItem as Product;

        private void Reactivar_Click(object sender, RoutedEventArgs e)
        {
            if (productoDesactivado == null) return;

            MessageBox.Show(logica.ReactivarProducto(productoDesactivado.ProductId) ? "Producto reactivado" : "Error al reactivar");
            Limpiar();
        }
    }
}
