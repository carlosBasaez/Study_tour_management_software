using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biblioteca
{
    public class Utilities
    {
        public static string DateTimeToString(DateTime dt)
        {
            return $"{dt.Date.Year}-{dt.Date.Month}-{dt.Date.Day}";
        }
    }
}
