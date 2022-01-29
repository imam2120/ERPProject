using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace EmunaERP
{
    public static class Common
    {
        public static string ConStrForAll = WebConfigurationManager.ConnectionStrings["DbConStr"].ConnectionString;
        private static string ConnectionString = WebConfigurationManager.ConnectionStrings["DbConStr"].ConnectionString;
        private static SqlConnection connection;
        private static SqlCommand command;
        private static SqlDataReader reader;
        private static string strSql = "";
        public static DateTime MaxDate;

        private static SqlConnection _Connection;

        private static SqlConnection Connection
        {
            get
            {

                if (_Connection == null)
                {
                    _Connection = new SqlConnection(ConnectionString);
                }
                return _Connection;
            }
        }

        //--------------------New Methode-------------------------------

        public static Int32 ExeNonQuery(string strSqlQueryString)
        {
            try
            {
                Int32 objReturnValue;
                connection = new SqlConnection(ConnectionString);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                command = new SqlCommand(strSqlQueryString);
                command.CommandText = strSqlQueryString;
                command.Connection = connection;

                objReturnValue = command.ExecuteNonQuery();

                return objReturnValue;
            }
            catch (Exception ex)
            {
                return 0;
            }

            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public static object ExeScalar(string strQueryString)
        {
            try
            {
                object objReturnValue;
                connection = new SqlConnection(ConnectionString);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                command = new SqlCommand(strQueryString);
                command.CommandText = strQueryString;
                command.Connection = connection;

                objReturnValue = command.ExecuteScalar();

                return objReturnValue;
            }
            catch (Exception ex)
            {
                return null;
            }

            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public static string GetNewTransaction(string strEntryInitial, Int32 intEntryTypeID)
        {
            try
            {

                string strNewTransNo, strSqlQueryString = "";

                strSqlQueryString = "Select SeqTransNo from Loan_Sys_EntryTypeInfo Where EntryInitial='" + strEntryInitial + "' and EntryTypeID=" + intEntryTypeID + " ";

                strNewTransNo = Convert.ToString(ExeScalar(strSqlQueryString));


                if (!string.IsNullOrEmpty(strNewTransNo))
                {
                    strNewTransNo = Convert.ToString(Convert.ToInt32(strNewTransNo) + 1);
                    strSqlQueryString = "Update Loan_Sys_EntryTypeInfo set SeqTransNo = " + strNewTransNo + " Where EntryInitial = '" + strEntryInitial + "' and EntryTypeID=1 ";
                    ExeNonQuery(strSqlQueryString);
                }
                else
                {
                    return null;
                }
                return strEntryInitial + strNewTransNo.PadLeft(11, '0');
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string GetNewTransaction(string strEntryInitial, Int32 intEntryTypeID,SqlTransaction transaction)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = transaction.Connection;
                command.Transaction = transaction;

                string strNewTransNo = "";

                command.CommandText = "Select SeqTransNo from Loan_Sys_EntryTypeInfo Where EntryInitial='" + strEntryInitial + "' and EntryTypeID=" + intEntryTypeID + " ";

                strNewTransNo = Convert.ToString(command.ExecuteScalar());


                if (!string.IsNullOrEmpty(strNewTransNo))
                {
                    strNewTransNo = Convert.ToString(Convert.ToInt32(strNewTransNo) + 1);
                    command.CommandText = "Update Loan_Sys_EntryTypeInfo set SeqTransNo = " + strNewTransNo + " Where EntryInitial = '" + strEntryInitial + "' and EntryTypeID=" + intEntryTypeID + " ";
                    command.ExecuteNonQuery();
                }
                else
                {
                    throw new Exception("TransactionNo getting null value !. Please Check");
                }
                return strEntryInitial + strNewTransNo.PadLeft(11, '0');
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        //--------------------End New ----------------------------------

        public static DataSet NewGetFunctionDS(DataTable datatable, string WhereClause)
        {
            string qry = String.Format("SELECT * {0}", datatable.TableName + " " + WhereClause);
            SqlConnection con = new SqlConnection(ConnectionString);
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(qry, con);
            adapter.Fill(ds);
            return ds;
        }
        public static SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection();
            if (_Connection == null)
            {

                connection.ConnectionString = Common.ConStrForAll;
                return connection;
            }

            return _Connection;
        }
        private static SqlDataAdapter GetAdapter(string sql)
        {
            try
            {

                command = new SqlCommand(sql, Connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Dispose();

                return adapter;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public static DataSet GetData(string sql)
        {
            try
            {

                DataSet ds = new DataSet();
                ds.Clear();
                ds.Reset();
                SqlCommand sqlcmd = new SqlCommand(sql, Connection);
                SqlDataAdapter adapters = new SqlDataAdapter(sqlcmd);
                adapters.Fill(ds);
                sqlcmd.Dispose();
                return ds;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        public static DataTable GetDataTable(string sql)
        {
            try
            {
                DataTable dt = new DataTable();
               
                GetAdapter(sql).Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public static DataSet GetTableData(string tableName)
        {
            return GetData(String.Format("SELECT * FROM {0}", tableName));
        }
        public static DataSet GetTableData(DataTable datatable)
        {
            string tableName = datatable.TableName;
            return GetData(String.Format("SELECT * FROM {0}", tableName));
        }
        //public static DataSet GetDataTable(string  strQry)
        //{         
        //    return GetData(String.Format("SELECT * FROM {0}", tableName));
        //}
        public static DataSet GetTableData(string tableName, string WhereClause)
        {
            string qry = String.Format("SELECT * FROM {0}", tableName + " " + WhereClause);
            return GetData(qry);
        }
        public static DataSet GetTableData(DataTable datatable, string WhereClause)
        {
            string qry = String.Format("SELECT * FROM {0}", datatable.TableName + " " + WhereClause);
            return GetData(qry);
        }
        public static int UpdateData(DataSet ds, string tableName)
        {
            return UpdateData(ds.Tables[tableName]);
        }
        public static int UpdateData(DataTable dt)
        {
            SqlDataAdapter adapter = GetAdapter(String.Format("SELECT * FROM {0}", dt.TableName));
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
            int rowsEffected = adapter.Update(dt);
            return rowsEffected;
        }
        public static int UpdateData(DataTable dt, string tableName)
        {
            SqlDataAdapter adapter = GetAdapter(String.Format("SELECT * FROM {0}", tableName));
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);

            int rowsEffected = adapter.Update(dt);
            return rowsEffected;
        }
        public static DataSet GetDataBySP(string spName)
        {
            SqlCommand cmd = new SqlCommand("[" + spName + "]", _Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            return ds;
        }
        public static DataSet GetDataBySP(string spName, String[][] Parameters)
        {
            SqlCommand cmd = new SqlCommand("[" + spName + "]", _Connection);
            for (int i = 0; i < Parameters.Length; i++)
            {
                cmd.Parameters.Add(new SqlParameter(Parameters[i][0], Parameters[i][1]));

            }
            cmd.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            return ds;
        }
        public static int DeleteAllData(DataTable datatable)
        {
            SqlDataAdapter adapter = GetAdapter(String.Format("SELECT * FROM {0}", datatable.TableName));
            adapter.DeleteCommand = new SqlCommand("DELETE FROM " + datatable.TableName.ToString(), adapter.SelectCommand.Connection);
            adapter.DeleteCommand.Connection.Open();
            int rowsEffected = adapter.DeleteCommand.ExecuteNonQuery();
            adapter.DeleteCommand.Connection.Close();
            return rowsEffected;
        }

        //-----------------New function----------------//

        public static SqlDataReader getReader(string query)
        {
            connection = new SqlConnection(ConnectionString);
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            command = new SqlCommand(query);
            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader();

            return reader;
        }
        public static Int32 getTableMaxId_Sql(string columnName, string tableName, string whereCondition)
        {
            Int32 MaxId = 0;
            string strSql = "";
            try
            {
                if (whereCondition != "")
                {
                    strSql = "Select IsNull(Max(" + columnName + "),0)+1 As MaxId From " + tableName + " Where  " + whereCondition + "";
                }
                else
                {
                    strSql = "Select IsNull(Max(" + columnName + "),0)+1 As MaxId From " + tableName;
                }
                connection = new SqlConnection(ConnectionString);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                command = new SqlCommand(strSql);
                command.CommandText = strSql;
                command.Connection = connection;
                MaxId = Convert.ToInt32(command.ExecuteScalar());

                if (MaxId == 0)
                {
                    MaxId = MaxId + 1;
                }

                return MaxId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            
        }
        public static Int32 getTableMaxId_Sql(string columnName, string tableName, string whereCondition,SqlTransaction transaction)
        {
           
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = transaction.Connection;
                command.Transaction = transaction;

                Int32 MaxId = 0;
                string strSql = "";

                if (whereCondition != "")
                {
                    strSql = "Select IsNull(Max(" + columnName + "),0)+1 As MaxId From " + tableName + " Where  " + whereCondition + "";
                }
                else
                {
                    strSql = "Select IsNull(Max(" + columnName + "),0)+1 As MaxId From " + tableName;
                }
                
                command.CommandText = strSql;
                MaxId = Convert.ToInt32(command.ExecuteScalar());

                if (MaxId == 0)
                {
                    MaxId = MaxId + 1;
                }
                return MaxId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            
        }

        public static Int32 DayOpenCloseStatus()
        {
            try
            {
                int intStatus = 0;

                string strSql,OpenDate = "";

                strSql = "Select Max(EntryDate) From Acc_DayOpenClose Where Status=0 ";

                connection = new SqlConnection(ConnectionString);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = strSql;
                OpenDate = Convert.ToString(command.ExecuteScalar());

                if (!string.IsNullOrEmpty(OpenDate))
                {
                    intStatus = 0;
                }
                else
                {
                    intStatus = 1;
                }

                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                return intStatus;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public static string GetOpenDate(string columnName, string tableName, string whereCondition)
        {
            try
            {
                string strSql = "";

                if (whereCondition != "")
                {
                    strSql = "Select IsNull(Max(" + columnName + "),0) As MaxDate From " + tableName + " Where  " + whereCondition + "";
                }
                else
                {
                    strSql = "Select IsNull(Max(" + columnName + "),0) As MaxDate From " + tableName + " ";
                }

                connection = new SqlConnection(ConnectionString);
                connection.Open();
                command = new SqlCommand(strSql);
                command.CommandText = strSql;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                {

                    if (reader.Read())
                    {
                        MaxDate = (DateTime)(reader[0]);
                    }

                    connection.Close();
                    return MaxDate.ToString("dd-MMM-yyyy");
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public static Int32 BranchStatus()
        {
            int intStatus = 1;
            try
            {
                string strOpenDate = Common.OpenDate();

                if (!string.IsNullOrEmpty(strOpenDate))
                {
                    intStatus = 0;
                }

            }
            catch (Exception ex)
            {
                Common.ErrorMsg(ex.ToString());
            }

            return intStatus;
        }
        public static string CollectionDate(string savcolcdate)
        {
            DateTime returndate ;
            returndate = Convert.ToDateTime(savcolcdate);
            return Convert.ToString(returndate.DayOfWeek);
        }

        public static string OpenDate()
        {
            try
            {
                DateTime dtmOpenDate = new DateTime();
                string strSql,OpenDate = "";

                strSql = "Select Max(EntryDate) from Acc_DayOpenClose Where CloseTime  IS NULL and Status<>1";

                connection = new SqlConnection(ConnectionString);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                command = new SqlCommand(strSql);
                command.CommandText = strSql;
                command.Connection = connection;

                dtmOpenDate = Convert.ToDateTime(command.ExecuteScalar());

                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }

                OpenDate = dtmOpenDate.ToString("dd-MMM-yyyy");

                return OpenDate;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        public static string GetDate(string columnName, string tableName, string whereCondition)
        {
            try
            {
                string strSql = "";

                if (whereCondition != "")
                {
                    strSql = "Select IsNull(Max(" + columnName + "),0) As MaxDate From " + tableName + " Where  " + whereCondition + "";
                }
                else
                {
                    strSql = "Select IsNull(Max(" + columnName + "),0) As MaxDate From " + tableName + " ";
                }

                connection = new SqlConnection(ConnectionString);
                connection.Open();
                command = new SqlCommand(strSql);
                command.CommandText = strSql;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                {

                    if (reader.Read())
                    {
                        MaxDate = (DateTime)(reader[0]);
                    }
                    connection.Close();

                    return MaxDate.ToString("dd-MMM-yyyy");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        public static Int32 ErrorMsg(string strMsg)
        {
            int result = 0;
            try
            {
                string strqry = "";
                string strFinalMsg = strMsg.Replace("'", "");
                connection = new SqlConnection(ConnectionString);
                strqry += "Insert Into ErrorMessage(ErrorMsg,Status) Values('" + strFinalMsg + "',0)";
                command = new SqlCommand(strqry);
                command.Connection = connection;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                result = command.ExecuteNonQuery();

                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static string ThrowErrorMsg(string strMessage)
        {

            try
            {
                //ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "alert('" + strMessage + "');", true); 


            }
            catch (Exception)
            {

            }

            return strMessage;
        }
        public static Int32 QueryExecute(string strQuery)
        {
            int result = 0;
            try
            {
                //con = new SqlConnection(ConnectionString);
                command = new SqlCommand(strQuery);
                command.Connection = connection;
                connection.Open();
                result = command.ExecuteNonQuery();
                connection.Close();
                connection.Dispose();


                return result;
            }
            catch (Exception)
            {

                return result;
            }
        }
        public static Int32 executeNonQuerySP(string query)
        {
            int result = 0;
            connection = new SqlConnection(ConnectionString);
            command = new SqlCommand(query);
            command.Connection = connection;
            connection.Open();
            result = command.ExecuteNonQuery();
            connection.Close();
            connection.Dispose();


            return result;
        }
        public static Int32 executeNonQueryTransaction(List<string> query, int counter)
        {

            SqlConnection db = new SqlConnection(ConnectionString);
            SqlTransaction transaction;
            db.Open();
            transaction = db.BeginTransaction();
            try
            {
                for (int i = 0; i < counter; i++)
                {
                    new SqlCommand(query[i], db, transaction).ExecuteNonQuery();
                }
                transaction.Commit();
                db.Close();
                return 1;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                db.Close();
                throw ex;

            }
        }
        public static Int32 exeTranWithDetTable(string query, SqlConnection db, SqlTransaction transaction)
        {
            int result = 0;
            try
            {
                new SqlCommand(query, db, transaction).ExecuteNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static Int32 executeNonQuerySP(string spName, String[][] Parameters)
        {
            connection = new SqlConnection(ConnectionString);
            command = new SqlCommand("[" + spName + "]");

            for (int i = 0; i < Parameters.Length; i++)
            {
                command.Parameters.Add(new SqlParameter(Parameters[i][0], Parameters[i][1]));
            }
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = connection;
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            connection.Dispose();

            int result = 0;
            return result;
        }
        public static void DataloadToGrid(string strSql, GridView grid)
        {
            try
            {
                DataTable dt = GetData(strSql).Tables[0];
                grid.DataSource = dt;
                grid.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static void DataLoadToDropDownList(string strQuery, DropDownList ddl, string txtField, string vluField)
        {
            //ddl.Items.Add(new ListItem("", ""));
            //DataTable dt = GetData(strQuery).Tables[0];
            DataTable dt = GetDataThoughDataTabel(strQuery);
            ddl.DataSource = dt;
            ddl.DataTextField = txtField;
            ddl.DataValueField = vluField;
            ddl.DataBind();
            ddl.Items.Insert(0, "");
        }
        public static DataTable GetDataThoughDataTabel(string strQueryString)
        {
            try
            {
                SqlConnection connection = new SqlConnection(Common.ConStrForAll);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand command = new SqlCommand();
                command = connection.CreateCommand();
                command.Connection = connection;

                command.CommandText = strQueryString;

                DataTable objDataFill = new DataTable();
                SqlDataAdapter getda = new SqlDataAdapter(command.CommandText, connection);
                getda.Fill(objDataFill);

                return objDataFill;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public static List<string> getSearchList(string query)
        {

            List<string> ListResult = new List<string>();
            DataTable dt = Common.GetData(query).Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListResult.Add(dt.Rows[i][0].ToString());
                }
            }

            return ListResult;
        }
        public static int getID(string ColumnName, string tableName, string WhereColumn, string optr, string WhereColumnValue)
        {
            int getResult = 0;
            string query = "Select " + ColumnName + " From " + tableName + " Where " + WhereColumn + optr + "'" + WhereColumnValue + "'";
            DataTable dt = GetData(query).Tables[0];

            if (dt.Rows.Count > 0)
            {
                getResult = int.Parse(dt.Rows[0][0].ToString());
            }

            return getResult;
        }
        public static string GetSingleData(string ColumnName, string tableName, string WhereCondition)
        {
            String strGetData = "";
            String query = "";

            if (WhereCondition == "")
            {
                query = "Select " + ColumnName + " From " + tableName + "";
            }
            else
            {
                query = "Select " + ColumnName + " From " + tableName + " Where " + WhereCondition + "";
            }

            DataTable dt = GetData(query).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strGetData = dt.Rows[0][0].ToString();
            }

            return strGetData;
        }

        public static string DateConvert(string day, string month, string year)
        {
            try
            {
                string date = "";
                if (!string.IsNullOrEmpty(day) && !string.IsNullOrEmpty(month) && !string.IsNullOrEmpty(year))
                {
                    string y = year.Substring(6, 4);
                    string m = month.Substring(0, 2);
                    string d = day.Substring(3, 2);
                    date = d + "-" + m + "-" + y;
                }
                return date;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
          
        }
        public static string MakeChartOfAccountNo(int ChartOfAccountId, int ChartOfAccountTypeId)
        {
            DataTable dt = new DataTable();
            string ChartOfAccountNo = "";

            dt = Common.GetData("Select IsNull(max(Right(ChartOfAccountNo,4)),0)+1 From tblACC_ChartOfAccount").Tables[0];
            int maxNo = 0;
            if (dt.Rows.Count > 0)
            {
                maxNo = int.Parse(dt.Rows[0][0].ToString());
            }

            dt = Common.GetData("Select ChartOfAccountNo From tblACC_ChartOfAccount Where ChartOfAccountId='" + ChartOfAccountId + "'").Tables[0];
            string AccNo = "";
            if (dt.Rows.Count > 0)
            {
                AccNo = dt.Rows[0][0].ToString();
            }

            if (maxNo.ToString().Length == 1)
            {
                ChartOfAccountNo = AccNo.Substring(0, 2) + "-0" + ChartOfAccountTypeId + "-000" + maxNo;
            }

            if (maxNo.ToString().Length == 2)
            {
                ChartOfAccountNo = AccNo.Substring(0, 2) + "-0" + ChartOfAccountTypeId + "-00" + maxNo;
            }

            if (maxNo.ToString().Length == 3)
            {
                ChartOfAccountNo = AccNo.Substring(0, 2) + "-0" + ChartOfAccountTypeId + "-0" + maxNo;
            }

            if (maxNo.ToString().Length == 4)
            {
                ChartOfAccountNo = AccNo.Substring(0, 2) + "-0" + ChartOfAccountTypeId + "-" + maxNo;
            }

            return ChartOfAccountNo;
        }
        public static double getIntlamount(double dblPrincipalAmt, string Productno, string ColcOption, double Duration, double dblNoOfInstl)
        {
            try
            {
                double dblInstlAmount, dblBufferamount, dblNoOfinstl = 0;

                dblNoOfinstl = Convert.ToDouble(Common.GetSingleData("NoOfInstl", "Loan_V_ProductPrincipalUnit", "Productno='" + Productno + "' and ColcOption=" + ColcOption + " and Duration='" + Duration + "'"));

                string strQueryString = "";
                strQueryString += "Select ((" + dblPrincipalAmt + "*InstlAmtPerUnit/UnitAmount)+" + dblPrincipalAmt + ")/" + dblNoOfInstl + " from Loan_V_ProductPrincipalUnit "
                + " Where Productno='" + Productno + "' and ColcOption=" + ColcOption + " and Duration='" + Duration + "' and NoOfInstl=" + dblNoOfinstl + " ";

                dblBufferamount = Convert.ToDouble(ExeScalar(strQueryString));

                dblInstlAmount = Convert.ToDouble(Common.GetSingleData("Round(" + dblBufferamount + ",0)", "Loan_V_ProductPrincipalUnit", "Productno='" + Productno + "' and ColcOption=" + ColcOption + " and Duration='" + Duration + "'"));

                if (dblInstlAmount < dblBufferamount)
                {
                    dblInstlAmount = dblInstlAmount + 1;
                }

                return dblInstlAmount;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static double getTotalPayableAmount(double dblPrincipalAmt, string Productno, string ColcOption, double Duration, double dblNoOfInstl)
        {
            try
            {
                double dblTotalPayAmount, dblNoOfinstl = 0;

                dblNoOfinstl = Convert.ToDouble(Common.GetSingleData("NoOfInstl", "Loan_V_ProductPrincipalUnit", "Productno='" + Productno + "' and ColcOption=" + ColcOption + " and Duration='" + Duration + "'"));

                string strQueryString = "";
                strQueryString += "Select ((" + dblPrincipalAmt + "*InstlAmtPerUnit/UnitAmount)+" + dblPrincipalAmt + ") from Loan_V_ProductPrincipalUnit "
                + " Where Productno='" + Productno + "' and ColcOption=" + ColcOption + " and Duration='" + Duration + "' and NoOfInstl=" + dblNoOfinstl + " ";

                dblTotalPayAmount = Convert.ToDouble(ExeScalar(strQueryString));

                return dblTotalPayAmount;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static string GetDate(string savcolcdate)
        {
            string returndate = "";
            returndate = Convert.ToDateTime(savcolcdate).ToString("dd-MMM-yyyy");
            return returndate;
        }
        public static string GetNextColcDate(string savcolcdate)
        {
            try
            {
                string ReturnNextColcDate = "";

                savcolcdate = GetDate(savcolcdate);

                string SystemOpenDate = OpenDate();

                if (Convert.ToDateTime(SystemOpenDate) > Convert.ToDateTime(savcolcdate))
                {
                    ReturnNextColcDate = SystemOpenDate;
                }
                else
                {
                    ReturnNextColcDate = savcolcdate;
                }

                return ReturnNextColcDate;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static string GetCalcIntrDate(string SetupDate,SqlTransaction transaction)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = transaction.Connection;
                command.Transaction = transaction;

                string ReturnDate = "";

                command.CommandText = "Select DATEADD(yy,1,'" + SetupDate + "')";

                ReturnDate = Convert.ToString(command.ExecuteScalar());

                ReturnDate = Common.GetDate(ReturnDate);

                return ReturnDate;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static object GetSingleData(string ColumnName, string tableName, string WhereCondition,SqlTransaction transaction)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = transaction.Connection;
                command.Transaction = transaction;

                string strSqlQuery = ""; object ReturnValue;

                if (WhereCondition == "")
                {
                    strSqlQuery = "Select " + ColumnName + " From " + tableName + "";
                }
                else
                {
                    strSqlQuery = "Select " + ColumnName + " From " + tableName + " Where " + WhereCondition + "";
                }

                command.CommandText = strSqlQuery;
                ReturnValue = command.ExecuteScalar();

                return ReturnValue;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static string MaxOpenDate(SqlTransaction transaction)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = transaction.Connection;
                command.Transaction = transaction;

                DateTime date = new DateTime();
                string ReturnDate = "";

                command.CommandText = "Select Max(EntryDate) from Acc_DayOpenClose Where Status = 0 ";

                date = Convert.ToDateTime(command.ExecuteScalar());
                ReturnDate = date.ToString("dd-MMM-yyyy");

                return ReturnDate;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
