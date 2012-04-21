using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NASAExplorer.Models;

namespace NASAExplorer.Controllers
{
    public class WebController : Controller
    {
        //
        // GET: /Web/

        public ActionResult Default()
        {
            return View(new BaseViewModel());
        }

    }
}
