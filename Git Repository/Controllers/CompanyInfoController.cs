using EmunaERP.Models;
using EmunaERP.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace EmunaERP.Controllers
{
    public class CompanyInfoController : Controller
    {
        // GET: CompanyInfo
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetCompanyInfo()
        {
            var objitem = new CompanyService();
            List<CompanyModel> model = new List<CompanyModel>();
            model = objitem.GetCompanyInfo();
            var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public ActionResult Save_UpdateCompanyInfo(FormCollection formCollection)
        {
            var jsonResult =  Json("", JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        


  
       
    }
}