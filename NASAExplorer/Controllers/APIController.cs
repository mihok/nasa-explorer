using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NASAExplorer.Controllers
{
    public class APIController : Controller
    {
        //
        // GET: /API/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetMajorObjects()
        {
            return Json(null);
        }

        public JsonResult GetCoords(int id)
        {
            return Json(null);
        }
    }
}
