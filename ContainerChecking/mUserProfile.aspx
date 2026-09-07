<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterMobile.Master" CodeBehind="mUserProfile.aspx.vb" Inherits="ContainerChecking.MobileUserProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="server">
    <asp:Panel runat="server" Width="100%" HorizontalAlign="Center">
        <asp:Label ID="Label1" runat="server" Font-Names="Calibri" Text=".:: User Profile ::." Font-Bold="True" Font-Size="Large" ForeColor="DarkBlue"></asp:Label>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <style>
        .button {
                    -moz-border-radius: 5px;
                    -webkit-border-radius: 5px;
                    border-radius: 5px;
                    background-color:dodgerblue;
                    border-color:#CCCCCC;
                    border-style:solid;
                    font: bold 18px Calibri;
                    color:white;
        }
        .button:hover{
            cursor:pointer;
        }
    </style>
    <br />
    <asp:Label runat="server" Text="Profile:" Font-Bold="true" Font-Names="Calibri" />
    <hr style="color:darkblue" />
    <div id="divUserInfo" style="font-family:Calibri">
        <table>
            <tr>
                <td><asp:Label runat="server" Text="ID" /></td>
                <td><asp:Label runat="server" Text=":" /></td>
                <td><asp:Label ID="lbUserId" runat="server" Text="[UserId]" /></td>
            </tr>
            <tr>
                <td><asp:Label runat="server" Text="Full Name" /></td>
                <td><asp:Label runat="server" Text=":" /></td>
                <td><asp:TextBox ID="tbUserName" runat="server" Text="[UserName]" Height="23px" /></td>
            </tr>
            <tr>
                <td><asp:Label runat="server" Text="Email" /></td>
                <td><asp:Label runat="server" Text=":" /></td>
                <td><asp:TextBox ID="tbEmail" runat="server" Text="[Email]" Height="23px" /></td>
            </tr>
            <tr>
                <td><asp:Label runat="server" Text="Role" /></td>
                <td><asp:Label runat="server" Text=":" /></td>
                <td><asp:Label ID="lbRole" runat="server" Text="[RoleId]" /></td>
            </tr>

        </table>
        <br />
        <asp:Panel runat="server" HorizontalAlign="Center"><asp:Button ID="btUpdate" runat="server" CssClass="button" Text="Update" Width="127px" OnClientClick = "return confirm('Continue to Update ?')" /></asp:Panel>
    </div>
    <br />
    <asp:Label runat="server" Text="Change Password:" Font-Bold="true" Font-Names="Calibri" />
    <hr style="color:darkblue" />
    <div id="divChangePassword" style="font-family: Calibri">
        <asp:Label runat="server" Text="Current Password" /><br />
        <asp:TextBox ID="tbPass" runat="server" TextMode="Password" Width="270px" Height="23px"></asp:TextBox>
        <br />
        <br />
        <asp:Label runat="server" Text="New Password" /><br />
        <asp:TextBox ID="tbPassNew" runat="server" TextMode="Password" MaxLength="20" Width="270px" Height="23px" ></asp:TextBox>
        <br />
        <br />
        <asp:Label runat="server" Text="Confirm New Password" /><br />
        <asp:TextBox ID="tbPassNewConfirm" runat="server" TextMode="Password" Width="270px" Height="23px" ></asp:TextBox>
        <br />
        <br />
        <asp:Panel runat="server" HorizontalAlign="Center"><asp:Button ID="btSubmit" runat="server" CssClass="button" Text="Change Password" OnClientClick = "return confirm('Continue to Change Password ?')" /></asp:Panel>
    </div>
</asp:Content>
