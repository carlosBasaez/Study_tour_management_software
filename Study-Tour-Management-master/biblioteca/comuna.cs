using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biblioteca
{
    public class Comuna
    {
        public int id_comuna;
        public string nombre;
        public int id_region;

        public override string ToString()
        {
            return id_comuna + ": " + nombre;
        }
    }
    
}
