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

namespace Todo.Dashboard
{
    public partial class Edit : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn"].ToString());
        private SqlDataAdapter da = new SqlDataAdapter();
        protected ArrayList errors = new ArrayList();

        protected DataTable userData = new DataTable();
        protected Boolean saved = false;
        protected Boolean changedPassword = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.con.Open();
            SqlCommand cmd = new SqlCommand("select * from users where id=@id", this.con);
            cmd.Parameters.AddWithValue("@id", User.Identity.Name);
            da.SelectCommand = cmd;
            da.Fill(this.userData);
            this.con.Close();

            if (!IsPostBack)
            {
                Email.Text = this.userData.Rows[0]["email"].ToString();
                First_Name.Text = this.userData.Rows[0]["first_name"].ToString();
                Last_Name.Text = this.userData.Rows[0]["last_name"].ToString();
            }
        }

        protected void Edit_Submit(object sender, EventArgs e)
        {
            ValidtorLibrary Validator = new ValidtorLibrary();
            this.con.Open();
            SqlCommand cmd;

            if (String.IsNullOrWhiteSpace(Email.Text))
            {
                this.errors.Add("Email address is required");
            }
            else if (!Validator.IsValidEmailAddress(Email.Text))
            {
                this.errors.Add("Email address is not valid");
            }
            else if (this.userData.Rows[0]["email"].ToString() != Email.Text)
            {
                cmd = new SqlCommand("select id from users where email=@email and id != @id", this.con);
                cmd.Parameters.AddWithValue("@email", Email.Text);
                cmd.Parameters.AddWithValue("@id", User.Identity.Name);
                int result = Convert.ToInt32(cmd.ExecuteScalar());

                if (result != 0)
                {
                    this.errors.Add("This email id is already in use");
                }
            }

            if (String.IsNullOrEmpty(First_Name.Text))
            {
                this.errors.Add("First name is required");
            }

            string hashedPassword = this.userData.Rows[0]["password"].ToString();
            if (OldPassword.Text.Length > 0)
            {
                if (HelperLibrary.Hash_Password(OldPassword.Text, "test") != hashedPassword)
                {
                    this.errors.Add("Old Password is wrong");
                }
                else if (Password.Text.Length < 6)
                {
                    this.errors.Add("Password needs to be minimum 6 characters");
                }
                else if (Password.Text != Confirm_Password.Text)
                {
                    this.errors.Add("Password do not match with confirm password");
                }
                else
                {
                    hashedPassword = HelperLibrary.Hash_Password(Password.Text, "test");
                    this.changedPassword = true;
                }
            }

            if (this.errors.Count == 0)
            {
                cmd = new SqlCommand("update users set email=@email, password=@password, first_name=@first_name, last_name=@last_name where id=@id", this.con);

                cmd.Parameters.AddWithValue("@email", Email.Text);
                cmd.Parameters.AddWithValue("@password", hashedPassword);
                cmd.Parameters.AddWithValue("@first_name", First_Name.Text);
                cmd.Parameters.AddWithValue("@last_name", Last_Name.Text);
                cmd.Parameters.AddWithValue("@id", User.Identity.Name);
                cmd.ExecuteNonQuery();

                this.saved = true;
            }

            this.con.Close();
        }
    }
}