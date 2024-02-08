using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace E_CommerceWebSite.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
        // GET: Rol
        public ActionResult Index()
        {
            List<string> roles = Roles.GetAllRoles().ToList();
            return View(roles);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(string RoleName)
        {
          
            Roles.CreateRole(RoleName);
            return RedirectToAction("Index");
        }
    }
}