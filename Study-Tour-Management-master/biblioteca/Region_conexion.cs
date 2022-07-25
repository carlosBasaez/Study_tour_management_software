using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace biblioteca
{
    public class Region_conexion
    {
        public static List<Region> Mostrar_regiones()
        {
            List<Region> All_regions = new List<Region>();
            Region _region = null;
            using (OracleConnection ora = new OracleConnection(InicioSesionBD.connection))
            {
                try
                {
                    ora.Open();
                    OracleCommand command = new OracleCommand("SELECT * FROM REGION", ora);
                    OracleDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        _region = new Region();
                        _region.id_region = Int32.Parse(reader[0].ToString());
                        _region.nombre = reader.GetString(1);
                        All_regions.Add(_region);
                        
                    }

                }
                catch(Exception ignacio)
                {
                    Console.WriteLine("error.----------------" + ignacio);
                }
                ora.Close();
                
            }
            return All_regions;
        }
    }
}
