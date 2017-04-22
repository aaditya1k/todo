<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Todo.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <% foreach (Object err in errors) { %>
        <li><%: err %></li>
    <% } %>
    <form id="register" runat="server">
        Email: <asp:TextBox ID="Email" runat="server"></asp:TextBox><br />
        Password: <asp:TextBox ID="Password" TextMode="Password" runat="server"></asp:TextBox><br />
        Confirm Password: <asp:TextBox ID="Confirm_Password" TextMode="Password" runat="server"></asp:TextBox><br />
        FirstName: <asp:TextBox ID="First_Name" runat="server"></asp:TextBox><br />
        LastName: <asp:TextBox ID="Last_Name" runat="server"></asp:TextBox><br />
        <asp:Button ID="Button1" runat="server" Text="Register" OnClick="Register_Submit" />
    </form>
</asp:Content>
