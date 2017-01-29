<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShiftTimes.aspx.cs" Inherits="VolManager.ShiftTimes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        DataObjectTypeName="NQN.DB.ShiftTimesObject" InsertMethod="Save"  OnInserted="OnInserted"
        SelectMethod="FetchAll" TypeName="NQN.DB.ShiftTimesDM" UpdateMethod="Update">
    </asp:ObjectDataSource>
    
    <asp:Button ID="AddButton" runat="server" Text="Add a new Start and End Time" OnClick ="ShowAdd" />
    <asp:FormView ID="AddForm" runat="server"  DataSourceID="ObjectDataSource1" DefaultMode="Insert" Visible="false">
        <InsertItemTemplate>
            <table>
                <tr><td class="formlabel">Time Slot Name:</td>
                    <td> <asp:TextBox ID="NameTextBox" runat="server" Text='<%#Bind("TimeSlotName") %>'></asp:TextBox>
                    </td>
                </tr>
                 <tr><td class="formlabel">Shift Start Time:</td>
                     <td>  <asp:TextBox ID="StartTimeTextBox" runat="server" Text='<%#Bind("ShiftStart") %>'></asp:TextBox>
                   </td>
                </tr>
                 <tr><td class="formlabel">Shift End Time:</td>
                     <td> <asp:TextBox ID="EndTimeTextBox" runat="server" Text='<%#Bind("ShiftEnd") %>'></asp:TextBox>
                   </td>
                </tr>
                  
            </table>
            <cc2:NQNLinkButton ID="SaveButton" runat="server" CommandName="Insert" Text="Save"></cc2:NQNLinkButton>&nbsp;&nbsp;
            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" OnClick="HideAdd" Text="Cancel" />
        </InsertItemTemplate>
    </asp:FormView>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataSourceID="ObjectDataSource1" DataKeyNames="ShiftTimeID">
        <Columns>
            <asp:CommandField ButtonType="Image" CancelImageUrl="~/Images/cancel.gif" 
                EditImageUrl="~/Images/iedit.gif" ShowEditButton="True" 
                UpdateImageUrl="~/Images/save.gif" />
            <asp:BoundField DataField="TimeSlotName" HeaderText="Time Slot Name" />
            <asp:BoundField DataField="ShiftStart" DataFormatString="{0:t}"  ApplyFormatInEditMode="true"
                HeaderText="Shift Start"  />
            <asp:BoundField DataField="ShiftEnd" DataFormatString="{0:t}"   ApplyFormatInEditMode="true"
                HeaderText="Shift End" />
        </Columns>
    </asp:GridView>
    

</asp:Content>
