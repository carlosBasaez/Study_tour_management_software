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
    /// Lógica de interacción para agregar_ejecutivo.xaml
    /// </summary>
    public partial class agregar_ejecutivo : Window
    {
        public agregar_ejecutivo()
        {
            InitializeComponent();
            lblUsuario.Content = Usuario.NombreCurrent;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MenuDueño ini = new MenuDueño();
            ini.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ini.Show ();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MenuDueño mend = new MenuDueño();
            mend.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mend.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if(txt_rut_usuario.GetLineLength(0) != 10)
            {
                MessageBox.Show("El largo de rut debe ser 10");
                return;
            }

            EjecutivoVentas ej = new EjecutivoVentas();
            ej.Rut = txt_rut_usuario.Text;
            ej.Email = txt_email.Text;
            ej.Pass = txt_contraseña.Text;
            ej.Nombre = txt_nombre.Text;
            ej.Apellido = txt_apellido.Text;
            if (ej.Insert())
            {
                MessageBox.Show("Ejecutivo de ventas creado con exito");
                txt_rut_usuario.Clear();
                txt_email.Clear();
                txt_contraseña.Clear();
                txt_nombre.Clear();
                txt_apellido.Clear();

            }
            else
            {
                MessageBox.Show("No se pudo insertar al ejecutivo de ventas");
            }
            
        }

        private void txt_rut_usuario_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_rut_usuario.GetLineLength(0) == 10)
            {
                EjecutivoVentas ej = EjecutivoVentas.New_SelectEjecutivoVenta(txt_rut_usuario.Text);
                if (ej != null)
                {
                    txt_email.Text = ej.Email;
                    txt_contraseña.Text = ej.Pass;
                    txt_nombre.Text = ej.Nombre;
                    txt_apellido.Text = ej.Apellido;
                }
            }
            
        }
    }
}
