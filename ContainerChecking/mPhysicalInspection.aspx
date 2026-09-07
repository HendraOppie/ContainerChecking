<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterMobile.Master" CodeBehind="mPhysicalInspection.aspx.vb" Inherits="ContainerChecking.PhysicalCheck" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphHeader" runat="server">
    <asp:Panel runat="server" Width="100%" HorizontalAlign="Center">
        <asp:Label ID="Label3" runat="server" Font-Names="Calibri" Text=".:: Physical Inspection ::." Font-Bold="True" Font-Size="Large" ForeColor="DarkSlateBlue"></asp:Label>
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

        .linkbuttonAsLabel {
            color: #000000;
            text-decoration: none;
        }

        .linkbuttonAsLabel:hover {
            text-decoration: none;
        }

    </style>

    <asp:Panel runat="server" Width="100%">
        <br />
        <asp:Label ID="Label25" runat="server" Font-Names="Calibri" Text="Location"></asp:Label><br />
        <asp:DropDownList ID="ddLocation" runat="server" Font-Names="Calibri" Height="32px" Width="300px" AutoPostBack="True"></asp:DropDownList>
        <br /> 
        <br /> 
        <asp:Label ID="Label26" runat="server" Font-Names="Calibri" Text="Container Number"></asp:Label>
            <br />
        <asp:DropDownList ID="ddContainerNumber" runat="server" Font-Names="Calibri" Height="26px" Width="300px" AutoPostBack="True"></asp:DropDownList>        
        <br /> 
        <br /> 
        <asp:ImageButton ID="ibtRefresh" runat="server" ImageUrl="~/Images/Refresh_001.png" Width="30px"/>
        <asp:Label ID="Label4" runat="server" Font-Names="Calibri" Font-Bold="True" Text="Inpection Review" Height="25px" /><br />
        <asp:Table ID="tblDetail" runat="server" Font-Names="Calibri" GridLines="Both" BorderStyle="Solid" CellPadding="5" BorderColor="DarkBlue" >
                <asp:TableRow VerticalAlign="Top" >
                    <asp:TableCell Width="80" Font-Bold="True" BackColor="#00ccff">
	                    <asp:Label ID="Label6" runat="server" Text="Item" />
                    </asp:TableCell><asp:TableCell Width="150" Font-Bold="True" BackColor="#00ccff">
                        <asp:Label ID="Label2" runat="server" Text="Summary" Width="200px" />
                    </asp:TableCell></asp:TableRow></asp:Table></asp:Panel></asp:Content>