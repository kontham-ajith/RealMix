using MixedUseProperty.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MixedUseProperty.Controllers
{
    public class VariablesController : Controller
    {
        // GET: Variables
        public ActionResult Index()
        {
            MixProperty mixProperty = new MixProperty();
            MixProperty.dtProperty = ExcelHelper.GetDataTableFromExcel(Server.MapPath("~/Content/Data/MixedUseProperty_data.xlsx"), "Variable");
            return View(mixProperty);
        }

        public JsonResult AddVariable(MixProperty mixProperty)
        {
            bool isSuccess = ExcelHelper.WriteToExcelVariable("Variable", mixProperty, Server.MapPath("../Content/Data/MixedUseProperty_data.xlsx"));
            return Json(new { success = true, message = "Site Added" }, JsonRequestBehavior.AllowGet);
        }


    }
}