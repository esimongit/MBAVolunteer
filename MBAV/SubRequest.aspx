<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SubRequest.aspx.cs" Inherits="MBAV.SubRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="NQN.Bus.GuidesBusiness" SelectMethod="SelectForDate">
<SelectParameters>  
  <asp:QueryStringParameter QueryStringField="dt" Name="dt" Type="DateTime"   />
     <asp:SessionParameter SessionField="GuideID" Type="Int32"  Name="GuideID" DefaultValue="0" />
     <asp:SessionParameter SessionField="RoleID" Type="Int32"  Name="RoleID" DefaultValue="0" />
</SelectParameters>
</asp:ObjectDataSource>
 
    <asp:ObjectDataSource ID="ObjectDataSource2s" runat="server" TypeName="NQN.Bus.GuidesBusiness" SelectMethod="SelectOpenRequestsForDate">
<SelectParameters>  
  <asp:QueryStringParameter QueryStringField="dt" Name="dt" Type="DateTime"   />
     <asp:SessionParameter SessionField="GuideID" Type="Int32"  Name="GuideID" DefaultValue="0" />
     <asp:SessionParameter SessionField="RoleID" Type="Int32"  Name="RoleID" DefaultValue="0" />
</SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSource3" runat="server" TypeName="NQN.Bus.GuidesBusiness" SelectMethod="SelectSubsForDate">
<SelectParameters>  
  <asp:QueryStringParameter QueryStringField="dt" Name="dt" Type="DateTime"   />
     <asp:SessionParameter SessionField="RoleID" Type="Int32"  Name="RoleID" DefaultValue="0" />
</SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSource4" runat="server" TypeName="NQN.Bus.SubstitutesBusiness" SelectMethod="SelectShiftsForGuideAndDate">
<SelectParameters>  
    <asp:SessionParameter SessionField="GuideID" Type="Int32"  Name="GuideID" DefaultValue="0" />
  <asp:QueryStringParameter QueryStringField="dt" Name="dt" Type="DateTime"   />
</SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="ShiftsDataSource" runat="server" TypeName="NQN.DB.ShiftsDM" SelectMethod="ShiftsForDateAndGuide">
  <SelectParameters>
    <asp:SessionParameter SessionField="GuideID" Type="Int32"  Name="GuideID" DefaultValue="0" />
    <asp:QueryStringParameter QueryStringField="dt" Type="DateTime" Name="dt" />
  </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="RegularShiftsDataSource" runat="server" TypeName="NQN.DB.ShiftsDM" SelectMethod="RegularShiftsForGuideAndDate">
    <SelectParameters>
    <asp:SessionParameter SessionField="GuideID" Type="Int32"  Name="GuideID" DefaultValue="0" />
    <asp:QueryStringParameter QueryStringField="dt" Type="DateTime" Name="dt" />
  </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="NeedSubDataSource" runat="server" TypeName="NQN.DB.ShiftsDM" SelectMethod="NeedSubShiftsForGuideAndDate">
    <SelectParameters>
    <asp:SessionParameter SessionField="GuideID" Type="Int32"  Name="GuideID" DefaultValue="0" />
    <asp:QueryStringParameter QueryStringField="dt" Type="DateTime" Name="dt" />
  </SelectParameters>
</asp:ObjectDataSource>
 <asp:ObjectDataSource ID="AbsentShiftsDataSource" runat="server" TypeName="NQN.DB.ShiftsDM" SelectMethod="AbsentShiftsForGuideAndDate">
    <SelectParameters>
    <asp:SessionParameter SessionField="GuideID" Type="Int32"  Name="GuideID" DefaultValue="0" />
    <asp:QueryStringParameter QueryStringField="dt" Type="DateTime" Name="dt" />
  </SelectParameters>
</asp:ObjectDataSource>
<asp:MultiView ID="MultiView1" runat="server">
<asp:View ID="View1" runat="server">
    <style type="text/css">
 td, th
 {
     padding: 4px 4px 4px 4px
     
 }
 </style>
 

 
 <div class="row">
 <div class="col-md-2"> 
  </div>
<div class="col-md-10" style=" text-align:center; ">
<asp:Label ID="DateLabel" Font-Bold="true" Font-Size="X-Large" runat="server" ></asp:Label><br />
<asp:Label ID="NameLabel" runat="server" Font-Bold="true" Font-Size="X-Large"></asp:Label> - <asp:Label ID="RoleLabel" runat="server" Font-Bold="true" Font-Size="X-Large" ></asp:Label>
 </div>
