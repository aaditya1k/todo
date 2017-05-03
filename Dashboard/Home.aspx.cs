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

    /**
     * Need to validate list_theme in Add_New and Update_list
     */

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
                else if (!String.IsNullOrWhiteSpace(Request.QueryString["update"]) && !String.IsNullOrWhiteSpace(Request.QueryString["id"]))
                {
                    this.Update_List();
                }
                else if (!String.IsNullOrWhiteSpace(Request.QueryString["delete"]) && !String.IsNullOrWhiteSpace(Request.QueryString["id"]))
                {
                    this.Delete_List();
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
            Response.ContentType = "application/json; charset=utf-8";

            List<string> items = new List<string>();
            foreach (string key in Request.Form.AllKeys)
            {
                if (key.StartsWith("items"))
                {
                    items.Add(key);
                }
            }

            if (Request.Form["title"].Length == 0)
            {
                Response.Write(this.serializer.Serialize(new
                {
                    status = 0,
                    message = "Unable to create list, Please enter a title!"
                }));
            }
            else
            {
                this.con.Open();
                SqlCommand cmd = new SqlCommand("insert into lists (user_id, list_name, list_theme, created_at) output inserted.id values (@user_id, @list_name, @list_theme, @created_at)", this.con);
                cmd.Parameters.AddWithValue("@user_id", User.Identity.Name);
                cmd.Parameters.AddWithValue("@list_name", Request.Form["title"].Trim());
                cmd.Parameters.AddWithValue("@list_theme", Request.Form["color"].Trim());
                cmd.Parameters.AddWithValue("@created_at", DateTime.Now);
                int newId = (Int32)cmd.ExecuteScalar();

                if (newId > 0)
                {
                    foreach (var a in items)
                    {
                        cmd = new SqlCommand("insert into list_items (list_id, item_content, created_at) values (@list_id, @item_content, @created_at)", this.con);
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
            }

            Response.End();
        }

        private void Update_List()
        {
            Response.ContentType = "application/json; charset=utf-8";

            List<string> items = new List<string>();
            List<string> ids = new List<string>();
            List<string> delIds = new List<string>();
            foreach (string key in Request.Form.AllKeys)
            {
                if (key.StartsWith("items"))
                {
                    items.Add(key);
                }
                else if (key.StartsWith("ids"))
                {
                    ids.Add(key);
                }
                else if (key.StartsWith("delIds"))
                {
                    delIds.Add(key);
                }
            }

            int itemsLength = items.ToArray().Length;

            if (itemsLength != ids.ToArray().Length)
            {
                Response.Write(this.serializer.Serialize(new
                {
                    status = 0,
                    message = "Something went wrong, Please try again."
                }));
            }
            else
            {
                this.con.Open();

                DataTable lists = new DataTable();

                SqlCommand cmd = new SqlCommand("select * from lists where id=@id and user_id=@user_id", this.con);
                cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                cmd.Parameters.AddWithValue("@user_id", User.Identity.Name);
                da.SelectCommand = cmd;
                da.Fill(lists);
                
                if (lists.Rows.Count == 0)
                {
                    Response.Write(this.serializer.Serialize(new
                    {
                        status = 0,
                        message = "Something went wrong, Please try again."
                    }));
                    return;
                }

                cmd = new SqlCommand("update lists set list_name=@list_name, list_theme=@list_theme where id=@id and user_id=@user_id", this.con);
                cmd.Parameters.AddWithValue("@list_name", Request.Form["title"].Trim());
                cmd.Parameters.AddWithValue("@list_theme", Request.Form["color"].Trim());
                cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                cmd.Parameters.AddWithValue("@user_id", User.Identity.Name);
                cmd.ExecuteScalar();

                int delLength = delIds.ToArray().Length;

                for (int i = 0; i < itemsLength; i++)
                {
                    string id = Request.Form[ids.ElementAt(i)];

                    if (id == "0")
                    {
                        cmd = new SqlCommand("insert into list_items (list_id, item_content, created_at) values (@list_id, @item_content, @created_at)", this.con);
                        cmd.Parameters.AddWithValue("@list_id", Request.QueryString["id"]);
                        cmd.Parameters.AddWithValue("@item_content", Request.Form[items.ElementAt(i)]);
                        cmd.Parameters.AddWithValue("@created_at", DateTime.Now);
                        cmd.ExecuteScalar();
                    }
                    else
                    {
                        Boolean deleted = false;
                        for (int j = 0; j < delLength; j++)
                        {
                            if (Request.Form[delIds.ElementAt(j)] == id)
                            {
                                cmd = new SqlCommand("delete from list_items where list_id=@list_id and id=@id", this.con);
                                cmd.Parameters.AddWithValue("@list_id", Request.QueryString["id"]);
                                cmd.Parameters.AddWithValue("@id", id);
                                cmd.ExecuteScalar();
                                deleted = true;
                                break;
                            }
                        }
                        if (!deleted)
                        {
                            cmd = new SqlCommand("update list_items set item_content=@item_content where list_id=@list_id and id=@id", this.con);
                            cmd.Parameters.AddWithValue("@item_content", Request.Form[items.ElementAt(i)]);
                            cmd.Parameters.AddWithValue("@list_id", Request.QueryString["id"]);
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteScalar();
                        }
                    }
                }

                Response.Write(this.serializer.Serialize(new
                {
                    status = 1
                }));
            }

            Response.End();
        }

        private void Delete_List()
        {
            Response.ContentType = "application/json; charset=utf-8";

            this.con.Open();
            DataTable lists = new DataTable();

            SqlCommand cmd = new SqlCommand("select * from lists where id=@id and user_id=@user_id", this.con);
            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
            cmd.Parameters.AddWithValue("@user_id", User.Identity.Name);
            da.SelectCommand = cmd;
            da.Fill(lists);

            if (lists.Rows.Count == 0)
            {
                Response.Write(this.serializer.Serialize(new
                {
                    status = 0,
                    message = "Something went wrong, Please try again."
                }));
                Response.End();
                return;
            }

            cmd = new SqlCommand("delete from lists where id=@id", this.con);
            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
            cmd.ExecuteScalar();

            cmd = new SqlCommand("delete from list_items where list_id=@list_id", this.con);
            cmd.Parameters.AddWithValue("@list_id", Request.QueryString["id"]);
            cmd.ExecuteScalar();

            this.con.Close();

            Response.Write(this.serializer.Serialize(new
            {
                status = 1
            }));
            Response.End();
        }
    }
}
 