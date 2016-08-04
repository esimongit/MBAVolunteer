<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GuideList.aspx.cs" Inherits="VolManager.GuideList" %>
 <%@ Register Src="~/UserControls/GuideEdit.ascx" TagName="GuideEdit" TagPrefix="uc3" %> 
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
            <asp:CheckBoxField DataField="HasLogin" HeaderText="Login" SortExpression="HasLogin" />
            <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes" />
           
           
                 <asp:BoundField DataField="UpdateBy" HeaderText="UpdateBy" 
                SortExpression="UpdateBy" />
            <asp:BoundField DataField="LastUpdate" HeaderText="LastUpdate" 
                SortExpression="LastUpdate" />
        </Columns>
         </cc2:NQNGridView>
     </asp:View>
     <asp:View ID="View2" runat="server">
        <asp:LinkButton ID="ReturnButton" runat="server"  OnClick="ToView1" Font-Bold="true"
                     CausesValidation="False" CommandName="Cancel" Text="Return to List" />
     <uc3:GuideEdit id="GuideEdit1" runat="server"  ></uc3:GuideEdit>
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
