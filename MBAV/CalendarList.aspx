<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CalendarList.aspx.cs" Inherits="MBAV.CalendarList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="NQN.Bus.SubstitutesBusiness" SelectMethod="SelectInfoRequests">   
    </asp:ObjectDataSource>
 
  <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" TypeName="NQN.Bus.SubstitutesBusiness" SelectMethod="SelectInfoCommitments">   
      <SelectParameters>
          <asp:SessionParameter SessionField="GuideID" Name="GuideID" DefaultValue="0" Type="Int32" />
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
<asp:Label ID="NameLabel" runat="server" Font-Bold="true" Font-Size="X-Large"></asp:Label> - <asp:Label ID="RoleLabel" runat="server" Font-Bold="true" Font-Size="X-Large" ></asp:Label>
 </div>
</div>
<div class="clear"></div>
  
 <div  style="margin-left:10px;margin-right:auto; background-color:#c2d9e5 ">
    <div class="row">
     <div class="col-md-12">
         <h3>To request a substitute for a regular shift, click the "Main Calendar Page" Link.</h3>
       </div>
     </div>
     <div class="row">
          <div class="col-md-offset4 col-md-2 col-xs-4" style="text-align:center; padding-top:2px; padding-bottom:2px  ">
   <asp:HyperLink ID="HyperLink3" runat="server"  ForeColor="Black" Font-Underline="false"  Font-Size="11"
       CssClass="btn btn-info" Text="Main Calendar Page"  NavigateUrl="SubstituteCalendar.aspx"/>
    </div>
  </div>
    <div class="row">
     <div class="col-md-12">
         <h3>Current Substitute Commitments -- Select "Remove Me" to remove a commitment, then click "Submit" at the bottom of the page.</h3>
     </div>
 </div>
<div  class="row ">
    <div class="col-xs-12">
<asp:GridView ID="GridView0" runat="server"   AutoGenerateColumns="False" DataSourceID="ObjectDataSource2" AlternatingRowStyle-BackColor="Beige"  
          DataKeyNames="GuideSubstituteID" CellPadding="0"  HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="#71D4F1">
<AlternatingRowStyle BackColor="Beige"></AlternatingRowStyle>
    <HeaderStyle Font-Bold="True"></HeaderStyle>
        <Columns>
              <asp:BoundField DataField="SubDate" HeaderText="Date" DataFormatString="{0:d}" />
            <asp:BoundField DataField="ShortName" HeaderText="Shift" />
<asp:TemplateField HeaderText="Remove Me" ItemStyle-HorizontalAlign="Center">
 <ItemTemplate>
  <asp:CheckBox ID="RemoveCheckBox" runat="server"   />
 </ItemTemplate>
</asp:TemplateField>
 
</Columns>
    <EmptyDataTemplate>No Outstanding Commitments</EmptyDataTemplate>
    </asp:GridView>
</div> 
 </div>
 <div class="row">
     <div class="col-md-12">
         <h3>Info Center opportunities - select only one, then click on "Submit" at the bottom of the page.</h3>
     </div>
 </div>
<div  class="row ">
    <div class="col-xs-12">
<asp:GridView ID="GridView1" runat="server"    AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" AlternatingRowStyle-BackColor="Beige"  
          DataKeyNames="ShiftID, SubDate, GuideID" CellPadding="0"  HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="#71D4F1">
<AlternatingRowStyle BackColor="Beige"></AlternatingRowStyle>
    <HeaderStyle Font-Bold="True"></HeaderStyle>
        <Columns>
              <asp:BoundField DataField="SubDate" HeaderText="Date" DataFormatString="{0:d}" />
             <asp:BoundField DataField="ShortName" HeaderText="Shift"   /> 
            <asp:BoundField DataField="Times" HeaderText="Time" />
            <asp:BoundField DataField="PartnerName" HeaderText="Working with" ReadOnly="True"   />
   
<asp:TemplateField HeaderText="Sign up" ItemStyle-HorizontalAlign="Center">
 <ItemTemplate>
  <asp:CheckBox ID="SubCheckBox" runat="server"   />
 </ItemTemplate>
</asp:TemplateField>
</Columns>
    </asp:GridView>
</div>
 </div>
<hr />
 
 <div class="row">
   <div class="col-xs-6" style="text-align:center; padding-top:2px; padding-bottom:2px ">
   <asp:Button ID="ResetButton" OnClick="DoReset"  Text="Clear Selections" runat="server" CssClass="btn btn-danger"/> 
   </div>
   <div class="col-xs-3 col-xs-offset-1  " style="text-align:center; padding-top:2px; padding-bottom:2px ">
    <asp:Button ID="SubmitButton" runat="server" Text="Submit" OnCommand="DoSubmit" CssClass="btn btn-success" CommandArgument="1"/>
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
            page.</b>  </p>
            <asp:Button ID="ReturnButton" runat="server"  CssClass="btn btn-info"  Text="Return to List" OnClick="ToView1"/>
</div>
 
 </asp:View>
</asp:MultiView>
</asp:Content>
