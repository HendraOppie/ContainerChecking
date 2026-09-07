<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterMobile.Master" CodeBehind="mInfo.aspx.vb" Inherits="ContainerChecking.Info" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphHeader" runat="server">
    <asp:Panel runat="server" Width="100%" HorizontalAlign="Center">
        <asp:Label ID="Label3" runat="server" Font-Names="Calibri" Text=".:: Information ::." Font-Bold="True" Font-Size="Large" ForeColor="DarkSlateBlue"></asp:Label>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
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

    <asp:Panel runat="server" Width="100%">
        <div style="font-family: Calibri">
            <br />
            <asp:Label ID="lbInfo" runat="server" Font-Names="Calibri" Text="Info"></asp:Label> <br /> 
            <br />
            <br />
            <asp:Button ID="btOk" runat="server" CssClass="button" Text="Continue" Width="127px" />
        </div>

    </asp:Panel>
</asp:Content>

