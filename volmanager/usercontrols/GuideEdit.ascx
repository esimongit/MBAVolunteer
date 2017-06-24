<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GuideEdit.ascx.cs" Inherits="VolManager.UserControls.GuideEdit" %>
  <asp:ObjectDataSource ID="ObjectDataSource2" runat="server"  UpdateMethod="UpdateGuide"
        TypeName="NQN.Bus.GuidesBusiness"  SelectMethod="SelectGuide" OnUpdated="OnUpdated">
        <SelectParameters>
            <asp:SessionParameter Name="GuideID" SessionField="GuideID" DefaultValue="0" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
   
<asp:ObjectDataSource ID="ShiftsDataSource" runat="server" TypeName="NQN.DB.ShiftsDM" SelectMethod="FetchWithSubOffers">
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
  <asp:ObjectDataSource ID="ObjectDataSource1" runat='server' TypeName="NQN.Bus.SubstitutesBusiness" SelectMethod="FutureDatesForShift">
 <SelectParameters>
   <asp:SessionParameter SessionField="GuideID" Name="GuideID" Type="Int32" DefaultValue="0" />
   <asp:ControlParameter ControlID="ShiftSelect" Name="ShiftID" Type="Int32" DefaultValue="0" />
       <asp:Parameter Name="RoleID" Type="Int32" DefaultValue="0" />
 </SelectParameters>
 </asp:ObjectDataSource> 
<asp:ObjectDataSource ID="RolesDataSource" runat="server" TypeName="NQN.DB.GuideRoleDM" SelectMethod="FetchAllRoles">
    <SelectParameters>
    <asp:SessionParameter SessionField="GuideID" Name="GuideID" Type="Int32" DefaultValue="0" />
        </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="RequestHistoryDataSource" runat="server" TypeName="NQN.DB.GuideSubstituteDM" SelectMethod="RequestHistory">
    <SelectParameters>
    <asp:SessionParameter SessionField="GuideID" Name="GuideID" Type="Int32" DefaultValue="0" />
        </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="SubHistoryDataSource" runat="server" TypeName="NQN.DB.GuideSubstituteDM" SelectMethod="SubHistory">
    <SelectParameters>
    <asp:SessionParameter SessionField="GuideID" Name="GuideID" Type="Int32" DefaultValue="0" />
        </SelectParameters>
</asp:ObjectDataSource>
<style>
    td, th
    {
        padding:3px 3px 3px 3px;
    }
