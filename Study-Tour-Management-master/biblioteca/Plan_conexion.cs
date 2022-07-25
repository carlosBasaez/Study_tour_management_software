using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace biblioteca
{
    public class Plan_conexion
    {
        public static List<Plan> mostrar_plan()
        {
            List<Plan> all_plan = new List<Plan>();
            Plan _plan = null;
            using (OracleConnection ora = new OracleConnection(InicioSesionBD.connection))
            {
                try
                {
                    ora.Open();
                    OracleCommand command = new OracleCommand("SELECT * FROM PLAN_VIAJE", ora);
                    OracleDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        _plan = new Plan();
                        _plan.id_viaje = Int32.Parse(reader[0].ToString());
                        _plan.nombre = reader.GetString(1);
                        _plan.descripcion = reader[2].ToString();
                        all_plan.Add(_plan);
                    }
                }
                catch (Exception ignacio)
                {
                    Console.WriteLine("error.----------------" + ignacio);
                }
                ora.Close();

            }
            return all_plan;
        }
    }
}
