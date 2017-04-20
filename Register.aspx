<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Todo.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        Email: <input id="Text1" type="text" /><br />
        Password: <input id="Text2" type="password" /><br />
        FirstName: <input id="Text3" type="text" /><br />
        LastName: <input id="Text4" type="text" /><br />
        <input id="Submit1" type="submit" value="Submit" />
    </form>
</asp:Content>
