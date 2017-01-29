<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="MBAV.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  
 <asp:ObjectDataSource ID="ObjectDataSource1" runat='server' TypeName="NQN.DB.GuidesDM" SelectMethod="FetchGuide"
  UpdateMethod="Update" OnUpdated ="OnUpdated" >
 <SelectParameters>
   <asp:SessionParameter SessionField="GuideID" Name="GuideID" Type="Int32" DefaultValue="0" />
 </SelectParameters>
 </asp:ObjectDataSource>
 <asp:ObjectDataSource ID="ShiftsDataSource" runat="server" TypeName="NQN.DB.ShiftsDM" SelectMethod="FetchWithSubOffers">
  <SelectParameters>
   <asp:SessionParameter SessionField="GuideID" Name="GuideID" Type="Int32" DefaultValue="0" />
 </SelectParameters>
</asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource2" runat='server' TypeName="NQN.Bus.SubstitutesBusiness" SelectMethod="FutureDatesForShift"
  >
 <SelectParameters>
   <asp:SessionParameter SessionField="GuideID" Name="GuideID" Type="Int32" DefaultValue="0" />
   <asp:ControlParameter ControlID="ShiftSelect" Name="ShiftID" Type="Int32" DefaultValue="0" />
       <asp:ControlParameter ControlID="RoleSelect" Name="RoleID" Type="Int32" DefaultValue="0" />
 </SelectParameters>
 </asp:ObjectDataSource>
 <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" TypeName="NQN.DB.ShiftsDM" SelectMethod="FetchWithSubOffers">
  <SelectParameters>
   <asp:SessionParameter SessionField="GuideID" Name="GuideID" Type="Int32" DefaultValue="0" />
 </SelectParameters>
</asp:ObjectDataSource>
    <asp:ObjectDataSource ID="SubRequestsDataSource" runat="server" TypeName="NQN.DB.GuideSubstituteDM" SelectMethod="FetchAllForGuide"
 DeleteMethod="Delete">
 <SelectParameters>
            <asp:SessionParameter Name="GuideID" SessionField="GuideID" DefaultValue="0" Type="Int32" />
        </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="SubCommitmentsDataSource" runat="server" TypeName="NQN.Bus.SubstitutesBusiness" SelectMethod="SelectAllCommitments"
 DeleteMethod="DeleteCommitment">
 <SelectParameters>
            <asp:SessionParameter Name="GuideID" SessionField="GuideID" DefaultValue="0" Type="Int32" />
        </SelectParameters>
</asp:ObjectDataSource>
  
      <asp:ObjectDataSource ID="RolesDataSource" runat="server" TypeName="NQN.Bus.GuidesBusiness" SelectMethod="RolesForGuide">
  <SelectParameters>
   <asp:SessionParameter SessionField="GuideID" Name="GuideID" Type="Int32" DefaultValue="0" />
 </SelectParameters>
</asp:ObjectDataSource>
      <style type="text/css">
 td, th
 {
     padding: 4px 4px 4px 4px
     
 }
 </style>
