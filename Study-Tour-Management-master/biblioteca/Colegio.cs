using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biblioteca
{
    public class Colegio
    {
        public int id_colegio;
        public string nombre;
        public string direccion;
        public int id_comuna;

        public override string ToString()
        {
            return id_colegio + ": " + nombre;
        }
    }
}
