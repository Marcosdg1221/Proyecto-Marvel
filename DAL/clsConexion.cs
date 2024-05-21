using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class clsConexion
    {
        public static string CadenaConexion()
        {
            return "server=siobhantt.database.windows.net;database=LuisaBD;uid=prueba;pwd=fernandoG321;trustServerCertificate=true;";
        }
    }
}