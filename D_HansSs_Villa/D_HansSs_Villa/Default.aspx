<%@ Page Title="Welcome" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="D_HansSs_Villa._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1
        {
            width: 920px;
            height: 318px;
        }
        .style2
        {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <p>
        <img alt="HomePage" class="style1" longdesc="This is Home Page " 
            src="Images/HomePage.jpg" /></p>
    <p class="style2">
        Now Maintaining The Daily Expenses &amp; Generating Monthly Bill For Your Villa is 
        Just an effort of 1 Click !!!</p>

    </asp:Content>
<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
        <div class="content-wrapper">       
                <h1 class="style2">Welcome to D HansSs&#39;s Villa Account Manager</h1>
        </div>
</asp:Content>