<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GuideList.aspx.cs" Inherits="VolManager.GuideList" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        TypeName="NQN.DB.GuidesDM" SelectMethod="Search"   
        DataObjectTypeName="NQN.DB.GuidesObject" DeleteMethod="Delete"  
        InsertMethod="Save"   OnInserted="OnInserted" OnDeleted="OnDeleted">
    <SelectParameters>
      <asp:ControlParameter ControlID="PatternTextBox" Name="Pattern" Type="String" DefaultValue="" />
      <asp:ControlParameter ControlID="ShiftSelect" Name="ShiftID" Type="Int32" DefaultValue="0" />
       <asp:ControlParameter ControlID="SearchInactiveCheckBox" Name="Inactive" Type="Boolean" DefaultValue="false" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="pkey" Type="Int32" />
    </DeleteParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server"  UpdateMethod="UpdateGuide"
        TypeName="NQN.Bus.GuidesBusiness"  SelectMethod="SelectGuide" OnUpdated="OnDeleted">
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
    <asp:MultiView ID="MultiView1" runat="server">
    <asp:View ID="View1" runat="server">
  <table id="searchtable" width="100%" runat="server" border="1" class="searchtable">
        <tr>
            <td>
                <strong> Search Criteria</strong></td><td  >
         Filter by Shift: <cc2:ShiftSelector ID="ShiftSelect" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DoSearch" ></cc2:ShiftSelector>
                                    
     </td></tr>               
    <tr><td >
      
        <asp:TextBox ID="PatternTextBox" runat="server"  Width="200px"  AutoCompleteType="None" ></asp:TextBox>
          <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="DoSearch"  /> 
        Search for ID or names by any partial first or last name
       </td><td >
       
         <asp:Button ID="AddButton" runat="server" Text="Add New Guide" OnClick="ToView3" />  &nbsp;&nbsp;
          <asp:CheckBox ID="SearchInactiveCheckBox" Text="Search Inactive" runat="server" />
           </td> 
        </tr>
    </table>
   
    <cc2:NQNGridView ID="GridView1" runat="server" AutoGenerateColumns="False"  RowStyle-ForeColor="Black"  PageSize="100" 
        DataKeyNames="GuideID"   Selectable="true"  OnSorting="TaskGridView_Sorting"  OnSelectedIndexChanged="GuideSelected"
        AlternatingRowStyle-BackColor="White" AllowSorting="True" DeleteMessage="Remove this guide and all commitments!">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="False" 
                ButtonType="Image" CancelImageUrl="~/Images/cancel.gif" 
                DeleteImageUrl="~/Images/delete.gif" EditImageUrl="~/Images/iedit.gif" 
                UpdateImageUrl="~/Images/save.gif" />
            <asp:BoundField DataField="VolID" HeaderText="ID" SortExpression="VolID" />
            <asp:BoundField DataField="FirstName" HeaderText="First Name" 
                SortExpression="FirstName" />
            <asp:BoundField DataField="LastName" HeaderText="Last Name" 
                SortExpression="LastName" />
             <asp:BoundField DataField="ShiftName" HeaderText="Shift" 
                SortExpression="Sequence" />
           <asp:BoundField DataField="RoleName" HeaderText="Role" 
                SortExpression="RoleName" />
            <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:CheckBoxField DataField="Inactive" HeaderText="Inactive" 
                SortExpression="Inactive" />
            <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes" />
           
           
                 <asp:BoundField DataField="UpdateBy" HeaderText="UpdateBy" 
                SortExpression="UpdateBy" />
            <asp:BoundField DataField="LastUpdate" HeaderText="LastUpdate" 
                SortExpression="LastUpdate" />
        </Columns>
         </cc2:NQNGridView>
     </asp:View>
     <asp:View ID="View2" runat="server">
     
       <div style="float:left; padding-left:10px">
      
         <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource2" DataKeyNames="GuideID"
           DefaultMode="Edit" >
             <EditItemTemplate>
                  
                <table><tr><td class="formlabel">
                 ID:</td><td>
                 <asp:TextBox ID="VolIDTextBox" runat="server" Width="60" Text='<%# Bind("VolID") %>' />&nbsp;&nbsp;
                  <asp:HyperLink ID="MBAVLink" runat="server" Text="Open MBAV site" Target="_blank"  NavigateUrl='<%#Eval("UserId", "http://mbav.netqnet.com/Login.aspx?ID={0}") %>' ></asp:HyperLink>
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
                 Phone:</td><td>
                 <asp:TextBox ID="PhoneTextBox" runat="server" TextMode="Phone" Text='<%# Bind("Phone") %>' />
                  </td></tr>
                 <tr><td class="formlabel">
                 Email:</td><td>
                 <asp:TextBox ID="EmailTextBox" runat="server"  Width="250" Text='<%# Bind("Email") %>' />
                  </td></tr>
                 <tr><td class="formlabel">
                 Shift:</td><td>
                 <cc2:ShiftSelector ID="ShiftSelector1" runat="server" SelectedValue='<%#Bind("ShiftID") %>'></cc2:ShiftSelector>
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
                 Has Login:</td><td>
                 <asp:CheckBox ID="CheckBox1" runat="server" 
                     Checked='<%# Eval("HasLogin") %>' />
                     <asp:Button ID="AddLoginButton" runat="server" Text="Setup Login" Visible = '<%#Eval("NeedsLogin") %>'  OnClick="AddLogin" />
                
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
                   <%-- <tr><td class="formlabel">
                 Preferred Name:</td><td>
                 <asp:TextBox ID="PreferredNameTextBox" runat="server" 
                     Text='<%# Bind("PreferredName") %>' /> 
                
               </td></tr>--%>
               </table>
                 <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                     CommandName="Update" Text="Update" />
                 &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server"  OnClick="ToView1"
                     CausesValidation="False" CommandName="Cancel" Text="Cancel" />
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
          <asp:Button ID="ReturnButton" runat="server" Text="Return to List" OnClick="ToView1" />
          <h2>Available to Sub on Shifts:</h2>
         <asp:Repeater ID="Repeater1" runat="server" DataSourceID="ShiftsDataSource" >
            <HeaderTemplate><table cellpadding="5"></HeaderTemplate>
            <ItemTemplate><tr style='<%#Container.ItemIndex %2==0 ? "background-color:#efefef":"background-color:#ffffff" %>'> <td><asp:Label ID="Label1" runat="server" Text='<%#Eval("ShiftName") %>'></asp:Label></td><td>
            <asp:CheckBox ID="ShiftCheckBox" runat="server" Checked='<%#Eval("Selected") %>' OnCheckedChanged="CheckChanged"  AutoPostBack="true" />
            <asp:HiddenField ID="ShiftIDHidden" runat="server" Value='<%#Eval("ShiftID") %>' />
            </td></tr>
            </ItemTemplate>
            <FooterTemplate></table></FooterTemplate>
         </asp:Repeater>
         </div>
        
    </asp:View>
     <asp:View ID="View3" runat="server">
         <asp:FormView ID="FormView2" runat="server" DataSourceID="ObjectDataSource1" DefaultMode="Insert">
               <InsertItemTemplate>
                <table>
                 <tr><td class="formlabel">
                 ID:</td><td>
                 <asp:TextBox ID="VolIDTextBox" runat="server" Text='<%# Bind("VolID") %>' />
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
                 Phone:</td><td>
                 <asp:TextBox ID="PhoneTextBox" runat="server" TextMode="Phone" Text='<%# Bind("Phone") %>' />
                   </td></tr>
                 <tr><td class="formlabel">
                 Email:</td><td>
                 <asp:TextBox ID="EmailTextBox" runat="server" Text='<%# Bind("Email") %>' Width="250" />
                 </td></tr>
                  <tr><td class="formlabel">
                 Shift:</td><td>
                  <cc2:ShiftSelector ID="ShiftSelector1" runat="server" SelectedValue='<%#Bind("ShiftID") %>'></cc2:ShiftSelector>
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
                 Notes:</td><td>
                 <asp:TextBox ID="NotesTextBox" runat="server" Text='<%# Bind("Notes") %>' />
                  </td></tr>
                         
                <%-- <tr><td class="formlabel">
               
                 Preferred Name:</td><td>
                 <asp:TextBox ID="PreferredNameTextBox" runat="server" 
                     Text='<%# Bind("PreferredName") %>' />
              
                
                   </td></tr>--%></table>
                 <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                     CommandName="Insert" Text="Insert" />
                 &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server"  OnClick="ToView1"
                     CausesValidation="False" CommandName="Cancel" Text="Cancel" />
             </InsertItemTemplate>
         </asp:FormView>
    </asp:View>
    </asp:MultiView>
</asp:Content>
