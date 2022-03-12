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
    public class CompanyService
    {       
        public List<CompanyModel> GetCompanyInfo()
        {
            List<CompanyModel> _modelList = new List<CompanyModel>();
            DataTable dt = new DataTable();
            try
            {
                CompanyDBAL _objList = new CompanyDBAL();
                dt = _objList.GetCompanyInfo();
                if (dt.Rows.Count > 0)
                {
                    _modelList = (from DataRow row in dt.Rows
                                  select new CompanyModel
                                  {
                                      CompanyId =Convert.ToInt32(row["CompanyId"]),
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