</style>
<table><tr><td style="vertical-align:top">
      
         <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource2" DataKeyNames="GuideID" OnDataBound ="SetLink"
           DefaultMode="Edit"   >
             <EditItemTemplate>
                  
                <table><tr><td class="formlabel">
                 ID:</td><td>
                 <asp:TextBox ID="VolIDTextBox" runat="server" Width="60" Text='<%# Bind("VolID") %>' />&nbsp;&nbsp;
                  <asp:HyperLink ID="MBAVLink" runat="server" Text="Open MBAV site"  Visible='<%#Eval("HasLogin") %>' Target="_blank"   ></asp:HyperLink>
                  </td></tr>
                 <tr><td class="formlabel">
               First Name:</td><td>
                 <asp:TextBox ID="FirstNameTextBox" runat="server" 
                     Text='<%# Bind("FirstName") %>' />
                 </td></tr>
                 <tr><td class="formlabel">
                 Last Name:</td><td>
                 <asp:TextBox ID="LastNameTextBox" runat="server" 
                     Text='<%# Bind("LastName") %>' />
                </td></tr>
                 <tr><td class="formlabel">
                 Home Phone:</td><td>
                 <asp:TextBox ID="PhoneTextBox" runat="server" TextMode="Phone" Text='<%# Bind("Phone") %>' />
                  </td></tr>
                 <tr><td class="formlabel">
                 Cell Phone:</td><td>
                 <asp:TextBox ID="CellTextBox" runat="server" TextMode="Phone" Text='<%# Bind("Cell") %>' />&nbsp;&nbsp;
                     <asp:CheckBox ID="CellPreferredCheckBox" runat="server" Checked='<%#Bind("CellPreferred") %>'  Text="Preferred"/>
                  </td></tr>

                 <tr><td class="formlabel">
                 Email:</td><td>
                 <asp:TextBox ID="EmailTextBox" runat="server"  Width="250" Text='<%# Bind("Email") %>' />
                  </td></tr>
                 <tr><td class="formlabel">
                 Shift:</td><td>
                     <div  ><asp:Label ID="ShiftLabel" runat="server" Text='<%#Eval("ShiftName") %>'></asp:Label></div>
                     <div style="float:left; padding-left:10px">
                     
                 Add Shift: <cc2:ShiftSelector ID="ShiftSelector1" runat="server" SelectedValue='<%#Bind("AddShift") %>'></cc2:ShiftSelector><br />
                         <asp:GridView ID="ShiftView" runat="server" DataSource='<%#Eval("Shifts") %>' AutoGenerateColumns="false" DataKeyNames="ShiftID"
                              OnRowDeleting="DeleteShift">
                             <Columns>
                                  <asp:CommandField ShowDeleteButton="True"  
                                    ButtonType="Image"   DeleteImageUrl="~/Images/delete.gif"  />
                                 <asp:BoundField DataField="ShiftName" />
                             </Columns>
                         </asp:GridView>
                         </div>
                 </td></tr>
                   <tr><td class="formlabel">
                
                 Primary Role:</td><td>
                 <cc2:RoleSelector ID="RoleSelect1" runat="server" SelectedValue='<%#Bind("RoleID") %>'></cc2:RoleSelector>
                  </td></tr>
                   <tr><td class="formlabel">
                 Alternate Roles:</td><td>
               <div  ><asp:Label ID="Label2" runat="server" Text='<%#Eval("AltRoleName") %>'></asp:Label></div>
                     <div style="float:left; padding-left:10px">
                     
                 Add Role: <cc2:RoleSelector ID="RoleSelector2" runat="server" SelectedValue='<%#Bind("AddRole") %>'></cc2:RoleSelector><br />
                         <asp:GridView ID="RoleView" runat="server" DataSource='<%#Eval("Roles") %>' AutoGenerateColumns="false" DataKeyNames="RoleID"
                              OnRowDeleting="DeleteRole">
                             <Columns>
                                  <asp:CommandField ShowDeleteButton="True"  
                                    ButtonType="Image"   DeleteImageUrl="~/Images/delete.gif"  />
                                 <asp:BoundField DataField="RoleName" />
                             </Columns>
                         </asp:GridView>
                         </div>
                   </td></tr>
                 <tr><td class="formlabel">
                 Inactive:</td><td>
                 <asp:CheckBox ID="InactiveCheckBox" runat="server" 
                     Checked='<%# Bind("Inactive") %>' /> Checking this box will remove all substitute requests and commitments for this guide.
                
                 </td></tr>
                    <tr><td class="formlabel">
                 Mask Contact Info:</td><td>
                 <asp:CheckBox ID="CheckBox2" runat="server" 
                     Checked='<%# Bind("MaskPersonalInfo") %>' />  
                
                 </td></tr>
                      <tr><td class="formlabel">
                 Irregular Schedule:</td><td>
                 <asp:CheckBox ID="IrregularCheckBox" runat="server" 
                     Checked='<%# Bind("IrregularSchedule") %>'   /> Check here if this guide is not assumed to be on an assigned shift. Select dates in the Irregular Schedule tab.
                
                 </td></tr>
                 <tr><td class="formlabel">
                 Has Login:</td><td>
                 <asp:CheckBox ID="CheckBox1" runat="server" 
                     Checked='<%# Eval("HasLogin") %>' />
                     <asp:Button ID="AddLoginButton" runat="server" Text="Setup Login" Visible = '<%#Eval("NeedsLogin") %>'  OnClick="AddLogin"
                          ToolTip="Clicking this button will generate an Email message to the Guide to allow them to set a new password." />
                 <asp:Button ID="ChangePWButton" runat="server" Text="ResetPW" Visible = '<%#Eval("HasLogin") %>'  OnClick="ResetPW" 
                      ToolTip="Clicking this button will generate an Email message to the Guide to allow them to set a new password." />
                
                 </td></tr>
               <tr><td class="formlabel">
                 Notes:</td><td>
                 <asp:TextBox ID="NotesTextBox" runat="server" Text='<%# Bind("Notes") %>' />
                  </td></tr>
               
                 <tr><td class="formlabel">
                 Update By:</td><td>
                 <asp:TextBox ID="UpdateByTextBox" runat="server" 
                     Text='<%# Eval("UpdateBy") %>' />
                  </td></tr>
                 <tr><td class="formlabel">
                 Last Update:</td><td>
                 <asp:TextBox ID="LastUpdateTextBox" runat="server" 
                     Text='<%# Eval("LastUpdate") %>' />
                 </td></tr>
                
               </table>
                 <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                     CommandName="Update" Text="Update" />
               
             </EditItemTemplate>
       
     
         </asp:FormView>
     
           
     </td><td style ="vertical-align:top"> 
          <asp:Menu ID="Menu1" runat="server" 
    onmenuitemclick="Menu1_MenuItemClick" Orientation="Horizontal" 
    BackColor="#FFFBD6" DynamicHorizontalOffset="2" Font-Names="Verdana" 
    Font-Size="12px" ForeColor="#000000"  >
    <StaticSelectedStyle BackColor="#FFCC66" />
    <StaticMenuItemStyle HorizontalPadding="10px" VerticalPadding="2px"  BorderStyle="Solid" BorderWidth="1"/>
    
    <StaticHoverStyle BackColor="#990000" ForeColor="White" />
    <Items>
     <asp:MenuItem Text="Sub Commitments" Selected="true" Value="0"></asp:MenuItem>
      <asp:MenuItem Text="Need Subs" Value="1"></asp:MenuItem>
        <asp:MenuItem   Text="Available" Value="2"></asp:MenuItem>
        <asp:MenuItem Text="Irreg Schedule" Value="3"></asp:MenuItem>        
        <asp:MenuItem Text="Sub History" Value="4"></asp:MenuItem> 
    </Items>
    </asp:Menu>
   <asp:MultiView ID="MultiView1" runat="server">
         <asp:View ID="View0" runat="server">
               <cc2:NQNGridView  ID="GridView3" runat="server" 
                  DataSourceID="SubCommitmentsDataSource"  
                 AllowMultiColumnSorting="False" AutoGenerateColumns="False"  DataKeyNames="GuideSubstituteID"
                 DeleteMessage="Are you sure you want to delete?" Privilege="" 
                  AllowSorting="True">
              <Columns>
                   <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/delete.gif" 
                       ShowDeleteButton="True" />
                   <asp:BoundField DataField="SubDate" HeaderText="Date"   DataFormatString="{0:d}" />
                  <asp:BoundField DataField="ShiftName" HeaderText="Shift"   />
                 
                  <asp:BoundField DataField="FirstName" HeaderText="First Name"   />
                  <asp:BoundField DataField="LastName" HeaderText="Last Name"   />
                  <asp:BoundField DataField="VolID" HeaderText="VolID"  />
                   
              </Columns>
              <EmptyDataTemplate>No Future Commitments</EmptyDataTemplate>
             </cc2:NQNGridView>
         </asp:View>
         <asp:View ID="View1" runat="server">
               <cc2:NQNGridView  ID="GridView2" runat="server"   
                 DataSourceID="SubRequestsDataSource" AllowMultiColumnSorting="False" 
                 AutoGenerateColumns="False" DeleteMessage="Are you sure you want to delete?" 
                 Privilege="" AllowSorting="True" DataKeyNames="GuideSubstituteID">
             <Columns>
                 <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/delete.gif" 
                     ShowDeleteButton="True" />
                 <asp:BoundField DataField="SubDate" HeaderText="Date"   DataFormatString="{0:d}" />
                 <asp:BoundField DataField="ShiftName" HeaderText="Shift"  />
          
                 <asp:BoundField DataField="Sub" HeaderText="Sub ID"   />
                
                 <asp:BoundField DataField="SubName" HeaderText="Sub Name"  />
                
                
             </Columns>
             <EmptyDataTemplate>No Future Requests</EmptyDataTemplate>
             </cc2:NQNGridView>
         </asp:View>
         <asp:View ID="View2" runat="server">


          <h2>Available to Sub on Shifts:</h2>
         <asp:Repeater ID="Repeater1" runat="server" DataSourceID="ShiftsDataSource" >
            <HeaderTemplate><table ></HeaderTemplate>
            <ItemTemplate><tr style='<%#Container.ItemIndex %2==0 ? "background-color:#efefef":"background-color:#ffffff" %>'> <td><asp:Label ID="Label1" runat="server" Text='<%#Eval("ShiftName") %>'></asp:Label></td><td>
            <asp:CheckBox ID="ShiftCheckBox" runat="server" Checked='<%#Eval("Selected") %>' OnCheckedChanged="CheckChanged"  AutoPostBack="true" />
            <asp:HiddenField ID="ShiftIDHidden" runat="server" Value='<%#Eval("ShiftID") %>' />
            </td></tr>
            </ItemTemplate>
            <FooterTemplate></table></FooterTemplate>
         </asp:Repeater>
      
       </asp:View>
         <asp:View ID="View3" runat="server">
     
    <div   style="float:left; font-size:large; color:purple">  First select a Shift: </div>
   <div style="float:left"> 
 <asp:DropDownList ID="ShiftSelect" runat="server" DataSourceID="ShiftsDataSource" DataTextField="ShiftName" DataValueField="ShiftID" AppendDataBoundItems="true"
  AutoPostBack="true">
  <asp:ListItem Value="0" Text="(Select a shift)"></asp:ListItem>
 </asp:DropDownList> 
 </div> 
    <div style="clear:both"></div>
             <asp:Button ID="OptionButton" runat="server" Text="Select a Role for Each Date" OnClick="ChangeOption" />
