<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterMobile.Master" CodeBehind="mPhysicalInspectionItem.aspx.vb" Inherits="ContainerChecking.PhysicalCheckItem" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphHeader" runat="server">
    <asp:Panel runat="server" Width="100%" HorizontalAlign="Center">
        <asp:Label ID="Label3" runat="server" Font-Names="Calibri" Text=".:: Physical Inspection Item ::." Font-Bold="True" Font-Size="Large" ForeColor="DarkBlue"></asp:Label>
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
        <br />
        <asp:Label ID="lbItem" runat="server" Font-Names="Calibri" Text="Itemside" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:Panel ID="panBody" runat="server" BorderStyle="None" Width="300px" >
            <asp:CheckBox ID="cbBroken" runat="server" Text="&nbsp;Found scratch/dent/broke" Font-Names="Calibri"/><br />
            <br />
            <asp:Label ID="lbRemark" runat="server" Font-Names="Calibri" Text="&nbsp;Remark (255 chars)"></asp:Label><br />
            <asp:TextBox ID="tbRemark" runat="server" Font-Names="Calibri" Height="100px" MaxLength="255" TextMode="MultiLine" Width="285px"></asp:TextBox><br />
        </asp:Panel>
        <br />
        <asp:Table ID="tblSeal" runat="server" Font-Names="Calibri">
            <asp:TableRow>
                <asp:TableCell Width="100">
                    <asp:CheckBox ID="cbIsSealed" runat="server" Text="&nbsp;Sealed" Font-Names="Calibri" AutoPostBack="True" /><br />
                </asp:TableCell><asp:TableCell Width="200">
                    <asp:CheckBox ID="cbBrokenSeal" runat="server" Text="&nbsp;Broken" Font-Names="Calibri" AutoPostBack="True" /><br />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell >
                    <asp:Label ID="lbSealNumber" runat="server" Text="&nbsp;Seal Number"></asp:Label>
                </asp:TableCell><asp:TableCell >
                    <asp:TextBox ID="tbSealNumber" runat="server" MaxLength="50" Width="180px" AutoPostBack="True"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br />            

        <asp:Image ID="imgThumbnail" runat="server" Visible="false" /><br />
        <asp:Label ID="lbUpload" runat="server" Font-Names="Calibri" Text="&nbsp;Upload Picture"></asp:Label><br />
        <asp:FileUpload ID="fuPicture" runat="server" Font-Names="Calibri" Width="280px" /> <br />
        <br />
        <asp:Table ID="Table1" runat="server" Font-Names="Calibri">
            <asp:TableRow>
                <asp:TableCell Width="100">
                    <asp:Button ID="btCancel" runat="server" CssClass="button" Text="Cancel" Width="127px" />
                </asp:TableCell><asp:TableCell Width="35">
                    <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                </asp:TableCell><asp:TableCell Width="100">
                    <asp:Button ID="btSubmit" runat="server" CssClass="button" Text="Inspected" Width="127px" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

    </asp:Panel>
</asp:Content>

