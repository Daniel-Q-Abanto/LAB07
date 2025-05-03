using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LAB07.Negocio;
using System.Collections.Generic;
using System.Linq;
using LAB07.Entidad;

namespace LAB07
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ProductBO logica = new ProductBO();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtBuscar.Text.Trim();
            dgProductos.ItemsSource = logica.ObtenerProductos(nombre);
        }
    }
}