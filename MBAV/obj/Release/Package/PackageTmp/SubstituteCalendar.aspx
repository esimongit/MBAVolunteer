<%@ Page Title="Substitute Calendar" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="SubstituteCalendar.aspx.cs" Inherits="MBAV.SubstituteCalendar" %>

 
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
 <div class="row">
 <div class="col-md-2"> 
 <img alt="Orca" src="/Images/orca.jpg"/></div>
<div class="col-md-10" style=" text-align:center; ">
<div class="row" style="font-size:32pt;color:Black">Monterey Bay Aquarium</div>
<div class="row" style="font-size:32pt;color:Black">Volunteer Guide Substitute Calendar</div>
</div>
</div>
 <div class="row">
 <div class="col-md-5" style="margin-left:20px"> 
  <asp:Label ID="NameLabel" runat="server" Font-Bold="true"></asp:Label>
   <p>Click on the date you need or can be a substitute.
   </p><p>We need substitutes on dates indicated in: <span style='color:red;font-weight:bold; font-style:italic'>red</span>.
   </p>
   <p>You are expected on a shift on dates that are  <span style="color:#330088; font-weight:bold; text-decoration:underline">underlined</span>.
   </p>
 </div>
<div class="col-md-6"> 
    
        <div class="row"    > 
        <div class="col-md-5 col-xs-6" style="margin-left:5px;padding-top:10px; padding-bottom:10px"> 
          <asp:HyperLink ID="HyperLink3" runat="server"  ForeColor="Black" Font-Underline="false"  Font-Size="11" BorderColor="Gray"
           BorderStyle="Solid" BorderWidth="1" BackColor="ButtonFace"  Text="My Volunteer Profile"  NavigateUrl="Profile.aspx"/>
        </div>
        <div class="col-md-5 col-xs-6" style="margin-left:5px;padding-top:10px; padding-bottom:10px"> 
          <asp:HyperLink ID="HyperLink4" runat="server"  ForeColor="Black" Font-Underline="false"  Font-Size="11" BorderColor="Gray"
           BorderStyle="Solid" BorderWidth="1" BackColor="ButtonFace"  Text="Change Password"  NavigateUrl="ChangePassword.aspx"/>
        </div>
        </div>
       <div class="row"  > 
        <div class="col-md-5 col-xs-6" style="margin-left:5px;padding-top:10px; padding-bottom:10px"> 
          <asp:HyperLink ID="SubListLink" runat="server"  ForeColor="Black" Font-Underline="false"  Font-Size="11" BorderColor="Gray" BorderStyle="Solid" 
          BorderWidth="1" BackColor="ButtonFace"  Text="Substitutes for Mon2" Target="_blank" NavigateUrl="SubList.aspx?ShiftID=0"/>
           </div>
        <div class="col-md-5 col-xs-6"  style="margin-left:5px;padding-top:10px; padding-bottom:10px"> 
          
            <asp:HyperLink ID="RosterButton" runat="server"   Text="Daily Roster" ForeColor="Black" Font-Underline="false"  Font-Size="11"
             BorderColor="Gray" BorderStyle="Solid" BorderWidth="1" Target="_blank" BackColor="ButtonFace" NavigateUrl="ShiftRoster.aspx"></asp:HyperLink>
         </div> 
      </div>
      <div class="row"  > 
      <div class="col-md-5 col-xs-6" style="margin-left:5px;padding-top:10px; padding-bottom:10px"> 
           <div class="row" > 
            <div class="col-md-5 " style=" max-width:100px;background-color:#AAAAFF" align="center">A Weeks</div>
          <div class="col-md-5 " style="max-width:100px;background-color:#FFAAAA" align="center">B Weeks</div>
       </div>
       </div>
        <div class="col-md-5 col-xs-6"  style="margin-left:5px;padding-top:10px; padding-bottom:10px"> 
            <asp:HyperLink ID="HyperLink1" runat="server" Text="Sub for Dates on a Shift" ForeColor="Black" Font-Underline="false"  Font-Size="11"
             BorderColor="Gray" BorderStyle="Solid" BorderWidth="1" BackColor="ButtonFace"  NavigateUrl="Dropins.aspx"></asp:HyperLink>
        </div>
      </div>
    </div>
  
