<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CalendarList.aspx.cs" Inherits="MBAV.CalendarList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="NQN.Bus.SubstitutesBusiness" SelectMethod="SelectAllRequests">
        <SelectParameters>
            <asp:SessionParameter SessionField="GuideID" Name="SubstituteID" Type="Int32" DefaultValue="0" />
              <asp:SessionParameter SessionField="RoleID" Type="Int32"  Name="RoleID" DefaultValue="0" />
        </SelectParameters>
    </asp:ObjectDataSource>
 
    
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
<asp:Label ID="NameLabel" runat="server" Font-Bold="true" Font-Size="X-Large"></asp:Label> - <asp:Label ID="RoleLabel" runat="server" Font-Bold="true" Font-Size="X-Large" ></asp:Label>
 </div>
</div>
<div class="clear"></div>
  
  
 
<div  style="margin-left:10px;margin-right:auto;  ">
<div  class="row ">
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" AlternatingRowStyle-BackColor="Beige"  
          DataKeyNames="GuideSubstituteID" CellPadding="0"  HeaderStyle-Font-Bold="true">
<AlternatingRowStyle BackColor="Beige"></AlternatingRowStyle>
    <HeaderStyle Font-Bold="True"></HeaderStyle>
        <Columns>
            <asp:HyperLinkField HeaderText="Date (Click to Edit)" DataNavigateUrlFormatString="~/SubRequest.aspx?dt={0}" Text="Select" DataNavigateUrlFields="dtString"  DataTextField="SubDate" DataTextFormatString="{0:D}" />
           
             <asp:BoundField DataField="ShortName" HeaderText="Shift" SortExpression="ShortName" />
           
            <asp:BoundField DataField="PartnerName" HeaderText="Working with" ReadOnly="True"   />
   <asp:TemplateField  HeaderText="Current Sub">
     <ItemTemplate>
        <asp:Label  runat="server"  ForeColor="#330088"    Font-Bold="true"  
             Enabled='<%#Eval("HasSub") %>' Text='<%#Eval("SubName") %>' /> 
    </ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Sign up">
 <ItemTemplate>
  <asp:CheckBox ID="SubCheckBox" runat="server" Enabled = '<%#Eval("CanSub") %>' Visible='<%#Eval("CanSub") %>' Checked = '<%#Bind("SubOffer") %>' />
 </ItemTemplate>
</asp:TemplateField>
</Columns>
    </asp:GridView>
</div>
 
<hr />
<p style="text-align:center;font-size:12pt; font-weight:bold">
    Click on "Submit" to record changes. </p>
     
    <p style="text-align:center; font-size:12pt; font-weight:bold">
    Click on "Return to Calendar" to return you to the calendar without
    changing anything.</p>
<div class="row">
  <div class="col-md-2 col-xs-1"></div>
   <div class="col-md-3  " style="text-align:center; padding-top:2px; padding-bottom:2px ">
    <asp:Button ID="SubmitButton" runat="server" Text="Submit" OnCommand="DoSubmit" CssClass="btn btn-success" CommandArgument="1"/>
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
