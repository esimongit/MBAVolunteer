<%@ Page Title="Shift Detail" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShiftDetail.aspx.cs" Inherits="VolManager.ShiftDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName = "NQN.Bus.GuidesBusiness" SelectMethod="Roster" UpdateMethod="UpdateRoster"
 InsertMethod="AddDropIn"  DeleteMethod="RemoveDropIn" OnUpdated="OnChanged" OnDeleted="OnChanged" OnInserted="OnChanged">
 <SelectParameters>
   <asp:QueryStringParameter QueryStringField="ShiftID" Name="ShiftID" Type="Int32" DefaultValue="0" />
   <asp:QueryStringParameter QueryStringField="ShiftDate" Name="dt" Type="DateTime" DefaultValue="1/2/2016" />
 </SelectParameters>
 <UpdateParameters>
 <asp:QueryStringParameter QueryStringField="ShiftDate" Name="dt" Type="DateTime" DefaultValue="1/2/2016" />
      <asp:QueryStringParameter QueryStringField="ShiftID" Name="ShiftID" Type="Int32" DefaultValue="0" />
 </UpdateParameters>
 <DeleteParameters>
 <asp:QueryStringParameter QueryStringField="ShiftDate" Name="ShiftDate" Type="DateTime" DefaultValue="1/2/2016" />
      <asp:QueryStringParameter QueryStringField="ShiftID" Name="ShiftID" Type="Int32" DefaultValue="0" />
 </DeleteParameters>
 <InsertParameters>
  <asp:QueryStringParameter QueryStringField="ShiftID" Name="ShiftID" Type="Int32" DefaultValue="0" />
 <asp:QueryStringParameter QueryStringField="ShiftDate" Name="dt" Type="DateTime" DefaultValue="1/2/2016" />
 </InsertParameters>
</asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ShiftDataSource" runat="server" TypeName = "NQN.Bus.ShiftsBusiness" SelectMethod="SelectShift"   >
 <SelectParameters>
   <asp:QueryStringParameter QueryStringField="ShiftID" Name="ShiftID" Type="Int32" DefaultValue="0" />
   <asp:QueryStringParameter QueryStringField="ShiftDate" Name="dt" Type="DateTime" DefaultValue="1/2/2016" />
 </SelectParameters>
    </asp:ObjectDataSource>
<asp:FormView ID="ShiftView" runat="server" DataSourceID="ShiftDataSource" DefaultMode="ReadOnly">
    <ItemTemplate>
        <table>
            <tr><td class="formlabel">
                Shift Name:</td><td>
                    <asp:Label ID="ShiftNameLabel" Text='<%#Eval("ShortName") %>' runat="server"></asp:Label> 
                       (<asp:Label ID="TypeLabel" Text='<%#Eval("Type") %>' runat="server"></asp:Label>) &nbsp;&nbsp;
                    Minimum: <asp:Label ID="ShiftQuotaLabel" Text='<%#Eval("ShiftQuota") %>' runat="server"></asp:Label>
                     
                </td></tr>
             <tr><td class="formlabel">
                Date:</td><td>
                    <asp:Label ID="DateLabel" Text='<%#Eval("ShiftDate", "{0:D}") %>' runat="server"></asp:Label>
                </td></tr>
        </table>
    </ItemTemplate>

</asp:FormView>
    <div style="float:left">
<cc2:NQNHyperLink ID="ReturnButton" runat="server" Text="Return to Calendar"  NavigateUrl="ShiftCalendar.aspx"></cc2:NQNHyperLink>
 </div> <div style="float:left; padding-left:100px">
<cc2:NQNButton ID="AddButton" runat="server" Text="Add Drop-in" OnClick="ShowDropin" />    
      </div> <div style="float:left; padding-left:100px">
   <asp:HyperLink runat="server" ID="ReportLink" Target="_blank" NavigateUrl="~/Reports/Roster.aspx" Text="Report" Font-Bold="true"></asp:HyperLink>
          </div>
    <div style="clear:both"></div>
    <br />
    <cc2:NQNGridView ID="GridView1" runat="server" AutoGenerateColumns="False"  PageSize="50"
        DataSourceID="ObjectDataSource1" DataKeyNames="GuideID"  Caption="<b>Delete only removes drop-ins or special shifts.</b>"
        AllowMultiColumnSorting="False" 
        DeleteMessage="Are you sure you want to remove this guide from the shift for this date" Privilege="">
        <Columns>
         
                 <asp:CommandField ButtonType="Image" CancelImageUrl="~/Images/cancel.gif"  ShowEditButton="true" 
                     EditImageUrl="~/Images/iedit.gif" UpdateImageUrl="~/Images/save.gif" ShowDeleteButton="true" DeleteImageUrl="~/Images/delete.gif" />
                 
                 <asp:BoundField DataField="VolID" HeaderText="ID" SortExpression="VolInt"  ReadOnly="true"/>
            <asp:BoundField DataField="FirstName" HeaderText="First Name"  ReadOnly="true"
                SortExpression="FirstName" />
            <asp:BoundField DataField="LastName" HeaderText="Last Name"  ReadOnly="true"
                SortExpression="LastName" />
            <asp:BoundField DataField="PreferredPhone" HeaderText="Phone" SortExpression="PreferredPhone"  ReadOnly="true" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email"  ReadOnly="true" />
            <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes"  ReadOnly="true"/> 
            <asp:BoundField DataField="RoleName" HeaderText="Role"  ReadOnly="true"
                SortExpression="RoleName" />
           
            <asp:CheckBoxField DataField="SubRequested" HeaderText="Need Sub" 
                SortExpression="SubRequested" />
          <asp:BoundField DataField="Sub" HeaderText="Sub ID"  ControlStyle-Width="60"  />
            <asp:BoundField DataField="SubDescription" HeaderText="Sub Description"  ReadOnly="true"
                SortExpression="SubDescription" />
          
            
            
        </Columns>
    </cc2:NQNGridView>
    <asp:FormView ID="DropinView" runat="server"  DataSourceID="ObjectDataSource1"   DefaultMode="Insert" Visible ="false">
    <InsertItemTemplate>
      Enter Drop-in Guide ID: <asp:TextBox runat="server" ID="DropInTextBox" Text='<%#Bind("VolID") %>'  ></asp:TextBox>
      <br />
       <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                     CommandName="Insert" Text="Save" />
                 &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server"  OnClick="HideDropin" 
                     CausesValidation="False" CommandName="Cancel" Text="Cancel" />
    </InsertItemTemplate>
    </asp:FormView>
</asp:Content>
