<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterMobile.Master" CodeBehind="mMenu.aspx.vb" Inherits="ContainerChecking.MenuMobile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="server">
        <asp:Panel runat="server" Width="100%" HorizontalAlign="Center">
        <asp:Label ID="Label1" runat="server" Font-Names="Calibri" Text=".:: M E N U ::." Font-Bold="True" Font-Size="Large" ForeColor="DarkSlateBlue"></asp:Label>
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
                    height:30px;
                    
        }
        .button:hover{
            cursor:pointer;
        }
        .linkbuttonAsLabel {
            color: #000000;
            text-decoration: none;
        }

        .linkbuttonAsLabel:hover {
            text-decoration: none;
        }
    </style>

        <div>
            <asp:Table runat="server" Width="300px" GridLines="Both" BorderStyle="Solid" BorderColor="DarkBlue" CellPadding="5">
                <asp:TableRow HorizontalAlign="Center" VerticalAlign="Middle" Height="100px">
                    <asp:TableCell Width="50%">
                        <asp:ImageButton ID="ibtInspection" runat="server" ImageUrl="~/Images/Inspection_001.png" Width="70px" /><br />
                        <asp:LinkButton ID="lbtInspection" runat="server" CssClass="linkbuttonAsLabel" Font-Names="Calibri" Text="Physical Inspection" Font-Bold="True" Font-Size="Medium" ForeColor="DarkSlateBlue"></asp:LinkButton>
                    </asp:TableCell>
                    <asp:TableCell Width="50%">
                        <asp:ImageButton ID="ibtTemp" runat="server" ImageUrl="~/Images/Temperature_001.png" Width="60px" /> <br />
                        <asp:LinkButton ID="lbtTemp" runat="server" CssClass="linkbuttonAsLabel" Font-Names="Calibri" Text="Temperature Log" Font-Bold="True" Font-Size="Medium" ForeColor="DarkSlateBlue"></asp:LinkButton>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow HorizontalAlign="Center" VerticalAlign="Middle" Height="100px">
                    <asp:TableCell Width="50%">
                        <asp:ImageButton ID="ibtHandover" runat="server" ImageUrl="~/Images/Signature_002.png" Width="60px" /><br />
                        <asp:LinkButton ID="lbtHandover" runat="server" CssClass="linkbuttonAsLabel" Font-Names="Calibri" Text="DN and POD" Font-Bold="True" Font-Size="Medium" ForeColor="DarkSlateBlue"></asp:LinkButton>
                    </asp:TableCell>
                    <asp:TableCell Width="50%">
                        <asp:ImageButton ID="ibtLogout" runat="server" ImageUrl="~/Images/Logout_001.png" Width="60px" /> <br />
                        <asp:LinkButton ID="lbtLogout" runat="server" CssClass="linkbuttonAsLabel" Font-Names="Calibri" Text="Logout" Font-Bold="True" Font-Size="Medium" ForeColor="DarkSlateBlue"></asp:LinkButton>
                    </asp:TableCell>
                </asp:TableRow>

            </asp:Table>
        </div>

</asp:Content>
