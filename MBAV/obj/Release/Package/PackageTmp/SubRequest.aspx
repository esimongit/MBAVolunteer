<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SubRequest.aspx.cs" Inherits="MBAV.SubRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="NQN.Bus.GuidesBusiness" SelectMethod="SelectForDate">
<SelectParameters>  
  <asp:QueryStringParameter QueryStringField="dt" Name="dt" Type="DateTime"   />
</SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSource2" runat="server" TypeName="NQN.Bus.GuidesBusiness" SelectMethod="SelectRequestsForDate">
<SelectParameters>  
  <asp:QueryStringParameter QueryStringField="dt" Name="dt" Type="DateTime"   />
</SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSource3" runat="server" TypeName="NQN.Bus.GuidesBusiness" SelectMethod="SelectSubsForDate">
<SelectParameters>  
  <asp:QueryStringParameter QueryStringField="dt" Name="dt" Type="DateTime"   />
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
<asp:MultiView ID="MultiView1" runat="server">
<asp:View ID="View1" runat="server">

<div class="row">
 <div class="col-md-2"> 
 <img alt="Orca" src="/Images/orca.jpg"/></div>
<div class="col-md-10" style=" text-align:center; "><h1 style="color:Black">Monterey Bay Aquarium<br />
Volunteer Guide Substitute Request/Sign-up</h1>
</div>
</div>

 
 <div class="row">
 <div class="col-md-2"> 
  </div>
<div class="col-md-10" style=" text-align:center; ">
<asp:Label ID="DateLabel" Font-Bold="true" Font-Size="X-Large" runat="server" ></asp:Label><br />
<asp:Label ID="NameLabel" runat="server" Font-Bold="true" Font-Size="X-Large"></asp:Label> - <asp:Label ID="RoleLabel" runat="server" Font-Bold="true" Font-Size="X-Large" ></asp:Label>
 </div>
</div>
<div class="clear"></div>
 
 <div class="row td1" id="NeedSubCell" runat="server">
  <div class="col-md-6 "> 
    If you need a substitute, check here:
    <asp:CheckBox ID="NeedSubCheckBox" runat="server" />
  </div>
 
 
 <div class="col-md-2  "> 
  Shift&nbsp;
   <asp:Label ID="ShiftLabel" Font-Bold="true"   runat="server" ></asp:Label> 
   </div>
   </div>
    <div class="row td4" id="UndoSubCell" runat="server"> 
 <div class="col-md-6  "> 
   I no longer need a substitute for shift <asp:Label ID="SeqLabel" runat="server"></asp:Label>, check here:  
   <asp:CheckBox ID="NoNeedCheckbox" runat="server" /> 
   </div>
   </div>
  <div class="row td2" id="HaveSubCell" runat="server"> 
 <div class="col-md-6  "> 
    If you will be absent and have arranged a substitute,<br/>enter the
    substitute's Volunteer ID number in this field:&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="SubTextBox" runat="server" Width="80"></asp:TextBox>
  </div>
  <div class="col-md-2 td2">Shift&nbsp; 
   <asp:Label ID="ShiftLabel2" Font-Bold="true"   runat="server" ></asp:Label> 
   </div>
  </div>
 
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
  <div class="col-md-6  "> 
    If you will be dropping in, check here: &nbsp;
    <asp:CheckBox ID="NewDropCheckBox" runat="server" />   and select the correct shift.
    </div>
    <div class="col-md-3">
    <div style="float:left"> Shift
    </div>
    <div style="float:left; padding-left:5px">
    
    <asp:RadioButtonList ID="ShiftSelect" runat="server" DataSourceID="ShiftsDataSource" RepeatDirection="Horizontal" DataTextField="Sequence"  Font-Bold="true" DataValueField="ShiftID">
    </asp:RadioButtonList>
    </div>
    
 </div>
 </div>
 
 
 <style type="text/css">
 td, th
 {
     padding-left:2px;
     padding-right:2px;
 }
 </style>
 
<div  style="margin-left:10px;margin-right:auto;  ">
<div  class="row hidden-xs">
<asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AlternatingRowStyle-BackColor="Beige"  
        AutoGenerateColumns="false"  DataKeyNames="GuideSubstituteID" CellPadding="0"  HeaderStyle-Font-Bold="true"
 >
<Columns>
<asp:BoundField DataField="Sequence" HeaderText="Shift" />
<asp:BoundField DataField="Role" HeaderText="Role" />
<asp:BoundField DataField="VolID" HeaderText="ID" /> 
<asp:HyperLinkField DataNavigateUrlFields="GuideID" ControlStyle-ForeColor="#330088"  ControlStyle-Font-Underline="false" ControlStyle-Font-Bold="true" DataNavigateUrlFormatString="ContactInfo.aspx?ID={0}"     Target="_blank" 
  HeaderText="Requestor (Click for Contact Info)" DataTextField="GuideName" /> 
