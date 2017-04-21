using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using Todo.Library;

namespace Todo
{
    public partial class Register : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn"].ToString());
        protected ArrayList errors = new ArrayList();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
            }
        }

        protected void Register_Submit(object sender, EventArgs e)
        {
            ValidtorLibrary Validator = new ValidtorLibrary();

            if (String.IsNullOrWhiteSpace(Email.Text)) {
                this.errors.Add("Please enter your email address");
            } else if (!Validator.IsValidEmailAddress(Email.Text)) {
                this.errors.Add("Please enter a valid email address");
            }

            if (Password.Text.Length <= 7) {
                this.errors.Add("Password needs to be atleast 8 characters");
            } else if (Password.Text != Confirm_Password.Text) {
                this.errors.Add("Password do not match with confirm password");
            }
        }
    }
}