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
<style>
    td, th
    {
        padding:3px 3px 3px 3px;
    }
</style>
<div style="float:left; padding-left:10px">
      
         <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource2" DataKeyNames="GuideID"
           DefaultMode="Edit"   >
             <EditItemTemplate>
                  
                <table><tr><td class="formlabel">
                 ID:</td><td>
                 <asp:TextBox ID="VolIDTextBox" runat="server" Width="60" Text='<%# Bind("VolID") %>' />&nbsp;&nbsp;
                  <asp:HyperLink ID="MBAVLink" runat="server" Text="Open MBAV site"  Visible='<%#Eval("HasLogin") %>' Target="_blank"  NavigateUrl='<%#Eval("UserId", "http://mbav.netqnet.com/Login.aspx?ID={0}") %>' ></asp:HyperLink>
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
                     <div style="float:left"><asp:Label ID="ShiftLabel" runat="server" Text='<%#Eval("ShiftName") %>'></asp:Label></div>
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
                 Alternate Role:</td><td>
                  <cc2:RoleSelector ID="RoleSelect2" runat="server" SelectedValue='<%#Bind("AltRoleID") %>'></cc2:RoleSelector>
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
                 Has Login:</td><td>
                 <asp:CheckBox ID="CheckBox1" runat="server" 
                     Checked='<%# Eval("HasLogin") %>' />
                     <asp:Button ID="AddLoginButton" runat="server" Text="Setup Login" Visible = '<%#Eval("NeedsLogin") %>'  OnClick="AddLogin" />
                 <asp:Button ID="ChangePWButton" runat="server" Text="ResetPW" Visible = '<%#Eval("HasLogin") %>'  OnClick="ResetPW" />
                
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
     
          <div style=" padding-left:10px">
         <cc2:NQNGridView  ID="GridView2" runat="server"  Caption="Sub Requests"
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
         <br />
          <cc2:NQNGridView  ID="GridView3" runat="server" 
                  DataSourceID="SubCommitmentsDataSource"  Caption="Sub Commitments"
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
         </div>
          </div>
         <div style="float:left; padding-left:10px">
          
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
         </div>
        