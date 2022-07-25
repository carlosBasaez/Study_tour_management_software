using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biblioteca
{
    public class Region
    {
        public int id_region;
        public string nombre;

        public override string ToString()
        {
            return id_region + ": " + nombre;
        }
    }
}
