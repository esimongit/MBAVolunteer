<%@ Page Title="" Language="C#" MasterPageFile="~/Secondary.Master" AutoEventWireup="true" CodeBehind="Special.aspx.cs" Inherits="MBAV.Special" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <asp:ObjectDataSource ID="SpecialsDataSource" runat="server" TypeName="NQN.DB.ShiftsDM" SelectMethod="SpecialShiftsForGuide">
  <SelectParameters>
   <asp:SessionParameter SessionField="GuideID" Name="GuideID" Type="Int32" DefaultValue="0" />
      <asp:ControlParameter ControlID="RoleSelect" Name="RoleID" DefaultValue="0" Type="Int32" />
 </SelectParameters>
</asp:ObjectDataSource>
     <style type="text/css">
 td, th
 {
     padding: 4px 4px 4px 4px
     
 }
 </style>
   
<div class="row"><div class="col-xs-12">
<h3>In the table below,  check all the boxes for special shifts on which 
you can participate. </h3><hr/>
  <div class="row" style="padding-bottom:10px"><div class="col-md-6 col-md-offset-2 col-xs-12">
         <asp:HyperLink ID="HyperLink2" runat="server"   CssClass="btn btn-info"  Text="Return to Calendar"  NavigateUrl="SubstituteCalendar.aspx"/>
  </div></div>
     <div class="row" style="padding-bottom:8px">
        <div class="col-xs-12">
            <asp:DropDownList ID="RoleSelect" runat="server" Font-Bold="true" DataTextField="RoleName" AutoPostBack="true" DataValueField="RoleID"></asp:DropDownList>
        </div>
    </div>
<asp:Repeater ID="SpecialRepeater" runat="server" DataSourceID="SpecialsDataSource" >
<HeaderTemplate><table cellpadding="5" border="1">
    <tr><th>Check to Join</th><th>Shift</th><th>Date</th><th>DOW</th><th>Start Time</th><th>End Time</th><th>Count</th><th>Needed</th></tr></HeaderTemplate>
<ItemTemplate><tr style='<%#Container.ItemIndex %2==0 ? "background-color:#bfbfbf":"background-color:#ffffff" %>'> 
      <td style="text-align:center"> 
    <asp:CheckBox ID="ShiftCheckBox" runat="server" Checked='<%#Eval("Selected") %>' OnCheckedChanged="SpecialShiftChanged"  AutoPostBack="true" />
    <asp:HiddenField ID="ShiftIDHidden" runat="server" Value='<%#Eval("ShiftID") %>' />
   </td><td><asp:Label runat="server" Text='<%#Eval("ShiftName") %>'></asp:Label></td>
    <td><asp:Label runat="server" Text='<%#Eval("ShiftDate","{0:d}") %>'></asp:Label></td>
     <td><asp:Label runat="server" Text='<%#Eval("ShiftDate","{0:ddd}") %>'></asp:Label></td>
    <td><asp:Label runat="server" Text='<%#Eval("ShiftStart","{0:t}") %>'></asp:Label></td>
     <td><asp:Label runat="server" Text='<%#Eval("ShiftEnd","{0:t}") %>'></asp:Label></td>
   
    <td><asp:Label runat="server" Text='<%#Eval("Attendance") %>'></asp:Label></td>
     <td><asp:Label runat="server" Text='<%#Eval("Needed") %>'></asp:Label></td>
    </tr> 
</ItemTemplate>
<FooterTemplate></table></FooterTemplate>
</asp:Repeater>
    </div></div>
<div class="row" style="padding-top:10px"><div class="col-md-6 col-md-offset-2">
        <asp:HyperLink ID="HyperLink1" runat="server"   CssClass="btn btn-info"  Text="Return to Calendar"  NavigateUrl="SubstituteCalendar.aspx"/>
  </div></div>
</asp:Content>
