<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_2_2aventyrliga_kontakter.Default" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Äventyrliga kontakter</title>
    <link href="Content/Style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="main">
            <h1>Äventyrliga kontakter</h1>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                CssClass="ErrorMessages" HeaderText="Fel inträffade. Åtgärda och försök igen" />

            <%--DataKeyNames är Jätteviktig för hantering av primärnycklar/Id --%>
            <asp:ListView ID="ContactListView" runat="server"
                ItemType="_2_2aventyrliga_kontakter.Model.Contact"
                SelectMethod="ContactListView_GetDataPageWise"
                InsertMethod="ContactListView_InsertItem"
                UpdateMethod="ContactListView_UpdateItem"
                DeleteMethod="ContactListView_DeleteItem"
                OnItemEditing="ContactListView_ItemEditing"
                OnItemCanceling="ContactListView_ItemCanceling"
                DataKeyNames="ContactId"
                InsertItemPosition="None">
                <LayoutTemplate>
                    <table>
                        <tr>
                            <th>Förnamn</th>
                            <th>Efternamn</th>
                            <th>E-post</th>
                        </tr>
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                    </table>
                    <asp:DataPager ID="DataPager1" runat="server" PageSize="25">
                            <Fields>
                                <asp:NextPreviousPagerField  ShowFirstPageButton="True" ShowNextPageButton="False" 
                                    ShowPreviousPageButton="False" FirstPageText="|<" />
                                <asp:NumericPagerField />
                                <asp:NextPreviousPagerField  ShowLastPageButton="True" 
                                    LastPageText=">|" ShowNextPageButton="False" 
                                    ShowPreviousPageButton="False" /> 
                            </Fields>
                        </asp:DataPager>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%#: Item.FirstName %>
                        </td>
                        <td>
                            <%#: Item.LastName %>
                        </td>
                        <td>
                            <%#: Item.EmailAddress %>
                        </td>
                        <td class="command">
                            <%-- "Kommandknappar" för att ta bort och redigera kunduppgifter. Kommandonamnen är VIKTIGA! --%>
                            <asp:LinkButton runat="server" CommandName="Delete" Text="Ta bort" CausesValidation="false" />
                            <asp:LinkButton runat="server" CommandName="Edit" Text="Redigera" CausesValidation="false" />
                        </td>

                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <%--Det här visas när kunduppgifter saknas i databasen --%>
                    <table class="grid">
                        <tr>
                            <td>Kunduppgifter saknas.
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <InsertItemTemplate>
                    <%-- Mall för rad i tabellen för att lägga till nya kunduppgifter. Visas bara om InsertItemPosition 
                     har värdet FirstItemPosition eller LasItemPosition.--%>
                    <tr>
                        <td>
                            <asp:TextBox ID="FirstName" runat="server" Text='<%#: BindItem.FirstName %>' />
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ErrorMessage="Förnamn måste fyllas i"
                            ControlToValidate="FirstName"
                            SetFocusOnError="True"
                            Text="*"
                            Display="Dynamic" CssClass="ErrorMessages" />
                        </td>
                        <td>
                            <asp:TextBox ID="LastName" runat="server" Text='<%#: BindItem.LastName %>' />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                            ErrorMessage="Efternamn måste fyllas i"
                            ControlToValidate="LastName"
                            SetFocusOnError="True"
                            Text="*"
                            Display="Dynamic" CssClass="ErrorMessages" />
                        </td>
                        <td>
                            <asp:TextBox ID="EmailAddress" runat="server" Text='<%#: BindItem.EmailAddress %>' />
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                            ErrorMessage="E-mailadress måste fyllas i"
                            ControlToValidate="EmailAddress"
                            SetFocusOnError="True"
                            Text="*"
                            Display="Dynamic" CssClass="ErrorMessages" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                            ErrorMessage="Angiven e-mailadress är inte giltig"
                            Display="Dynamic" CssClass="ErrorMessages"
                            ControlToValidate="EmailAddress"
                            SetFocusOnError="True"
                            Text="*"
                            ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$" /> 
                        </td>
                         <td>
                            <%--Knappar/länkar för att lägga till och rensa textfält--%>
                            <asp:LinkButton runat="server" CommandName="Insert" Text="Lägg till" />
                            <asp:LinkButton runat="server" CommandName="Cancel" Text="Rensa" CausesValidation="false" />
                        </td>
                    </tr>
                </InsertItemTemplate>
                <EditItemTemplate>
                    <%-- Mall för rad i tabellen för att redigera kunduppgifter. --%>
                    <tr>
                        <td>
                            <asp:TextBox ID="FirstName" runat="server" Text='<%#: BindItem.FirstName %>' />
                        </td>

                        <td>
                            <asp:TextBox ID="LastName" runat="server" Text='<%#: BindItem.LastName %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="EmailAddress" runat="server" Text='<%#: BindItem.EmailAddress %>' />
                        </td>
                         <%--Validering
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ErrorMessage="Förnamn måste fyllas i"
                            ControlToValidate="FirstName"
                            Text="Spara"
                            SetFocusOnError="True"
                            Display="Dynamic" CssClass="ErrorMessages" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                            ErrorMessage="Efternamn måste fyllas i"
                            ControlToValidate="LastName"
                            SetFocusOnError="True"
                            Display="Dynamic" CssClass="ErrorMessages" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                            ErrorMessage="E-mailadress måste fyllas i"
                            ControlToValidate="EmailAddress"
                            SetFocusOnError="True"
                            Display="Dynamic" CssClass="ErrorMessages" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                            ErrorMessage="Angiven e-mailadress är inte giltig"
                            Display="Dynamic" CssClass="ErrorMessages"
                            ControlToValidate="EmailAddress"
                            SetFocusOnError="True"
                            ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$" /> --%>
                        <td>
                            <%--Knappar/länkar för att spara och rensa textfält--%>
                            <asp:LinkButton runat="server" CommandName="Update" Text="Spara" />
                            <asp:LinkButton runat="server" CommandName="Cancel" Text="Avbryt" CausesValidation="false" />
                        </td>
                    </tr>
                </EditItemTemplate>

            </asp:ListView>
            
        </div>
    </form>

</body>
</html>
