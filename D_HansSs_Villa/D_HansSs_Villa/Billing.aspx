<%@ Page Title="Billing" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Billing.aspx.cs" Inherits="D_HansSs_Villa.Billing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="Table1" runat="server" 
        style="z-index: 1; left: 145px; top: 262px; " Height="87px" Width="348px">
       <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label1" runat="server" Text="Name:"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:DropDownList ID="DropDownList1" runat="server" Width="130px">
                </asp:DropDownList>
            </asp:TableCell>
       </asp:TableRow>  
       <asp:TableRow>  
            <asp:TableCell>
                <asp:Label ID="Label2" runat="server" Text="No Of Members:"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="Label8" runat="server" Text="" Font-Bold="True" ></asp:Label>
            </asp:TableCell>    
        </asp:TableRow>  
         <asp:TableRow>  
            <asp:TableCell>
                <asp:Label ID="Label3" runat="server" Text="House rent:"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>        
                <asp:Label ID="Label4" runat="server" Text="" Font-Bold="True" ></asp:Label>
            </asp:TableCell>   
        </asp:TableRow>  
         </asp:Table>   

    <br />

    <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="White" 
          BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" 
          Height="190px" NextPrevFormat="FullMonth" Width="350px" 
        SelectionMode="DayWeekMonth">
        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" 
            VerticalAlign="Bottom" />
        <OtherMonthDayStyle ForeColor="#999999" />
        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
        <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" 
            Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
        <TodayDayStyle BackColor="#CCCCCC" />
      </asp:Calendar>

    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
        style="z-index: 1; left: 732px; top: 173px; position: absolute; height: 34px; width: 232px" 
        Text="Generate Bill" />
    <asp:Label ID="Label5" runat="server"   
            style="z-index: 1; left: 738px; top: 238px; position: absolute; height: 24px; width: 344px; margin-top: 0px;" 
            Text="Label" Visible="false" Font-Bold  = "true"></asp:Label>
    </asp:Content>