<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShiftTimes.aspx.cs" Inherits="VolManager.ShiftTimes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        DataObjectTypeName="NQN.DB.ShiftTimesObject" InsertMethod="Save" 
        SelectMethod="FetchAll" TypeName="NQN.DB.ShiftTimesDM" UpdateMethod="Update">
    </asp:ObjectDataSource>
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
