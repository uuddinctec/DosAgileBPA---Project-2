using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace R2MD
{
    public partial class ReadAttachment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var fileName = "";
            byte[] data = new byte[] { 0, 0, 0, 0 };
            var attID = Request.QueryString["ID"];
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(@"SELECT [PatientAttachmentId],[PatientId]
                                                    ,[AttachedDocument],[FileName],[FileType]
                                                    FROM [PI].[PatientAttachment]
                                                    where PatientAttachmentId=@id", conn);
                    cmd.Parameters.AddWithValue("@id", attID);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr != null && dr.HasRows) {
                        while (dr.Read())
                        {
                            fileName = (string)dr["FileName"];
                            data = (byte[])dr["AttachedDocument"];
                        }
                    }                    
                    dr.Close();

                    Response.Clear();
                    Response.AddHeader("Cache-Control", "no-cache, must-revalidate, post-check=0, pre-check=0");
                    Response.AddHeader("Pragma", "no-cache");
                    Response.AddHeader("Content-Description", "File Download");
                    Response.AddHeader("Content-Type", "application/force-download");
                    Response.AddHeader("Content-Transfer-Encoding", "binary\n");
                    Response.AddHeader("content-disposition", "attachment;filename=\"" + fileName+"\"");
                    Response.BinaryWrite(data);
                    Response.End();
                }
                catch (Exception ex) { }
                finally
                {
                    if (conn != null) conn.Close();
                }
            }
        }
    }
}