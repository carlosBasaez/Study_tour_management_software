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
    /// Lógica de interacción para Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
            regiones = Region_conexion.Mostrar_regiones();
            regiones_mostrar = new List<string>();
            foreach (var item in regiones)
            {
                regiones_mostrar.Add(item.ToString());
            }
            cboRegion.ItemsSource = regiones_mostrar;

            planes = Plan_conexion.mostrar_plan();
            planes_mostrar = new List<string>();
            foreach (var item in planes)
            {
                planes_mostrar.Add(item.ToString());
            }
            id_plan_cbo.ItemsSource = planes_mostrar;

            lblUsu.Content = Usuario.NombreCurrent;
        }

        List<Region> regiones;
        List<string> regiones_mostrar;

        List<Comuna> comunas;
        List<string> comunas_mostrar;

        List<Colegio> colegios;
        List<string> colegios_mostrar;

        Contrato contrato = new Contrato();

        Curso curso = new Curso();

        List<Plan> planes;
        List<string> planes_mostrar;

        private void cboRegion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            if (cboRegion.SelectedIndex >= 0)
            {
                Region reg = regiones[cboRegion.SelectedIndex];
                comunas = Comuna_conexion.mostrar_comunas(reg.id_region);
                comunas_mostrar = new List<string>();
                foreach (var item in comunas)
                {
                    comunas_mostrar.Add(item.ToString());
                }
            }
            else
            {
                comunas_mostrar = null;
            }
            cboComuna.ItemsSource = comunas_mostrar;
            cboComuna.SelectedIndex = -1;

        }
        private void cboComuna_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboComuna.SelectedIndex >= 0)
            {
                Comuna com = comunas[cboComuna.SelectedIndex];
                colegios = Colegio_conexion.mostrar_colegio(com.id_comuna);
                colegios_mostrar = new List<string>();
                foreach (var item in colegios)
                {
                    colegios_mostrar.Add(item.ToString());
                }
            }
            else
            {
                colegios_mostrar = null;
            }
            cboColegio.ItemsSource = colegios_mostrar;
            cboColegio.SelectedIndex = -1;
        }
    
        private void BtnConfirmarCurso_Click(object sender, RoutedEventArgs e)
        {
            if (cboColegio.SelectedIndex==-1)
            {
                MessageBox.Show("Colegio no existe");
                return;
            }
            else if (txtCurso.Text.Length != 4)
            {
                MessageBox.Show("Curso no válido, tiene que ser 4 digitos obligatorio");
                return;
            }
            else
            {
                curso.id_colegio = colegios[cboColegio.SelectedIndex].id_colegio;
                curso.nombre = txtCurso.Text;
            }

            grdCurso.Visibility = Visibility.Collapsed;
            grdAlumnos.Visibility = Visibility.Visible;
        }

        private void BtnAtrasCurso_Click(object sender, RoutedEventArgs e)
        {
            MenuEjecutivo ejecutivo = new MenuEjecutivo();
            ejecutivo.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ejecutivo.Show();
            this.Close();
            vaciar();
        }

        private void BtnAnadirAlumno_Click(object sender, RoutedEventArgs e)
        {
            grdAñadirAlumnos.Visibility = Visibility.Visible;
        }

        private void BtnSiguienteAlumno_Click(object sender, RoutedEventArgs e)
        {
            grdAlumnos.Visibility = Visibility.Collapsed;
            grdRegContrato.Visibility = Visibility.Visible;
        }

        
        private void BtnCerrarAñadirAlumnos_Click(object sender, RoutedEventArgs e)
        {
            grdAñadirAlumnos.Visibility = Visibility.Collapsed;
        }

        List<Alumno> Alumnos = new List<Alumno>();
        List<Apoderado> Apoderados = new List<Apoderado>();
        private void BtnNuevoAlumno_Click(object sender, RoutedEventArgs e)
        {
            if (txtrutALumno1.Text.Length >= 9 && txtNombreAlumno1.Text.Trim().Length > 0 && txtapellidoAlumno1.Text.Trim().Length > 0 && txtrutApoderado1.Text.Trim().Length >= 9 && txtNombreApoderado1.Text.Trim().Length > 0 && txtNombreApoderado1.Text.Trim().Length > 0 && txtApellidoApoderado1.Text.Trim().Length > 0 && txtEmailApoderado1.Text.Trim().Length > 0 && txtNombreApoderado1.Text.Trim().Length > 0)
            {
                var datos = new DatosDataGrid { RutAlum = txtrutALumno1.Text, NomAlum = txtNombreAlumno1.Text, ApeAlum = txtapellidoAlumno1.Text, RutApo = txtrutApoderado1.Text, NomApo = txtNombreApoderado1.Text, ApeApo = txtApellidoApoderado1.Text };
                var alumnos = new Alumno { rut_alumno = txtrutALumno1.Text, nombre = txtNombreAlumno1.Text, apellido = txtapellidoAlumno1.Text, rut_apoderado = txtrutApoderado1.Text };
                Alumnos.Add(alumnos);
                var apoderados = new Apoderado { Nombre = txtNombreApoderado1.Text, Email = txtEmailApoderado1.Text, Apellido = txtApellidoApoderado1.Text, Rut = txtrutApoderado1.Text, Pass = txtrutApoderado1.Text.ToString().Substring(0, 3) };
                Apoderados.Add(apoderados);
                dtgCurso.Items.Add(datos);

                txtrutALumno1.Text = string.Empty;
                txtNombreAlumno1.Text = string.Empty;
                txtapellidoAlumno1.Text = string.Empty;

                txtrutApoderado1.Text = string.Empty;
                txtNombreApoderado1.Text = string.Empty;
                txtApellidoApoderado1.Text = string.Empty;
                txtEmailApoderado1.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("No pueden haber campos vaciós");
            }
        }
        
        private void BtnRetrocederContrato_Click(object sender, RoutedEventArgs e)
        {
            grdRegContrato.Visibility = Visibility.Collapsed;
            grdAlumnos.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MenuEjecutivo eje = new MenuEjecutivo();
            eje.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            eje.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            contrato.monto_objetivo = Utilities.TextBoxToInt(montotxt);
            if (contrato.monto_objetivo == -1)
            {
                MessageBox.Show("Monto objetivo es incorrecto");
                return;
            }
            if (id_plan_cbo.SelectedIndex == -1)
            {
                MessageBox.Show("El plan es incorrecto");
                return;
            }
            else
            {
                contrato.id_plan_viaje = planes[id_plan_cbo.SelectedIndex].id_viaje;
            }
            if (fecha_creacion_date.Text.Trim().Length == 0 && fecha_viaje_cbo.Text.Trim().Length == 0)
            {
                MessageBox.Show("Fecha incorrecta");
                return;
            }
            else
            {
                contrato.fecha_viaje = fecha_viaje_cbo.SelectedDate.Value;
                contrato.fecha_creacion = fecha_creacion_date.SelectedDate.Value;
            }
            if (observaciones_txt.Text.Trim().Length==0)
            {
                MessageBox.Show("Ningún campo puede estar vacío");
                return;
            }
            else
            {
                contrato.observaciones = observaciones_txt.Text;
            }
            CargarDatos();
        }


        void CargarDatos()
        {
            
            if (Contrato_conexion.CreareContrato(contrato, curso, representante, Apoderados, Alumnos))
            {
                MessageBox.Show("Contrato creado");
                MenuEjecutivo eje = new MenuEjecutivo();
                eje.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                eje.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Error en la creacion del contrato");
            }
            
        }
            

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MenuEjecutivo ini = new MenuEjecutivo();
            ini.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ini.Show();
            this.Close();
        }

        private void BtnEliminarFila_Click(object sender, RoutedEventArgs e)
        {
            int indice = dtgCurso.SelectedIndex;
            if (indice>=0)
            {
                dtgCurso.Items.RemoveAt(indice);
                Alumnos.RemoveAt(indice);
                Apoderados.RemoveAt(indice);
            }
            else
            {
                MessageBox.Show("Primero seleccione un alumno para eliminar");
            }

        }

        Representante representante;
        private void BtnValidarRepre_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Alumnos.LongCount(); i++)
            {
                if (Alumnos[i].rut_alumno.Equals(txtRutRepresentante.Text) && txtEmailRepresentante.Text.Trim().Length>0)
                {
                    representante = new Representante { Rut = Alumnos[i].rut_alumno, Nombre = Alumnos[i].nombre, Apellido = Alumnos[i].rut_alumno, Email = txtEmailRepresentante.Text, Pass = Alumnos[i].rut_alumno.Substring(0, 3) };
                    MessageBox.Show("Representante añadido correctamente");
                    grdBusquedaRepr.Visibility = Visibility.Collapsed;
                    txtRutRepresentante.Clear();
                    txtEmailRepresentante.Clear();
                    break;
                }
                else
                {
                    representante = null;
                }
            }
            if (representante == null)
            {
                MessageBox.Show("Hay campos vacíos o el Rut no está registrado");
            }
        }

        private void BtnCerrarRepre_Click(object sender, RoutedEventArgs e)
        {
            grdBusquedaRepr.Visibility = Visibility.Collapsed;
            txtRutRepresentante.Clear();
            txtEmailRepresentante.Clear();
        }

        private void BtnAtrasContra_Click(object sender, RoutedEventArgs e)
        {
            grdRegContrato.Visibility = Visibility.Collapsed;
            grdBusquedaRepr.Visibility = Visibility.Collapsed;
            grdAlumnos.Visibility = Visibility.Visible;
        }

        private void BtnMostrarRepre_Click(object sender, RoutedEventArgs e)
        {
            grdBusquedaRepr.Visibility = Visibility.Visible;
        }

        private void TxtrutApoderado1_TextChanged(object sender, TextChangedEventArgs e)
        {   
            if (txtrutApoderado1.Text.Length==10)
            {
                Apoderado apoderado;
                apoderado = Apoderado.AutoCompletar(txtrutApoderado1.Text);
                if (apoderado != null)
                {
                    txtNombreApoderado1.Text = apoderado.Nombre;
                    txtEmailApoderado1.Text = apoderado.Email;
                    txtApellidoApoderado1.Text = apoderado.Apellido;
                    txtNombreApoderado1.IsReadOnly = true;
                    txtEmailApoderado1.IsReadOnly = true;
                    txtApellidoApoderado1.IsReadOnly = true;
                }
            }
            if (txtrutApoderado1.Text.Length == 0)
            {
                txtNombreApoderado1.Clear();
                txtEmailApoderado1.Clear();
                txtApellidoApoderado1.Clear();
                txtNombreApoderado1.IsReadOnly = false;
                txtEmailApoderado1.IsReadOnly = false;
                txtApellidoApoderado1.IsReadOnly = false;
            }
        }

        private void BtnAtrasAlumno_Click(object sender, RoutedEventArgs e)
        {
            grdAlumnos.Visibility = Visibility.Collapsed;
            grdCurso.Visibility = Visibility.Visible;
        }

        public void vaciar()
        {
            observaciones_txt.Clear();
            txtapellidoAlumno1.Clear();
            txtApellidoApoderado1.Clear();
            txtCurso.Clear();
            txtEmailApoderado1.Clear();
            txtEmailRepresentante.Clear();
            txtNombreAlumno1.Clear();
            txtNombreApoderado1.Clear();
            txtrutALumno1.Clear();
            txtrutApoderado1.Clear();
            txtRutRepresentante.Clear();
            montotxt.Clear();

            cboColegio.SelectedIndex = -1;
            cboComuna.SelectedIndex = -1;
            cboRegion.SelectedIndex = -1;
            id_plan_cbo.SelectedIndex = -1;

            fecha_viaje_cbo.Text = string.Empty;
            fecha_creacion_date.Text = string.Empty;



        }

        private void txtrutALumno1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
