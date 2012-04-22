using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NASAExplorer.Services;

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

        public JsonResult GetCoords(int id = 399) // default to earth
        {
            HorizonInterface horizon = new HorizonInterface();
            var xyz = horizon.GetCoordinates(id);         

            return Json(xyz, JsonRequestBehavior.AllowGet);
        }
    }
}
