using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace proTienda.Class
{
   public class Conexion
    {
       public static string CnnStr
       {
           get
           {
               return ConfigurationManager.ConnectionStrings["ConnectionAccesProTienda"].ConnectionString;
           }
       }
    }
}
