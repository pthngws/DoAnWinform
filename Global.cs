using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn01
{
    internal class Global
    {
        public static string GlobalID { get; private set; }
        public static void setID(string uid)
        {
            GlobalID = uid;
        }
        public static string GlobalRole { get; private set; }
        public static void setRole(string role)
        {
            GlobalRole = role;
        }
    }
}
