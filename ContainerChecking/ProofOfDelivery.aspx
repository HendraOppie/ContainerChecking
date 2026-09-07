<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterMain.Master" CodeBehind="ProofOfDelivery.aspx.vb" Inherits="ContainerChecking.ProofOfDelivery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
        <style>
        .ribbon{
            width:22px;
        }
        .ribbon:hover{
            border-top-style:solid;
            border-top-color:white;
        }
        .textboxEntry{
            width:200px;
            height:20px;
        }
        .mandatoryLabel{
            border-bottom-style:solid;
            border-bottom-width:1px;
            border-bottom-color:red;
        }

        .rowHeaderFilter{
            border-bottom-style:double;
            border-bottom-color:#fed8b1;
        }

    </style>

    <asp:Label runat="server" Font-Names="Calibri" Font-Size="Larger" Font-Bold="true" Text="Proof Of Delivery" ForeColor="DarkSlateBlue" /><br /><br />
    <div id="divRibbonDetail" runat="server" style="border-bottom-style:solid; border-bottom-color:#CCCCCC; border-bottom-width:1px; border-top-style:solid; border-top-color:#CCCCCC; border-top-width:1px">
        <asp:Table runat="server" Width="100%" Font-Names="Calibri" CellPadding="5">
            <asp:TableRow>
                <asp:TableCell Height="35px" Width="25px" VerticalAlign="Middle" HorizontalAlign="Center">
                    <asp:ImageButton ID="ibtSearch" runat="server" CssClass="ribbon" ImageUrl="~/Images/Search_002.png" ToolTip="search" Height="18px" Width="18px" /></asp:TableCell>
                <asp:TableCell Height="35px" Width="25px" VerticalAlign="Middle" HorizontalAlign="Center">
                    <asp:ImageButton ID="ibtClearSearch" runat="server" CssClass="ribbon" ImageUrl="~/Images/Clear_901.png" ToolTip="clear search" Height="18px" Width="18px" /></asp:TableCell>
                <asp:TableCell Height="35px" Width="25px" VerticalAlign="Middle" HorizontalAlign="Center">
                    <asp:ImageButton ID="ibtDeleteList" runat="server" CssClass="ribbon" ImageUrl="~/Images/DeleteList_901.png" ToolTip="delete selected" Height="25px" Width="25px"  OnClientClick = "return confirm('Continue to Delete ?')" /></asp:TableCell>
                <asp:TableCell Height="35px" Width="25px" VerticalAlign="Middle" HorizontalAlign="Center">
                    <asp:ImageButton ID="ibtExport" runat="server" CssClass="ribbon" ImageUrl="~/Images/ExportToXl_901.png" ToolTip="export selected" Height="20px" Width="20px"  OnClientClick = "return confirm('Continue to Export ?')" /></asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" Text=" | " />
                    <asp:Label runat="server" Text="Browse / Search" Font-Bold="true" Font-Size="Medium" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div><br />

    <div id="divDatagrid" runat="server" style="font-family:Calibri;font-size:14px;Height:730px;width:1700px;overflow:auto">
        <asp:Table ID="tblDetail" runat="server" GridLines="Both" CellPadding="5" >
            <asp:TableHeaderRow HorizontalAlign="Left"  BackColor="#fed8b1">
                <asp:TableHeaderCell>&nbsp</asp:TableHeaderCell>
                <asp:TableHeaderCell Text="ID" />
                <asp:TableHeaderCell Text="Handover Type" />
                <asp:TableHeaderCell Text="Received Who" />
                <asp:TableHeaderCell Text="Department" />
                <asp:TableHeaderCell Text="Remark" />
                <asp:TableHeaderCell Text="List HAWB" />
                <asp:TableHeaderCell Text="List Container" />
                <asp:TableHeaderCell Text="POD Location" />
                <asp:TableHeaderCell Text="User" />
                <asp:TableHeaderCell Text="Lastupdated" />
                <asp:TableHeaderCell Text="Signature" />
            </asp:TableHeaderRow>
            <asp:TableHeaderRow HorizontalAlign="Left" CssClass="rowHeaderFilter">
                <asp:TableHeaderCell><asp:CheckBox ID="cbDetail" runat="server" AutoPostBack="true" /></asp:TableHeaderCell>
                <asp:TableHeaderCell><asp:TextBox ID="tbIdSearch" runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell><asp:DropDownList ID="ddHandoverTypeSearch" runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell><asp:TextBox ID="tbReceivedSearch" runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell><asp:TextBox ID="tbDepartmentSearch" runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell><asp:TextBox ID="tbRemarkSearch" runat="server" Width="250px" /></asp:TableHeaderCell>
                <asp:TableHeaderCell><asp:TextBox ID="tbHawbSearch" runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell><asp:TextBox ID="tbContainerNumberSearch" runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell>&nbsp</asp:TableHeaderCell>
                <asp:TableHeaderCell><asp:TextBox ID="tbUserSearch" runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell><asp:TextBox ID="tbLastUpdatedSearch" runat="server" TextMode="Date" /></asp:TableHeaderCell>
                <asp:TableHeaderCell><asp:DropDownList ID="ddPictureSearch" runat="server" /></asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>

    </div>

</asp:Content>

