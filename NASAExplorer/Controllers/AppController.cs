using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NASAExplorer.Controllers
{
    public class AppController : Controller
    {
        //
        // GET: /App/

        public ActionResult Explore1()
        {
            return View("Explore");
        }

        public ActionResult Explore2()
        {
            return View("Explore2");
        }

        public ActionResult MiniMap()
        {
            return View();
        }

    }
}
