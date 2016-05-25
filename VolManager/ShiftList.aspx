<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShiftList.aspx.cs" Inherits="VolManager.ShiftList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        TypeName="NQN.DB.ShiftsDM" SelectMethod="FetchAll" 
        DataObjectTypeName="NQN.DB.ShiftsObject" UpdateMethod="Update">
  
</asp:ObjectDataSource>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataSourceID="ObjectDataSource1" DataKeyNames="ShiftID">
        <Columns> 
            <asp:BoundField DataField="ShiftName" HeaderText="Shift Name"  />
           
            <asp:CheckBoxField DataField="AWeek" HeaderText="A Week" 
                 />
            <asp:CheckBoxField DataField="BWeek" HeaderText="B Week" 
                />
            <asp:BoundField DataField="Sequence" HeaderText="Sequence"  ReadOnly="true"/>
         
            <asp:BoundField DataField="ShortName" HeaderText="Short Name"   />
             <asp:BoundField DataField="ShiftStart" HeaderText="Shift Start"  DataFormatString="{0:h\:mm tt}" ItemStyle-HorizontalAlign="Right"  />
              <asp:BoundField DataField="ShiftEnd" HeaderText="Shift End"  DataFormatString="{0:h\:mm tt}" ItemStyle-HorizontalAlign="Right" />
         <asp:BoundField DataField="Captains" HeaderText="Captains" />
         <asp:BoundField DataField="InfoDesk" HeaderText="Info Desk" />
        </Columns>
    </asp:GridView>
</asp:Content>
