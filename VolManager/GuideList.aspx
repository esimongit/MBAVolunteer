<%@ Page Title="Guide List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GuideList.aspx.cs" Inherits="VolManager.GuideList" %>
 <%@ Register Src="~/UserControls/GuideEdit.ascx" TagName="GuideEdit" TagPrefix="uc3" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        TypeName="NQN.DB.GuidesDM" SelectMethod="Search"   
        DataObjectTypeName="NQN.DB.GuidesObject" DeleteMethod="Delete"  
        InsertMethod="Save"   OnInserted="OnInserted" OnDeleted="OnDeleted">
    <SelectParameters>
      <asp:ControlParameter ControlID="PatternTextBox" Name="Pattern" Type="String" DefaultValue="" />
      <asp:ControlParameter ControlID="ShiftSelect" Name="ShiftID" Type="Int32" DefaultValue="0" />
        <asp:ControlParameter ControlID="RoleSelect" Name="RoleID" Type="Int32" DefaultValue="0" />
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
          &nbsp;&nbsp; Filter by Role: <cc2:RoleSelector ID="RoleSelect" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DoSearch"></cc2:RoleSelector>                        
     </td></tr>               
    <tr><td >
      
        <asp:TextBox ID="PatternTextBox" runat="server"  Width="200px"  AutoCompleteType="None" ></asp:TextBox>
          <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="DoSearch"  /> 
        Search for ID or names by any partial first or last name
       </td><td >
       
         <asp:Button ID="AddButton" runat="server" Text="Add New Guide" OnClick="ToView3" />  &nbsp;&nbsp;
          <asp:CheckBox ID="SearchInactiveCheckBox" Text="Search Inactive" runat="server" AutoPostBack="true" />
           </td> 
        </tr>
    </table>
   
    <cc2:NQNGridView ID="GridView1" runat="server" AutoGenerateColumns="False"  RowStyle-ForeColor="Black"  PageSize="100" 
        DataKeyNames="GuideID"   Selectable="true"  OnSorting="TaskGridView_Sorting"  OnSelectedIndexChanged="GuideSelected"
        AlternatingRowStyle-BackColor="White" AllowSorting="True"   OnPageSizeChanged="DoSearch" >
        <Columns>
         
            <asp:BoundField DataField="VolID" HeaderText="ID" SortExpression="VolInt" />
            <asp:BoundField DataField="FirstName" HeaderText="First Name" 
                SortExpression="FirstName" />
            <asp:BoundField DataField="LastName" HeaderText="Last Name" 
                SortExpression="LastName" />
             <asp:BoundField DataField="ShiftName" HeaderText="Shift" 
                SortExpression="dow,sequence" />
           <asp:BoundField DataField="RoleName" HeaderText="Role" 
                SortExpression="RoleName" />
            <asp:BoundField DataField="PreferredPhone" HeaderText="Phone" SortExpression="PreferredPhone" />
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
        <asp:LinkButton ID="LinkButton1" runat="server"  OnClick="ToView1" Font-Bold="true"
                     CausesValidation="False" CommandName="Cancel" Text="Return to List" /><br />
     <uc3:GuideEdit id="GuideEdit1" runat="server"  ></uc3:GuideEdit>
         <div style="clear:both"></div>
          <asp:LinkButton ID="ReturnButton" runat="server"  OnClick="ToView1" Font-Bold="true"
                     CausesValidation="False" CommandName="Cancel" Text="Return to List" />
    </asp:View>
     <asp:View ID="View3" runat="server">
         <asp:ValidationSummary runat="server" ForeColor="Red" />
         <asp:FormView ID="FormView2" runat="server" DataSourceID="ObjectDataSource1" DefaultMode="Insert">
               <InsertItemTemplate>
                <table>
                 <tr><td class="formlabel">
                     <asp:RequiredFieldValidator ID="Required1" runat="server" ControlToValidate="VolIDTextBox"  Display="Dynamic" ErrorMessage="ID is required">*</asp:RequiredFieldValidator>
                 ID:</td><td>
                 <asp:TextBox ID="VolIDTextBox" runat="server" Text='<%# Bind("VolID") %>' />
                  </td></tr>
                  <tr><td class="formlabel">
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FirstNameTextBox"  Display="Dynamic" ErrorMessage="First Name is required">*</asp:RequiredFieldValidator>
          First Name:</td><td>
                 <asp:TextBox ID="FirstNameTextBox" runat="server" 
                     Text='<%# Bind("FirstName") %>' />
               </td></tr>
                 <tr><td class="formlabel">
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="LastNameTextBox"  Display="Dynamic" ErrorMessage="Last Name is required">*</asp:RequiredFieldValidator>
       Last Name:</td><td>
                 <asp:TextBox ID="LastNameTextBox" runat="server" 
                     Text='<%# Bind("LastName") %>' />
                  </td></tr>
                 <tr><td class="formlabel">
                 Phone:</td><td>
                 <asp:TextBox ID="PhoneTextBox" runat="server" TextMode="Phone" Text='<%# Bind("Phone") %>' />
                   </td></tr>
                 <tr><td class="formlabel">
             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="EmailTextBox"  Display="Dynamic" ErrorMessage="Email is required">*</asp:RequiredFieldValidator>
     
                 Email:</td><td>
                 <asp:TextBox ID="EmailTextBox" runat="server" Text='<%# Bind("Email") %>' Width="250"  TextMode="Email"/>
                 </td></tr>
                  <tr><td class="formlabel">
                 Shift:</td><td>
                  <cc2:ShiftSelector ID="ShiftSelector1" runat="server" SelectedValue='<%#Bind("ShiftID") %>'></cc2:ShiftSelector>
                   </td></tr>
                    <tr><td class="formlabel">
                 Irregular Schedule:</td><td>
                  <asp:CheckBox ID="IrregularScheduleCheckBox" runat="server" Checked='<%#Bind("IrregularSchedule") %>' />
                   </td></tr>
                <tr><td class="formlabel">
                 Primary Role:</td><td>
                  <cc2:RoleSelector ID="RoleSelect1" runat="server" SelectedValue='<%#Bind("RoleID") %>'></cc2:RoleSelector>
                   </td></tr>
                  <tr><td class="formlabel">
                 
                Alternate Roles:</td><td>
               <div style="float:left"><asp:Label ID="Label2" runat="server" Text='<%#Eval("AltRoleName") %>'></asp:Label></div>
                     <div style="float:left; padding-left:10px">
                     
                 Add Role: <cc2:RoleSelector ID="RoleSelector2" runat="server" SelectedValue='<%#Bind("AddRole") %>'></cc2:RoleSelector><br />
                         <asp:GridView ID="GridView1" runat="server" DataSource='<%#Eval("Roles") %>' AutoGenerateColumns="false" DataKeyNames="RoleID" >
                             <Columns>
                                  <asp:CommandField ShowDeleteButton="True"  
                                    ButtonType="Image"   DeleteImageUrl="~/Images/delete.gif"  />
                                 <asp:BoundField DataField="RoleName" />
                             </Columns>
                         </asp:GridView>
                         </div>
                   </td></tr>
                 
                 <tr><td class="formlabel">
                 Notes:</td><td>
                 <asp:TextBox ID="NotesTextBox" runat="server" Text='<%# Bind("Notes") %>' />
                  </td></tr>
                         
                </table>
                 <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                     CommandName="Insert" Text="Insert" />
                 &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server"  OnClick="ToView1"
                     CausesValidation="False" CommandName="Cancel" Text="Cancel" />
             </InsertItemTemplate>
         </asp:FormView>
    </asp:View>
    </asp:MultiView>
</asp:Content>
