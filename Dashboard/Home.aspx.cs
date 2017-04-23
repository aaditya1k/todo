using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Todo.Dashboard
{
    public partial class Home : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn"].ToString());
        SqlDataAdapter da = new SqlDataAdapter();
        protected DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.con.Open();
            SqlCommand cmd = new SqlCommand("select * from lists where user_id=@user_id", this.con);
            cmd.Parameters.AddWithValue("@user_id", User.Identity.Name);
            da.SelectCommand = cmd;
            da.Fill(dt);
            this.con.Close();
        }
    }
}