using E_CommerceWebSite;
using E_CommerceWebSite.Classes;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Mvc_template.Controllers
{
    // [Authorize]
    public class UserController : Controller
    {
        // GET: Kullanici
        public ActionResult Index()
        {
            MembershipUserCollection users = Membership.GetAllUsers();
           
            return View(users);
        }
        [AllowAnonymous]
        public ActionResult Add()
        {

            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Add(User u)
        {
            MembershipCreateStatus status;
            Membership.CreateUser(u.UserName, u.Password, u.Email, u.SecurityQuestion, u.SqAnswer, true, out status);
            string message = "";
            switch (status)
            {
                case MembershipCreateStatus.Success:
                    break;
                case MembershipCreateStatus.InvalidUserName:
                    message += "Invalid user name ";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    message += "Invalid Password ";
                    break;
                case MembershipCreateStatus.InvalidQuestion:
                    break;
                case MembershipCreateStatus.InvalidAnswer:
                    message += "Invalid Answer ";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    message += "Invalid Email Adress";
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    message += "Duplicate User Name Error";
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    message += "Duplicated email";
                    break;
                case MembershipCreateStatus.UserRejected:
                    message += "User rejected/blocked ";
                    break;
                case MembershipCreateStatus.InvalidProviderUserKey:
                    message += "This user name is already taken";
                    break;
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    break;
                case MembershipCreateStatus.ProviderError:
                    message += "Provider Error";
                    break;
                default:
                    break;
                  

            }
            ViewBag.Message = message;
            if (status == MembershipCreateStatus.Success)

                return RedirectToAction("Login","Member");
            else
                return View();

        }
       // [Authorize(Roles = "Admin")]
        public ActionResult AssignRole()
        {
            ViewBag.Roles = Roles.GetAllRoles().ToList();
            ViewBag.Users = Membership.GetAllUsers();
            return View();

        }
      
        [HttpPost]
        public ActionResult AssignRole(string UserName, string RoleName)
        {
            Roles.AddUserToRole(UserName, RoleName);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public string UserRoles(string userName)
        {
            List<string> roles = Roles.GetRolesForUser(userName).ToList();
            string role = "";
            foreach (string r in roles)
            {
                role += r + "\n";
            }
           // role = role.Remove(role.Length - 1, 1);
            return role;
        }


    }
}