<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterMain.Master" CodeBehind="Location.aspx.vb" Inherits="ContainerChecking.Location" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <style>
        .ribbon{
            width:22px;
        }
        .ribbon:hover{
            border-top-style:solid;
            border-top-color:white;
        }
        .textboxEntry{
            width:200px;
            height:20px;
        }
        .mandatoryLabel{
            border-bottom-style:solid;
            border-bottom-width:1px;
            border-bottom-color:red;
        }

        .rowHeaderFilter{
            border-bottom-style:double;
            border-bottom-color:#fed8b1;
        }

    </style>

    <asp:Label runat="server" Font-Names="Calibri" Font-Size="Larger" Font-Bold="true" Text="Location" ForeColor="DarkSlateBlue" /><br /><br />
</asp:Content>
