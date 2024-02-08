using E_CommerceWebSite.Classes;
using E_CommerceWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace E_CommerceWebSite.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
     

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(User u, string Remember)
        {
           
            bool result = Membership.ValidateUser(u.UserName, u.Password);
            bool role = Roles.IsUserInRole(u.UserName, "Admin");
            if (result)
            {
               

                // string user = Roles.FindUsersInRole("user", u.UserName).;

                if (Remember == "on")
                    FormsAuthentication.RedirectFromLoginPage(u.UserName, true);
                else FormsAuthentication.RedirectFromLoginPage(u.UserName, false);

                Session["UserName"] =u.UserName;
                Session["UserID"] = u.UserId;
         

                var query = from client in Context.Connection.Client join
               clientadress in Context.Connection.ClientAdress on client.Id equals clientadress.ClientID
                            where client.UserName == u.UserName
                            select new { Client = client, ClientAdress = clientadress };

                //var admin=Context.Connection.aspnet_UsersInRoles.FirstOrDefault(x=>x.UserId==u.UserId)
                if (query.Count() == 0)
                {
                    return RedirectToAction("AddClient", "Member");
                }
                else if (role==true)
                {
                    Session["AdminUser"] = u.UserName;
                    return RedirectToAction("Index", "Admin");
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "User name or password is incorrect";
                ViewBag.result = role.ToString();
                return View();
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Remove("UserName");
            Session.Remove("ActiveCart");
            Session.Remove("AdminUser");
            //Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult ForgotMyPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotMyPassword(User u)
        {
            MembershipUser mu = Membership.GetUser(u.UserName);
          
            if (mu.PasswordQuestion == u.SecurityQuestion)
            {
                string pwd = mu.ResetPassword(u.SqAnswer);
                mu.ChangePassword(pwd, u.Password);
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.message = "Error";
                return View();
            }

        }
     
       
        public ActionResult AddClient()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddClient(Client client,ClientAdress clientAdress)
        {
            
            Context.Connection.Client.Add(client);
            Context.Connection.SaveChanges();
            int id = client.Id;
           
            clientAdress.ClientID = id;
            Context.Connection.ClientAdress.Add(clientAdress);
            Context.Connection.SaveChanges();
            
            return View(client);

        }

        public ActionResult Profile()
        {
            string membership = Session["UserName"].ToString();
           
            var query = Context.Connection.Client.FirstOrDefault(x => x.UserName == membership);
            if (query!=null)
            {
                Client client = Context.Connection.Client.Where(x => x.UserName == membership).FirstOrDefault();
                var adress = Context.Connection.ClientAdress.Where(x => x.ClientID == client.Id).FirstOrDefault();
                ViewBag.Adress = adress;
                return View(client);
            }
            else
                return RedirectToAction("AddClient");
        }
        public ActionResult UpdatePassword(User u,string oldPassword)
        {
            try
            {
                u.UserName =Session["UserName"].ToString();
                MembershipUser membershipUser = Membership.GetUser(u.UserName);
                membershipUser.ChangePassword(oldPassword, u.Password);
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("Profile");

        }
        public ActionResult SignUp()
        {
            return View();
        }
      
    }
   
}