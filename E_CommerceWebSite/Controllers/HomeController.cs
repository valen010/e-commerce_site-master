using E_CommerceWebSite.Classes;
using E_CommerceWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace E_CommerceWebSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(Context.Connection.Category.ToList());
        }
      
        [HttpGet]
        public ActionResult ProductList()
        {

            int id = Convert.ToInt32(Request.QueryString["id"].ToString());
            var products = Context.Connection.Products.Where(x => x.CategoryID == id).ToList();
            ViewBag.categories = Context.Connection.Category.ToList();
            List<Products> p = products.ToList();
            List<Images> i = Context.Connection.Images.Where(x => x.Default == true).ToList();
            var query = from product in p
                        join image in i on product.Id equals image.ProductID into Table1
                        from image in Table1.ToList()
                        select new ProductImage
                        {
                            product = product,
                            Image = image
                        };
                return View(query);
            
        }
        public ActionResult ProductDetail()
        {
           
            int id = Convert.ToInt32(Request.QueryString["id"].ToString());
            TempData["ProductId"]=id;
            TempData.Keep();
            ViewBag.ProductPropertyValues = Context.Connection.ProductProperty.Where(x => x.ProductID == id).ToList();

           List <Client> c = Context.Connection.Client.ToList();
            List<UserComments> u = Context.Connection.UserComments.Where(x => x.IsApproved == true &&x.ProductID==id).ToList();
            var query = from client in c
                        join
            usercomments in u on client.Id equals usercomments.ClientID into Table1
                        from usercomments in Table1.ToList()
                         select new ClientDetails { client = client, usercomments = usercomments };


            Products products = Context.Connection.Products.FirstOrDefault(x => x.Id == id);
            ViewBag.ımage = Context.Connection.Images.Where(x => x.ProductID == id).ToList();
            ViewBag.FirstImage = Context.Connection.Images.Where(x => x.ProductID == id&&x.Default==true ).Take(1);
            ViewBag.Comments =query.ToList();
            return View(products);
          

        }
        [HttpPost]
        public void AddToCart(int id)
        {
            try
            {
                Cart s;
                if (Session["ActiveCart"] == null)
                {
                    s = new Cart();

                }
                else
                {
                    s = (Cart)Session["ActiveCart"];

                }
                Products u = Context.Connection.Products.FirstOrDefault(x => x.Id == id);

                s.Products.Add(u);
               
                Session["ActiveCart"] = s;

            }
            catch (Exception x)
            {

                return;
            }
            finally
            {
                RedirectToAction("Home", "ProductDetail?id=" + id);
               // Response.Redirect("/Home/ProductDetail?id=" + id);
            }
          
           

        }
        
        public ActionResult AssignCookie()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AssignCookie(string CookieName, string CookieValue)
        {
            HttpCookie ck = new HttpCookie(CookieName);
            ck.Value = CookieValue;
            ck.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(ck);
            return View();
        }

        [HttpPost]
        public ActionResult PostComment(string comment)
        {
            int id =(int)TempData["ProductId"];
            var uName=Session["UserName"].ToString();
            Client c = Context.Connection.Client.FirstOrDefault(x => x.UserName ==uName );
            UserComments comments = new UserComments();
            comments.IsApproved = false;
            comments.Comment = comment;
            comments.ClientID =(int)c.Id;
            comments.DateCreated = DateTime.Now;
            comments.ProductID = id;
            Context.Connection.UserComments.Add(comments);
            Context.Connection.SaveChanges();
            return RedirectToAction("Index");
        }
       
        public ActionResult MyCart()
        {
           
            Cart s = new Cart();
            if (Session["ActiveCart"] != null)
            {
                s = (Cart)Session["ActiveCart"];
               
            }

            if (s.Products.Count > 0)
            {
                return View(s.Products.ToList());
            }
            else
                return RedirectToAction("Index");

        }
        public ActionResult Remove(int id)
        {
            Cart s;
            if (Session["ActiveCart"] != null)
            {
                s = (Cart)Session["ActiveCart"];
                Products u = Context.Connection.Products.FirstOrDefault(x => x.Id == id);

                s.Products.Remove(u);

            }
            return RedirectToAction("MyCart");
        }

    }
}