using MixedUseProperty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MixedUseProperty.Controllers
{
    public class SitesController : Controller
    {
        // GET: Sites
        public ActionResult Index()
        {
            MixProperty obj = new MixProperty();
            MixProperty.dtSites = obj.GetSitesData();
            MixProperty.dtSpace = obj.GetSpaceData();
            MixProperty.dtProperty = obj.GetData();
            return View(obj);
        }
    }
}