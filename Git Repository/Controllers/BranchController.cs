using EmunaERP.Models;
using EmunaERP.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmunaERP.Controllers
{
    public class BranchController : Controller
    {
        // GET: Branch
        public ActionResult BranchInfo()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetBranchInfo()
        {
            var objitem = new BranchInfoService();
            List<BranchInfoModel> model = new List<BranchInfoModel>();
            model = objitem.GetBranchInfo();
            var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
    }
}