using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;

namespace R2MD
{
    /// <summary>
    /// Summary description for DatatableHandler
    /// </summary>
    public class DatatableHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int displayLength = int.Parse(context.Request["iDisplayLength"]);
            int displayStart = int.Parse(context.Request["iDisplayStart"]);
            int sortCol = int.Parse(context.Request["iSortCol_0"]) -2;
            string sortDir = context.Request["sSortDir_0"];  //Name=" + name + "&dateFrom=" + datefrom + "&dateTo=
            string Name = context.Request["Name"];
            string dateFrom = context.Request["dateFrom"];
            string dateTo = context.Request["dateTo"];
            string Status = context.Request["Status"];
            string Service = context.Request["Service"];
            
            ServiceObj result=_GetServiceListN(displayLength, displayStart, sortCol, sortDir, Name, Service, Name, dateFrom, dateTo, Status);
            JavaScriptSerializer js = new JavaScriptSerializer();
            context.Response.Write(js.Serialize(result));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


        private ServiceObj _GetServiceListN(int displayLength, int displayStart, int sortCol, string sortDir, string Name = null, string Service = null, string Provider = null, string dateFrom = null, string dateTo = null, string Status = null)
        {
            ServiceObj result = new ServiceObj();
            try
            {
                Service se = new Service();
                DataTable dt = se.getServiceListNew(displayLength, displayStart, sortCol, sortDir, Name, Service, Provider, dateFrom, dateTo, Status);

                
                if (dt != null && dt.Rows.Count > 0)
                {
                    result.iTotalDisplayRecords = Convert.ToInt32(dt.Rows[0]["TotalCount"].ToString());
                    result.iTotalRecords = Convert.ToInt32(dt.Rows[0]["TotalCount"].ToString());
                    foreach (DataRow dr in dt.Rows)
                    {
                        ServiceData sd = new ServiceData();
                        sd.ServiceId = dr["ID"] != DBNull.Value ? dr["ID"].ToString() : "";
                        sd.Name = dr["Name"] != DBNull.Value ? dr["Name"].ToString() : "";
                        sd.MRN = dr["MRN"] != DBNull.Value ? dr["MRN"].ToString() : "";
                        sd.PatientId = dr["PatientId"] != DBNull.Value ? dr["PatientId"].ToString() : "";
                        sd.RequestType = dr["RequestType"] != DBNull.Value ? dr["RequestType"].ToString() : "";
                        sd.LevelofService = dr["LevelofService"] != DBNull.Value ? dr["LevelofService"].ToString() : "";
                        DateTime dtime = new DateTime();
                        if (dr["ScheduleTime"] != DBNull.Value)
                        {
                            dtime = Convert.ToDateTime(dr["ScheduleTime"].ToString());
                            sd.ScheduleTime = dtime.ToString("yyyy-MM-dd HH:mm");
                        }
                        else
                        {
                            sd.ScheduleTime = "";
                        }
                        sd.Status = dr["Status"] != DBNull.Value ? dr["Status"].ToString() : "Unassigned";
                        sd.typeID = dr["ServiceRequestTypeId"] != DBNull.Value ? dr["ServiceRequestTypeId"].ToString() : "";
                        result.aaData.Add(sd);
                    }
                    
                }


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
            return result;
        }

    }
}