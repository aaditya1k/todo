<%@ Page Title="Register" Language="C#" MasterPageFile="~/Guest.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Todo.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mid-form">
        <div class="form-title"><i class="fa fa-user-plus" aria-hidden="true"></i> Register</div>
        <% if (errors.Count != 0) { %>
            <ul class="errors">
            <% foreach (Object err in errors) { %>
                <li class="error"><%: err %></li>
            <% } %>
            </ul>
        <% }  %>
        <div class="register">
            <form id="register" runat="server">
                <div class="form-row"><div class="form-label">Email*</div> <asp:TextBox ID="Email" runat="server" placeholder="Email"></asp:TextBox></div>
                <div class="form-row"><div class="form-label">Password*</div> <asp:TextBox ID="Password" TextMode="Password" runat="server" placeholder="Password"></asp:TextBox></div>
                <div class="form-row"><div class="form-label">Confirm Password*</div> <asp:TextBox ID="Confirm_Password" TextMode="Password" runat="server" placeholder="Confirm Password"></asp:TextBox></div>
                <div class="form-row"><div class="form-label">FirstName*</div> <asp:TextBox ID="First_Name" runat="server" placeholder="First Name"></asp:TextBox></div>
                <div class="form-row"><div class="form-label">LastName</div> <asp:TextBox ID="Last_Name" runat="server" placeholder="Last Name"></asp:TextBox></div>
                <div class="form-row"><asp:Button ID="Button1" runat="server" Text="Register" OnClick="Register_Submit" /></div>
            </form>
        </div>
    </div>
</asp:Content>
