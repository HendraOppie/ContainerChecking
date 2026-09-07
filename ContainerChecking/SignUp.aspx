<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SignUp.aspx.vb" Inherits="ContainerChecking.SignUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
</head>
<body>
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

    <form id="form1" runat="server">
        <div id="divHead" runat="server" style="width:300px; font-family:Calibri" >
            <asp:Table runat="server" Width="300px" >
                <asp:TableRow VerticalAlign="Middle">
                    <asp:TableCell>
                        <asp:ImageButton ID="ibtLogoAgility" runat="server" ImageUrl="~/Images/LogoAgility.png" width="120px" />   
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Label ID="lbAppsName" runat="server" Text="Container Checking" Font-Bold="true" ForeColor="DarkBlue" Font-Size="18px"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <br />
            <asp:Panel runat="server" Width="100%" HorizontalAlign="Center" >
                <asp:Label ID="Label1" runat="server" Text=".:: Sign-up ::." Font-Bold="True" Font-Size="Large" ForeColor="DarkSlateBlue"></asp:Label>
                <hr style="border-style:solid; border-color:darkblue" />
            </asp:Panel>
        </div>

    </form>
</body>
</html>
