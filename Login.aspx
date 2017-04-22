<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Todo.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
         <asp:Login ID="Login1" runat="server"
             OnAuthenticate="Login1_Authenticate"
             UserNameLabelText="E-mail Address:" 
             UserNameRequiredErrorMessage="E-mail Address is required"
             ></asp:Login>
    </form>
</asp:Content>