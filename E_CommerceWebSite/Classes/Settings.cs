using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;

namespace E_CommerceWebSite.Classes
{
    public class Settings
    {

        public static Size ProductMediumSize
        {
            get
            {
                Size sz = new Size();
                sz.Width = Convert.ToInt32(ConfigurationManager.AppSettings["ProductMediumWidth"]);
                sz.Height = Convert.ToInt32(ConfigurationManager.AppSettings["ProductMediumHeight"]);
                return sz;

            }

        }
        public static Size ProductLargeSize
        {
            get
            {
                Size sz = new Size();
                sz.Width = Convert.ToInt32(ConfigurationManager.AppSettings["ProductLargeWidth"]);
                sz.Height = Convert.ToInt32(ConfigurationManager.AppSettings["ProductLargeHeight"]);
                return sz;

            }

        }
        public static Size ProductSmallSize
        {
            get
            {
                Size sz = new Size();
                sz.Width = Convert.ToInt32(ConfigurationManager.AppSettings["ProductSmallWidth"]);
                sz.Height = Convert.ToInt32(ConfigurationManager.AppSettings["ProductSmallHeight"]);
                return sz;

            }

        }
        public static Size CategoryMediumSize
        {
            get
            {
                Size sz = new Size();
                sz.Width = Convert.ToInt32(ConfigurationManager.AppSettings["CatgeoryWidth"]);
                sz.Height = Convert.ToInt32(ConfigurationManager.AppSettings["CatgeoryHeight"]);
                return sz;

            }

        }
      




    }
}