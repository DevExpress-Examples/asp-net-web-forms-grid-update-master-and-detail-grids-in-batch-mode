<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>How to update a master grid and all its detail grids operating in Batch Edit mode simultaneously</title>
    <script src="UpdateLogic.js"></script>
</head>
<body>
    <form id="frmMain" runat="server">
        <b>How to update a master grid and all its detail grids operating in Batch Edit mode simultaneously</b>
        <dx:ASPxGridView ID="Grid" runat="server" KeyFieldName="CategoryID" OnCommandButtonInitialize="Grid_CommandButtonInitialize" OnCustomCallback="Grid_CustomCallback" ClientInstanceName="grid" OnBatchUpdate="Grid_BatchUpdate" AutoGenerateColumns="False" DataSourceID="nwd1">
            <Columns>
                <dx:GridViewCommandColumn ShowNewButtonInHeader="True" VisibleIndex="0" />
                <dx:GridViewDataTextColumn FieldName="CategoryID" ReadOnly="True" VisibleIndex="1">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CategoryName" VisibleIndex="2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="3">
                </dx:GridViewDataTextColumn>
            </Columns>
            <ClientSideEvents DetailRowExpanding="OnExpanding" DetailRowCollapsing="OnCollapsing" EndCallback="OnEndCallback" CallbackError="OnCallbackError" BatchEditConfirmShowing="OnConfirm" />
            <SettingsDetail ShowDetailButtons="true" ShowDetailRow="true" />
            <Templates>
                <DetailRow>
                    <dx:ASPxGridView runat="server" OnCommandButtonInitialize="Grid_CommandButtonInitialize" KeyFieldName="ProductID" OnBatchUpdate="grid_BatchUpdate" OnBeforePerformDataSelect="grid2_BeforePerformDataSelect" ID="grid2" AutoGenerateColumns="False" DataSourceID="nwd2">
                        <Columns>
                            <dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowDeleteButton="True" VisibleIndex="0" />
                            <dx:GridViewDataTextColumn FieldName="ProductID" ReadOnly="True" VisibleIndex="1">
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ProductName" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataCheckColumn FieldName="Discontinued" VisibleIndex="4" />
                        </Columns>
                        <ClientSideEvents BatchEditConfirmShowing="OnConfirm" />
                        <SettingsEditing Mode="Batch" />
                    </dx:ASPxGridView>
                </DetailRow>
                <StatusBar>
                    <div style="text-align: right">
                        <dx:ASPxButton ID="btn" Text="Save Changes" RenderMode="Link" AutoPostBack="false" runat="server">
                            <ClientSideEvents Click="OnClick" />
                        </dx:ASPxButton>
                        <dx:ASPxButton ID="btn2" Text="Cancel Changes" RenderMode="Link" AutoPostBack="false" runat="server">
                            <ClientSideEvents Click="OnCancel" />
                        </dx:ASPxButton>
                    </div>
                </StatusBar>
            </Templates>
            <SettingsEditing Mode="Batch" />
        </dx:ASPxGridView>
        <asp:AccessDataSource ID="nwd1" runat="server" DataFile="~/App_Data/nwind.mdb" DeleteCommand="DELETE FROM [Categories] WHERE [CategoryID] = ?" InsertCommand="INSERT INTO [Categories] ([CategoryName], [Description]) VALUES (?, ?)" SelectCommand="SELECT [CategoryID], [CategoryName], [Description] FROM [Categories]" UpdateCommand="UPDATE [Categories] SET [CategoryName] = ?, [Description] = ? WHERE [CategoryID] = ?">
            <DeleteParameters>
                <asp:Parameter Name="CategoryID" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="CategoryName" Type="String" />
                <asp:Parameter Name="Description" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="CategoryName" Type="String" />
                <asp:Parameter Name="Description" Type="String" />
                <asp:Parameter Name="CategoryID" Type="Int32" />
            </UpdateParameters>
        </asp:AccessDataSource>
        <asp:AccessDataSource ID="nwd2" runat="server" DataFile="~/App_Data/nwind.mdb" DeleteCommand="DELETE FROM [Products] WHERE [ProductID] = ?" InsertCommand="INSERT INTO [Products] ([ProductName], [CategoryID], [Discontinued]) VALUES (?, ?, ?)" SelectCommand="SELECT [ProductID], [ProductName], [CategoryID] , [Discontinued] FROM [Products] WHERE ([CategoryID] = ?)" UpdateCommand="UPDATE [Products] SET [ProductName] = ?, [Discontinued] = ? WHERE [ProductID] = ?">
            <DeleteParameters>
                <asp:Parameter Name="ProductID" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="ProductName" Type="String" />
                <asp:Parameter Name="CategoryID" Type="Int32" />
                <asp:Parameter Name="Discontinued" Type="Boolean" />
            </InsertParameters>
            <SelectParameters>
                <asp:SessionParameter DefaultValue="?" Name="CategoryID" SessionField="Category" Type="Int32" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="ProductName" Type="String" />
                <asp:Parameter Name="Discontinued" Type="Boolean" />
                <asp:Parameter Name="ProductID" Type="Int32" />
            </UpdateParameters>
        </asp:AccessDataSource>
        <dx:ASPxLabel runat="server" ForeColor="Red" Font-Size="Large" Font-Bold="true" Text="" ID="lbl" ClientInstanceName="lbl"></dx:ASPxLabel>
    </form>
</body>
</html>