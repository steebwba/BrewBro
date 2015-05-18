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

        public ActionResult ViewGroup()
        {

            return PartialView("Group/View");
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

        public ActionResult StartBrew()
        {
            return PartialView("Brew/Add");
        }
    }
}