using EmunaERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EmunaERP.DBAccess
{
    public class BranchInfoDBAL
    {
        //string sqlConnection = Common.getConnection().ToString();
        public DataTable GetBranchInfo()
        {
            SqlConnection conn = new SqlConnection("Data Source=.\\DATAEXPRESS;Initial Catalog=CRVLInventoryDatabase;User Id=sa;Password=RyansofT2012;pooling=true;min pool size=5;Max Pool Size=60;");
            conn.Open();
            SqlCommand dAd = new SqlCommand("USP_GetBranchInfo", conn);
            SqlDataAdapter sda = new SqlDataAdapter(dAd);
            dAd.CommandType = CommandType.StoredProcedure;
            dAd.CommandTimeout = 1000;
            //dAd.Parameters.AddWithValue("@FromDate", FromDate);
            //dAd.Parameters.AddWithValue("@ToDate", ToDate);
            //dAd.Parameters.AddWithValue("@QryOption", 1);

            DataTable dSet = new DataTable();

            //DataTable dSet = new DataTable();
            try
            {
                //sda.Fill(dSet);
                //return dSet;
                sda.Fill(dSet);
                return dSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dSet.Dispose();
                dAd.Dispose();
                conn.Close();
                conn.Dispose();
            }
        }

        public int SaveCompanyInfo(CompanyModel companyModel)
        {
            return 0;
        }
    }
}