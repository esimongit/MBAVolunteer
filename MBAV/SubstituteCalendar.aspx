<%@ Page Title="Substitute Calendar" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="SubstituteCalendar.aspx.cs" Inherits="MBAV.SubstituteCalendar" %>

 
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ObjectDataSource ID="ShiftsDataSource" TypeName="NQN.DB.ShiftsDM" SelectMethod="ShiftsForGuide" runat="server">
        <SelectParameters>
            <asp:SessionParameter Name="GuideID" SessionField="GuideID" Type="Int32" DefaultValue="0" />
        </SelectParameters>
    </asp:ObjectDataSource>
 <style>
     th { text-align:center}
 </style>
 <div class="row">
 <div class="col-md-5" style="margin-left:20px; font-size:12pt"> 
    <div class="row">
        <div class="col-md-12">
  <asp:Label ID="NameLabel" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
   <p>Click on the date you need or can be a substitute.
   </p><p>We need substitutes on dates indicated in: <span style='color:blue;font-weight:bold; font-style:italic'>blue</span>.
   </p>
     <p>We have a critical need for substitutes on dates indicated in: <span style='color:red;font-weight:bold; font-style:italic'>red</span>.
   </p>
   <p>You are expected on a shift on dates that are  <span style="color:#222; font-weight:bold; text-decoration:underline">underlined</span>.
   </p>
 </div>
  </div>     
       
           <div class="row" > 
            <div class="col-xs-6 " style=" max-width:120px;background-color:#9aff9a; font-size:large" >A Weeks</div>
          <div class="col-xs-6 " style="max-width:120px;background-color:#FFFFFF ; font-size:large" >B Weeks</div>
       </div>
      
      </div>
   
 <style>
     th { text-align:center}
 </style>
<div class="col-md-6"> 
    
        <div class="row" > 
        <div class="col-md-5 col-xs-6" style="margin-left:5px;padding-top:10px; padding-bottom:10px"> 
          <asp:HyperLink ID="HyperLink3" runat="server" CssClass="btn btn-info" Text="My Volunteer Profile"  NavigateUrl="Profile.aspx"/>
        </div>
        <div class="col-md-5 col-xs-6" style="margin-left:5px;padding-top:10px; padding-bottom:10px"> 
          <asp:HyperLink ID="HyperLink4" runat="server"  CssClass="btn btn-info" Text="Change Password"  NavigateUrl="ChangePassword.aspx"/>
        </div>
        </div>
       <div class="row" > 
        <div class="col-md-5 col-xs-6" style="margin-left:5px;padding-top:10px; padding-bottom:10px"> 
          <asp:Repeater ID="SubLinkRepeater" runat="server" DataSourceID="ShiftsDataSource">
              <ItemTemplate>
            <div class="row" style="padding-bottom:3px"  >
                <div class="col-md-3"> 
          <asp:HyperLink ID="SubListLink" runat="server"  CssClass="btn btn-info" Text='<%#Eval("ShortName", "Substitutes for {0}") %>' Target="_blank"  NavigateUrl='<%#Eval("ShiftID", "~/SubList.aspx?ShiftID={0}") %>'/>
            </div></div>
            </ItemTemplate>
              </asp:Repeater>
       
           </div>
        <div class="col-md-5 col-xs-6"  style="margin-left:5px;padding-top:10px; padding-bottom:10px"> 
          
            <asp:HyperLink ID="RosterButton" runat="server"   Text="Daily Roster" CssClass="btn btn-info" Target="_blank" NavigateUrl="ShiftRoster.aspx"></asp:HyperLink>
         </div> 
      </div>
     <div class="row" > 
        <div class="col-md-5 col-xs-6" style="margin-left:0px;padding-top:16px; padding-bottom:10px"> 
             <asp:HyperLink ID="SearchLink" runat="server"  CssClass="btn btn-info" Text="Search Guides"  NavigateUrl="Search.aspx"/>
            </div>
            <div  runat="server" id="ToggleDiv" class="col-md-5 col-xs-6" style="margin-left:0px;padding-top:16px; padding-bottom:10px"> 
             <asp:Button ID="ToggleCalendar" runat="server"  CssClass="btn btn-info" Text="Info Center Calendar"  OnClick="CalendarToggle"/>
            </div>
         <div class="col-md-5 col-xs-6" style="margin-left:0px;padding-top:16px; padding-bottom:10px"> 
             <asp:HyperLink ID="SpecialShiftButton" runat="server"  CssClass="btn btn-info" Text="Special Shifts"  NavigateUrl ="Special.aspx"/>
            </div>
   </div>
             
    </div>
   
 
