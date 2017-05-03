<%@ Page Title="" Language="C#" MasterPageFile="~/Guest.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Todo.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="home">
        <div class="spotlight">
            <div class="content">
                <div class="bold-line">Do More, Every Day!</div>
                <div class="light-line">Become more organized and productive online with ToDo lists.</div>

                <a href="/Register.aspx" class="button">Get Started for Free</a>
            </div>
        </div>

        <div class="features">
            <div class="content">
                <div class="bold-line"><i class="fa fa-star-o" aria-hidden="true"></i> What you get?</div>

                <div class="light-line">Access Todo Lists Everywhere</div>
                <div class="access">
                    <div class="access-icon">
                        <i class="fa fa-desktop" aria-hidden="true"></i>
                        <div class="label">Desktop</div>
                    </div>
                    <div class="access-icon">
                        <i class="fa fa-tablet" aria-hidden="true"></i>
                        <div class="label">Tablet</div>
                    </div>
                    <div class="access-icon">
                        <i class="fa fa-mobile" aria-hidden="true"></i>
                        <div class="label">Mobile</div>
                    </div>
                </div>

                <div class="light-line">Separate Lists with colors</div>
                <img src="/images/Todo.png" style="width: 100%"/>
            </div>
        </div>

        <div class="social-access">
            <div class="content">
                <div class="bold-line"><i class="fa fa-paper-plane-o" aria-hidden="true"></i> Connect with us</div>

                <div class="social-icons">
                    <a href="#" target="_blank"><i class="fa fa-facebook-official" aria-hidden="true"></i></a>
                    <a href="#" target="_blank"><i class="fa fa-twitter-square" aria-hidden="true"></i></a>
                    <a href="#" target="_blank"><i class="fa fa-instagram" aria-hidden="true"></i></a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
