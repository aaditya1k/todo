<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Todo.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <% if (errors.Count != 0) { %>
        <ul class="errors">
        <% foreach (Object err in errors) { %>
            <li class="error"><%: err %></li>
        <% } %>
        </ul>
    <% }  %>
    <form id="register" runat="server">
        Email*: <asp:TextBox ID="Email" runat="server" placeholder="Email"></asp:TextBox><br />
        Password*: <asp:TextBox ID="Password" TextMode="Password" runat="server" placeholder="Password"></asp:TextBox><br />
        Confirm Password*: <asp:TextBox ID="Confirm_Password" TextMode="Password" runat="server" placeholder="Confirm Password"></asp:TextBox><br />
        FirstName*: <asp:TextBox ID="First_Name" runat="server" placeholder="First Name"></asp:TextBox><br />
        LastName: <asp:TextBox ID="Last_Name" runat="server" placeholder="Last Name"></asp:TextBox><br />
        <asp:Button ID="Button1" runat="server" Text="Register" OnClick="Register_Submit" />
    </form>
</asp:Content>
