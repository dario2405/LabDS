using LabDS.Models;
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
        internal static bool IsAdmin(HttpSessionStateBase session)
        {
            if (session["username"] != null)
            {
                if ((RoleType)session["role"] == RoleType.Admin)
                {
                    return true;
                }

            }
            return false;
        }internal static bool IsUser(HttpSessionStateBase session)
        {
            if (session["username"] != null)
            {
                if ((RoleType)session["role"] == RoleType.User)
                {
                    return true;
                }

            }
            return false;
        }
    }
}