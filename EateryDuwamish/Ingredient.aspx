﻿<%@ Page Title="Ingredients" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ingredient.aspx.cs" Inherits="EateryDuwamish.Ingredient" %>
<%@ Register Src="~/UserControl/NotificationControl.ascx" TagName="NotificationControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <%--Datatable Configuration--%>
    <script type="text/javascript">
        function ConfigureDatatable() {
            var table = null;
            if ($.fn.dataTable.isDataTable('#htblIngredient')) {
                table = $('#htblIngredient').DataTable();
            }
            else {
                table = $('#htblIngredient').DataTable({
                    stateSave: false,
                    order: [[1, "asc"]],
                    columnDefs: [{ orderable: false, targets: [0] }]
                });
            }
            return table;
        }
    </script>
    <%--Checkbox Event Configuration--%>
    <script type="text/javascript">
        function ConfigureCheckboxEvent() {
            $('.checkDelete input').change(function () {
                var parent = $(this).parent();
                var value = $(parent).attr('data-value');
                var deletedList = [];

                if ($('#<%=hdfDeletedIngredients.ClientID%>').val())
                    deletedList = $('#<%=hdfDeletedIngredients.ClientID%>').val().split(',');

                if ($(this).is(':checked')) {
                    deletedList.push(value);
                    $('#<%=hdfDeletedIngredients.ClientID%>').val(deletedList.join(','));
                }
                else {
                    var index = deletedList.indexOf(value);
                    if (index >= 0)
                        deletedList.splice(index, 1);
                    $('#<%=hdfDeletedIngredients.ClientID%>').val(deletedList.join(','));
                }
            });
        }
    </script>
    <%--Main Configuration--%>
    <script type="text/javascript">
        function ConfigureElements() {
            ConfigureDatatable();
            ConfigureCheckboxEvent();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script type="text/javascript">
                $(document).ready(function () {
                    ConfigureElements();
                });
                <%--On Partial Postback Callback Function--%>
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_endRequest(function () {
                    ConfigureElements();
                });
            </script>
            <uc1:NotificationControl ID="notifIngredient" runat="server" />
            <div class="page-title">Master Ingredient</div><hr style="margin:0"/>
            <%--FORM INGREDIENT--%>
            <asp:Panel runat="server" ID="pnlFormIngredient" Visible="false">
                <div class="form-slip">
                    <div class="form-slip-header">
                        <div class="form-slip-title">
                            FORM INGREDIENT - 
                            <asp:Literal runat="server" ID="litFormType"></asp:Literal>
                        </div>
                        <hr style="margin:0"/>
                    </div>
                    <div class="form-slip-main">
                        <asp:HiddenField ID="hdfIngredientId" runat="server" Value="0"/>
                        <asp:HiddenField ID="hdfRecipeId" runat="server" Value="0" />
                        <div>
                            <%--Ingredient Name Field--%>
                            <div class="col-lg-6 form-group">
                                <div class="col-lg-4 control-label">
                                    Ingredient Name*
                                </div>
                                <div class="col-lg-6">
                                    <asp:TextBox ID="txtIngredientName" CssClass="form-control" runat="server"></asp:TextBox>
                                    <%--Validator--%>
                                    <asp:RequiredFieldValidator ID="rfvIngredientName" runat="server" ErrorMessage="Please fill this field"
                                        ControlToValidate="txtIngredientName" ForeColor="Red" 
                                        ValidationGroup="InsertUpdateIngredient" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revIngredientName" runat="server" ErrorMessage="This field has a maximum of 200 characters"
                                        ControlToValidate="txtIngredientName" ValidationExpression="^[\s\S]{0,200}$" ForeColor="Red"
                                        ValidationGroup="InsertUpdateIngredient" Display="Dynamic">
                                    </asp:RegularExpressionValidator>
                                    <%--End of Validator--%>
                                </div>
                            </div>
                            <%--End of Ingredient Name Field --%>
                            <%--Quantity Field--%>
                            <div class="col-lg-3 form-group">
                                <div class="col-lg-4 control-label">
                                    Quantity*
                                </div>
                                <div class="col-lg-6">
                                    <asp:TextBox ID="txtIngredientQuantity" CssClass="form-control" runat="server" type="number"
                                            Min="0" Max="999999999"></asp:TextBox>
                                    <%--Validator--%>
                                    <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ErrorMessage="Please fill this field"
                                        ControlToValidate="txtIngredientQuantity" ForeColor="Red"
                                        ValidationGroup="InsertUpdateIngredient" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <%--End of Validator--%>
                                </div>
                            </div>
                            <%--End of Quantity Field--%>
                            <%--Ingredient Unit Field--%>
                            <div class="col-lg-3 form-group">
                                <div class="col-lg-4 control-label">
                                    Unit*
                                </div>
                                <div class="col-lg-6">
                                    <asp:TextBox ID="txtIngredientUnit" CssClass="form-control" runat="server"></asp:TextBox>
                                    <%--Validator--%>
                                    <asp:RequiredFieldValidator ID="rfvUnit" runat="server" ErrorMessage="Please fill this field"
                                        ControlToValidate="txtIngredientUnit" ForeColor="Red" 
                                        ValidationGroup="InsertUpdateIngredient" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revUnit" runat="server" ErrorMessage="This field has a maximum of 100 characters"
                                        ControlToValidate="txtIngredientUnit" ValidationExpression="^[\s\S]{0,100}$" ForeColor="Red"
                                        ValidationGroup="InsertUpdateIngredient" Display="Dynamic">
                                    </asp:RegularExpressionValidator>
                                    <%--End of Validator--%>
                                </div>
                            </div>
                            <%--End of Ingredient Name Field --%>
                        </div>
                        <div class="col-lg-12">
                            <div class="col-lg-2">
                            </div>
                            <div class="col-lg-2">
                                <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary" Width="100px"
                                    Text="SAVE" OnClick="btnSave_Click" ValidationGroup="InsertUpdateIngredient">
                                </asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <%--END OF FORM INGREDIENT--%>

            <div class="row">
                <div class="table-header">
                    <div class="table-header-title">
                        <asp:Literal ID="litTableTitle" runat="server"></asp:Literal>
                    </div>
                    <div class="table-header-button">
                        <asp:Button ID="btnAdd" runat="server" Text="ADD" CssClass="btn btn-primary" Width="100px"
                            OnClick="btnAdd_Click" />
                        <asp:Button ID="btnDelete" runat="server" Text="DELETE" CssClass="btn btn-danger" Width="100px"
                            OnClick="btnDelete_Click" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="table-main col-sm-12">
                    <asp:HiddenField ID="hdfDeletedIngredients" runat="server" />
                    <asp:Repeater ID="rptIngredient" runat="server" OnItemDataBound="rptIngredient_ItemDataBound" OnItemCommand="rptIngredient_ItemCommand">
                        <HeaderTemplate>
                            <table id="htblIngredient" class="table">
                                <thead>
                                    <tr role="row">
                                        <th aria-sort="ascending" style="" colspan="1" rowspan="1"
                                            tabindex="0" class="sorting_asc center">
                                        </th>
                                        <th aria-sort="ascending" style="" colspan="1" rowspan="1" tabindex="0"
                                            class="sorting_asc text-center">
                                            Ingredient
                                        </th>
                                        <th aria-sort="ascending" style="" colspan="1" rowspan="1" tabindex="0"
                                            class="sorting_asc text-center">
                                            Quantity
                                        </th>
                                        <th aria-sort="ascending" style="" colspan="1" rowspan="1" tabindex="0"
                                            class="sorting_asc text-center">
                                            Unit
                                        </th>
                                        <th aria-sort="ascending" style="" colspan="1" rowspan="1" tabindex="0"
                                            class="sorting_asc text-center">
                                            Toggle
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="odd" role="row" runat="server" onclick="">
                                <td>
                                    <div style="text-align: center;">
                                        <asp:CheckBox ID="chkChoose" CssClass="checkDelete" runat="server">
                                        </asp:CheckBox>
                                    </div>
                                </td>
                                <td>
                                    <asp:Literal ID="litIngredientName" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="litIngredientQuantity" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="litIngredientUnit" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lbEdit" runat="server" CommandName="EDIT"></asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody> 
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="row">
                <div class="recipe-desc">
                    <div class="recipe-desc-title">
                        Recipe Description
                    </div>
                    <div class="recipe-desc-content">
                        <asp:TextBox ID="txtRecipeDescription" TextMode="MultiLine" Rows="10" runat="server" Enabled="false" Style="width:50%; margin-right: 50%;resize: none;"></asp:TextBox>
                    </div>
                    <div class="recipe-desc-button" >
                        <asp:Button ID="btnEditRecipeDesc" runat="server" Text="EDIT" CssClass="btn btn-secondary" Width="100px"
                                OnClick="btnEditRecipeDesc_Click" />
                        <asp:Button ID="btnSaveRecipeDesc" runat="server" Text="SAVE" CssClass="btn btn-primary" Width="100px"
                                OnClick="btnSaveRecipeDesc_Click" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
