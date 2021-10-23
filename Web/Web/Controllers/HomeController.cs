using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "ACCESS DENIED!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
