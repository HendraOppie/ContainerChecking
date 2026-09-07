<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="ContainerChecking.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="icon" href="~/Images/LogoAgilityIcon_001.png" />
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
                    height:30px;
                    
        }
        .button:hover{
            cursor:pointer;
        }
    </style>

    <form id="formLogin" runat="server">
        <div style="font-family: Calibri">
            <br />
            <br />
            <asp:Panel ID="Panel1" runat="server" BorderStyle="Groove" Height="380px" Width="300px" Font-Names="Calibri" BorderColor="DarkBlue">
                <asp:Panel ID="Panel2" runat="server" BorderStyle="None" Height="130px" Width="300px" HorizontalAlign="Center" ForeColor="White">
                    <asp:Image ID="imLogoAgility" runat="server" ImageUrl="~/Images/LogoAgility.png"/><br />
                    <br />
                    <asp:Label ID="Label1" runat="server" Font-Names="Calibri" Text="Container Checking" Font-Bold="True" Font-Size="Large" ForeColor="DarkSlateBlue"></asp:Label>
                </asp:Panel>
                <br />
                &nbsp; Username<br />
                &nbsp; <asp:TextBox ID="tbUser" runat="server" Width="270px" Height="26px" BorderStyle="None"></asp:TextBox>
                <hr style="border-color:darkslateblue; position: fixed; left: 18px; width: 270px; top: 240px;"/>
                <br />
                <br />
                <br />
                &nbsp;Password<br />
                &nbsp; <asp:TextBox ID="tbPass" runat="server" TextMode="Password" Width="270px" Height="26px" BorderStyle="None"></asp:TextBox>
                <hr style="border-color:darkslateblue; position: fixed; left: 17px; width: 270px; top: 325px;"/>
                <br />
                <br />
                &nbsp; <asp:Button ID="btLogin" runat="server" CssClass="button" Text="Login" Width="127px" />
                &nbsp; <asp:Button ID="btSignUp" runat="server" CssClass="button" Text="Sign Up" Width="127px" />
                <br />

            </asp:Panel>

        </div><p style="font-family: calibri; font-size: small;">
            <asp:Label ID="lbIT" runat="server" Font-Names="Calibri" Text=".: IT @ 2021 :."></asp:Label></p></form></body></html>