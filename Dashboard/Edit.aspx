<%@ Page Title="" Language="C#" MasterPageFile="~/Guest.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Todo.Dashboard.Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mid-form">
        <div class="form-title"><i class="fa fa-user-plus" aria-hidden="true"></i> Edit Profile</div>
        <% if (errors.Count != 0) { %>
            <ul class="errors">
            <% foreach (Object err in errors) { %>
                <li class="error"><%: err %></li>
            <% } %>
            </ul>
        <% }  %>

        <% if (saved) { %>
            <ul class="success">
                <li>Profile saved successfully!</li>
                <% if (changedPassword) { %>
                    <li>Password changed successfully!</li>
                <% } %>
            </ul>
        <% } %>
        <div class="register">
            <form id="edit" runat="server">
                <div class="form-row"><div class="form-label">Email*</div> <asp:TextBox ID="Email" runat="server" placeholder="Email"></asp:TextBox></div>
                <div class="form-row"><div class="form-label">FirstName*</div> <asp:TextBox ID="First_Name" runat="server" placeholder="First Name"></asp:TextBox></div>
                <div class="form-row"><div class="form-label">LastName</div> <asp:TextBox ID="Last_Name" runat="server" placeholder="Last Name"></asp:TextBox></div>
                
                <div class="form-title"><i class="fa fa-key" aria-hidden="true"></i> Password</div>
                <div style="margin: 5px 0 0 25px;color: #686868"><small>Leave blank if you don't want to update password</small></div>

                <div class="form-row"><div class="form-label">Old Password</div> <asp:TextBox ID="OldPassword" TextMode="Password" runat="server" placeholder="Old Password"></asp:TextBox></div>
                <div class="form-row"><div class="form-label">New Password</div> <asp:TextBox ID="Password" TextMode="Password" runat="server" placeholder="New Password"></asp:TextBox></div>
                <div class="form-row"><div class="form-label">Confirm New Password</div> <asp:TextBox ID="Confirm_Password" TextMode="Password" runat="server" placeholder="Confirm New Password"></asp:TextBox></div>

                <div class="form-row"><asp:Button ID="Button1" runat="server" Text="Save" OnClick="Edit_Submit" /></div>
            </form>
        </div>
    </div>
</asp:Content>
