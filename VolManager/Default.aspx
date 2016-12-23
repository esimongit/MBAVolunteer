<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="VolManager._Default" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="NQN.Bus.SubstitutesBusiness" SelectMethod="SelectOpen"></asp:ObjectDataSource>
     <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" TypeName="NQN.Bus.ShiftsBusiness" SelectMethod="ShiftsToday"></asp:ObjectDataSource>
    <h2>
        Welcome to Volunteer Substitute Management Site
    </h2>
 
    <p> 
         <asp:Label ID="Label1" CssClass="formtext" runat="server" ></asp:Label> <br />
   
    </p>
    <asp:GridView DataSourceID="ObjectDataSource1" runat="server" AutoGenerateColumns="False" Caption="No Subs Today" >
        <Columns>
            <asp:BoundField DataField="ShiftName" HeaderText="Shift"   /> 
            <asp:BoundField DataField="GuideName" HeaderText="Guide" ReadOnly="True"  />
            <asp:BoundField DataField="VolID" HeaderText="ID"  />
            <asp:BoundField DataField="DateEntered" HeaderText="Date Entered"   />
            <asp:BoundField DataField="Role" HeaderText="Role"  />
            <asp:BoundField DataField="Email" HeaderText="Email"   />
            <asp:BoundField DataField="Phone" HeaderText="Phone"  />
        </Columns>
        <EmptyDataTemplate>No Missing Substitutes</EmptyDataTemplate>
    </asp:GridView>
    <asp:GridView DataSourceID="ObjectDataSource2" runat="server" AutoGenerateColumns="false" Caption="Today's Shifts">
        <Columns>
            <asp:BoundField DataField="ShiftName" HeaderText="Shift" />
            <asp:BoundField DataField="Captains" HeaderText="Captains" />
            <asp:BoundField DataField="Info" HeaderText="Info Desk" />
            <asp:BoundField DataField="Total" HeaderText="Planned Number" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right" />
        </Columns>

    </asp:GridView>
</asp:Content>
