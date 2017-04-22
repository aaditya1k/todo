<%@ Page Title="" Language="C#" MasterPageFile="~/Guest.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Todo.Dashboard.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/js/jquery-3.2.1.min.js"></script>
    <script src="/js/dashboard.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="dashboard">
        <a href="#" id="new_list" class="button">New List</a>

        <div id="lists">
            <div class="list">
                <div class="list-title">
                    <input type="text" value="" placeholder="List Title"/>
                </div>
                <div class="list-items">
                    <div class="item">
                        <input type="checkbox" />
                        <input type="text" placeholder="Item 1"/>
                        <a href="#" class="remove-item"><i class="fa fa-remove"></i></a>
                    </div>
                    <div class="item">
                        <input type="checkbox" />
                        <input type="text" placeholder="Item 2"/>
                        <a href="#" class="remove-item"><i class="fa fa-remove"></i></a>
                    </div>
                    <div class="item">
                        <input type="checkbox" />
                        <input type="text" placeholder="Item 3"/>
                        <a href="#" class="remove-item"><i class="fa fa-remove"></i></a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
