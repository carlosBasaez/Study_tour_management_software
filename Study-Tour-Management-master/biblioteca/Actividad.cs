using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biblioteca
{
    public class Actividad
    {
        public int id_actividad;
        public int id_curso;
        public string nombre;
        public string descripcion;
        public float monto_recaudado;

        public static int MontoRecaudado(int idCurso)
        {
            int monto = -1;
            using (OracleConnection ora = new OracleConnection(InicioSesionBD.connection))
            {
                try
                {
                    ora.Open();
                    OracleCommand command = new OracleCommand($"SELECT SUM(RECAUDADO) FROM ACTIVIDAD WHERE IDCURSO = {idCurso}", ora);
                    OracleDataReader reader = command.ExecuteReader();
                    reader.Read();
                    monto = Int32.Parse(reader[0].ToString());
                }
                catch (Exception ignacio)
                {
                    Console.WriteLine("error.----------------" + ignacio);
                }
                ora.Close();

            }
            return monto;
        }

        public static List<Actividad> AllActividades(int idCurso)
        {
            List<Actividad> actividades = new List<Actividad>();
            Actividad _actividad = null;
            using (OracleConnection ora = new OracleConnection(InicioSesionBD.connection))
            {
                try
                {
                    ora.Open();
                    OracleCommand command = new OracleCommand($"SELECT NOMBRE, DESCRIPCION, RECAUDADO FROM ACTIVIDAD WHERE IDCURSO= {idCurso}", ora);
                    OracleDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        _actividad = new Actividad
                        {
                            nombre = reader.GetString(0),
                            descripcion = reader.GetString(1),
                            monto_recaudado = Int32.Parse(reader[2].ToString())
                           
                        };
                        Console.WriteLine(reader.GetString(0));
                        actividades.Add(_actividad);
                    }

                }
                catch (Exception ignacio)
                {
                    Console.WriteLine("error.----------------" + ignacio);
                }
                ora.Close();

            }
            return actividades;
        }
    }
}
