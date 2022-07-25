using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace biblioteca
{
    public class Colegio_conexion
    {
        public static List<Colegio> mostrar_colegios()
        {
            List<Colegio> all_colegios = new List<Colegio>();
            Colegio _colegio = null;
            using (OracleConnection ora = new OracleConnection(InicioSesionBD.connection))
            {
                try
                {
                    ora.Open();
                    OracleCommand command = new OracleCommand("SELECT * FROM COLEGIO", ora);
                    OracleDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        _colegio = new Colegio();
                        _colegio.id_colegio = Int32.Parse(reader[0].ToString());
                        _colegio.nombre = reader.GetString(1);
                        _colegio.direccion = reader.GetString(2);
                        _colegio.id_comuna = Int32.Parse(reader[3].ToString());
                        all_colegios.Add(_colegio);
                    }
                }
                catch (Exception ignacio)
                {
                    Console.WriteLine("error.----------------" + ignacio);
                }
                ora.Close();

            }
            return all_colegios;
        }

        public static List<Colegio> mostrar_colegio(int comuna)
        {
            List<Colegio> all_colegio = new List<Colegio>();
            Colegio _colegio = null;
            using (OracleConnection ora = new OracleConnection(InicioSesionBD.connection))
            {
                try
                {
                    ora.Open();
                    OracleCommand command = new OracleCommand($"SELECT * FROM COLEGIO WHERE IDCOMUNA = {comuna}", ora);
                    OracleDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        _colegio = new Colegio();
                        _colegio.id_colegio = Int32.Parse(reader[0].ToString());
                        _colegio.nombre = reader.GetString(1);
                        _colegio.direccion = reader.GetString(2);
                        _colegio.id_comuna = Int32.Parse(reader[3].ToString());
                        all_colegio.Add(_colegio);
                    }
                }
                catch (Exception ignacio)
                {
                    Console.WriteLine("error.----------------" + ignacio);
                }
                ora.Close();

            }
            return all_colegio;
        }

        public static List<string> Colegio_curso(int idCurso)
        {
            List<string> _colegio = null;
            using (OracleConnection ora = new OracleConnection(InicioSesionBD.connection))
            {
                try
                {
                    ora.Open();
                    OracleCommand command = new OracleCommand($"SELECT COLE.NOMBRE,CUR.NOMBRE FROM COLEGIO COLE JOIN CURSO CUR ON(CUR.IDCOLEGIO=COLE.IDCOLEGIO) WHERE IDCURSO = {idCurso}", ora);
                    OracleDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        _colegio = new List<string>();
                        _colegio.Add(reader.GetString(0));
                        _colegio.Add(reader.GetString(1));
                    }

                }
                catch (Exception ignacio)
                {
                    Console.WriteLine("error.----------------" + ignacio);
                }
                ora.Close();

            }
            return _colegio;
        }
    }
}