</div>
<div class="clear"></div>
 <asp:Repeater ID="ShiftNeedRepeater" runat="server" DataSourceID="RegularShiftsDataSource" >
     <ItemTemplate>
         <asp:HiddenField ID="ShiftIDHidden" runat="server" Value='<%#Eval("ShiftID") %>' />
         <div class="row td1"   runat="server">
             <div class="col-md-5 ">
                 If you need a substitute for shift <asp:Label ID="SequenceLabel1" runat="server" Text='<%#Eval("Sequence") %>'></asp:Label>, 
                 check here:
                <asp:CheckBox ID="NeedSubCheckBox" runat="server" />
             </div>

             <div class="col-md-7  ">
                  If you arranged for a substitute, enter the Substitute ID: <asp:TextBox ID="SubTextBox" runat="server" Width="80"></asp:TextBox>
             </div>
         </div>
     </ItemTemplate>
</asp:Repeater>
 <asp:Repeater ID="AbsentShiftsRepeater" runat="server" DataSourceID="AbsentShiftsDataSource" >
     <ItemTemplate>
        <asp:HiddenField ID="ShiftIDHidden" runat="server" Value='<%#Eval("ShiftID") %>' />
         <div class="row td4"  runat="server">
             <div class="col-md-6  ">
                 I no longer need a substitute for shift
                 <asp:Label ID="SeqLabel" runat="server"  Text='<%#Eval("Sequence") %>'></asp:Label>, check here:  
        <asp:CheckBox ID="NoNeedCheckbox" runat="server" />
             </div>
         </div>
        
      </ItemTemplate>
</asp:Repeater>
 <asp:Repeater ID="NeedSubRepeater" runat="server" DataSourceID="NeedSubDataSource" >
     <ItemTemplate>
         <asp:HiddenField ID="ShiftIDHidden" runat="server" Value='<%#Eval("ShiftID") %>' />
         <div class="row td2"  runat="server">
             <div class="col-md-6  ">
                 If you have arranged a substitute for shift  <asp:Label ID="Label1" runat="server"  Text='<%#Eval("Sequence") %>'></asp:Label> ,<br />
                 enter the  substitute's Volunteer ID number in this field:&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="SubTextBox" runat="server" Width="80"></asp:TextBox>
             </div>
             
         </div>
     </ItemTemplate>
</asp:Repeater>

 <div id="CurrentSubCell"  class="row td4" runat="server">
 <div class="col-md-6  "> 
If you can no longer substitute for shift <asp:Label ID="SequenceLabel" runat="server"></asp:Label>, check here:&nbsp;&nbsp; 
<asp:CheckBox ID="NoSubCheckBox" runat="server" /> 
</div> <div class="col-md-6">
<asp:Repeater ID="SubRepeater" runat="server" DataSourceID="ObjectDataSource4">
 <ItemTemplate>
 <cc2:CalendarLink runat="server" ID="CalendarLink1"
            CalendarType='<%#DataBinder.Eval(Container.DataItem,"CalendarType") %>' 
            HostName='<%#GetHostName() %>'
             Title='Volunteer Substitute Commitment'
             UseTimes='true'
            Description='<%#DataBinder.Eval(Container.DataItem, "Sequence", "Shift {0}") %>'
           Notes =''
            Date='<%#DataBinder.Eval(Container.DataItem,"SubDate") %>'
             StartTime= '<%#DataBinder.Eval(Container.DataItem,"ShiftStart") %>'
             Location='Monterey Bay Aquarium'
              EndTime= '<%#DataBinder.Eval(Container.DataItem,"ShiftEnd") %>'></cc2:CalendarLink>
   </ItemTemplate>
   </asp:Repeater> 
     </div>
</div>
 <div class="row td3" id="DropinCell" runat="server">
  <div class="col-md-6" style="padding-top:5px"> 
     If you will be dropping in, check here:  &nbsp;
    <asp:CheckBox ID="NewDropCheckBox" runat="server" />   and select the correct shift.
    </div>
    <div class="col-md-3">
    <div style="float:left; padding-top:5px"> Shift
    </div>
    <div style="float:left; padding-left:5px">
    
    <asp:RadioButtonList ID="ShiftSelect" runat="server" DataSourceID="ShiftsDataSource" RepeatDirection="Horizontal" DataTextField="Sequence"  Font-Bold="true" DataValueField="ShiftID">
    </asp:RadioButtonList>
    </div>
    
 </div>
 </div>
 
 
 
 
