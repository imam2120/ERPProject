using Elmah;
using EmunaERP.DBAccess;
using EmunaERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EmunaERP.Service
{
    public class BranchInfoService
    {       
        public List<BranchInfoModel> GetBranchInfo()
        {
            List<BranchInfoModel> _modelList = new List<BranchInfoModel>();
            DataTable dt = new DataTable();
            try
            {
                BranchInfoDBAL _objList = new BranchInfoDBAL();
                dt = _objList.GetBranchInfo();
                if (dt.Rows.Count > 0)
                {
                    _modelList = (from DataRow row in dt.Rows
                                  select new BranchInfoModel
                                  {
                                      BranchID =Convert.ToInt32(row["BranchID"]),
                                      BranchName = row["BranchName"].ToString(),
                                      CompanyID = Convert.ToInt32(row["CompanyID"]),
                                      CompanyName = row["chName"].ToString(),
                                      //MobileNumber = row["MobileNumber"].ToString(),
                                      //Email = row["Email"].ToString(),
                                      //WebAddress = row["WebAddress"].ToString(),
                                      Status = row["Status"].ToString()
                                  }).ToList();
                }
                


                return _modelList;
            }
            catch (Exception ex)
            {
               // ErrorSignal.FromCurrentContext().Raise(ex);
                throw ex;
            }
        }

        public List<CompanyModel> SaveCompanyInfo()
        {
            CompanyModel companyModel = new CompanyModel();
            List<CompanyModel> _modelList = new List<CompanyModel>();
            DataTable dt = new DataTable();
            try
            {
                CompanyDBAL _objList = new CompanyDBAL();
               
                int i = _objList.SaveCompanyInfo(companyModel);
                if (dt.Rows.Count > 0) 
                {
                    _modelList = (from DataRow row in dt.Rows
                                  select new CompanyModel
                                  {
                                      CompanyId = Convert.ToInt32(row["CompanyId"]),
                                      CompanyName = row["CompanyName"].ToString(),
                                      Address = row["Address"].ToString(),
                                      Contact = row["Contact"].ToString(),
                                      Email = row["Email"].ToString(),
                                      WebAddress = row["WebAddress"].ToString()
                                  }).ToList();
                }


                return _modelList;
            }
            catch (Exception ex)
            {
              ErrorSignal.FromCurrentContext().Raise(ex);
                throw ex;
            }
        }

    }
}