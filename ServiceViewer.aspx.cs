using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Web.Services;
using System.Data;
using System.IO;

namespace R2MD
{
    public partial class ServiceViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        public void ExportExcel(string Name = null, string Service = null, string Provider = null, string dateFrom = null, string dateTo = null, string Status = null)
        {
            Service se = new Service();
            DataTable dt = se.getServiceList(Name, Service, Provider, dateFrom, dateTo, Status);
            //first column name cannot be "ID" https://support.microsoft.com/en-us/help/323626/-sylk-file-format-is-not-valid-error-message-when-you-open-file
            string attach = "attachment;filename=Service_Requests_" + DateTime.Now +".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attach);
            Response.ContentType = "application/ms-excel";
            if (dt != null)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    Response.Write(dc.ColumnName  + "\t");
                }
                Response.Write(System.Environment.NewLine);
                foreach (DataRow dr in dt.Rows)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        Response.Write("\""+dr[i].ToString()+ "\"" + "\t");
                    }
                    Response.Write("\n");
                }
                Response.End();
            }
        }

        protected void Excel_Click(object sender, EventArgs e)
        {
            
            string Name = HideName.Value;
            string status = HideStatus.Value;
            string from = HideFrom.Value;
            string to = HideTo.Value;
            if (string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(status) && string.IsNullOrEmpty(from) && string.IsNullOrEmpty(to)) {
                Name = Request.QueryString["Name"] != null? Request.QueryString["Name"].ToString():"";
                from = Request.QueryString["dateFrom"] != null ? Request.QueryString["dateFrom"].ToString() : "";
                to = Request.QueryString["dateTo"] != null ? Request.QueryString["dateTo"].ToString() : "";
                status = Request.QueryString["Status"] != null ? Request.QueryString["Status"].ToString() : "";
            }
            ExportExcel(Name,null,null,from,to,status);
        }
    }
}