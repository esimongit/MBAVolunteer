<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShiftList.aspx.cs" Inherits="VolManager.ShiftList" %>
 <%@ Register Src="~/UserControls/GuideEdit.ascx" TagName="GuideEdit" TagPrefix="uc3" %> 
 <%@ Register Src="~/UserControls/DateSelector.ascx" TagName="DateSelector" TagPrefix="uc3" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        TypeName="NQN.DB.ShiftsDM" SelectMethod="FetchByCategory" DeleteMethod="Delete" OnDeleted="OnDeleted" > 
   
    <SelectParameters>
        <asp:ControlParameter  Name="Recurring" ControlID="RecurringSelect" Type="Boolean" DefaultValue="True" />

    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
        TypeName="NQN.DB.ShiftsDM" SelectMethod="FetchShift"  InsertMethod="Save" OnInserted="OnInserted" OnUpdated="OnInserted"
        DataObjectTypeName="NQN.DB.ShiftsObject" UpdateMethod="Update">
    <SelectParameters>
        <asp:ControlParameter ControlID="GridView1" Type="Int32" DefaultValue="0" Name="ShiftID" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSource3" runat="server" 
        TypeName="NQN.DB.GuidesDM" SelectMethod="FetchForShift" 
        DataObjectTypeName="NQN.DB.GuidesObject" UpdateMethod="Update">
    <SelectParameters>
        <asp:ControlParameter ControlID="GridView1" Type="Int32" DefaultValue="0" Name="ShiftID" />
    </SelectParameters>
</asp:ObjectDataSource>
 <asp:ObjectDataSource ID="ShiftTimesDataSource" runat="server" 
        TypeName="NQN.DB.ShiftTimesDM" SelectMethod="FetchAll"   >
   