<asp:MultiView ID="MultiView1" runat="server">
    <asp:View runat="server" ID="View1">

    <div class="row">
        <div class="col-md-7">
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
 <asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" DataSourceID="ObjectDataSource1"  DataKeyNames="GuideID">
     <EditItemTemplate>
        <div class="row">  
            <div class="col-xs-12">
        <h3>Please correct this information</h3>
        </div></div>
        <div class="row" style="padding-bottom:4px">
        <div class="col-md-4" style="color:blue; font-weight:bold">
        <asp:RequiredFieldValidator ID="FirstRequired" ControlToValidate="FirstNameTextBox" runat="server" ErrorMessage="First Name is required"
          Display="Dynamic">*</asp:RequiredFieldValidator>
         First Name: </div>
         <div class="col-md-4"> 
         <asp:TextBox ID="FirstNameTextBox" runat="server" 
             Text='<%# Bind("FirstName") %>' />
           </div>
         </div>
        <div class="row" style="padding-bottom:4px">
        <div class="col-md-4" style="color:blue; font-weight:bold">
         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="LastNameTextBox" runat="server" ErrorMessage="Last Name is required"
          Display="Dynamic">*</asp:RequiredFieldValidator>
         Last Name:</div>
         <div class="col-md-4"> 
         <asp:TextBox ID="LastNameTextBox" runat="server" 
             Text='<%# Bind("LastName") %>' />
       </div>
       </div>
        <div class="row" style="padding-bottom:4px">
        <div class="col-md-4" style="color:blue; font-weight:bold">
          <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="PhoneTextBox"
                    ValidationExpression='<%# NQN.DB.GuidesObject.ValidPhone %>' ErrorMessage="Phone must have 10 digits"
             Display="Dynamic">*</asp:RegularExpressionValidator>
         Home Phone:</div>
         <div class="col-md-4"> 
         <asp:TextBox ID="PhoneTextBox" runat="server" TextMode="Phone"  Width="120" Text='<%# Bind("Phone") %>' />
        </div>
        </div>
          <div class="row" style="padding-bottom:4px">
        <div class="col-md-4" style="color:blue; font-weight:bold">
          <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="CellPhoneTextBox"
                    ValidationExpression='<%# NQN.DB.GuidesObject.ValidPhone %>' ErrorMessage="Phone must have 10 digits"
             Display="Dynamic">*</asp:RegularExpressionValidator>
         Mobile Phone:</div>
         <div class="col-md-4"> 
         <asp:TextBox ID="CellPhoneTextBox" runat="server" TextMode="Phone" Width="120"  Text='<%# Bind("Cell") %>' />
        </div>
         
         <div class="col-md-3">
             <asp:CheckBox ID="CellPreferredCheckBox" runat="server" Text="&nbsp;Preferred" Checked='<%#Bind("CellPreferred") %>' />
         </div>
        </div>
        <div class="row" style="padding-bottom:4px">
       <div class="col-md-4" style="color:blue; font-weight:bold">
         <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="EmailTextBox"
                    ValidationExpression='<%# NQN.DB.GuidesObject.ValidEmail %>' ErrorMessage="Email is not Valid"
             Display="Dynamic">*</asp:RegularExpressionValidator>
         Email Address:</div>
         <div class="col-md-4"> 
         <asp:TextBox ID="EmailTextBox"  TextMode="Email" Width="200" runat="server" Text='<%# Bind("Email") %>' />
        </div></div>
          <div class="row" style="padding-bottom:4px">
         <div class="col-md-4" style="color:blue; font-weight:bold">
         Personal Info:</div>
              <div class="col-md-7"> 
             <div style="float:left">My contact information should be visible to guides</div>
                  <div style="float:left; padding-left:4px"> 
              <asp:RadioButtonList ID="MaskRadio" RepeatDirection="Horizontal" runat="server"  RepeatColumns="2"   SelectedValue='<%#Bind("MaskPersonalInfo") %>'
               >
             <asp:ListItem Value="True" Text="&nbsp;No" ></asp:ListItem>
              <asp:ListItem Value="False" Text="&nbsp;Yes" Selected="True"></asp:ListItem>
           </asp:RadioButtonList></div>
          <div class="clear"></div>
         
        </div></div>
           <div class="row" style="padding-bottom:4px">
          <div class="col-md-4" style="color:blue; font-weight:bold">
         Email Notices:</div>
              <div class="col-md-7"> 
                  <asp:CheckBox ID="NotifyCheckBox" runat="server" Checked='<%#Bind("NotifySubRequests") %>' Text="&nbsp;&nbsp;Email me Sub requests." />
            </div>
        </div>
         <div class="row" style="padding-bottom:4px">
        <div class="col-md-4" style="color:blue; font-weight:bold">
         Personal Calendar:</div>
         <div class="col-md-4"> 
         <asp:DropDownList ID="CalendarSelect" runat="server" SelectedValue='<%#Bind("CalendarType") %>'>
         <asp:ListItem Value="" Text="(None)"></asp:ListItem>
           <asp:ListItem Value="Google" Text="Google"></asp:ListItem>
             <asp:ListItem Value="Yahoo" Text="Yahoo"></asp:ListItem>
         </asp:DropDownList>
        </div></div>
        <div class="row" style="padding-bottom:4px">
        <div class="col-md-4" style="color:blue; font-weight:bold">      
         ID: </div>
        <div class="col-md-4"> 
         <asp:Label ID="VolIDTextBox" runat="server"   Font-Bold="true" Text='<%# Eval("VolID") %>' />
       </div>
        </div> 
        <div class="row" style="padding-bottom:4px">
        <div class="col-md-4" style="color:blue; font-weight:bold">    
         Shift:  </div>
        <div class="col-md-8">        
         <asp:Label ID="Label1" runat="server"  Font-Bold="true" Text='<%# Eval("ShiftName") %>' />&nbsp;
            <asp:Label ID="IrregLabel" runat="server" Font-Bold="true"  Font-Italic="true" Text="Irregular" Visible='<%#Eval("IrregularSchedule") %>'></asp:Label>
       </div>
       </div>
          <div class="row" style="padding-bottom:4px">
        <div class="col-md-4" style="color:blue; font-weight:bold"> 
         Role:  </div>
        <div class="col-md-8"> 
       
         <asp:Label ID="Label2" runat="server"  Font-Bold="true" Text='<%# Eval("RoleName") %>' />
       </div>
       </div>
          <div class="row" style="padding-bottom:4px">
        <div class="col-md-4" style="color:blue; font-weight:bold">     
         Alternate Role:  </div>
        <div class="col-md-8"> 
       
         <asp:Label ID="Label3" runat="server"  Font-Bold="true" Text='<%# Eval("AltRoleName") %>' />
       </div>
       </div>
    <div class="row">
        <div class="col-xs-3 col-xs-offset-1">
         <asp:LinkButton ID="UpdateButton" runat="server"   CausesValidation="True"  CssClass="btn btn-success"
             CommandName="Update" Text="Update"  />
            </div>
         <div class="col-xs-3 col-xs-offset-2"><asp:LinkButton ID="UpdateCancelButton" runat="server" CssClass="btn btn-info"
             CausesValidation="False" CommandName="Cancel" Text="Return to Calendar"  OnClick="ClosePage" />
        </div>
     </EditItemTemplate>
    
 </asp:FormView>
 </div>
 <div class="col-md-5">
    <div class="row" style="padding-top:50px">
        <div class="col-xs-12">
