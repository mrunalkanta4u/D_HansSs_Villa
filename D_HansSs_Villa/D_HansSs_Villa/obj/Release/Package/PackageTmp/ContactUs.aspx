<%@ Page Title="Contact Us" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="D_HansSs_Villa.ContactUs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style4
        {
            text-align: left;
            height: 94px;
            float: left;
            width: 88px;
            font-size: medium;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="Table1" runat="server" 
        style="z-index: 1; left: 145px; top: 262px; " Height="87px" class="style4" 
        Width="430px">
       <asp:TableRow>
            <asp:TableCell>
                    <img alt="Mrunal " class="style4" longdesc="Mrunal Kanta Muduli (D hansSs)" 
                        src="Images/Mrunal.jpg" />
            </asp:TableCell>
            <asp:TableCell>
                <i>Mrunal Kanta Muduli<br /><i>Contact No: +91 9663865026</i><br />
                <i>Email ID:<a href="mailto:mrunalkanta4u@gmail.com">
                mrunalkanta4u@gmail.com</a></i>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