</div>
      <div class="clear"></div>
      <div class="row">
      <div class="col-xs-12 hidden-md hidden-lg hidden-sm">
      <div style="display:block;   margin-top:10px;margin-left:auto; margin-right: auto">
       <asp:Calendar ID="Calendar1"  SelectionMode="None"  Width="100%"  TitleStyle-BackColor="#0099cc"   TitleStyle-HorizontalAlign="Center" 
      DayStyle-HorizontalAlign="Center" DayStyle-VerticalAlign="Top"  NextMonthText=">>" TitleStyle-Font-Bold="false"
       TitleStyle-Font-Size="18pt"   DayHeaderStyle-Font-Bold="true"   DayStyle-ForeColor="#666666"
       PrevMonthText="<<" NextPrevStyle-HorizontalAlign="Center"  NextPrevStyle-Font-Size="Large"
      ShowGridLines="true"   DayStyle-BorderStyle="Groove"  DayStyle-BorderWidth="4" DayStyle-BorderColor="White"
            CellSpacing="2" OtherMonthDayStyle-ForeColor="Transparent" DayStyle-Font-Size="14" DayStyle-Font-Bold="true"
       runat="server"   OnDayRender="DayRenderHandler"   FirstDayOfWeek="Saturday"   OnVisibleMonthChanged="MonthChanged" 
        ></asp:Calendar>
   </div>
 </div></div>
 <div class="row">
      <div class="col-md-6 hidden-xs">
      <div style="display:block;   margin-top:10px;margin-left:auto; margin-right: auto">
       <asp:Calendar ID="Calendar2"  SelectionMode="None"  Width="90%"  TitleStyle-BackColor="#0099cc"   TitleStyle-HorizontalAlign="Center" 
      DayStyle-HorizontalAlign="Center" DayStyle-VerticalAlign="Top"   TitleStyle-Font-Bold="false"
       TitleStyle-Font-Size="18pt"   DayHeaderStyle-Font-Bold="true"   DayStyle-ForeColor="#666666"
       PrevMonthText="<<" NextPrevStyle-HorizontalAlign="Center"  NextPrevStyle-Font-Size="Large"  ShowNextPrevMonth="false"
      ShowGridLines="true"   DayStyle-BorderStyle="Groove"  DayStyle-BorderWidth="4" DayStyle-BorderColor="White"
            CellSpacing="2" OtherMonthDayStyle-ForeColor="Transparent" DayStyle-Font-Size="14" DayStyle-Font-Bold="true"
       runat="server"   OnDayRender="DayRenderHandler"   FirstDayOfWeek="Saturday"     
        ></asp:Calendar>
   </div>
 </div> 
      <div class="col-md-6 hidden-xs">
      <div style="display:block;   margin-top:10px;margin-left:auto; margin-right: auto">
       <asp:Calendar ID="Calendar3"  SelectionMode="None"  Width="90%"  TitleStyle-BackColor="#0099cc"   TitleStyle-HorizontalAlign="Center" 
      DayStyle-HorizontalAlign="Center" DayStyle-VerticalAlign="Top"   TitleStyle-Font-Bold="false"
       TitleStyle-Font-Size="18pt"   DayHeaderStyle-Font-Bold="true"   DayStyle-ForeColor="#666666"  ShowNextPrevMonth="false"
       NextPrevStyle-HorizontalAlign="Center"  NextPrevStyle-Font-Size="Large"
      ShowGridLines="true"   DayStyle-BorderStyle="Groove"  DayStyle-BorderWidth="4" DayStyle-BorderColor="White"
            CellSpacing="2" OtherMonthDayStyle-ForeColor="Transparent" DayStyle-Font-Size="14" DayStyle-Font-Bold="true"
       runat="server"   OnDayRender="DayRenderHandler"   FirstDayOfWeek="Saturday"   
        ></asp:Calendar>
   </div>
 </div></div>
 <div class="row">
        <div class="col-md-6 hidden-xs">
      <div style="display:block;   margin-top:10px;margin-left:auto; margin-right: auto">
       <asp:Calendar ID="Calendar4"  SelectionMode="None"  Width="90%"  TitleStyle-BackColor="#0099cc"   TitleStyle-HorizontalAlign="Center" 
      DayStyle-HorizontalAlign="Center" DayStyle-VerticalAlign="Top"    TitleStyle-Font-Bold="false"  ShowNextPrevMonth="false"
       TitleStyle-Font-Size="18pt"   DayHeaderStyle-Font-Bold="true"   DayStyle-ForeColor="#666666"
       PrevMonthText="<<" NextPrevStyle-HorizontalAlign="Center"  NextPrevStyle-Font-Size="Large"
      ShowGridLines="true"   DayStyle-BorderStyle="Groove"  DayStyle-BorderWidth="4" DayStyle-BorderColor="White"
            CellSpacing="2" OtherMonthDayStyle-ForeColor="Transparent" DayStyle-Font-Size="14" DayStyle-Font-Bold="true"
       runat="server"   OnDayRender="DayRenderHandler"   FirstDayOfWeek="Saturday"   
        ></asp:Calendar>
   </div>
 </div> 
 
       <div class="col-md-6 hidden-xs">
      <div style="display:block;   margin-top:10px;margin-left:auto; margin-right: auto">
       <asp:Calendar ID="Calendar5"  SelectionMode="None"  Width="90%"  TitleStyle-BackColor="#0099cc"   TitleStyle-HorizontalAlign="Center" 
      DayStyle-HorizontalAlign="Center" DayStyle-VerticalAlign="Top"  TitleStyle-Font-Bold="false" ShowNextPrevMonth="false"
       TitleStyle-Font-Size="18pt"   DayHeaderStyle-Font-Bold="true"   DayStyle-ForeColor="#666666"
       PrevMonthText="<<" NextPrevStyle-HorizontalAlign="Center"  NextPrevStyle-Font-Size="Large"
      ShowGridLines="true"   DayStyle-BorderStyle="Groove"  DayStyle-BorderWidth="4" DayStyle-BorderColor="White"
            CellSpacing="2" OtherMonthDayStyle-ForeColor="Transparent" DayStyle-Font-Size="14" DayStyle-Font-Bold="true"
       runat="server"   OnDayRender="DayRenderHandler"   FirstDayOfWeek="Saturday"    
        ></asp:Calendar>
   </div>
 </div></div>
 
</asp:Content>
