using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrewBro.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Landing()
        {
            return View();
        }

        public ActionResult Groups()
        {

            return PartialView("Group/Index");
        }

        //TODO Move to correct controllers
        public ActionResult NewGroup()
        {
            return PartialView("Group/AddEdit");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return PartialView("User/Login");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return PartialView("User/Register");
        }
    }
}