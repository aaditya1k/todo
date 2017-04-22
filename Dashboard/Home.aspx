<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Todo.Dashboard.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/js/jquery-3.2.1.min.js"></script>
    <script src="/js/dashboard.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <% for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        { %>
            <%: ds.Tables[0].Rows[i][0] %>
        <%  } %>
</asp:Content>
