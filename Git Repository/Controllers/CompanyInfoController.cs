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
            int result = 0;
            var objitem = new CompanyService();
            CompanyModel companyModel = new CompanyModel();
           int companyId =Convert.ToInt32(formCollection["hdnCompanyId"]);
            companyModel.CompanyName = formCollection["txtCompanyName"];
            companyModel.Address = formCollection["txtAddress"];
            companyModel.Email = formCollection["txtEmail"];
            if (companyId > 0)
            {
                result= objitem.UpdateCompanyInfo(companyModel);
            }
            else
            {
                result= objitem.SaveCompanyInfo(companyModel);
            }

            if (result == 1)
                ViewBag.Message = "Data Save Successfully..!";
            // return Json(new { success = true, Message = "Data Save Successfully..!" });          
            else
                ViewBag.Message = "Data Save Failed..!";
            //return Json(new { success = false, Message = "Data Save Failed..!" });
            return View();
        }
   
    }
}