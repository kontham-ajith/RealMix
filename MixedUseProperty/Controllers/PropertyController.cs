using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MixedUseProperty.Models;

namespace MixedUseProperty.Controllers
{
    public class PropertyController : Controller
    {
        // GET: Property
        public ActionResult Index()
        {
            MixProperty obj = new MixProperty();
            MixProperty.dtProperty = obj.GetData();
            MixProperty.dtSpace = obj.GetSpaceData();
            return View(obj);
        }
        [HttpPost]
        public ActionResult AddProperty(MixProperty _objmixproperty)
        {
            string strTemplateFilePath = Server.MapPath("~/Content/Data/MixedUseProperty_data.xlsx");
            bool bln = ExcelHelper.WriteToExcel("Property", _objmixproperty, strTemplateFilePath);
            if (bln)
                return Json(new { success = true, message = "Property Added" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = false, message = "Unable to add" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddSite(MixProperty _objmixproperty)
        {
            string strTemplateFilePath = Server.MapPath("~/Content/Data/MixedUseProperty_data.xlsx");
            bool bln = ExcelHelper.WriteSiteToExcel("Site", _objmixproperty, strTemplateFilePath);
            if (bln)
                return Json(new { success = true, message = "Site Added" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = false, message = "Unable to add" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public DataTable GetData()
        {
            string strTemplateFilePath = Server.MapPath("~/Content/Data/MixedUseProperty_data.xlsx");
            DataTable dt = ExcelHelper.GetDataTableFromExcel(strTemplateFilePath, "Property", true);
            return dt;
        }
    }
}