<asp:MultiView ID="MultiView2" runat="server">
     <asp:View id="View3AB" runat="server">
<div style="clear:both; padding-top:10px;padding-bottom:10px; font-size:large; color:purple"  >
    Select the role for A and B weeks for the next year. </div>
    <div>
        For A Weeks: <asp:DropDownList runat="server" ID="RoleSelectA" DataSourceID="RolesDataSource" DataValueField="RoleID" DataTextField="RoleName"
               AppendDataBoundItems="true">
        <asp:ListItem Value="0" Text="(Select a Role)"></asp:ListItem>
            </asp:DropDownList>
    </div>
             <div>
        For B Weeks: <asp:DropDownList runat="server" ID="RoleSelectB" DataSourceID="RolesDataSource" DataValueField="RoleID" DataTextField="RoleName" 
              AppendDataBoundItems="true">
        <asp:ListItem Value="0" Text="(Select a Role)"></asp:ListItem>
            </asp:DropDownList>
    </div>
     <div  >
        <asp:Button ID="Button1" OnClick="DoSubmitAB" runat="server" Text="Submit" CssClass="btn btn-success" />
    </div>
    </asp:View>
   <asp:View id="View3Detail" runat="server">
       <div>
       In the table below,  check all the boxes for dates  
planned and select a role. Uncheck dates to remove them. Then click "Submit".  </div>
  
