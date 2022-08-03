using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace LabDS.App_Start
{
    internal static class Utils
    {
        internal static string DB_CONNECTION_STRING { get; private set; }
        internal static void ReadConfig()
        {
            DB_CONNECTION_STRING = ConfigurationManager.ConnectionStrings["LabDSDbConnectionString"].ConnectionString;
        }
    }
}