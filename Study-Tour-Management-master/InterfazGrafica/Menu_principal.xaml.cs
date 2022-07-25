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
using biblioteca;

namespace InterfazGrafica
{
    /// <summary>
    /// Lógica de interacción para Menu_principal.xaml
    /// </summary>
    public partial class Menu_principal : Window
    {
        public Menu_principal()
        {
            InitializeComponent();
            Preparar();
        }

        private void Preparar()
        {
            Dueño dueño = new Dueño();
            Representante representante = new Representante();
            EjecutivoVentas ejecutivoVentas = new EjecutivoVentas();
            Apoderado apoderado = new Apoderado();
            if (dueño.SelectInicioSesion())
            {
                Dueño_button.Visibility = Visibility.Visible;
            }
            if (representante.SelectInicioSesion())
            {
                Representante_button.Visibility = Visibility.Visible;
            }
            if (ejecutivoVentas.SelectInicioSesion())
            {
                Ejecutivo_ventas_button.Visibility = Visibility.Visible;
            }
            if (apoderado.SelectInicioSesion())
            {
                Apoderado_button.Visibility = Visibility.Visible;
            }
        }

        private void Ejecutivo_ventas_button_Click(object sender, RoutedEventArgs e)
        {
            MenuEjecutivo eje = new MenuEjecutivo();
            eje.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            eje.lblUsuario.Content = biblioteca.Usuario.NombreCurrent;
            eje.Show();
            this.Close();
        }

        private void Salir_button_Click(object sender, RoutedEventArgs e)
        {
            InicioSesion ini = new InicioSesion();
            ini.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ini.Show();
            this.Close();
        }

        private void Apoderado_button_Click(object sender, RoutedEventArgs e)
        {
            InterfazApoderado apo = new InterfazApoderado();
            apo.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            apo.lblUsario.Content = biblioteca.Usuario.NombreCurrent;
            apo.Show();
            this.Close();
        }

        private void Dueño_button_Click(object sender, RoutedEventArgs e)
        {
            MenuDueño due = new MenuDueño();
            due.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            due.lblUsu.Content = biblioteca.Usuario.NombreCurrent;
            due.Show();
            this.Close();
        }

        private void Representante_button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