<div  style="margin-left:10px;margin-right:auto;  ">
<div  class="row hidden-xs">
<asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AlternatingRowStyle-BackColor="Beige"  
        AutoGenerateColumns="false"  DataKeyNames="GuideSubstituteID" CellPadding="0"  HeaderStyle-Font-Bold="true"
 >
<Columns>
<asp:BoundField DataField="Sequence" HeaderText="Shift" />
<asp:BoundField DataField="Role" HeaderText="Role" />
<asp:BoundField DataField="VolID" HeaderText="ID" /> 
<asp:TemplateField  HeaderText="Requestor (Click for Contact Info)">
     <ItemTemplate>
        <asp:HyperLink  runat="server"  ForeColor="#330088"   Font-Underline="false" Font-Bold="true" NavigateUrl='<%#Eval("GuideID","ContactInfo.aspx?ID={0}") %>'
             Target="_blank"  Enabled='<%#Eval("HasName") %>' Text='<%#Eval("GuideName") %>' /> 
    </ItemTemplate>
</asp:TemplateField>
 
<asp:TemplateField HeaderText="I can sub"  ItemStyle-HorizontalAlign="Center">
 <ItemTemplate>
 
  <asp:Label ID="SubLabel" runat="server" Visible ='<%#Eval("IsSub") %>' Text="Already subbing"></asp:Label>
   <asp:CheckBox ID="SubCheckBox" runat="server"  Enabled = '<%#Eval("CanSub") %>' Visible='<%#Eval("NoSub") %>' Checked = '<%#Bind("SubOffer") %>' />
 </ItemTemplate>
</asp:TemplateField>
 
<asp:TemplateField HeaderText="Substitute">
    <ItemTemplate>
        <asp:Label  runat="server"   ForeColor='<%#Eval("SubColor") %>'  Text='<%#Eval("SubName") %>' Font-Underline="false"  Font-Bold="true"    /> 
  </ItemTemplate>
</asp:TemplateField>
 
<asp:BoundField DataField="Sub" HeaderText="Sub ID" />
</Columns>
  
</asp:GridView>
</div>
<div  class="row hidden-md hidden-lg hidden-sm">
    <div class="col-xs-12  "  >  
<asp:GridView ID="GridView2" runat="server" DataSourceID="ObjectDataSource2s" Caption="Current Requests" AlternatingRowStyle-BackColor="Beige"  
        AutoGenerateColumns="false"  DataKeyNames="GuideSubstituteID" CellPadding="0"  HeaderStyle-Font-Bold="true" >
<Columns>
<asp:BoundField DataField="Sequence" HeaderText="Shift" />
<asp:BoundField DataField="Role" HeaderText="Role" /> 
<asp:TemplateField  HeaderText="Requestor (Click for Contact Info)">
     <ItemTemplate>
        <asp:HyperLink  runat="server"  ForeColor="#330088"   Font-Underline="false" Font-Bold="true" NavigateUrl='<%#Eval("GuideID","ContactInfo.aspx?ID={0}") %>'
             Target="_blank"  Enabled='<%#Eval("HasName") %>' Text='<%#Eval("GuideName") %>' /> 
    </ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="I can sub">
 <ItemTemplate>
  <asp:CheckBox ID="SubCheckBox" runat="server" Enabled = '<%#Eval("CanSub") %>' Visible='<%#Eval("CanSub") %>' Checked = '<%#Bind("SubOffer") %>' />
 </ItemTemplate>
</asp:TemplateField>
</Columns>
<EmptyDataTemplate>No Pending Requests</EmptyDataTemplate>  
</asp:GridView>
<br />
<asp:GridView ID="GridView3" runat="server" DataSourceID="ObjectDataSource3" Caption="Current Subs" AlternatingRowStyle-BackColor="Beige"  
        AutoGenerateColumns="false"  DataKeyNames="GuideSubstituteID" CellPadding="0"  HeaderStyle-Font-Bold="true" >