</div>
      
    <div class="row" >
        <div class="col-xs-7 col-xs-offset-1" style="padding-top:9px">
         <asp:Label ID="CalendarTypeLabel" runat="server" Text="Guide Calendar"   Font-Bold="true" Font-Size="24pt"></asp:Label></div>  
  
   
         <div id="ListLink" runat="server" class="col-xs-3"><asp:HyperLink ID="CalendarListLink" runat="server"   Text="List View" ForeColor="Black" CssClass="btn btn-lg btn-info" Target="_blank" NavigateUrl="CalendarList.aspx"></asp:HyperLink>
        </div>
                
    </div>
    
  
    
      <div class="row">
      <div class="col-xs-12 hidden-md hidden-lg hidden-sm">
      <div style="display:block;   margin-top:10px;margin-left:auto; margin-right: auto">
       <asp:Calendar ID="Calendar1"  SelectionMode="None"  Width="100%"  TitleStyle-BackColor="#0099cc"   TitleStyle-HorizontalAlign="Center" 
      DayStyle-HorizontalAlign="Center" DayStyle-VerticalAlign="Top"   NextPrevStyle-ForeColor="White" NextMonthText=">>" TitleStyle-Font-Bold="false"  TitleStyle-ForeColor="White"
       TitleStyle-Font-Size="18pt"   DayHeaderStyle-Font-Bold="true"   DayStyle-ForeColor="#666666"
       PrevMonthText="<<" NextPrevStyle-HorizontalAlign="Center"  NextPrevStyle-Font-Size="Large" TitleStyle-BorderWidth="3" NextPrevStyle-Font-Underline="false"
      ShowGridLines="true"   DayStyle-BorderStyle="Groove"  DayStyle-BorderWidth="4" DayStyle-BorderColor="White"  
            CellSpacing="2" OtherMonthDayStyle-ForeColor="Transparent" DayStyle-Font-Size="14" DayStyle-Font-Bold="true"
       runat="server"   OnDayRender="DayRenderHandler"   FirstDayOfWeek="Sunday"   OnVisibleMonthChanged="MonthChanged"
             DayHeaderStyle-BorderColor="White"
           DayHeaderStyle-BorderStyle="Groove" DayHeaderStyle-BorderWidth="4"  DayHeaderStyle-HorizontalAlign="Center" >
           
       </asp:Calendar>
   </div>
 </div></div>
 <div class="row">
      <div class="col-md-6 hidden-xs">
      <div style="display:block;   margin-top:10px;margin-left:auto; margin-right: auto">
       <asp:Calendar ID="Calendar2"  SelectionMode="None"  Width="90%"  TitleStyle-BackColor="#0099cc"   TitleStyle-HorizontalAlign="Center" 
      DayStyle-HorizontalAlign="Center" DayStyle-VerticalAlign="Top"   TitleStyle-Font-Bold="false" TitleStyle-ForeColor="White"
       TitleStyle-Font-Size="18pt"   DayHeaderStyle-Font-Bold="true"   DayStyle-ForeColor="#666666" TitleStyle-BorderWidth="3"
        ShowNextPrevMonth="false"
      ShowGridLines="true"   DayStyle-BorderStyle="Groove"  DayStyle-BorderWidth="4" DayStyle-BorderColor="White"
            CellSpacing="2" OtherMonthDayStyle-ForeColor="Transparent" DayStyle-Font-Size="14" DayStyle-Font-Bold="true" 
       runat="server"   OnDayRender="DayRenderHandler"   FirstDayOfWeek="Sunday"  DayHeaderStyle-BorderColor="White"
           DayHeaderStyle-BorderStyle="Groove" DayHeaderStyle-BorderWidth="4"  DayHeaderStyle-HorizontalAlign="Center" >
      

       </asp:Calendar>
   </div>
 </div> 
      <div class="col-md-6 hidden-xs">
      <div style="display:block;   margin-top:10px;margin-left:auto; margin-right: auto">
       <asp:Calendar ID="Calendar3"  SelectionMode="None"  Width="90%"  TitleStyle-BackColor="#0099cc"   TitleStyle-HorizontalAlign="Center" 
      DayStyle-HorizontalAlign="Center" DayStyle-VerticalAlign="Top"   TitleStyle-Font-Bold="false" TitleStyle-ForeColor="White"
       TitleStyle-Font-Size="18pt"   DayHeaderStyle-Font-Bold="true"   DayStyle-ForeColor="#666666"  ShowNextPrevMonth="false"
       TitleStyle-BorderWidth="3"
      ShowGridLines="true"   DayStyle-BorderStyle="Groove"  DayStyle-BorderWidth="4" DayStyle-BorderColor="White"
            CellSpacing="2" OtherMonthDayStyle-ForeColor="Transparent" DayStyle-Font-Size="14" DayStyle-Font-Bold="true"
       runat="server"   OnDayRender="DayRenderHandler"   FirstDayOfWeek="Sunday"   DayHeaderStyle-BorderColor="White"
           DayHeaderStyle-BorderStyle="Groove" DayHeaderStyle-BorderWidth="4"  DayHeaderStyle-HorizontalAlign="Center"   >
          <DayHeaderStyle HorizontalAlign="Center" />

       </asp:Calendar>
   </div>
 </div>

 </div>
 <div class="row">
        <div class="col-md-6 hidden-xs">
      <div style="display:block;   margin-top:10px;margin-left:auto; margin-right: auto">
       <asp:Calendar ID="Calendar4"  SelectionMode="None"  Width="90%"  TitleStyle-BackColor="#0099cc"   TitleStyle-HorizontalAlign="Center" 
      DayStyle-HorizontalAlign="Center" DayStyle-VerticalAlign="Top"    TitleStyle-Font-Bold="false"  ShowNextPrevMonth="false"
       TitleStyle-Font-Size="18pt"   DayHeaderStyle-Font-Bold="true"   DayStyle-ForeColor="#666666" TitleStyle-ForeColor="White"
      TitleStyle-BorderWidth="3"
      ShowGridLines="true"   DayStyle-BorderStyle="Groove"  DayStyle-BorderWidth="4" DayStyle-BorderColor="White"
            CellSpacing="2" OtherMonthDayStyle-ForeColor="Transparent" DayStyle-Font-Size="14" DayStyle-Font-Bold="true"
       runat="server"   OnDayRender="DayRenderHandler"   FirstDayOfWeek="Sunday"   DayHeaderStyle-BorderColor="White"
           DayHeaderStyle-BorderStyle="Groove" DayHeaderStyle-BorderWidth="4"  DayHeaderStyle-HorizontalAlign="Center"   >
         <DayHeaderStyle HorizontalAlign="Center" />

       </asp:Calendar>
   </div>
 </div> 
 
       <div class="col-md-6 hidden-xs">
      <div style="display:block;   margin-top:10px;margin-left:auto; margin-right: auto">
       <asp:Calendar ID="Calendar5"  SelectionMode="None"  Width="90%"  TitleStyle-BackColor="#0099cc"   TitleStyle-HorizontalAlign="Center" 
      DayStyle-HorizontalAlign="Center" DayStyle-VerticalAlign="Top"  TitleStyle-Font-Bold="false" ShowNextPrevMonth="false"
       TitleStyle-Font-Size="18pt"   DayHeaderStyle-Font-Bold="true"   DayStyle-ForeColor="#666666" TitleStyle-ForeColor="White"
        DayHeaderStyle-HorizontalAlign="Center"
      ShowGridLines="true"   DayStyle-BorderStyle="Groove"  DayStyle-BorderWidth="4" DayStyle-BorderColor="White" ShowDayHeader="true"
            CellSpacing="2" OtherMonthDayStyle-ForeColor="Transparent" DayStyle-Font-Size="14" DayStyle-Font-Bold="true"
             DayHeaderStyle-BorderColor="White" TitleStyle-BorderWidth="3"
           DayHeaderStyle-BorderStyle="Groove" DayHeaderStyle-BorderWidth="4"  
       runat="server"   OnDayRender="DayRenderHandler"   FirstDayOfWeek="Sunday"    >
            

       </asp:Calendar>
   </div>
   </div>
</div>
     
</asp:Content>
