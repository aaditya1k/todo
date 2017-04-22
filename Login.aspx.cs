using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using Todo.Library;
using System.Web.Security;

/**
 * Page.User.Identity.IsAuthenticated || User.Identity.IsAuthenticated || Request.IsAuthenticated
 */

namespace Todo
{
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn"].ToString());
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            this.con.Open();
            SqlCommand cmd = new SqlCommand("select id from users where email=@email and password=@password", this.con);
            cmd.Parameters.AddWithValue("@email", SqlDbType.VarChar).Value = Login1.UserName;
            cmd.Parameters.AddWithValue("@password", SqlDbType.VarChar).Value = HelperLibrary.Hash_Password(Login1.Password, "test");
            int result = Convert.ToInt32(cmd.ExecuteScalar());

            this.con.Close();
            if (result == 0)
            {
                e.Authenticated = false;
            }
            else
            {
                e.Authenticated = true;
                this.Authenticate(result, Login1.RememberMeSet);
                // FormsAuthentication.RedirectFromLoginPage(Login1.UserName, Login1.RememberMeSet);
            }
        }

        private void Authenticate(int UserId, Boolean RememberUser)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1,
                UserId.ToString(), // HttpContext.Current.User.Identity.Name || User.Identity.Name
                DateTime.Now,
                DateTime.Now.AddMinutes(50),
                RememberUser,
                "user", // HttpContext.Current.User.IsInRole("admin")
                FormsAuthentication.FormsCookiePath
             );

            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie authCookie = new HttpCookie(
                            FormsAuthentication.FormsCookieName,
                            encryptedTicket);

            authCookie.HttpOnly = true;
            authCookie.Expires = DateTime.Now.AddHours(12);

            Response.Cookies.Add(authCookie);

            string returnUrl = Request.QueryString["ReturnUrl"];
            if (returnUrl == null)
            {
                returnUrl = "~/Dashboard/Home.aspx";
            }
            Response.Redirect(returnUrl);
        }
    }
}