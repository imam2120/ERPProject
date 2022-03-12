using EmunaERP.Models;
using EmunaERP.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmunaERP.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult ProductInfo()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult ProductGroup()
        {
            return View();
        }
        public ActionResult ProductCategory()
        {
            return View();
        }
        public ActionResult ProductSubCategory()
        {
            return View();
        }
        public ActionResult ProductUnit()
        {
            return View();
        }
        public ActionResult ProductInformation()
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

    }
}