<asp:Button ID="View2Button" runat="server" Text="Select shifts for which you can substitute" CssClass="btn btn-info"  CausesValidation="false" BackColor="#5bc0fe" OnClick="ToView2" />
        </div></div>
 <div class="row" style="padding-top:20px">
     <div class="col-xs-12">
<asp:Button ID="IrregularButton" runat="server" Text="Select dates you plan to be on a shift" CssClass="btn btn-info" OnClick="ToView3"   CausesValidation="false"  BackColor="#6e8ade"/>
        </div></div>
 <div class="row" style="padding-top:20px">
     <div class="col-xs-12">
<asp:Button ID="Button2" runat="server" Text="Review your substitute commitments" CssClass="btn btn-info" OnClick="ToView4"    CausesValidation="false" BackColor="#6e8aa0"/>
        </div></div>
 
 </div>
    </div>
  
</asp:View>
     <asp:View runat="server" ID="View2">
<div class="row"><div class="col-xs-12">
<h3>In the table below,  check all the boxes for shifts on which 
you can substitute. </h3><hr/>
    </div></div>
  <div class="row">
      <div class="col-md-3">
          <asp:HyperLink runat="server" ID="OpportunitiesLink" Text="View All Requests for these Shifts" CssClass="btn btn-info"
               NavigateUrl="~/Opportunities.aspx"  Target="_blank"></asp:HyperLink>
      </div><div class="col-md-6">
         <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-info"
             CausesValidation="False" CommandName="Cancel" Text="Return to Profile"    OnClick="ToView1" />
  </div></div>
    <div class="row" style="padding-top:10px"><div class="col-md-5">
<asp:Repeater ID="Repeater1" runat="server" DataSourceID="ShiftsDataSource" >
<HeaderTemplate><table cellpadding="5"></HeaderTemplate>
<ItemTemplate><tr style='<%#Container.ItemIndex %2==0 ? "background-color:#efefef":"background-color:#ffffff" %>'> <td><asp:Label runat="server" Text='<%#Eval("ShiftName") %>'></asp:Label></td><td>
<asp:CheckBox ID="ShiftCheckBox" runat="server" Checked='<%#Eval("Selected") %>' OnCheckedChanged="CheckChanged"  AutoPostBack="true" />
<asp:HiddenField ID="ShiftIDHidden" runat="server" Value='<%#Eval("ShiftID") %>' />
</td></tr>
</ItemTemplate>
<FooterTemplate></table></FooterTemplate>
</asp:Repeater>
     </div></div>
 
<div class="row"><div class="col-md-6 col-md-offset-3">
         <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-info"
             CausesValidation="False" CommandName="Cancel" Text="Return to Profile"    OnClick="ToView1" />
  </div></div>
        </asp:View>
    <asp:View ID="View3" runat="server">

        <div class="row" style="padding-top:10px">
    <div class="col-md-4" style="font-size:large; color:purple">  First select a Shift: </div>
   <div class="col-md-4"> 
 <asp:DropDownList ID="ShiftSelect" runat="server" DataSourceID="ShiftsDataSource" DataTextField="ShiftName" DataValueField="ShiftID" AppendDataBoundItems="true"
  AutoPostBack="true">
  <asp:ListItem Value="0" Text="(Select a shift)"></asp:ListItem>
 </asp:DropDownList> 
 </div></div>
  
        <div class="row" style="padding-top:10px">
    <div class="col-md-4" style="font-size:large; color:purple">  Next select a Role: </div>
   <div class="col-md-4"> 
 <asp:DropDownList ID="RoleSelect" runat="server" DataSourceID="RolesDataSource" DataTextField="RoleName" DataValueField="RoleID" AppendDataBoundItems="true"
  AutoPostBack="true">
  <asp:ListItem Value="0" Text="(Select a role)"></asp:ListItem>
 </asp:DropDownList> 
 </div></div>
   
