using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InterfazGrafica
{
    /// <summary>
    /// Lógica de interacción para Reporte.xaml
    /// </summary>
    public partial class Reporte : Window
    {
        public Reporte()
        {
            InitializeComponent();
            lblUsu.Content = biblioteca.Usuario.NombreCurrent;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MenuDueño dueño = new MenuDueño();
            dueño.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            dueño.Show();
            this.Close();
        }
        
        private void BtnVolverReporte_Click(object sender, RoutedEventArgs e)
        {
            MenuDueño dueño = new MenuDueño();
            dueño.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            dueño.Btn_generar_reporte_Click(sender, e);
            dueño.Show();
            this.Close();
        }
    }
}