</asp:ObjectDataSource>
      <atk:ToolkitScriptManager ID="ScriptManager1" runat="server" />
    <asp:Multiview id="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <div style="float:left">
        <asp:RadioButtonList ID="RecurringSelect" runat="server" RepeatDirection="Horizontal"  AutoPostBack="true">
            <asp:ListItem Text="Recurring" Value="True" Selected="true"></asp:ListItem>
             <asp:ListItem Text="Special" Value="False"></asp:ListItem>
        </asp:RadioButtonList></div>
            <div style="float:left;padding-left:10px">
            <asp:Button ID="AddButton" runat="server" Text="Define a Shift" OnClick="ToView4" /></div>
        <div style="clear:both"></div>
    <cc2:NQNGridView ID="GridView1" runat="server" AutoGenerateColumns="False"  OnSelectedIndexChanged="ToView2"
        DataSourceID="ObjectDataSource1" DataKeyNames="ShiftID" Selectable="true" PageSize="100" AllowMultiColumnSorting="False" DeleteMessage="Are you sure you want to delete?" Privilege="">
        <Columns> 
            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/delete.gif" ShowDeleteButton="True" />
            <asp:BoundField DataField="ShiftName" HeaderText="Shift Name" />
            <asp:CheckBoxField DataField="AWeek" HeaderText="A Week" />
            <asp:CheckBoxField DataField="BWeek" HeaderText="B Week" />
            <asp:BoundField DataField="Sequence" HeaderText="Sequence" ReadOnly="true" />
            <asp:BoundField DataField="ShortName" HeaderText="Short Name" />
            <asp:BoundField DataField="ShiftStart" DataFormatString="{0:h\:mm tt}" HeaderText="Shift Start" ItemStyle-HorizontalAlign="Right">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="ShiftEnd" DataFormatString="{0:h\:mm tt}" HeaderText="Shift End" ItemStyle-HorizontalAlign="Right">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="Captains" HeaderText="Captains" />
            <asp:BoundField DataField="InfoDesk" HeaderText="Info Desk" />
        </Columns>
    </cc2:NQNGridView>
            </asp:View>
        <asp:View ID="View2" runat="server">

            <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource2" DataKeyNames="ShiftID"
                 DefaultMode="Edit">
                <EditItemTemplate>
                    <table>
                        <tr><td class="formlabel"> 
                    Recurring:</td><td>
                    <asp:CheckBox ID="RecurringCheckBox" runat="server" Checked='<%# Eval("Recurring") %>' Enabled="false" />  
                     </td></tr>
                     <tr><td class="formlabel">
                    Shift Date:</td><td>
                      <asp:Label ID="DateLabel" runat="server" Text='<%#Bind("ShiftDate", "{0:d}") %>' Visible='<%#Eval("NonRecurring") %>' />
                    </td></tr>
                        <tr><td class="formlabel">
                    Shift Name:</td><td>
                    <asp:TextBox ID="ShiftNameTextBox" runat="server" Text='<%# Bind("ShiftName") %>' />
                    </td></tr>
                    <tr><td class="formlabel">
                    Day of Week:</td><td>
                    <cc2:DowSelector id="DOWSelect1" runat="server" SelectedValue='<%# Bind("DOW") %>' />
                     </td></tr>
                    <tr><td class="formlabel">
                    AWeek:</td><td>
                    <asp:CheckBox ID="AWeekCheckBox" runat="server" Checked='<%# Bind("AWeek") %>' />
                      &nbsp;&nbsp;
                    BWeek:&nbsp;&nbsp;
                    <asp:CheckBox ID="BWeekCheckBox" runat="server" Checked='<%# Bind("BWeek") %>' />
                     </td></tr>
                    <tr><td class="formlabel">
                    Sequence:</td><td>
                    <asp:TextBox ID="SequenceTextBox" runat="server" Width="20" Text='<%# Bind("Sequence") %>' />
                   </td></tr>
                    <tr><td class="formlabel">
                    Short Name:</td><td>
                    <asp:TextBox ID="ShortNameTextBox" runat="server" Text='<%# Bind("ShortName") %>' />
                      </td></tr>
                    <tr><td class="formlabel">
                    Shift Time:</td><td>
                    <asp:DropDownList ID="ShiftTimeSelect" DataValueField="ShiftTimeID" DataTextField="TimeString" DataSourceID="ShiftTimesDataSource" runat="server" Text='<%# Bind("ShiftTimeID") %>' />
                    </td></tr>
                    <tr><td class="formlabel">
                    
                    Captains:</td><td>
                    <asp:Label ID="CaptainsLabel" runat="server" Text='<%# Eval("Captains") %>' />
                    </td></tr>
                    <tr><td class="formlabel">
                    Info Desk:</td><td>
                    <asp:Label ID="InfoDeskLabel" runat="server" Text='<%# Eval("InfoDesk") %>' />
                    </td></tr></table>
                   
                    <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
                    &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Return to List"
                        OnClick="ToView1" />
                </EditItemTemplate>
            
            </asp:FormView>

            <cc2:NQNGridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource3" 
                 PageSize="100" Selectable="true" OnSelectedIndexChanged="GuideSelected" Caption="<b>Current Guides</b> - click to edit"
                 DataKeyNames="GuideID">
                <Columns>
                    <asp:BoundField DataField="VolID" HeaderText="ID" SortExpression="VolID" />
                    <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
                    <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
                    <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:BoundField DataField="RoleName" HeaderText="Role" SortExpression="RoleName" />
                    <asp:BoundField DataField="AltRoleName" HeaderText="Alt Role" SortExpression="AltRoleName" />
                    <asp:CheckBoxField DataField="Inactive" HeaderText="Inactive" SortExpression="Inactive" />
                    <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes" />
                    <asp:CheckBoxField DataField="HasLogin" HeaderText="Has Login" SortExpression="HasLogin" />
                </Columns>
            </cc2:NQNGridView>

        </asp:View>
        <asp:View ID="View3" runat="server">
            <asp:Button ID="ReturnButton" runat="server" Text="Return to Shift" OnClick="ToView2" />
            <uc3:GuideEdit ID="GuideEdit1" runat="server" />
            
        </asp:View>
         <asp:View ID="View4" runat="server">
              <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
              <asp:FormView ID="FormView2" runat="server" DataSourceID="ObjectDataSource2" DataKeyNames="ShiftID"
                 DefaultMode="Insert">
                <InsertItemTemplate>
                    <table><tr><td class="formlabel">
                
                    Recurring:</td><td>
                    <asp:CheckBox ID="RecurringCheckBox" runat="server" Checked='<%# Bind("Recurring") %>' AutoPostBack="true"
                         OnCheckedChanged="RecurringChanged" />  
                     </td></tr>
                    <tr><td class="formlabel">
                        <asp:RequiredFieldValidator runat="server" ID="Required1" ControlToValidate="ShiftNameTextBox" ErrorMessage="Name is required." Display="Dynamic">*</asp:RequiredFieldValidator>
                    Shift Name:</td><td>
                    <asp:TextBox ID="ShiftNameTextBox" runat="server" Text='<%# Bind("ShiftName") %>' />
                    </td></tr>
                    <asp:Panel ID="SpecialPanel" runat="server" Visible ="true" >
                        
                   
                     <tr><td class="formlabel">
                    Shift Date:</td><td>
                      <uc3:DateSelector LabelText="" ID="DateSelect" runat="server" bDate='<%#Bind("ShiftDate") %>' />
                    </td></tr>
                     </asp:Panel>
                    <asp:Panel ID="RecurringPanel" runat="server" Visible="false">
                    <tr><td class="formlabel">
                    Day of Week:</td><td>
                    <cc2:DowSelector id="DOWSelect1" runat="server" SelectedValue='<%# Bind("DOW") %>' />
                     </td></tr>
                    <tr><td class="formlabel">
                    AWeek:</td><td>
                    <asp:CheckBox ID="AWeekCheckBox" runat="server" Checked='<%# Bind("AWeek") %>' />
                      &nbsp;&nbsp;
                    BWeek:&nbsp;&nbsp;
                    <asp:CheckBox ID="BWeekCheckBox" runat="server" Checked='<%# Bind("BWeek") %>' />
                     </td></tr>
                   </asp:Panel>
                    <tr><td class="formlabel">
                    Sequence:</td><td>
                    <asp:TextBox ID="SequenceTextBox" runat="server" Width="20" Text='<%# Bind("Sequence") %>' />
                   </td></tr>
                    <tr><td class="formlabel">
                    Short Name:</td><td>
                    <asp:TextBox ID="ShortNameTextBox" runat="server" Text='<%# Bind("ShortName") %>' />
                      </td></tr>
                    <tr><td class="formlabel">
                    Shift Time:</td><td>
                    <asp:DropDownList ID="ShiftTimeSelect" DataValueField="ShiftTimeID" DataTextField="TimeString" DataSourceID="ShiftTimesDataSource" runat="server" Text='<%# Bind("ShiftTimeID") %>' />
                    </td></tr>
                    </table>
                   
                    <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Save" />
                    &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Return to List"
                        OnClick="ToView1" />
                </InsertItemTemplate>
            
            </asp:FormView>
             </asp:View>
    </asp:Multiview>
</asp:Content>
