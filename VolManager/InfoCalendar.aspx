<%@ Page Title="Substitute Calendar" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="InfoCalendar.aspx.cs" Inherits="VolManager.InfoCalendar" %>

 
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
 
 
 
 <div   style="margin-left:20px"> 
  
    </div>
 
      <div class="clear"></div>
     
     
      <div style=" margin-top:10px;margin-left:auto; margin-right: auto">
       <asp:Calendar ID="Calendar1"  SelectionMode="None"  Width="90%"  TitleStyle-BackColor="#0099cc"   TitleStyle-HorizontalAlign="Center" 
      DayStyle-HorizontalAlign="Center" DayStyle-VerticalAlign="Top"  NextMonthText=">>" TitleStyle-Font-Bold="false"  
       TitleStyle-Font-Size="18pt"   DayHeaderStyle-Font-Bold="true"   DayStyle-ForeColor="#666666" NextPrevFormat="FullMonth"
       PrevMonthText="<<" NextPrevStyle-HorizontalAlign="Center"  NextPrevStyle-Font-Size="Large"  
      ShowGridLines="true"   DayStyle-BorderStyle="Groove"  DayStyle-BorderWidth="4" DayStyle-BorderColor="White"
            CellSpacing="2" OtherMonthDayStyle-ForeColor="Transparent" DayStyle-Font-Size="14" DayStyle-Font-Bold="true"
       runat="server"   OnDayRender="DayRenderHandler"   FirstDayOfWeek="Saturday"   OnVisibleMonthChanged="MonthChanged" 
        ></asp:Calendar>
   
 </div> 
 
</asp:Content>
