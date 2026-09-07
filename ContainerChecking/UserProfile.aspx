<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterMain.Master" CodeBehind="UserProfile.aspx.vb" Inherits="ContainerChecking.UserProfile" %>
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

    <asp:Label runat="server" Font-Names="Calibri" Font-Size="Larger" Font-Bold="true" Text="User Profile" ForeColor="DarkBlue" /><br /><br />

    <div id="divRibbon" runat="server" style="border-bottom-style:solid; border-bottom-color:#CCCCCC; border-bottom-width:1px; border-top-style:solid; border-top-color:#CCCCCC; border-top-width:1px">
        <asp:Table runat="server" Width="100%" Font-Names="Calibri" CellPadding="5">
            <asp:TableRow>
                <asp:TableCell Height="35px" Width="25px" VerticalAlign="Middle" HorizontalAlign="Center">
                    <asp:ImageButton ID="ibtNew" runat="server" CssClass="ribbon" ImageUrl="~/Images/AddNewList_001.png" ToolTip="clear entry" Height="25px" Width="25px" Enabled="false" /></asp:TableCell>
                <asp:TableCell Height="35px" Width="25px" VerticalAlign="Middle" HorizontalAlign="Center">
                    <asp:ImageButton ID="ibtSave" runat="server" CssClass="ribbon" ImageUrl="~/Images/Database_001.png" ToolTip="save" Height="20px" Width="20px"  OnClientClick = "return confirm('Continue to Save ?')"/></asp:TableCell>
                <asp:TableCell Height="35px" Width="25px" VerticalAlign="Middle" HorizontalAlign="Center">
                    <asp:ImageButton ID="ibtDelete" runat="server" CssClass="ribbon" ImageUrl="~/Images/Trash_002.png" ToolTip="delete" Height="20px" Width="20px" Enabled="false"/></asp:TableCell>
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
                            <asp:TableCell><asp:Label runat="server" CssClass="mandatoryLabel" Text="ID" /></asp:TableCell>
                            <asp:TableCell><asp:TextBox ID="tbUserId" runat="server" CssClass="textboxEntry" ReadOnly="true"/></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell><asp:Label runat="server" CssClass="mandatoryLabel" Text="Full Name" /></asp:TableCell>
                            <asp:TableCell><asp:TextBox ID="tbUsername" runat="server" CssClass="textboxEntry" /></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell><asp:Label runat="server" Text="Email" /></asp:TableCell>
                            <asp:TableCell><asp:TextBox ID="tbEmail" runat="server" CssClass="textboxEntry" /></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell><asp:CheckBox ID="cbEnabled" runat="server" Enabled="false" Text="Enabled" /></asp:TableCell>
                            <asp:TableCell><asp:CheckBox ID="cbChangePassword" runat="server" Enabled="false" Text="Push to Change Password" /></asp:TableCell>
                        </asp:TableRow>
                    </asp:table>
                </td>
                <td>
                    <asp:table runat="server" Font-Names="Calibri" Width="300px">
                        <asp:TableRow >
                            <asp:TableCell><asp:Label runat="server" Text="Role ID" /></asp:TableCell>
                            <asp:TableCell><asp:TextBox ID="tbRoleId" runat="server" CssClass="textboxEntry" ReadOnly="true"/></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell><asp:Label runat="server" Text="Role Name" /></asp:TableCell>
                            <asp:TableCell><asp:TextBox ID="tbRoleName" runat="server" CssClass="textboxEntry" ReadOnly="true"/></asp:TableCell>
                        </asp:TableRow>
                    </asp:table>

                </td>
            </tr>
        </table>
    </div><br />


    <div id="divChangePassword" runat="server" style="font-family:Calibri;border-top-style:solid; border-top-color:#CCCCCC; border-top-width:1px">
        <asp:table runat="server" Font-Names="Calibri" CellPadding="5" Width="350px">
            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server" Text="Current Password" /></asp:TableCell><asp:TableCell><asp:TextBox ID="tbPassword" runat="server" CssClass="textboxEntry" TextMode="Password" /></asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell><asp:Label runat="server" Text="New Password" /></asp:TableCell><asp:TableCell><asp:TextBox ID="tbPasswordNew" runat="server" CssClass="textboxEntry" TextMode="Password" /></asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell><asp:Label runat="server" Text="New password Confirmation" /></asp:TableCell><asp:TableCell><asp:TextBox ID="tbNewPasswordConfirmation" runat="server" CssClass="textboxEntry" TextMode="Password" /></asp:TableCell></asp:TableRow><asp:TableRow>
            </asp:TableRow>
        </asp:table>
        <asp:Button ID="btChangePassword" runat="server" Text="Change Password" CssClass="button" OnClientClick = "return confirm('Continue to Change Password ?')" />
    </div>

</asp:Content>