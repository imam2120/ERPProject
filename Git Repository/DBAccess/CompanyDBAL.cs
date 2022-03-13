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
    public class CompanyDBAL
    {
        //string sqlConnection = Common.getConnection().ToString();
        public DataTable GetCompanyInfo()
        {
            conn.Open();
            SqlCommand dAd = new SqlCommand("CompanyInfo", conn);
            SqlDataAdapter sda = new SqlDataAdapter(dAd);
            dAd.CommandType = CommandType.StoredProcedure;
            dAd.CommandTimeout = 1000;
            try
            {
                //dAd.Parameters.AddWithValue("@FromDate", FromDate);
                //dAd.Parameters.AddWithValue("@ToDate", ToDate);
                dAd.Parameters.AddWithValue("@QryOption", 2);

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
            int result = 0;
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-134JQLJ;Initial Catalog=CRVLInventoryDatabase;User Id=sa;Password=sa123;pooling=true;min pool size=5;Max Pool Size=60;");
            conn.Open();
            SqlCommand sqlCommand = new SqlCommand("CompanyInfo", conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandTimeout = 1000;
            sqlCommand.Parameters.AddWithValue("@CompanyId", companyModel.CompanyId);
            sqlCommand.Parameters.AddWithValue("@comName", companyModel.CompanyName);
            sqlCommand.Parameters.AddWithValue("@chName", companyModel.CompanyChairManName);
            sqlCommand.Parameters.AddWithValue("@comAddress", companyModel.Address);
            sqlCommand.Parameters.AddWithValue("@tntNumber", companyModel.TntNumber);
            sqlCommand.Parameters.AddWithValue("@cellnumber", companyModel.Contact);
            sqlCommand.Parameters.AddWithValue("@eMail", companyModel.Email);
            sqlCommand.Parameters.AddWithValue("@webaddress", companyModel.WebAddress);
            sqlCommand.Parameters.AddWithValue("@QryOption", 1);

     
            try
            {
                result = sqlCommand.ExecuteNonQuery();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //dSet.Dispose();
                //dAd.Dispose();
                conn.Close();
                conn.Dispose();
            }
       
        }

        public int UpdateCompanyInfo(CompanyModel companyModel)
        {
            int result = 0;
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-134JQLJ;Initial Catalog=CRVLInventoryDatabase;User Id=sa;Password=sa123;pooling=true;min pool size=5;Max Pool Size=60;");
            conn.Open();
            SqlCommand sqlCommand = new SqlCommand("CompanyInfo", conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandTimeout = 1000;
            sqlCommand.Parameters.AddWithValue("@CompanyId", companyModel.CompanyId);
            sqlCommand.Parameters.AddWithValue("@comName", companyModel.CompanyName);
            sqlCommand.Parameters.AddWithValue("@chName", companyModel.CompanyChairManName);
            sqlCommand.Parameters.AddWithValue("@comAddress", companyModel.Address);
            sqlCommand.Parameters.AddWithValue("@tntNumber", companyModel.TntNumber);
            sqlCommand.Parameters.AddWithValue("@cellnumber", companyModel.Contact);
            sqlCommand.Parameters.AddWithValue("@eMail", companyModel.Email);
            sqlCommand.Parameters.AddWithValue("@webaddress", companyModel.WebAddress);
            sqlCommand.Parameters.AddWithValue("@QryOption", 1);


            try
            {
                result = sqlCommand.ExecuteNonQuery();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //dSet.Dispose();
                //dAd.Dispose();
                conn.Close();
                conn.Dispose();
            }

        }


    }
}