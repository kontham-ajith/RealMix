using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using MixedUseProperty.Models;

namespace MixedUseProperty.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            string strTemplateFilePath = Server.MapPath("~/Content/Data/MixedUseProperty_data.xlsx");
            DataTable dt = ExcelHelper.GetDataTableFromExcel(strTemplateFilePath, "Property", true);

            List<MixProperty> MixProperty = new List<MixProperty>();
            foreach (DataRow item in dt.Rows)
            {
                MixProperty mixProperty = new MixProperty();
                mixProperty.strPropID = item["PropertyID"].ToString();
                mixProperty.strPropName = item["Property Name"].ToString();
                MixProperty.Add(mixProperty);
            }
            ViewBag.Prop = MixProperty;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult GetSites(string PropID)
        {
            string strTemplateFilePath = Server.MapPath("~/Content/Data/MixedUseProperty_data.xlsx");
            DataTable dt = ExcelHelper.GetDataTableFromExcel(strTemplateFilePath, "Site", true);
            DataTable Rdt = new DataTable();
            List<MixProperty> mixes = new List<MixProperty>();
            try
            {

                Rdt = dt.Select("PropertyID= " + PropID).CopyToDataTable();

               
                foreach (DataRow item in Rdt.Rows)
                {
                    MixProperty mixProperty = new MixProperty();
                    mixProperty.strSiteId = item["Site ID"].ToString();
                    mixProperty.strSiteName = item["Site name"].ToString();
                    mixes.Add(mixProperty);
                }

            }
            catch (Exception ex)
            {
                
            }


            return Json(new { success = true, message = "Site Added", mixes = mixes, Cnt = mixes.Count}, JsonRequestBehavior.AllowGet);


        }
   

        public ActionResult NoLayout()
        {
            return View();
        }
        public ActionResult PropLayout(int SiteCnt,string PropName)
        {
            ViewBag.SiteCnt = SiteCnt;
            ViewBag.PropName = PropName;
            return View();
        }

        public ActionResult SiteLayout()
        {
            return View();
        }

        public ActionResult ApartmentUnitLayout()
        {
            return View();
        }

        public ActionResult OfficeUnitLayout()
        {
            return View();
        }


        public ActionResult ParkingUnitLayout()
        {
            return View();
        }

        public ActionResult UnitLayout()
        {
            return View();
        }




        //public ActionResult GetFloorDetails(string propid,string siteid)
        //{

        //    string strTemplateFilePath = Server.MapPath("~/Content/Data/MixedUseProperty_data.xlsx");
        //    DataTable dt = ExcelHelper.GetDataTableFromExcel(strTemplateFilePath, "Property", true);

        //    try
        //    {
        //        DataTable Rdt = dt.Select("PropertyID").CopyToDataTable();
        //        DataTable dt1 = ExcelHelper.GetDataTableFromExcel(strTemplateFilePath, "Site", true);
        //        DataTable Rdt1 = dt1.Select("PropertyID").CopyToDataTable();


        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return Json(new { })

        //}
    }
}