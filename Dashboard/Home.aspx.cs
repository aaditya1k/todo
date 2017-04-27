using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Collections;

namespace Todo.Dashboard
{
    public partial class Home : System.Web.UI.Page
    {
        private SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn"].ToString());
        private SqlDataAdapter da = new SqlDataAdapter();

        protected DataTable userLists = new DataTable();
        protected DataTable[] userListsItems;
        protected JavaScriptSerializer serializer = new JavaScriptSerializer();
        protected string[] availableColors = new string[] {
            "green",
            "purple",
            "pink",
            "black",
            "grey",
            "yellow",
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.HttpMethod == "POST")
            {
                if (!String.IsNullOrWhiteSpace(Request.QueryString["add-new"]) && Request.QueryString["add-new"] == "1")
                {
                    this.Add_New();
                }
                else
                {
                    Response.ContentType = "application/json; charset=utf-8";
                    Response.Write(this.serializer.Serialize(new
                    {
                        status = 0,
                        message = "Not allowed"
                    }));
                    Response.End();
                }
            }
            else
            {
                this.con.Open();
                SqlCommand cmd = new SqlCommand("select * from lists where user_id=@user_id order by id desc", this.con);
                cmd.Parameters.AddWithValue("@user_id", User.Identity.Name);
                da.SelectCommand = cmd;
                da.Fill(this.userLists);

                this.userListsItems = new DataTable[this.userLists.Rows.Count];

                // foreach (DataRow a in userLists.Rows)
                for (int i = 0; i < this.userLists.Rows.Count; ++i)
                {
                    this.userListsItems[i] = new DataTable();
                    cmd = new SqlCommand("select * from list_items where list_id=@list_id order by id asc", this.con);
                    cmd.Parameters.AddWithValue("@list_id", this.userLists.Rows[i]["id"]);
                    da.SelectCommand = cmd;
                    da.Fill(this.userListsItems[i]);
                }

                this.con.Close();
            }
        }

        private void Add_New()
        {
            List<string> items = new List<string>();
            foreach (string key in Request.Form.AllKeys)
            {
                if (key.StartsWith("items"))
                {
                    items.Add(key);
                }
            }

            Response.ContentType = "application/json; charset=utf-8";

            this.con.Open();
            SqlCommand cmd = new SqlCommand("insert into lists (user_id, list_name, list_theme, is_completed, created_at) output inserted.id values (@user_id, @list_name, @list_theme, 0, @created_at)", this.con);
            cmd.Parameters.AddWithValue("@user_id", User.Identity.Name);
            cmd.Parameters.AddWithValue("@list_name", Request.Form["title"].Trim());
            cmd.Parameters.AddWithValue("@list_theme", Request.Form["color"].Trim());
            cmd.Parameters.AddWithValue("@created_at", DateTime.Now);
            int newId = (Int32)cmd.ExecuteScalar();

            if (newId > 0)
            {
                foreach (var a in items)
                {
                    cmd = new SqlCommand("insert into list_items (list_id, item_content, is_completed, created_at) values (@list_id, @item_content,0, @created_at)", this.con);
                    cmd.Parameters.AddWithValue("@list_id", newId);
                    cmd.Parameters.AddWithValue("@item_content", Request.Form[a].Trim());
                    cmd.Parameters.AddWithValue("@created_at", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }

                Response.Write(this.serializer.Serialize(new
                {
                    status = 1
                }));
            }
            else
            {
                Response.Write(this.serializer.Serialize(new
                {
                    status = 0,
                    message = "Something went wrong: " + newId
                }));
            }

            this.con.Close();
            Response.End();
        }
    }
}