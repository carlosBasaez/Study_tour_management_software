using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biblioteca
{
    public class EjecutivoVentas : Usuario
    {
        public override bool Delete(string ERut)
        {
            using (OracleConnection ora = new OracleConnection(connection))
            {
                try
                {
                    ora.Open();
                    OracleCommand command = new OracleCommand("DELETE FROM EJECUTIVO_VENTA WHERE RUT=:RUT", ora);
                    command.Parameters.Add(":RUT", OracleDbType.Varchar2).Value = "123";
                    command.ExecuteNonQuery();
                }
                catch (Exception CarlosXualo)
                {
                    Console.WriteLine("error--------:" + CarlosXualo);
                }
                ora.Close();
            }
            return true;
        }

        public bool ExistEjecutivo()
        {
            return ExistEjecutivo(Rut);
        }

        public static bool ExistEjecutivo(string nrut)
        {
            bool exist = false;

            using (OracleConnection ora = new OracleConnection(connection))
            {
                try
                {
                    ora.Open();
                    OracleCommand command = new OracleCommand("SELECT rutEjecutivoVenta FROM Ejecutivo_Venta WHERE rutEjecutivoVenta = :rut", ora);
                    command.Parameters.Add(":rut", OracleDbType.Varchar2).Value = nrut;
                    object obj_user = command.ExecuteScalar();

                    exist = obj_user != null;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error en Ejecutivo Venta: Exist.\n" + Ex.Message);
                }
                ora.Close();
            }

            return exist;
        }

        public bool Insert()
        {
            return Insert(Rut, Email, Pass, Nombre, Apellido);
        }

        public static bool Insert(EjecutivoVentas ejecutivo)
        {
            return ejecutivo.Insert();
        }

        public override bool Insert(string NRut, string NEmail, string NPass, string NNombre, string NApellido)
        {
            bool insertCorrect = false;
            using (OracleConnection ora = new OracleConnection(connection))
            {
                ora.Open();
                OracleCommand command = new OracleCommand(null,ora);
                OracleTransaction transaction = ora.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                command.Transaction = transaction;
                try
                {
                    //Exist user?
                    command.CommandText = "SELECT rut FROM usuario WHERE rut = :rut";
                    command.Parameters.Add(":rut", OracleDbType.Varchar2).Value = NRut;
                    object scalar_user = command.ExecuteScalar();

                    if (scalar_user == null)
                    {
                        //insert user
                        command.Parameters.Clear();
                        command.CommandText = "INSERT INTO USUARIO VALUES(:RUT,:EMAIL,:PASS,:NOMBRE,:APELLIDO)";
                        command.Parameters.Add(":RUT", OracleDbType.Varchar2).Value = NRut;
                        command.Parameters.Add(":EMAIL", OracleDbType.Varchar2).Value = NEmail;
                        command.Parameters.Add(":PASS", OracleDbType.Varchar2).Value = NPass;
                        command.Parameters.Add(":NOMBRE", OracleDbType.Varchar2).Value = NNombre;
                        command.Parameters.Add(":APELLIDO", OracleDbType.Varchar2).Value = NApellido;
                        command.ExecuteNonQuery();
                    }

                    //exist ejecutivo?
                    command.Parameters.Clear();
                    command.CommandText = "SELECT rutEjecutivoVenta FROM Ejecutivo_Venta WHERE rutEjecutivoVenta = :rut";
                    command.Parameters.Add(":rut", OracleDbType.Varchar2).Value = NRut;
                    object scalar_ejec = command.ExecuteScalar();

                    if (scalar_ejec == null)
                    {
                        //insert ejecutivo
                        command.Parameters.Clear();
                        command.CommandText = "INSERT INTO Ejecutivo_Venta VALUES(:RUT)";
                        command.Parameters.Add(":RUT", OracleDbType.Varchar2).Value = NRut;
                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    insertCorrect = true;
                }
                catch (Exception jungla)
                {
                    transaction.Rollback();
                    Console.WriteLine("Error: Ejecutivo de ventas insert.\n" + jungla.Message);
                    insertCorrect = false;
                }
                ora.Close();
            }
            return insertCorrect;
        }

        public override Usuario Select()
        {
            EjecutivoVentas ejecutivo=null;
            using (OracleConnection ora = new OracleConnection(connection))
            {
                try
                {
                    ora.Open();
                    OracleCommand command = new OracleCommand(null, ora);
                    command.CommandText = $"SELECT rutEjecutivoVenta FROM Ejecutivo_Venta WHERE rutEjecutivoVenta = '{Rut}'";
                    object obj_ejec = command.ExecuteScalar();


                    if (obj_ejec != null) {
                        command.CommandText = $"SELECT * FROM USUARIO WHERE rut = '{Rut}'";
                        OracleDataReader lector = command.ExecuteReader();
                        if (lector.Read())
                        {
                            ejecutivo = new EjecutivoVentas();
                            ejecutivo.Rut = lector["rut"].ToString();
                            ejecutivo.Email = lector["email"].ToString();
                            ejecutivo.Pass = lector["pass"].ToString();
                            ejecutivo.Nombre = lector["nombre"].ToString();
                            ejecutivo.Apellido = lector["apellido"].ToString();
                        }
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error en ejecutivo de ventas, Select.\n"+Ex.Message);
                }
                ora.Close();
            }
            return ejecutivo;
        }

        public EjecutivoVentas New_SelectEjecutivoVenta()
        {
            return Select() as EjecutivoVentas;
        }

        public void SelectEjecutivoVenta(string rut)
        {
            EjecutivoVentas aux = New_SelectEjecutivoVenta(rut);
            Rut = aux.Rut;
            Email = aux.Email;
            Pass = aux.Pass;
            Nombre = aux.Nombre;
            Apellido = aux.Apellido;
        }

        public static EjecutivoVentas New_SelectEjecutivoVenta(string rut)
        {
            EjecutivoVentas ejv = new EjecutivoVentas();
            ejv.Rut = rut;
            return ejv.New_SelectEjecutivoVenta();
        }
        

        public override bool SelectInicioSesion()
        {
            bool indicador = false;
            using (OracleConnection ora = new OracleConnection(connection))
            {
                try
                {
                    ora.Open();
                    OracleCommand command = new OracleCommand("SELECT * FROM EJECUTIVO_VENTA WHERE RUTEJECUTIVOVENTA = :RUT", ora);
                    command.Parameters.Add(":RUT", OracleDbType.Varchar2).Value = RutCurrent;
                    OracleDataReader lector = command.ExecuteReader();
                    if (lector.Read())
                    {
                        indicador = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                ora.Close();
            }
            return indicador;
        }

        public override Usuario SelectWhere(string Rut)
        {
            throw new NotImplementedException();
        }

        public override bool Update(string ARut, string AEmail, string APass, string ANombre, string AApellido)
        {
            throw new NotImplementedException();
        }
    }
}
