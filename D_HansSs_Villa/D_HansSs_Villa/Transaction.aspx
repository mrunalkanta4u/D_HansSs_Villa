<%@ Page Title="Transaction" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Transaction.aspx.cs" Inherits="D_HansSs_Villa.Transaction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
      <asp:Table ID="Table1" runat="server" 
          style="z-index: 1; left: 145px; top: 262px; " Height="85px" Width="65px">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label1" runat="server" Text="Name:"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:DropDownList ID="DropDownList1" runat="server" Width="130px"></asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
         <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label2" runat="server" Text="Amount:"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="TextBox2" runat="server" Width="130px"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
         <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label3" runat="server" Text="Description:"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="TextBox3" runat="server" TextMode="MultiLine" Height="25px" Width="350px"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
         <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label4" runat="server" Text="Date:"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="White" 
          BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" 
          Height="190px" NextPrevFormat="FullMonth" Width="350px">
        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" 
            VerticalAlign="Bottom" />
        <OtherMonthDayStyle ForeColor="#999999" />
        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
        <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" 
            Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
        <TodayDayStyle BackColor="#CCCCCC" />
      </asp:Calendar>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
        style="z-index: 1; left: 829px; top: 173px; position: absolute; height: 34px; width: 232px" 
        Text="Save Transaction" />
      <asp:Label ID="Label5" runat="server" 
          style="z-index: 1; left: 823px; top: 257px; position: absolute" 
          Text="Label" Visible="False" Font-Bold  = "true"></asp:Label>
      </asp:Content>
