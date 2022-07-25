using biblioteca;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InterfazGrafica
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class InicioSesion : Window
    {
        public InicioSesion()
        {
            InitializeComponent();
        }

        private void TxtNombreDeUsuario_GotFocus(object sender, RoutedEventArgs e)
        {
            string contenido = txtNombreDeUsuario.Text.Trim();
            if (contenido.Equals("Nombre de usuario"))
            {
                txtNombreDeUsuario.Text = "";
            }
        }

        private void TxtNombreDeUsuario_LostFocus(object sender, RoutedEventArgs e)
        {
            string contenido = txtNombreDeUsuario.Text.Trim();
            if (contenido.Equals(""))
            {
                txtNombreDeUsuario.Text = "Nombre de usuario";
            }
        }

        private void PsbContraseña_GotFocus(object sender, RoutedEventArgs e)
        {
            string contenido = psbContraseña.Password.ToString().Trim();
            if (contenido.Equals("Contraseña"))
            {
                psbContraseña.Password = "";
            }
        }

        private void PsbContraseña_LostFocus(object sender, RoutedEventArgs e)
        {
            string contenido = psbContraseña.Password.ToString().Trim();
            if (contenido.Equals(""))
            {
                psbContraseña.Password = "Contraseña";
            }
        }

        private void BtnIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            if (InicioSesionBD.Prueba(txtNombreDeUsuario.Text,psbContraseña.Password.ToString()))
            {
                Menu_principal principal = new Menu_principal();
                principal.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                principal.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Los Datos No Son Correctos");
            }
        }
    }
}
