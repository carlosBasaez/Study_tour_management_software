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
    /// Lógica de interacción para MenuEjecutivo.xaml
    /// </summary>
    public partial class MenuEjecutivo : Window
    {
        public MenuEjecutivo()
        {
            InitializeComponent();
            lblUsuario.Content = biblioteca.Usuario.NombreCurrent;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window2 window2 = new Window2();
            window2.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window2.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Menu_principal ini = new Menu_principal();
            ini.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ini.Show();
            this.Close();
        }
    }
}