<asp:Repeater ID="Repeater2" runat="server" DataSourceID="ObjectDataSource1" >
<HeaderTemplate><table cellpadding="5"></HeaderTemplate>
<ItemTemplate><tr style='<%#Container.ItemIndex %2==0 ? "background-color:#afcfdf":"background-color:#ffffff" %>'> 
<td style="padding:2px 4px 2px 2px; text-align:right"><asp:Label ID="DateLabel" runat="server" Text='<%#Eval("DropinDate",  "{0:d}") %>'></asp:Label></td><td>
    <asp:DropDownList runat="server" ID="RoleSelect" DataSourceID="RolesDataSource" DataValueField="RoleID" DataTextField="RoleName" SelectedValue='<%#Eval("RoleID") %>' AppendDataBoundItems="true">
        <asp:ListItem Value="0" Text="(Select a Role)"></asp:ListItem>
    </asp:DropDownList>
 </td>
 <td>
<asp:CheckBox ID="DateCheckBox" runat="server" Checked='<%#Eval("Selected") %>'   />
<asp:HiddenField ID="DropinIDHidden" runat="server" Value='<%#Eval("GuideDropinID") %>' />
</td></tr>
</ItemTemplate>
<FooterTemplate></table></FooterTemplate>
</asp:Repeater> 
<div  >
<asp:Button ID="SubmitButton" OnClick="DoSubmit" runat="server" Text="Submit" CssClass="btn btn-success" />
</div>
     </asp:View>
    </asp:MultiView>
         </asp:View> 
       <asp:View ID="View4" runat="server">

           <asp:GridView ID="SubRequestGridView" runat="server" DataSourceID ="RequestHistoryDataSource" PageSize="20" AutoGenerateColumns="False">

               <Columns>                   
                   <asp:BoundField DataField="SubDate" HeaderText="Sub Date" DataFormatString="{0:d}" SortExpression="SubDate" />                 
                   <asp:CheckBoxField DataField="HasSub" HeaderText="Had a Sub" SortExpression="HasSub" />   
                   <asp:BoundField DataField="LeadTime" HeaderText="Lead Days" SortExpression="LeadTime" />
                  
               </Columns>
               <EmptyDataTemplate>No Substitute Requests</EmptyDataTemplate>
           </asp:GridView>
           <br />
           <asp:GridView ID="SubHistoryGridView"  Caption="<b>Substitute Shifts</b>" runat="server" DataSourceID ="SubHistoryDataSource" PageSize="20" AutoGenerateColumns="False">

               <Columns>               
                     <asp:BoundField DataField="SubDate" HeaderText="Sub Date" SortExpression="SubDate" DataFormatString="{0:d}" />       
                   <asp:BoundField DataField="ShiftName" HeaderText="Shift Name" SortExpression="ShiftName" />                  
                   <asp:BoundField DataField="SubRole" HeaderText="Role" SortExpression="SubRole" />                  
                   <asp:BoundField DataField="GuideName" HeaderText="Requestor Name" ReadOnly="True" SortExpression="GuideName" />  
               </Columns>
               <EmptyDataTemplate>No Substitute Shifts</EmptyDataTemplate>
           </asp:GridView>
           </asp:View>
</asp:MultiView>
  </td></tr></table>