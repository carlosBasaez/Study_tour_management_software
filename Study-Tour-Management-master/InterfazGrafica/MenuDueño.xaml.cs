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
using System.Windows.Shapes;

namespace InterfazGrafica
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class MenuDueño : Window
    {

        public MenuDueño()
        {
            InitializeComponent();
        }

        private void btn_agregar_Click(object sender, RoutedEventArgs e)
        {
            agregar_ejecutivo agr = new agregar_ejecutivo();
            agr.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            agr.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Menu_principal ini = new Menu_principal();
            ini.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ini.Show();
            this.Close();
        }

        public void Btn_generar_reporte_Click(object sender, RoutedEventArgs e)
        {
            grdReportes.Visibility = Visibility.Visible;
            grdGestionDueño.Visibility = Visibility.Collapsed;
            dtgCursosReportes.Items.Clear();
            List<Curso> cursos = Curso.mostrar_curso();
            foreach (var item in cursos)
            {
                var datos = new DatosDataGrid { IdCurso = item.id_curso, Nombre = item.nombre, NomCol = item.nomCole, Meta = item.meta};
                dtgCursosReportes.Items.Add(datos);
            }
        }

        private void DtgCursosReportes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dtgCursosReportes.SelectedCells.Count > 0)
            {
                int idCurso = int.Parse(GetSelectedValue(0,dtgCursosReportes));
                Reporte reporte = new Reporte();
                if (biblioteca.Actividad.MontoRecaudado(idCurso)>0)
                {
                    reporte.pgbProgecionCurso.Value = biblioteca.Actividad.MontoRecaudado(idCurso) * 100 / double.Parse(GetSelectedValue(3, dtgCursosReportes));
                    reporte.txbPorcentaje.Content = (biblioteca.Actividad.MontoRecaudado(idCurso) * 100 / double.Parse(GetSelectedValue(3, dtgCursosReportes))).ToString() + "%";
                }
                else
                {
                    reporte.txbPorcentaje.Content = "0";
                }
                List<Actividad> actividades = biblioteca.Actividad.AllActividades(idCurso);
                foreach (var item in actividades)
                {
                    var datos = new DatosDataGrid { Nombre = item.nombre, Descripcion = item.descripcion, Recaudado = item.monto_recaudado };
                    reporte.dtgTareas.Items.Add(datos);
                }
                reporte.lblColegio.Content = biblioteca.Colegio_conexion.Colegio_curso(idCurso)[0];
                reporte.lblCurso.Content = biblioteca.Colegio_conexion.Colegio_curso(idCurso)[1];
                reporte.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                reporte.Show();
                this.Close();
            }
        }

        private string GetSelectedValue(int index,DataGrid grid)
        {
            DataGridCellInfo cellInfo = grid.SelectedCells[index];
            if (cellInfo == null) return null;

            DataGridBoundColumn column = cellInfo.Column as DataGridBoundColumn;
            if (column == null) return null;

            FrameworkElement element = new FrameworkElement() { DataContext = cellInfo.Item };
            BindingOperations.SetBinding(element, TagProperty, column.Binding);

            return element.Tag.ToString();
        }

        private void BtnVolverRegistro_Click(object sender, RoutedEventArgs e)
        {
            grdGestionDueño.Visibility = Visibility.Visible;
            grdReportes.Visibility = Visibility.Collapsed;
        }

        private void TxtCursoBusqueda_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtCursoBusqueda.Text.Trim() == "Nombre del curso")
            {
                txtCursoBusqueda.Text = "";
            }
        }

        private void TxtCursoBusqueda_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtCursoBusqueda.Text.Trim() == "")
            {
                txtCursoBusqueda.Text = "Nombre del curso";
            }
        }
    }
}
