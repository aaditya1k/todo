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
                <a href="#" class="add-items"><i class="fa fa-plus" aria-hidden="true"></i></a>
                <div class="input" contenteditable="true">my_title</div>
            </div>
            <div class="sticky-items">
                <div class="sticky-item">
                    <div class="trash"><i class="fa fa-trash-o" aria-hidden="true"></i></div>
                    <div class="input" contenteditable="true">asd1</div>
                </div>
                <div class="sticky-item">
                    <div class="trash"><i class="fa fa-trash-o" aria-hidden="true"></i></div>
                    <div class="input" contenteditable="true">asd2</div>
                </div>
                <div class="sticky-item">
                    <div class="trash"><i class="fa fa-trash-o" aria-hidden="true"></i></div>
                    <div class="input" contenteditable="true">asd2</div>
                </div>
                <div class="sticky-item">
                    <div class="trash"><i class="fa fa-trash-o" aria-hidden="true"></i></div>
                    <div class="input" contenteditable="true">asd2</div>
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

        <% for (int listIndex = 0; listIndex < userLists.Rows.Count; listIndex++) { %>
            <div>
                <%: userLists.Rows[listIndex]["list_name"] %>
                <%  for (int itemIndex = 0; itemIndex < userListsItems[listIndex].Rows.Count; itemIndex++) { %>
                    &nbsp;&nbsp;<%: userListsItems[listIndex].Rows[itemIndex]["id"] %> <%: userListsItems[listIndex].Rows[itemIndex]["item_content"] %><br />
                <% } %>
            </div>
        <% } %>

        <div id="lists" class="clear">
            <% for (int i = 0; i < availableColors.Length; i++) { %>
                <div class="sticky sticky-th-<%: availableColors[i] %>">
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
                            <% for (int j = 0; j < availableColors.Length; j++) {  %>
                                <a href="#" class="slcl-<%: availableColors[j] %>"></a>
                            <% } %>
                        </div>
                        <a href="#" class="button">SAVE</a>
                    </div>
                </div>
            <% } %>
        </div>
    </div>
</asp:Content>