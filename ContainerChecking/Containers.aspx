<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterMain.Master" CodeBehind="Containers.aspx.vb" Inherits="ContainerChecking.Containers" %>

<asp:Content ID="contentContainers" ContentPlaceHolderID="cphContent" runat="server">
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
            border-bottom-width:thick;
        }

    </style>

    <asp:Label runat="server" Font-Names="Calibri" Font-Size="Larger" Font-Bold="true" Text="Container List" ForeColor="DarkSlateBlue" /><br /><br />

    <div id="ribbonContainers" runat="server" style="border-bottom-style:solid; border-bottom-color:#CCCCCC; border-bottom-width:1px; border-top-style:solid; border-top-color:#CCCCCC; border-top-width:1px">
        <asp:Table runat="server" Width="100%" Font-Names="Calibri" CellPadding="5">
            <asp:TableRow>
                <asp:TableCell Height="35px" Width="25px" VerticalAlign="Middle" HorizontalAlign="Center">
                    <asp:ImageButton ID="ibtNew" runat="server" CssClass="ribbon" ImageUrl="~/Images/AddNewList_001.png" ToolTip="clear entry" Height="25px" Width="25px" /></asp:TableCell>
                <asp:TableCell Height="35px" Width="25px" VerticalAlign="Middle" HorizontalAlign="Center">
                    <asp:ImageButton ID="ibtSave" runat="server" CssClass="ribbon" ImageUrl="~/Images/Database_001.png" ToolTip="save" Height="20px" Width="20px"  OnClientClick = "return confirm('Continue to Save ?')"/></asp:TableCell>
                <asp:TableCell Height="35px" Width="25px" VerticalAlign="Middle" HorizontalAlign="Center">
                    <asp:ImageButton ID="ibtDelete" runat="server" CssClass="ribbon" ImageUrl="~/Images/Trash_002.png" ToolTip="delete" Height="20px" Width="20px"  OnClientClick = "return confirm('Continue to Delete ?')"/></asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" Text="| Info : " />
                    <asp:Label ID="lbSysInfo" runat="server" Text="[ system info ]" />
                </asp:TableCell></asp:TableRow></asp:Table></div><br />

    <div id="divEntryContainers" runat="server" style="font-family:Calibri;">
        <table style="padding:5px;">
            <tr style="vertical-align:top">
                <td>
                    <asp:table runat="server" Font-Names="Calibri" CellPadding="5" Width="400px">
                        <asp:TableRow >
                            <asp:TableCell><asp:Label runat="server" CssClass="mandatoryLabel" Text="Container Number" /></asp:TableCell><asp:TableCell><asp:TextBox ID="tbContainer" runat="server" CssClass="textboxEntry" /></asp:TableCell></asp:TableRow><asp:TableRow>
                            <asp:TableCell><asp:Label runat="server" Text="MAWB" /></asp:TableCell><asp:TableCell>
                            <asp:TextBox ID="tbMawb" runat="server" CssClass="textboxEntry" /></asp:TableCell></asp:TableRow><asp:TableRow>
                            <asp:TableCell><asp:Label runat="server" CssClass="mandatoryLabel" Text="HAWB" /></asp:TableCell><asp:TableCell>
                            <asp:TextBox ID="tbHawb" runat="server" CssClass="textboxEntry" /></asp:TableCell></asp:TableRow></asp:table></td><td>
                    <asp:table runat="server" Font-Names="Calibri" Width="300px">
                        <asp:TableRow>
                            <asp:TableCell><asp:Label runat="server" CssClass="mandatoryLabel" Text="Customer" /></asp:TableCell></asp:TableRow><asp:TableRow>
                            <asp:TableCell >
                                <asp:TextBox ID="tbCustomerID" runat="server"  CssClass="textboxEntry" Enabled="false" />
                                <asp:Button runat="server" Text="..." CssClass="button" height="25px" BackColor="#CCCCCC" Enabled="false" />
                            </asp:TableCell></asp:TableRow><asp:TableRow><asp:TableCell><asp:Label ID="lbCustomerInfo" runat="server" Text="Customer Info" Width="200px"/></asp:TableCell></asp:TableRow></asp:table></td><td>
                    <asp:table runat="server" Font-Names="Calibri" CellPadding="5" Width="350px">
                        <asp:TableRow>
                            <asp:TableCell><asp:Label runat="server" Text="Status" /></asp:TableCell><asp:TableCell><asp:Label runat="server" ID="lbStatus" Font-Bold="true" /></asp:TableCell></asp:TableRow><asp:TableRow>
                            <asp:TableCell><asp:Label runat="server" Text="Received Date" /></asp:TableCell><asp:TableCell><asp:Label runat="server" ID="lbReceivedDate" Font-Bold="true" /></asp:TableCell></asp:TableRow><asp:TableRow>
                            <asp:TableCell><asp:Label runat="server" Text="Delivered Date" /></asp:TableCell><asp:TableCell><asp:Label runat="server" ID="lbDeliveredDate" Font-Bold="true" /></asp:TableCell></asp:TableRow><asp:TableRow>
                            <asp:TableCell><asp:Label runat="server" Text="Username" /></asp:TableCell><asp:TableCell><asp:Label runat="server" ID="lbUserName" Font-Bold="true" /></asp:TableCell></asp:TableRow><asp:TableRow>
                            <asp:TableCell><asp:Label runat="server" Text="Lastupdated" /></asp:TableCell><asp:TableCell><asp:Label runat="server" ID="lbLastUpdated" Font-Bold="true" /></asp:TableCell></asp:TableRow></asp:table></td></tr></table></div><br />

    <div id="ribbonContainersDetail" runat="server" style="border-bottom-style:solid; border-bottom-color:#CCCCCC; border-bottom-width:1px; border-top-style:solid; border-top-color:#CCCCCC; border-top-width:1px">
        <asp:Table runat="server" Width="100%" Font-Names="Calibri" CellPadding="5">
            <asp:TableRow>
                <asp:TableCell Height="35px" Width="25px" VerticalAlign="Middle" HorizontalAlign="Center">
                    <asp:ImageButton ID="ibtSearch" runat="server" CssClass="ribbon" ImageUrl="~/Images/Search_002.png" ToolTip="search" Height="18px" Width="18px" /></asp:TableCell>
                <asp:TableCell Height="35px" Width="25px" VerticalAlign="Middle" HorizontalAlign="Center">
                    <asp:ImageButton ID="ibtClearSearch" runat="server" CssClass="ribbon" ImageUrl="~/Images/Clear_901.png" ToolTip="clear search" Height="18px" Width="18px" /></asp:TableCell>
                <asp:TableCell Height="35px" Width="25px" VerticalAlign="Middle" HorizontalAlign="Center">
                    <asp:ImageButton ID="ibtDeleteList" runat="server" CssClass="ribbon" ImageUrl="~/Images/DeleteList_901.png" ToolTip="delete selected" Height="25px" Width="25px"  OnClientClick = "return confirm('Continue to Delete ?')" /></asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" Text=" | " />
                    <asp:Label runat="server" Text="Browse / Search" Font-Bold="true" Font-Size="Medium" />
                </asp:TableCell></asp:TableRow></asp:Table></div><br />

   <div id="divDatagrid" runat="server" style="font-family:Calibri;font-size:14px;Height:500px;width:1500px;overflow:auto">
        <asp:Table ID="tblDetail" runat="server" GridLines="Both" CellPadding="5">
            <asp:TableHeaderRow HorizontalAlign="Left"  BackColor="LightGray">
                <asp:TableHeaderCell>&nbsp</asp:TableHeaderCell><asp:TableHeaderCell Text="Container Number" />
                <asp:TableHeaderCell Text="HAWB" />
                <asp:TableHeaderCell Text="MAWB" />
                <asp:TableHeaderCell Text="Customer Name" />
                <asp:TableHeaderCell Text="Status" />
                <asp:TableHeaderCell Text="Received Date" />
                <asp:TableHeaderCell Text="Delivered Date" />
                <asp:TableHeaderCell Text="Username" />
                <asp:TableHeaderCell Text="Lastupdated" />
            </asp:TableHeaderRow>
            <asp:TableHeaderRow HorizontalAlign="Left" CssClass="rowHeaderFilter">
                <asp:TableHeaderCell><asp:CheckBox ID="cbDetail" runat="server" AutoPostBack="true" /></asp:TableHeaderCell>
                <asp:TableHeaderCell><asp:TextBox ID="tbContainerSearch" runat="server" /></asp:TableHeaderCell><asp:TableHeaderCell><asp:TextBox ID="tbHawbSearch" runat="server" Width="110px" /></asp:TableHeaderCell><asp:TableHeaderCell><asp:TextBox ID="tbMawbSearch" runat="server" Width="110px" /></asp:TableHeaderCell><asp:TableHeaderCell><asp:TextBox ID="tbCustomerSearch" runat="server" Width="150px" /></asp:TableHeaderCell><asp:TableHeaderCell><asp:DropDownList ID="ddStatusSearch" runat="server" Width="100px" /></asp:TableHeaderCell>
                <asp:TableHeaderCell><asp:TextBox ID="tbReceivedDateSearch" runat="server" TextMode="Date" /></asp:TableHeaderCell><asp:TableHeaderCell><asp:TextBox ID="tbDeliveredDateSearch" runat="server" TextMode="Date" /></asp:TableHeaderCell><asp:TableHeaderCell><asp:TextBox ID="tbUserSearch" runat="server" width="120px"/></asp:TableHeaderCell><asp:TableHeaderCell><asp:TextBox ID="tbLastUpdatedSearch" runat="server" TextMode="Date" /></asp:TableHeaderCell></asp:TableHeaderRow></asp:Table></div></asp:Content>