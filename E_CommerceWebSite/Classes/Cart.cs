using E_CommerceWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_CommerceWebSite.Classes
{
    public class Cart
    {
        private List<Products> products = new List<Products>();

        public List<Products> Products
        {
            get { return products; }
            set { products = value; }
        }

    }
}
