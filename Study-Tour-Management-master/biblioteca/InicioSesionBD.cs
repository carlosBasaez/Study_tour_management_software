using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biblioteca
{
    public class InicioSesionBD
    {
        public static string connection = "DATA SOURCE = xe ; " +
            "PASSWORD = 123 ; " +
            "USER ID = STUDY_TOUR_MANAGEMENT";
        public static bool Prueba(string email, string pass)
        {
            bool indicador = false;
            using (OracleConnection ora = new OracleConnection(connection))
            {
                try
                {
                    ora.Open();
                    OracleCommand command = new OracleCommand("SELECT RUT,NOMBRE||' '||APELLIDO FROM USUARIO WHERE :EMAIL IN (RUT, EMAIL) AND PASS=:PASS", ora);
                    command.Parameters.Add(":EMAIL", OracleDbType.Varchar2).Value = email;
                    command.Parameters.Add(":PASS", OracleDbType.Varchar2).Value = pass;
                    OracleDataReader lector = command.ExecuteReader();
                    if (lector.Read())
                    {
                        indicador = true;
                        Usuario.RutCurrent = lector.GetString(0);
                        Usuario.NombreCurrent = lector.GetString(1);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(">>>>>>>" + ex);
                }
                ora.Close();
            }
            return indicador;
        }
    }
}
