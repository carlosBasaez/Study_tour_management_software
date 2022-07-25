using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace biblioteca
{
    public abstract class Usuario
    {
        public static string RutCurrent = "";
        public static string NombreCurrent = "";
        public string Rut { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public static string connection { get { return InicioSesionBD.connection; } }
        public abstract Usuario Select();
        public abstract Usuario SelectWhere(string Rut);
        public abstract bool SelectInicioSesion();
        public abstract bool Insert(string NRut, string NEmail, string NPass, string NNombre, string NApellido);
        public abstract bool Update(string ARut, string AEmail, string APass, string ANombre, string AApellido);
        public abstract bool Delete(string ERut);

        public bool ExistUsuario()
        {
            return ExistUsuario(Rut);
        }

        public static bool ExistUsuario(string nrut)
        {
            bool exist = false;

            using (OracleConnection ora = new OracleConnection(connection))
            {
                try
                {
                    ora.Open();
                    OracleCommand command = new OracleCommand("SELECT rut FROM USUARIO WHERE rut = :rut", ora);
                    command.Parameters.Add(":rut", OracleDbType.Varchar2).Value = nrut;
                    object obj_user = command.ExecuteScalar();

                    exist = obj_user != null;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error en Usuario: Exist.\n" + Ex.Message);
                }
                ora.Close();
            }

            return exist;
        }

        public static string GetNombre(string nrut)
        {
            string nombre_user = "";

            using (OracleConnection ora = new OracleConnection(connection))
            {
                try
                {
                    ora.Open();
                    OracleCommand command = new OracleCommand("SELECT nombre FROM USUARIO WHERE rut = :rut", ora);
                    command.Parameters.Add(":rut", OracleDbType.Varchar2).Value = nrut;
                    object obj_user = command.ExecuteScalar();
                    if (obj_user != null)
                    {
                        nombre_user = obj_user.ToString();
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error en Usuario: GetNombre.\n" + Ex.Message);
                }
                ora.Close();
            }

            return nombre_user;
        }

        public static string GetApellido(string nrut)
        {
            string apellido_user = "";

            using (OracleConnection ora = new OracleConnection(connection))
            {
                try
                {
                    ora.Open();
                    OracleCommand command = new OracleCommand("SELECT apellido FROM USUARIO WHERE rut = :rut", ora);
                    command.Parameters.Add(":rut", OracleDbType.Varchar2).Value = nrut;
                    object obj_user = command.ExecuteScalar();
                    if (obj_user != null)
                    {
                        apellido_user = obj_user.ToString();
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error en Usuario: GetApellido.\n" + Ex.Message);
                }
                ora.Close();
            }

            return apellido_user;
        }
    }
}