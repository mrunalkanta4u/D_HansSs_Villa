<%@ Page Title="Admin Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="D_HansSs_Villa.Admin.AdminPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<asp:Table ID="Table1" runat="server" 
        style="z-index: 1; left: 145px; top: 262px; " Height="271px" 
        Width="920px" >
       <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label7" runat="server" style="z-index: 1; left: 43px; top: 181px;" Text="Query:"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="TextBox3" runat="server" 
                style="z-index: 1; left: 297px; top: 122px; height: 66px; width: 358px" 
                TextMode="MultiLine" ></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell> 
                <asp:Button ID="Button2" runat="server" onclick="Button2_Click"  Text="Submit" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:DropDownList ID="DropDownList1" runat="server" Width="130px" onselectedindexchanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="Send Mail" />&nbsp &nbsp
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Send Message" />
             </asp:TableCell>        
         </asp:TableRow>
         <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label1" runat="server" style="z-index: 1; left: 43px; top: 181px;" Text="Set Monthly House Rent Amount:"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="TextBox1" runat="server" style=" width: 150px" ></asp:TextBox>&nbsp &nbsp
                <asp:Button ID="Button4" runat="server" onclick="Button4_Click" Text="Submit" />
             </asp:TableCell>        
         </asp:TableRow>
         <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label2" runat="server" style="z-index: 1; left: 43px; top: 181px;" visible = "false" Font-Bold="True"></asp:Label>
             </asp:TableCell>        
         </asp:TableRow>
     </asp:Table>
</asp:Content>
