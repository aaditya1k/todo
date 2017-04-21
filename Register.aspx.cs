﻿using System;
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

            if (String.IsNullOrWhiteSpace(Email.Text))
            {
                this.errors.Add("Email address is required");
            }
            else if (!Validator.IsValidEmailAddress(Email.Text))
            {
                this.errors.Add("Email address is not valid");
            }

            if (Password.Text.Length <= 7)
            {
                this.errors.Add("Password needs to be minimum 8 characters");
            }
            else if (Password.Text != Confirm_Password.Text)
            {
                this.errors.Add("Password do not match with confirm password");
            }

            if (String.IsNullOrEmpty(First_Name.Text))
            {
                this.errors.Add("First name is required");
            }

            if (this.errors.Count == 0)
            {
                this.con.Open();
                SqlCommand cmd = new SqlCommand("insert into users (email, password, first_name, last_name, created_at) values (@email, @password, @first_name , @last_name, @created_at)", this.con);

                cmd.Parameters.AddWithValue("@email", Email.Text);
                cmd.Parameters.AddWithValue("@password", HelperLibrary.Hash_Password(Password.Text, "test"));
                cmd.Parameters.AddWithValue("@first_name", First_Name.Text);
                cmd.Parameters.AddWithValue("@last_name", Last_Name.Text);
                cmd.Parameters.AddWithValue("@created_at", DateTime.Now);
                int check = cmd.ExecuteNonQuery();
                con.Close();

                if (check == 1)
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "Alert", "alert('Register Successful');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "Alert", "alert('Something went wrong');", true);
                }
            }
        }
    }
}