<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterMain.Master" CodeBehind="User.aspx.vb" Inherits="ContainerChecking.User" %>
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
            border-bottom-width:thick;
        }

    </style>

    <asp:Label runat="server" Font-Names="Calibri" Font-Size="Larger" Font-Bold="true" Text="User Setting" ForeColor="DarkBlue" /><br /><br />

    <div id="divRibbon" runat="server" style="border-bottom-style:solid; border-bottom-color:#CCCCCC; border-bottom-width:1px; border-top-style:solid; border-top-color:#CCCCCC; border-top-width:1px">
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

    <div id="divEntry" runat="server" style="font-family:Calibri;">
        <table style="padding:5px;">
            <tr style="vertical-align:top">
                <td>
                    <asp:table runat="server" Font-Names="Calibri" CellPadding="5" Width="400px">
                        <asp:TableRow >
                            <asp:TableCell><asp:Label runat="server" CssClass="mandatoryLabel" Text="User ID" /></asp:TableCell><asp:TableCell><asp:TextBox ID="tbUserId" runat="server" CssClass="textboxEntry" Enabled="false" /></asp:TableCell></asp:TableRow><asp:TableRow>
                            <asp:TableCell><asp:Label runat="server" CssClass="mandatoryLabel" Text="Username" /></asp:TableCell><asp:TableCell><asp:TextBox ID="tbUsername" runat="server" CssClass="textboxEntry" /></asp:TableCell></asp:TableRow><asp:TableRow>
                            <asp:TableCell><asp:Label runat="server" Text="Email" /></asp:TableCell><asp:TableCell><asp:TextBox ID="tbEmail" runat="server" CssClass="textboxEntry" /></asp:TableCell></asp:TableRow><asp:TableRow>
                            <asp:TableCell><asp:CheckBox ID="cbEnabled" runat="server" Enabled="false" Text="Enabled" /></asp:TableCell>
                            <asp:TableCell><asp:CheckBox ID="cbChangePassword" runat="server" Enabled="false" Text="Push to Change Password" /></asp:TableCell>
                        </asp:TableRow>
                    </asp:table>
                </td>
                <td>
                    <asp:table runat="server" Font-Names="Calibri" Width="300px">
                        <asp:TableRow><asp:TableCell><asp:Label runat="server" CssClass="mandatoryLabel" Text="Role" /></asp:TableCell></asp:TableRow>
                        <asp:TableRow><asp:TableCell><asp:DropDownList ID="ddRole" runat="server" AutoPostBack="true" Height="25px"/></asp:TableCell></asp:TableRow>
                    </asp:table>

                </td>
            </tr>
        </table>
    </div><br />

    <div id="divRibbonDetail" runat="server" style="border-bottom-style:solid; border-bottom-color:#CCCCCC; border-bottom-width:1px; border-top-style:solid; border-top-color:#CCCCCC; border-top-width:1px">
        <asp:Table runat="server" Width="100%" Font-Names="Calibri" CellPadding="5">
            <asp:TableRow>
                <asp:TableCell Height="35px" Width="25px" VerticalAlign="Middle" HorizontalAlign="Center">
                    <asp:ImageButton ID="ibtSearch" runat="server" CssClass="ribbon" ImageUrl="~/Images/Search_002.png" ToolTip="search" Height="18px" Width="18px" /></asp:TableCell>
                <asp:TableCell Height="35px" Width="25px" VerticalAlign="Middle" HorizontalAlign="Center">
                    <asp:ImageButton ID="ibtClearSearch" runat="server" CssClass="ribbon" ImageUrl="~/Images/Clear_901.png" ToolTip="clear search" Height="18px" Width="18px" /></asp:TableCell>
                <asp:TableCell Height="35px" Width="25px" VerticalAlign="Middle" HorizontalAlign="Center">
                    <asp:ImageButton ID="ibtDeleteList" runat="server" CssClass="ribbon" ImageUrl="~/Images/DeleteList_901.png" ToolTip="delete selected" Height="25px" Width="25px" OnClientClick = "return confirm('Continue to Delete ?')" /></asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" Text=" | " />
                    <asp:Label runat="server" Text="Browse / Search" Font-Bold="true" Font-Size="Medium" />
                </asp:TableCell></asp:TableRow></asp:Table></div><br />

   <div id="divDatagrid" runat="server" style="font-family:Calibri;font-size:14px;Height:500px;width:1000px;overflow:auto">
        <asp:Table ID="tblDetail" runat="server" GridLines="Both" CellPadding="5">
            <asp:TableHeaderRow HorizontalAlign="Left"  BackColor="LightGray">
                <asp:TableHeaderCell>&nbsp</asp:TableHeaderCell>
                <asp:TableHeaderCell Text="User ID" />
                <asp:TableHeaderCell Text="Username" />
                <asp:TableHeaderCell Text="Email" />
                <asp:TableHeaderCell Text="Enabled" />
                <asp:TableHeaderCell Text="Role" />
            </asp:TableHeaderRow>
            <asp:TableHeaderRow HorizontalAlign="Left" CssClass="rowHeaderFilter">
                <asp:TableHeaderCell><asp:CheckBox ID="cbDetail" runat="server" AutoPostBack="true" /></asp:TableHeaderCell>
                <asp:TableHeaderCell><asp:TextBox ID="tbUserIdSearch" runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell><asp:TextBox ID="tbUsernameSearch" runat="server"  /></asp:TableHeaderCell>
                <asp:TableHeaderCell><asp:TextBox ID="tbEmailSearch" runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell><asp:DropDownList ID="ddEnabledSearch" runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell><asp:TextBox ID="tbRoleSearch" runat="server" /></asp:TableHeaderCell>
            </asp:TableHeaderRow>

        </asp:Table>

   </div>

</asp:Content>