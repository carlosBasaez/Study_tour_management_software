using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biblioteca
{
    public class Curso
    {
        public int id_curso;
        public string nombre;
        public string rut_representante;
        public int id_colegio;
        public string nomCole;
        public int meta;

        public static List<Curso> mostrar_curso()
        {
            List<Curso> all_curso = new List<Curso>();
            Curso _curso = null;
            using (OracleConnection ora = new OracleConnection(InicioSesionBD.connection))
            {
                try
                {
                    ora.Open();
                    OracleCommand command = new OracleCommand("SELECT * FROM CURSO CUR JOIN COLEGIO COL ON(COL.IDCOLEGIO=CUR.IDCOLEGIO) JOIN CONTRATO CON ON(CON.IDCURSO=CUR.IDCURSO)", ora);
                    OracleDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        _curso = new Curso();
                        _curso.id_curso = Int32.Parse(reader[0].ToString());
                        _curso.nombre = reader.GetString(1);
                        _curso.rut_representante = reader.GetString(2);
                        _curso.id_colegio = Int32.Parse(reader[3].ToString());
                        _curso.nomCole = reader.GetString(5);
                        _curso.meta = Int32.Parse(reader[10].ToString());
                        all_curso.Add(_curso);
                    }
                }
                catch (Exception ignacio)
                {
                    Console.WriteLine("error.----------------" + ignacio);
                }
                ora.Close();

            }
            return all_curso;
        }

    }
}
