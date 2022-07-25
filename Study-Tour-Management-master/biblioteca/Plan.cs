using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biblioteca
{
    public class Plan
    {
        public int id_viaje;
        public string nombre;
        public string descripcion;

        public override string ToString()
        {
            return id_viaje + ": " + nombre;
        }
    }
}
