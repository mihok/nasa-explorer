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

        public JsonResult GetPlanets()
        {
            HorizonInterface horizon = new HorizonInterface();
            Dictionary<string, List<Coord>> result = new Dictionary<string, List<Coord>>();

            result.Add("mercury", horizon.GetCoordinates(199));
            result.Add("venus", horizon.GetCoordinates(299));
            result.Add("earth", horizon.GetCoordinates(399));
            result.Add("mars", horizon.GetCoordinates(499));
            result.Add("jupiter", horizon.GetCoordinates(599));
            result.Add("saturn", horizon.GetCoordinates(699));
            result.Add("urainus", horizon.GetCoordinates(799));
            result.Add("neptune", horizon.GetCoordinates(899));
            result.Add("pluto", horizon.GetCoordinates(999));

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCoordsByDate(DateTime start, DateTime end, int id = 399)
        {
            HorizonInterface horizon = new HorizonInterface(start, end);
            var xyz = horizon.GetCoordinates(id);

            return Json(xyz, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCoords(int id = 399) // default to earth
        {
            HorizonInterface horizon = new HorizonInterface();
            var xyz = horizon.GetCoordinates(id);         

            return Json(xyz, JsonRequestBehavior.AllowGet);
        }
    }
}
