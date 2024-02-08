using E_CommerceWebSite.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Web;
using System.Web.Mvc;
using E_CommerceWebSite.Classes;

namespace E_CommerceWebSite.Controllers
{


    [ValidateAdminSession]
    
    public class AdminController : Controller
    {
        // GET: Admin

        //[Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            ViewBag.TotalUsers = Context.Connection.aspnet_Users.Count();
            ViewBag.TotalComments = Context.Connection.UserComments.Count();
            ViewBag.TotalProducts= Context.Connection.Products.Count();
            return View();
        }
        public ActionResult Products()
        {
            return View(Context.Connection.Products.ToList());

        }
        public ActionResult AddNewProduct()
        {

            ViewBag.Categories = Context.Connection.Category.ToList();
            ViewBag.Brands = Context.Connection.Brands.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult AddNewProduct(Products products)
        {
           
            Context.Connection.Products.Add(products);
            Context.Connection.SaveChanges();
            int id = products.Id;
            Images img = new Images();
            img.Medium = "/Content/ProductImage/Medium/6.jpg";
            img.ProductID = id;
            img.Default = true;
            Context.Connection.Images.Add(img);
            Context.Connection.SaveChanges();
            return RedirectToAction("Products");
        }
        public ActionResult UpdateProduct()
        {
            int id = Convert.ToInt32(Request.QueryString["productId"].ToString());
            TempData["productId"] = id;
            TempData.Keep();
            Products products = Context.Connection.Products.FirstOrDefault(x => x.Id == id);
            ViewBag.Categories = Context.Connection.Category.ToList();
            ViewBag.Brands = Context.Connection.Brands.ToList();
            return View(products);


        }
        [HttpPost]
       // [ValidateAntiForgeryToken]= ensures that data is coming from user-form and not via bots etc
        public ActionResult UpdateProduct(Products products)
        {
            if (ModelState.IsValid)
            {
                int id = (int)TempData["productId"];
                Products product = Context.Connection.Products.Where(x => x.Id == id).FirstOrDefault();
                UpdateModel(product);
                Context.Connection.SaveChanges();
            }
              return RedirectToAction("Products");
        }
        public ActionResult ProductDelete(int productId)
        {

            Products product = Context.Connection.Products.FirstOrDefault(x => x.Id == productId);
            Context.Connection.Products.Remove(product);
            Context.Connection.SaveChanges();
            return RedirectToAction("Products");
        }
       
        public ActionResult Category()
        {
            return View(Context.Connection.Category.ToList());
        }
        public ActionResult AddNewCategory()
        {
            ViewBag.Images = Context.Connection.Images.ToList();
            return View();
        }
        
        [HttpPost]
        public ActionResult AddNewCategory(Category category,HttpPostedFileBase File)
        {
            Images ımages = new Images();
            int ımageId = -1;
            if (File != null)
            {
                Image img = Image.FromStream(File.InputStream);
                int width = Convert.ToInt32(ConfigurationManager.AppSettings["CategoryWidth"].ToString());
                int height = Convert.ToInt32(ConfigurationManager.AppSettings["CategoryHeight"].ToString());
                string name = "/Content/CategoryImage/Medium/" + Guid.NewGuid() + Path.GetExtension(File.FileName);
                //GUID=(Globally unique ıdentifier) uses a  pseudo random 128 bit number------>auto generates a unique ID
                Bitmap bitmap = new Bitmap(img, width, height);
                bitmap.Save(Server.MapPath(name));
                ımages.Medium = name;
                Context.Connection.Images.Add(ımages);
                Context.Connection.SaveChanges();
                ımageId = ımages.Id;
            }
          
            if (ımageId != -1)
            category.ImageID = ımageId;
            Context.Connection.Category.Add(category);
            Context.Connection.SaveChanges();
            return RedirectToAction("Category");
        }



        public ActionResult Brands()
        {
            return View(Context.Connection.Brands.ToList());
        }
        public ActionResult AddNewBrand()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewBrand(Brands brands)
        {
          
            Context.Connection.Brands.Add(brands);
            Context.Connection.SaveChanges();
            return RedirectToAction("Brands");
        }
      
        public ActionResult PropertyType()
        {
            return View(Context.Connection.PropertyType.ToList());

        }
        public ActionResult AddPropertyType()
        {
            return View(Context.Connection.Category.ToList());
            
        }
        [HttpPost]
        public ActionResult AddPropertyType(PropertyType propertyType)
        {
            Context.Connection.PropertyType.Add(propertyType);
            Context.Connection.SaveChanges();
            return RedirectToAction("PropertyType");

        }
        public ActionResult PropertyValues()
        {
            return View(Context.Connection.PropertyValue.ToList());
        }
        public ActionResult AddPropertyValue()
        {

            return View(Context.Connection.PropertyType.ToList());
        }
        [HttpPost]
        public ActionResult AddPropertyValue(PropertyValue propertyValue)
        {
            Context.Connection.PropertyValue.Add(propertyValue);
            Context.Connection.SaveChanges();
            return RedirectToAction("PropertyValues");

        }
        public PartialViewResult ProductPropertyTypeWidget(int? categoryId)
        {
            if (categoryId != null)
            {
                var data = Context.Connection.PropertyType.Where(x => x.CategoryID == categoryId).ToList();
                return PartialView(data);
            }
            else
            {
                var data = Context.Connection.PropertyType.ToList();
                return PartialView(data);
            }
        }
        public PartialViewResult ProductPropertyValueWidget(int? typeId)
        {
            if (typeId != null)
            {
                var data = Context.Connection.PropertyValue.Where(x => x.PropertyTypeID == typeId).ToList();
                return PartialView(data);

            }
            else
            {
                var data = Context.Connection.PropertyValue.ToList();
                return PartialView(data);
            }
        }
        public ActionResult ProductProperties()
        {
            return View(Context.Connection.ProductProperty.ToList());
        }
        public ActionResult AddProductProperty()
        {
            return View(Context.Connection.Products.ToList());
        }
        [HttpPost]
        public ActionResult AddNewProductProperty(ProductProperty productProperty)
        {
            Context.Connection.ProductProperty.Add(productProperty);
            Context.Connection.SaveChanges();
            return RedirectToAction("ProductProperties");
        }
        public ActionResult ProductPropertyDelete(int productId, int typeId, int valueId) ///return(productproperty)
        {
            ProductProperty productProperty = Context.Connection.ProductProperty.FirstOrDefault(x => x.ProductID == productId && x.PropertyValueID == valueId);
            Context.Connection.ProductProperty.Remove(productProperty);
            Context.Connection.SaveChanges();
            return RedirectToAction("ProductProperties");
        }
        public ActionResult AddProductImage(int id)
        {
            return View(id);
        }
        [HttpPost]
        public ActionResult AddProductImage(int uId, HttpPostedFileBase fileUpload)
        {

            if (fileUpload != null)
            {
                Image img = Image.FromStream(fileUpload.InputStream);
                Bitmap MediumImage = new Bitmap(img, Settings.ProductMediumSize);
                Bitmap LargeImage = new Bitmap(img, Settings.ProductLargeSize);
                Bitmap SmallImage = new Bitmap(img, Settings.ProductSmallSize);
                string mediumWay = "/Content/ProductImage/Medium/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);
                string largeWay = "/Content/ProductImage/Large/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);
                string smallWay = "/Content/ProductImage/Small/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);
                MediumImage.Save(Server.MapPath(mediumWay));
                LargeImage.Save(Server.MapPath(largeWay));
                SmallImage.Save(Server.MapPath(smallWay));

                Images ımages = new Images();

                ımages.Large = largeWay;
                ımages.Medium = mediumWay;
                ımages.Small = smallWay;
                ımages.ProductID = uId;
              
                    ımages.Default = false;
                Context.Connection.Images.Add(ımages);
                Context.Connection.SaveChanges();
            }
            return RedirectToAction("Products");

        }
      
       
        public ActionResult CategoryImage(int id)
        {
            return View(id);
        }
        public ActionResult Comments()
        {
            return View(Context.Connection.UserComments.ToList());
        }
       
        public ActionResult SetDefaultImage()
        {
            int id = Convert.ToInt32(Request.QueryString["productId"].ToString());
            TempData["productId"] = id;
            TempData.Keep();

            return View(Context.Connection.Images.Where(x => x.ProductID == id).ToList());
        }
     
        public ActionResult SetAsDefaultImage()
        {
            int id = Convert.ToInt32(Request.QueryString["ImageId"].ToString());
            int pid =(int) TempData["productId"];
           
            var defaultImage = Context.Connection.Images.FirstOrDefault(x => x.Default == true && x.ProductID == pid);
            defaultImage.Default = false;
            var setDefault = Context.Connection.Images.First(x => x.Id == id);
            setDefault.Default = true;
            Context.Connection.SaveChanges();
            return RedirectToAction("Products");
        }
 
        public ActionResult ApproveComment()
        {
            try
            {
                int commentId = Convert.ToInt32(Request.QueryString["Id"].ToString());
                UserComments Comment = Context.Connection.UserComments.FirstOrDefault(x => x.Id == commentId);
                Comment.IsApproved = true;
               
                Context.Connection.SaveChanges();
                
            }
            catch (Exception x)
            {
                Response.Redirect("Error");
            }
           return RedirectToAction("Comments"); 
            

        }
        public ActionResult UserRoles()
        {
            return View(Context.Connection.vw_aspnet_UsersInRoles.ToList());
        }
    }

  
}