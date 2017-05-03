<%@ Page Title="" Language="C#" MasterPageFile="~/Guest.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Todo.Dashboard.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        var Dashboard = {};
        Dashboard.stickyColors = <%= serializer.Serialize(availableColors) %>;
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="dashboard">
        
        <div id="new-list" class="sticky sticky-th-purple">
            <div class="sticky-title">
                <a href="#" class="add-items" data-new-list="1"><i class="fa fa-plus" aria-hidden="true"></i></a>
                <div class="input" contenteditable="true"></div>
            </div>
            <div class="sticky-items">
                <div class="sticky-item">
                    <div data-new-list="1" class="trash"><i class="fa fa-trash-o" aria-hidden="true"></i></div>
                    <div class="input" contenteditable="true"></div>
                </div>
                <div class="sticky-item">
                    <div data-new-list="1" class="trash"><i class="fa fa-trash-o" aria-hidden="true"></i></div>
                    <div class="input" contenteditable="true"></div>
                </div>
                <div class="sticky-item">
                    <div data-new-list="1" class="trash"><i class="fa fa-trash-o" aria-hidden="true"></i></div>
                    <div class="input" contenteditable="true"></div>
                </div>
            </div>
            <div class="sticky-controls">
                <div class="select-color">
                    <% for (int i = 0; i < availableColors.Length; i++) {  %>
                        <a href="#" class="slcl-<%: availableColors[i] %>"></a>
                    <% } %>
                </div>
                <a href="#" id="create-list" class="button">CREATE</a>
            </div>
        </div>

        <div id="lists" class="clear">
        <% for (int listIndex = 0; listIndex < userLists.Rows.Count; listIndex++) { %>
            <div
                data-id="<%: userLists.Rows[listIndex]["id"] %>"
                class="sticky sticky-th-<%= HttpUtility.HtmlEncode(userLists.Rows[listIndex]["list_theme"]).Replace("\n", "<br/>").Replace(" ", "&nbsp;") %>">
                    <div class="sticky-title">
                        <a href="#" class="add-items"><i class="fa fa-plus" aria-hidden="true"></i></a>
                        <div class="input" contenteditable="true"><%: userLists.Rows[listIndex]["list_name"] %></div>
                    </div>
                    <div class="sticky-items">
                        <%  for (int itemIndex = 0; itemIndex < userListsItems[listIndex].Rows.Count; itemIndex++) { %>
                            <div class="sticky-item" data-li-id="<%: userListsItems[listIndex].Rows[itemIndex]["id"] %>">
                                <div class="trash"><i class="fa fa-trash-o" aria-hidden="true"></i></div>
                                <div class="input" contenteditable="true"><%= HttpUtility.HtmlEncode(userListsItems[listIndex].Rows[itemIndex]["item_content"]).Replace("\n", "<br/>").Replace(" ", "&nbsp;") %></div>
                            </div>
                        <% } %>
                    </div>
                    <div class="sticky-controls">
                        <div class="select-color">
                            <% for (int j = 0; j < availableColors.Length; j++) { %>
                                <a href="#" class="slcl-<%: availableColors[j] %>"></a>
                            <% } %>
                        </div>
                        <a href="#" class="save-list button">SAVE</a>
                        <a href="#" class="delete-list button" title="Permanent Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></a>
                    </div>
                </div>
        <% } %>
        </div>

        <% /*<div id="lists" class="clear">
            <% for (int i = 0; i < availableColors.Length; i++) { #>
                <div class="sticky sticky-th-<%: availableColors[i] #>">
                    <div class="sticky-title">
                        <a href="#" class="add-items"><i class="fa fa-plus" aria-hidden="true"></i></a>
                        <div class="input" contenteditable="true"></div>
                    </div>
                    <div class="sticky-items">
                        <div class="sticky-item">
                            <div class="trash"><i class="fa fa-trash-o" aria-hidden="true"></i></div>
                            <div class="input" contenteditable="true"></div>
                        </div>
                        <div class="sticky-item">
                            <div class="trash"><i class="fa fa-trash-o" aria-hidden="true"></i></div>
                            <div class="input" contenteditable="true"></div>
                        </div>
                    </div>
                    <div class="sticky-controls">
                        <div class="select-color">
                            <% for (int j = 0; j < availableColors.Length; j++) {  #>
                                <a href="#" class="slcl-<%: availableColors[j] #>"></a>
                            <% } #>
                        </div>
                        <a href="#" class="button">SAVE</a>
                    </div>
                </div>
            <% } #>
        </div>
        <% */ %>
    </div>
</asp:Content>