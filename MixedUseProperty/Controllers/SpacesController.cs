using MixedUseProperty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MixedUseProperty.Controllers
{
    public class SpacesController : Controller
    {
        // GET: Spaces
        public ActionResult Index()
        {
            MixProperty obj = new MixProperty();
            MixProperty.dtProperty = obj.GetData();
            MixProperty.dtSpace = obj.GetSpaceData();
            return View(obj);
        }
        [HttpPost]
        public ActionResult AddSpace(MixProperty _objmixproperty)
        {
            string strTemplateFilePath = Server.MapPath("~/Content/Data/MixedUseProperty_data.xlsx");
            bool bln = ExcelHelper.WriteSpaceToExcel("Space", _objmixproperty, strTemplateFilePath);
            if (bln)
                return Json(new { success = true, message = "Space Added" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = false, message = "Unable to add" }, JsonRequestBehavior.AllowGet);
        }
    }
}