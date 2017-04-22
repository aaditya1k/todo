<%@ Page Title="Login" Language="C#" MasterPageFile="~/Guest.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Todo.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mid-form">
        <div class="form-title"><i class="fa fa-user-plus" aria-hidden="true"></i> Login</div>
        <form runat="server">
             <asp:Login ID="Login1" runat="server"
                 OnAuthenticate="Login1_Authenticate"
                 UserNameLabelText="E-mail Address" 
                 PasswordLabelText="Password" 
                 UserNameRequiredErrorMessage="E-mail Address is required"
                 ></asp:Login>
        </form>
    </div>
</asp:Content>