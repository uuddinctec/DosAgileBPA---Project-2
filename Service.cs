using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace R2MD
{
    public class Service
    {
        public DataTable getServiceList(string Name, string Service, string Provider, string dateFrom, string dateTo, string Status) {
            DataTable result = new DataTable();
            SqlDataReader dr = null;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString)) {
                try {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("GetServiceTableList", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (!String.IsNullOrEmpty(Name)) cmd.Parameters.AddWithValue("@name",  Name);
                    if (!String.IsNullOrEmpty(dateFrom)) cmd.Parameters.AddWithValue("@dateFrom", dateFrom);
                    if (!String.IsNullOrEmpty(dateTo)) cmd.Parameters.AddWithValue("@dateTo", dateTo);
                    if(!String.IsNullOrEmpty(Status)&&(Status!="-1"))
                    cmd.Parameters.AddWithValue("@Status", Convert.ToInt32(Status));
                    dr = cmd.ExecuteReader();
                    result.Load(dr);
                }
                catch (Exception ex)
                {
                    Exception exceptionToThrow;
                    if (Diagnostics.ExceptionHandler.ExManager.HandleException(ex, "LogAndThrewException",
                        out exceptionToThrow))
                    {
                        if (exceptionToThrow == null)
                            throw;
                        else
                            throw exceptionToThrow;
                    }
                }
                finally
                {
                    if (conn != null) conn.Close();
                }
            }
            return result;
        }

        public DataTable getServiceListNew(int displayLength, int displayStart, int sortCol, string sortDir, string Name, string Service, string Provider, string dateFrom, string dateTo, string Status)
        {
            DataTable result = new DataTable();
            SqlDataReader dr = null;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("GetServiceTableList_withSortPagination", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (!String.IsNullOrEmpty(Name)) cmd.Parameters.AddWithValue("@name", Name);
                    if (!String.IsNullOrEmpty(dateFrom)) cmd.Parameters.AddWithValue("@dateFrom", dateFrom);
                    if (!String.IsNullOrEmpty(dateTo)) cmd.Parameters.AddWithValue("@dateTo", dateTo);
                    if (!String.IsNullOrEmpty(Status) && (Status != "-1"))
                        cmd.Parameters.AddWithValue("@Status", Convert.ToInt32(Status));
                    if(!string.IsNullOrEmpty(Service) && Service!="All") cmd.Parameters.AddWithValue("@Service", Service);
                    if (!String.IsNullOrEmpty(Provider)) cmd.Parameters.AddWithValue("@Provider", Provider);
                    cmd.Parameters.AddWithValue("@DisplayLength", displayLength);
                    cmd.Parameters.AddWithValue("@DisplayStart", displayStart);
                    cmd.Parameters.AddWithValue("@SortCol", sortCol);
                    cmd.Parameters.AddWithValue("@SortDir", sortDir);
                    dr = cmd.ExecuteReader();
                    result.Load(dr);
                }
                catch (Exception ex)
                {
                    Exception exceptionToThrow;
                    if (Diagnostics.ExceptionHandler.ExManager.HandleException(ex, "LogAndThrewException",
                        out exceptionToThrow))
                    {
                        if (exceptionToThrow == null)
                            throw;
                        else
                            throw exceptionToThrow;
                    }
                }
                finally
                {
                    if (conn != null) conn.Close();
                }
            }
            return result;
        }

        public DataTable getPInfo(string FName, string LName)
        {
            DataTable result = new DataTable();
            SqlDataReader dr = null;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("getPatientInfo", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", FName);
                    cmd.Parameters.AddWithValue("@LastName", LName);               
                    dr = cmd.ExecuteReader();
                    result.Load(dr);
                }
                catch (Exception ex)
                {
                    Exception exceptionToThrow;
                    if (Diagnostics.ExceptionHandler.ExManager.HandleException(ex, "LogAndThrewException",
                        out exceptionToThrow))
                    {
                        if (exceptionToThrow == null)
                            throw;
                        else
                            throw exceptionToThrow;
                    }
                }
                finally
                {
                    if (conn != null) conn.Close();
                }
            }
            return result;
        }
        public DataTable Invoice(string Name, string Client, string dateFrom, string Invoice, string Status, string id)
        {
            DataTable result = new DataTable();
            SqlDataReader dr = null;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[GetInvoiceTabelList]", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@Client", Client);
                    cmd.Parameters.AddWithValue("@dateFrom", dateFrom);
                    cmd.Parameters.AddWithValue("@Invoice", Invoice);
                    cmd.Parameters.AddWithValue("@Status", Status);
                    if(!string.IsNullOrEmpty(id)) cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(id));
                    dr = cmd.ExecuteReader();
                    result.Load(dr);
                }
                catch (Exception ex)
                {
                    Exception exceptionToThrow;
                    if (Diagnostics.ExceptionHandler.ExManager.HandleException(ex, "LogAndThrewException",
                        out exceptionToThrow))
                    {
                        if (exceptionToThrow == null)
                            throw;
                        else
                            throw exceptionToThrow;
                    }
                }
                finally
                {
                    if (conn != null) conn.Close();
                }
            }
            return result;
        }
        public DataTable GetTripInfo(string serviceID)
        {
            DataTable result = new DataTable();
            SqlDataReader dr = null;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT sr.ServiceRequestId as ID, sr.ServiceRequestTypeId,sr.[TransportType],sr.ScheduleTime,	        
                                  n.FullName, pinfo.MRN, p.PatientId,
                                  srt.ServiceRequestType as RequestType, srl.ServiceRequestLevel as LevelofService, srs.ServiceRequestStatus as Status
                                  FROM[dbo].[ServiceRequests] sr
                                  left join[dbo].[Patients] p on sr.PatientId = p.PatientId
                                  left join[dbo].[ServiceRequestStatus] srs on sr.ServiceRequestStatusId = srs.ServiceRequestStatusId
                                  left join[dbo].[ServiceRequestLevels] srl on sr.ServiceRequestLevelId = srl.ServiceRequestLevelId
                                  left join[dbo].[ServiceRequestTypes] srt on sr.ServiceRequestTypeId = srt.ServiceRequestTypeId
                                  left join PatientInfo n on sr.PatientId = n.PatientId
                                  left join[PI].[PatientInformation] pinfo on sr.PatientId = pinfo.PatientId
                                  where sr.ServiceRequestId=@sID", conn);                    
                    if(!string.IsNullOrEmpty(serviceID))cmd.Parameters.AddWithValue("@sID", serviceID);                    
                    dr = cmd.ExecuteReader();
                    result.Load(dr);
                }
                catch (Exception ex)
                {
                    Exception exceptionToThrow;
                    if (Diagnostics.ExceptionHandler.ExManager.HandleException(ex, "LogAndThrewException",
                        out exceptionToThrow))
                    {
                        if (exceptionToThrow == null)
                            throw;
                        else
                            throw exceptionToThrow;
                    }
                }
                finally
                {
                    if (conn != null) conn.Close();
                }
            }
            return result;
        }
    }
}