<asp:TemplateField HeaderText="I can sub">
 <ItemTemplate>
  <asp:CheckBox ID="SubCheckBox" runat="server" Enabled = '<%#Eval("NoSub") %>' Visible='<%#Eval("NoSub") %>' Checked = '<%#Bind("SubOffer") %>' />
 </ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="SubName" HeaderText="Substitute" />
<asp:BoundField DataField="Sub" HeaderText="Sub ID" />
</Columns>
  
</asp:GridView>
</div>
<div  class="row hidden-md hidden-lg hidden-sm">
<asp:GridView ID="GridView2" runat="server" DataSourceID="ObjectDataSource2" Caption="Current Requests" AlternatingRowStyle-BackColor="Beige"  
        AutoGenerateColumns="false"  DataKeyNames="GuideSubstituteID" CellPadding="0"  HeaderStyle-Font-Bold="true" >
<Columns>
<asp:BoundField DataField="Sequence" HeaderText="Shift" />
<asp:BoundField DataField="Role" HeaderText="Role" /> 
<asp:HyperLinkField DataNavigateUrlFields="GuideID" ControlStyle-ForeColor="#330088"  ControlStyle-Font-Bold="true" DataNavigateUrlFormatString="ContactInfo.aspx?ID={0}"     Target="_blank" 
  HeaderText="Requestor (Click for Contact Info)" DataTextField="GuideName" /> 
<asp:TemplateField HeaderText="I can sub">
 <ItemTemplate>
  <asp:CheckBox ID="SubCheckBox" runat="server" Enabled = '<%#Eval("NoSub") %>' Visible='<%#Eval("NoSub") %>' Checked = '<%#Bind("SubOffer") %>' />
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
<asp:BoundField DataField="SubName" HeaderText="Substitute" />
<asp:BoundField DataField="Sub" HeaderText="Sub ID" />
</Columns>
<EmptyDataTemplate>No Subs</EmptyDataTemplate>  
</asp:GridView>
</div>
<hr />
<p style="text-align:center;font-size:12pt">
    Click on "Submit" to record changes. </p>
    <p style="text-align:center;font-size:12pt">
    If you check multiple requests, only one will be processed. </p>
    <p style="text-align:center; font-size:12pt">
    Click on "Return to Calendar" to return you to the calendar without
    changing anything.</p>
<div class="row">
  <div class="col-md-2 col-xs-2"></div>
   <div class="col-md-3  hidden-xs" style="text-align:center; padding-top:2px; padding-bottom:2px ">
    <asp:Button ID="SubmitButton" runat="server" Text="Submit" OnCommand="DoSubmit" BackColor="ButtonFace" CommandArgument="1"/>
  </div>
  <div class="col-xs-3  hidden-md hidden-lg hidden-sm" style="text-align:center; padding-top:2px; padding-bottom:2px ">
    <asp:Button ID="Button1" runat="server" Text="Submit" OnCommand="DoSubmit" BackColor="ButtonFace" CommandArgument= "2"/>
  </div>
   <div class="col-md-3 col-xs-3" style="text-align:center; padding-top:2px; padding-bottom:2px ">
    <input type="reset"/> 
   </div>
    <div class="col-md-2 col-xs-3" style="text-align:center; padding-top:2px; padding-bottom:2px  ">
   <asp:HyperLink ID="HyperLink3" runat="server"  ForeColor="Black" Font-Underline="false"  Font-Size="11" BorderColor="Gray"
           BorderStyle="Solid" BorderWidth="1" BackColor="ButtonFace"  Text="Return to Calendar"  NavigateUrl="SubstituteCalendar.aspx"/>
  </div>
  <div class="col-md-2"></div>
  </div>  
 <div class="clear"></div>
 <div>
    <asp:CheckBox ID="TestBox" runat="server" />
    <span style="font-size:larger; color:#AA3333">Practice:</span>
    If this box is checked no email messages will be sent, but changes <b>will</b>
    be made to the database. Please undo any changes that you make in practice.
    <hr  />
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
            <span style="color:black;font-size:larger">Please remember that you are <b>always responsible</b> for finding a substitute. </span>
            <p> 
             This on-line request system is just for your convenience. <b><i>If you do not get a substitute,
              or five responses to your request within a week</i></b> of the date you will be absent,
               it is recommended that you <b><i>get on the phone</i></b> and call volunteers to find a substitute the old way. 
               </p>
               <p>
           While you are here, please
            check the substitute requests for any that you can fill.</p>
</div>
<div class="col-md-6">  
      <asp:Label ID="PracticeLabel" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="Large"></asp:Label><br />
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
