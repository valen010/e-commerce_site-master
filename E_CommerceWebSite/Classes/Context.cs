using E_CommerceWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_CommerceWebSite
{
    public class Context
    {
       private static Model1 connection;
        public static Model1 Connection
        {
            get { if (connection == null) connection = new Model1(); return connection; }
            set { connection = value; }

        }

    }
}