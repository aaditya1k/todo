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
        protected DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.con.Open();
            SqlCommand cmd = new SqlCommand("select * from lists where id=@id", this.con);
            cmd.Parameters.AddWithValue("@id", User.Identity.Name);

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(this.ds);
            this.con.Close();
        }
    }
}