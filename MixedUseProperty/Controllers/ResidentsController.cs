using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MixedUseProperty.Models;

namespace MixedUseProperty.Controllers
{
    public class ResidentsController : Controller
    {
        // GET: Residents
        public ActionResult Index()
        {
            MixProperty mixProperty = new MixProperty();
            MixProperty.dtResident = mixProperty.GetResidentData();
            return View();
        }
    }
}