<div class="row" style="padding-top:10px; padding-bottom:20px; font-size:large; margin-left:5px">
    In the table below,  check all the boxes for dates  
you plan to be a guide for this shift with this role, then click "Submit".  </div>
  
<asp:Repeater ID="Repeater2" runat="server" DataSourceID="ObjectDataSource2" >
<HeaderTemplate><table cellpadding="5"></HeaderTemplate>
<ItemTemplate><tr style='<%#Container.ItemIndex %2==0 ? "background-color:#afcfdf":"background-color:#ffffff" %>'> 
<td style="padding:2px 4px 2px 2px; text-align:right"><asp:Label ID="DateLabel" runat="server" Text='<%#Eval("DropinDate",  "{0:d}") %>'></asp:Label></td><td>
<asp:CheckBox ID="DateCheckBox" runat="server" Checked='<%#Eval("Selected") %>'   />
<asp:HiddenField ID="DropinIDHidden" runat="server" Value='<%#Eval("GuideDropinID") %>' />
</td></tr>
</ItemTemplate>
<FooterTemplate></table></FooterTemplate>
</asp:Repeater>
<div class="row"><div class="col-xs-offset-2 col-xs-4">
<asp:Button ID="SubmitButton" OnClick="DoSubmit" runat="server" Text="Submit" CssClass="btn btn-success" />
</div><div class="col-xs-4">
<asp:LinkButton ID="UpdateCancelButton" runat="server" CssClass="btn btn-info"
             CausesValidation="False" CommandName="Cancel" Text="Return to Profile"    OnClick="ToView1" />
</div></div>
    </asp:View>
    <asp:View ID="View4" runat="server">
        <div class="row">
            <div class="col-md-12"> 
         <cc2:NQNGridView  ID="GridView2" runat="server"  Caption="<strong>Sub Requests</strong> (click to change)" AlternatingRowStyle-BackColor="#efefef" HeaderStyle-ForeColor="White"
                 DataSourceID="SubRequestsDataSource" AllowMultiColumnSorting="False" HeaderStyle-BackColor="#0099cc"
                 AutoGenerateColumns="False"  
                 Privilege="" AllowSorting="True" DataKeyNames="GuideSubstituteID">
             <Columns>
                 <asp:HyperLinkField DataNavigateUrlFields="SubDate" DataNavigateUrlFormatString="SubRequest.aspx?dt={0:d}" DataTextField="SubDate"
                      DataTextFormatString="{0:d}" />
                 <asp:BoundField DataField="ShiftName" HeaderText="Shift"  />
                 <asp:BoundField DataField="Sub" HeaderText="Sub ID"   />              
                 <asp:BoundField DataField="SubName" HeaderText="Sub Name"  />               
             </Columns>
             <EmptyDataTemplate>No Future Requests</EmptyDataTemplate>
             </cc2:NQNGridView>
         </div>
        </div>
         <div class="row">
            <div class="col-md-12">
          <cc2:NQNGridView  ID="GridView3" runat="server" 
                  DataSourceID="SubCommitmentsDataSource"  Caption="<strong>Sub Commitments</strong> (click to change)" AlternatingRowStyle-BackColor="#efefef" HeaderStyle-ForeColor="White"
                 AllowMultiColumnSorting="False" AutoGenerateColumns="False"  DataKeyNames="GuideSubstituteID" HeaderStyle-BackColor="#0099cc"
                  Privilege="" 
                  AllowSorting="True">
              <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="SubDate" DataNavigateUrlFormatString="SubRequest.aspx?dt={0:d}" DataTextField="SubDate"
                      DataTextFormatString="{0:d}" />
                  
                  <asp:BoundField DataField="ShiftName" HeaderText="Shift"   />
                 
                  <asp:BoundField DataField="FirstName" HeaderText="First Name"   />
                  <asp:BoundField DataField="LastName" HeaderText="Last Name"   />
                  <asp:BoundField DataField="VolID" HeaderText="VolID"  />
                   
              </Columns>
              <EmptyDataTemplate>No Future Commitments</EmptyDataTemplate>
             </cc2:NQNGridView>
         </div>
          </div>
        <div class="row" style="padding-top:10px">
        <div class="col-xs-4 col-xs-offset-4">
<asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn btn-info"
             CausesValidation="False" CommandName="Cancel" Text="Return to Profile"    OnClick="ToView1" />
</div></div>
    </asp:View>
    
         </asp:MultiView>
</asp:Content>