<Columns>
<asp:BoundField DataField="Sequence" HeaderText="Shift" />
<asp:BoundField DataField="Role" HeaderText="Role" /> 
<asp:HyperLinkField DataNavigateUrlFields="GuideID" ControlStyle-ForeColor="#330088"  ControlStyle-Font-Bold="true" DataNavigateUrlFormatString="ContactInfo.aspx?ID={0}"     Target="_blank" 
  HeaderText="Requestor (Click for Contact Info)" DataTextField="GuideName" /> 
<asp:TemplateField HeaderText="Substitute">
    <ItemTemplate>
        <asp:Label  runat="server"   ForeColor='<%#Eval("SubColor") %>'  Text='<%#Eval("SubName") %>' Font-Underline="false"  Font-Bold="true"    /> 
  </ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="Sub" HeaderText="Sub ID" />
</Columns>
<EmptyDataTemplate>No Subs</EmptyDataTemplate>  
</asp:GridView>
</div>
    </div>
<hr />
<p style="text-align:center;font-size:12pt; font-weight:bold">
    Click on "Submit" to record changes. </p>
    <p style="text-align:center;font-size:14pt; font-weight:bold; color:red">
    If you check multiple requests, only one will be processed. </p>
    <p style="text-align:center; font-size:12pt; font-weight:bold">
    Click on "Return to Calendar" to return you to the calendar without
    changing anything.</p>
<div class="row">
  <div class="col-md-2 col-xs-1"></div>
   <div class="col-md-3  hidden-xs" style="text-align:center; padding-top:2px; padding-bottom:2px ">
    <asp:Button ID="SubmitButton" runat="server" Text="Submit" OnCommand="DoSubmit" CssClass="btn btn-success" CommandArgument="1"/>
  </div>
  <div class="col-xs-3    hidden-md hidden-lg hidden-sm" style="text-align:center; padding-top:2px; padding-bottom:2px ">
    <asp:Button ID="Button1" runat="server" Text="Submit" OnCommand="DoSubmit" CssClass="btn btn-success" CommandArgument= "2"/>
  </div>
   <div class="col-md-3 col-xs-3" style="text-align:center; padding-top:2px; padding-bottom:2px ">
   <asp:Button ID="ResetButton" OnClick="DoReset"  Text="Reset" runat="server" CssClass="btn btn-danger"/> 
   </div>
    <div class="col-md-2 col-xs-4" style="text-align:center; padding-top:2px; padding-bottom:2px  ">
   <asp:HyperLink ID="HyperLink3" runat="server"  ForeColor="Black" Font-Underline="false"  Font-Size="11"
       CssClass="btn btn-info" Text="Return to Calendar"  NavigateUrl="SubstituteCalendar.aspx"/>
  </div>
    
  
  
</div>
    </div>
</asp:View>
 <asp:View ID="View2" runat="server">
 <div class="row"> 
<h1 style="color:black; text-align:center;">Thank you for using the 
Volunteer Substitute System</h1>
 
</div>
<div class="row">
<div class="col-md-6">
            
            <asp:Label ID="InfoLabel" runat="server" Visible ="false"></asp:Label> 
            
           
    <cc2:CalendarLink runat="server" ID="ConfirmLink"       
             Title='Volunteer Substitute Commitment'
             UseTimes='true' Notes =''  Location='Monterey Bay Aquarium'></cc2:CalendarLink>
</div>
<div class="col-md-6">  
     
      <asp:Label ID="MsgLabel" runat="server" ></asp:Label>
 
          <p style="color:green">Recipients:</p>
            <asp:Repeater ID="RecipientsRepeater" runat="server">
            <HeaderTemplate><ul></HeaderTemplate>
            <ItemTemplate><li><asp:Label ID="RecipientLabel" runat="server" Text ='<%#Eval("GuideName") %>'></asp:Label></li></ItemTemplate>
            <FooterTemplate></ul></FooterTemplate>
            </asp:Repeater>
    </div> 
 </div>
 <div class="row">
   <p><b>NOTE: Please DO NOT refresh this
            page.</b> Every time you do, it sends off a copy of the
            message to everyone.</p>
            <asp:HyperLink ID="HyperLink1" runat="server"  CssClass="btn btn-info"  Text="Return to Calendar"  NavigateUrl="SubstituteCalendar.aspx"/>
</div>
 
 </asp:View>
</asp:MultiView>
</asp:Content>
