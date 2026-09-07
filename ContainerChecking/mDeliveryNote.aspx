<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterMobile.Master" CodeBehind="mDeliveryNote.aspx.vb" Inherits="ContainerChecking.Handover" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="server">
    <asp:Panel runat="server" Width="100%" HorizontalAlign="Center">
        <asp:Label ID="Label1" runat="server" Font-Names="Calibri" Text=".:: Delivery Note ::." Font-Bold="True" Font-Size="Large" ForeColor="DarkSlateBlue"></asp:Label>
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

        .linkbuttonAsLabel {
            color: #000000;
            text-decoration: none;
        }

        .linkbuttonAsLabel:hover {
            text-decoration: none;
        }

    </style>
    <script>
        function funCopyToClipboard() {
            /* Get the text field */
            var copyText = document.getElementById("<%=tbLink.ClientID%>");

            /* Select the text field */
            copyText.select();
            copyText.setSelectionRange(0, 99999); /* For mobile devices */

            /* Copy the text inside the text field */
            document.execCommand("copy");

            /* Alert the copied text = alert("Copied the text: " + copyText.value);*/
        }
    </script>

    <asp:Panel runat="server" Width="100%" Font-Names="Calibri">
        <br /> 
        <div id="divContainerList" runat="server">
            <asp:CheckBox ID="cbSelectAll" runat="server" Text="Select Container Number" AutoPostBack="true" Height="30px" />
        </div>
        <br />
        <asp:Panel runat="server" HorizontalAlign="Center">
            <asp:Button ID="btGeneratePOD" runat="server" CssClass="button" Text="Generate POD" />
        </asp:Panel>
        <br />
        <div id="divPOD" runat="server" >
            <asp:Panel runat="server" HorizontalAlign="Center">
                <asp:Label ID="Label4" runat="server" Text="Handover option:"></asp:Label>
                <br />
                <br />
                <asp:Label ID="Label5" runat="server" Text="Scan this QR Code using customer's device"></asp:Label>
                <br />
                <asp:Image ID="imgQR" runat="server" Width="290px" Height="290px" />
                <br />
                <br />
                <asp:Label ID="Label7" runat="server" Text="Or text this link to customer"></asp:Label>
                <br />
                <asp:TextBox ID="tbLink" runat="server" Text="[navigation link]" ReadOnly="true" BorderStyle="None" Width="290px" AutoPostBack="true"></asp:TextBox>
                <br />
                <asp:Button ID="btCopyLink" runat="server" CssClass="button" Text="Copy Link" OnClientClick="funCopyToClipboard()" />
                <br />
                <br />
                <asp:Label ID="Label3" runat="server" Text="Or proceed using this device"></asp:Label>
                <br />
                <asp:Button ID="btPOD" runat="server" CssClass="button" Text="Proof of Delivery" />
            </asp:Panel>
        </div>

    </asp:Panel>
</asp:Content>
