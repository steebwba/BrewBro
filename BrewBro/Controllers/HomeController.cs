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


        public ActionResult Groups()
        {

            return View();
        }

        //TODO Move to correct controllers
        public ActionResult NewGroup()
        {
            return View("AddEdit");
        }
    }
}