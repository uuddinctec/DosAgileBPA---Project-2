using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace R2MD
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void ValidateUser(object sender, EventArgs e)
        {
            int userId = 0;
            var password = ChirrpManagerUserLogin.Password;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ValidateUser", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", ChirrpManagerUserLogin.UserName);
                    cmd.Parameters.AddWithValue("@Password", ChirrpManagerUserLogin.Password);
                    conn.Open();
                    userId = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch(Exception ex)
                {

                }
                finally
                {
                    conn.Close();
                }
            }
            switch (userId)
            {
                case -1:
                    ChirrpManagerUserLogin.FailureText = "Username and/or password is incorrect.";
                    break;
                default:
                    Session["UserID"] = userId;
                    FormsAuthentication.RedirectFromLoginPage(ChirrpManagerUserLogin.UserName, ChirrpManagerUserLogin.RememberMeSet);
                    break;
            }
        }
    }
}