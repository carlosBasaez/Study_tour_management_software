using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace biblioteca
{
    public class Comuna_conexion
    {
        public static List<Comuna> mostrar_comunas()
        {
            List<Comuna> all_comunas = new List<Comuna>();
            Comuna _comuna = null;
            using (OracleConnection ora = new OracleConnection(InicioSesionBD.connection))
            {
                try
                {
                    ora.Open();
                    OracleCommand command = new OracleCommand("SELECT * FROM COMUNA", ora);
                    OracleDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        _comuna = new Comuna();
                        _comuna.id_region = Int32.Parse(reader[2].ToString());
                        _comuna.nombre = reader.GetString(1);
                        _comuna.id_comuna = Int32.Parse(reader[0].ToString());
                        all_comunas.Add(_comuna);
                    }
                }
                catch (Exception ignacio)
                {
                    Console.WriteLine("error.----------------" + ignacio);
                }
                ora.Close();

            }
            return all_comunas;
        }

        public static List<Comuna> mostrar_comunas(int region)
        {
            List<Comuna> all_comunas = new List<Comuna>();
            Comuna _comuna = null;
            using (OracleConnection ora = new OracleConnection(InicioSesionBD.connection))
            {
                try
                {
                    ora.Open();
                    OracleCommand command = new OracleCommand($"SELECT * FROM COMUNA WHERE IDREGION = {region}", ora);
                    OracleDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        _comuna = new Comuna();
                        _comuna.id_region = Int32.Parse(reader[2].ToString());
                        _comuna.nombre = reader.GetString(1);
                        _comuna.id_comuna = Int32.Parse(reader[0].ToString());
                        all_comunas.Add(_comuna);
                    }
                }
                catch (Exception ignacio)
                {
                    Console.WriteLine("error.----------------" + ignacio);
                }
                ora.Close();

            }
            return all_comunas;
        }
    }
}
