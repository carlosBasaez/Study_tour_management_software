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
    /// Lógica de interacción para InterfazApoderado.xaml
    /// </summary>
    public partial class InterfazApoderado : Window
    {
        public InterfazApoderado()
        {
            InitializeComponent();
            lblUsario.Content = Usuario.NombreCurrent;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Salir
            Menu_principal mp = new Menu_principal();
            mp.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mp.Show();

            this.Close();
        }
    }
}
