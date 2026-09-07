<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterMobile.Master" CodeBehind="mTemperatureLog.aspx.vb" Inherits="ContainerChecking.ContainerLog" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphHeader" runat="server">
    <asp:Panel runat="server" Width="100%" HorizontalAlign="Center">
        <asp:Label ID="Label7" runat="server" Font-Names="Calibri" Text=".:: Temperature Log ::." Font-Bold="True" Font-Size="Large" ForeColor="DarkBlue"></asp:Label>
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
        <div>
            <asp:Table ID="Table2" runat="server" Font-Names="Calibri" CellPadding="5">
                <asp:TableRow >
                    <asp:TableCell >
                        <asp:Label ID="Label3" runat="server" Text="Location"></asp:Label><br />
                        <asp:DropDownList ID="ddLocation" runat="server" Height="32px" Width="280px" AutoPostBack="True"></asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>

            <asp:Table ID="Table1" runat="server" Font-Names="Calibri" CellPadding="5">
                <asp:TableRow >
                    <asp:TableCell><asp:Label ID="Label2" runat="server" Text="Container"></asp:Label></asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="ddContainerNumber" runat="server" Height="32px" Width="160px" AutoPostBack="True"></asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow >
                    <asp:TableCell><asp:Label ID="Label4" runat="server" Text="Set Temperature"></asp:Label></asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="ddSetTemperature" runat="server" Height="32px" Width="60px" AutoPostBack="True" ></asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow >
                    <asp:TableCell><asp:Label ID="Label5" runat="server" Text="Temperature"></asp:Label></asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="ddTemperature" runat="server" Height="32px" Width="60px" AutoPostBack="True" ></asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow >
                    <asp:TableCell><asp:Label ID="Label6" runat="server" Text="Battery"></asp:Label></asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="ddBattery" runat="server" Height="32px" Width="60px" AutoPostBack="True"></asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>

            </asp:Table>
        </div>
        <div style="font-family: Calibri">
            <br />
            <asp:Label ID="lbRemark" runat="server" Font-Names="Calibri" Text="Remark (255 chars)"></asp:Label> <br /> 
            <asp:TextBox ID="tbRemark" runat="server" Font-Names="Calibri" Height="100px" MaxLength="255" TextMode="MultiLine" Width="300px"  ></asp:TextBox>
            <br />
            <br />
            Upload Picture Container# <br /> 
            <asp:FileUpload ID="fuContainer" runat="server" Font-Names="Calibri" Width="300px" /> <br />
            <br />
            <br />
            Upload Picture Temperature <br /> 
            <asp:FileUpload ID="fuTemperature" runat="server" Font-Names="Calibri" Width="300px" /> <br />
            <br />
            <br />
            <asp:Button ID="btSubmit" runat="server" CssClass="button" Text="Submit" Width="127px" />
        </div>

    </asp:Panel>
</asp:Content>
