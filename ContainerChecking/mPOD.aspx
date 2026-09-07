<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="mPOD.aspx.vb" Inherits="ContainerChecking.POD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
</head>
<body">
    <script>
        function getLocation() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(showPosition);
            } else {
                alert("Browser not support");
            }
        }

        function showPosition(position) {
            document.getElementById('<%=hidLat.ClientID%>').value = position.coords.latitude;
            document.getElementById('<%=hidLon.ClientID%>').value = position.coords.longitude;
        }
    </script>

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


    <form id="form1" runat="server">

        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript" src="https://cdn.rawgit.com/mobomo/sketch.js/master/lib/sketch.min.js"></script>
        <script type="text/javascript">
            $("body").on("click", "#ibtSubmit", function () {
                var base64 = $('#cvsSignature')[0].toDataURL();
                $("#inpByteSignature").val(base64);
            });
            $(function () {
                $('#cvsSignature').sketch();
                $(".tools a").eq(0).attr("style", "color:#000");
                $(".tools a").click(function () {
                    $(".tools a").removeAttr("style");
                    $(this).attr("style", "color:#000");
                });
            });
        </script>

        <asp:HiddenField id="hidLon" runat="server"></asp:HiddenField>
        <asp:HiddenField id="hidLat" runat="server"></asp:HiddenField>

        <div id="divHead" runat="server" style="width:300px; font-family:Calibri">
            <asp:Table runat="server" Width="300px" >
                <asp:TableRow VerticalAlign="Middle">
                    <asp:TableCell>
                        <asp:ImageButton ID="ibtLogoAgility" runat="server" ImageUrl="~/Images/LogoAgility.png" width="120px" />   
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Label runat="server" Text="Container Checking" Font-Bold="true" ForeColor="DarkBlue" Font-Size="18px"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <br />
            <asp:Panel runat="server" Width="100%" HorizontalAlign="Center">
                <asp:Label ID="Label1" runat="server" Text=".:: Proof of Delivery ::." Font-Bold="True" Font-Size="Large" ForeColor="DarkSlateBlue"></asp:Label>
                <hr style="border-style:solid; border-color:darkblue" />
            </asp:Panel>
        </div>
        <br />
        <asp:Label ID="Label3" runat="server" Font-Names="Calibri" Font-Bold="true" Font-Size="Medium" Text="Berita Acara Serah Terima Kontainer" ></asp:Label>
        <br />
        <asp:Label ID="Label2" runat="server" Font-Names="Calibri" Text="Nomor Kontainer : " />
        <div id="divContainerList" runat="server" style="font-family:Calibri">

        </div>
        <br />
        <br />
        <div id="divAcceptance" runat="server" style="font-family:Calibri">
            <asp:Label ID="Label4" runat="server" Font-Bold="true" Text="Tanda Terima : " />
            <br />
            <asp:Label ID="Label5" runat="server" Text="Nama : " />
            <br />
            <asp:TextBox ID="tbNama" runat="server" Width="290px" AutoCompleteType="DisplayName"/>
            <br />
            <br />
            <asp:Label ID="Label6" runat="server" Text="Departemen : " />
            <br />
            <asp:TextBox ID="tbDepartment" runat="server" Width="290px" AutoCompleteType="Department" />
            <br />
            <br />
            <asp:Label ID="Label9" runat="server" Text="Catatan : " />
            <br />
            <asp:TextBox ID="tbRemark" runat="server" Height="100px" MaxLength="255" TextMode="MultiLine" Width="300px"  />
            <br />
            <br />
            <asp:Label ID="Label7" runat="server" Text="Tanda-tangan : " />
            <br />
            <asp:Panel runat="server" >
                <canvas id="cvsSignature" runat="server" width="290" height="130" style="border: 1px solid #ccc" ></canvas>
                <br />
                <asp:Button ID="ibtClear" runat="server" CssClass="button" Text="Ulangi Tanda-tangan" Width="290px" />
                <br />
                <input id="inpByteSignature" runat="server" type="hidden" />
                <br />
                <asp:Button ID="ibtSubmit" runat="server" CssClass="button" Text="Submit" />

            </asp:Panel>
        </div>
        <asp:Panel runat="server" Width="300px" HorizontalAlign="Center">
            <br/>
            <asp:Label ID="Label8" runat="server" Font-Names="Calibri" Text=".::   I T   @   2 0 2 1   ::." Font-Size="Small" Font-Bold="true"></asp:Label>
        </asp:Panel>

    </form>
</body>
</html>
