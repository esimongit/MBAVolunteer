<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GuideImport.aspx.cs" Inherits="VolManager.GuideImport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="NQN.DB.BatchImportDM" SelectMethod="FetchAll"></asp:ObjectDataSource>
 <h3>Excel CSV File Import Instructions</h3><p>The imported file must have the following fields in order.  Some may be empty.  Records are identified by: <ul>

<li>ID or</li><li>First + Last + Email or</li>   
</ul></p>
<p>Fields:  ID, First, Last, Email, Phone, Cell,Shift,Role </p>
<p>Shift is the form "Mon2" or "Sat1A".</p>
<p>Role is one of: Aquarium Guide, Seasonal Guide, Shift Captain, TCL, Info Desk</p>
    <asp:FileUpload ID="FileUpload1" runat="server"  /> <asp:Button runat="server" ID="ImportButton" Text="Import" OnClick="ImportButton_Click" />
    <br />
 
<asp:Repeater ID="Repeater1" runat="server" DataSourceID="ObjectDataSource1" >
<HeaderTemplate>
<div style="color:Black">
<h3>Current Pending Records</h3>
<table><tr><th>ID</th><th>Last</th><th> First</th><th>Email</th><th>Phone</th><th>Cell</th><th>Shift</th><th>Role</th><th>Status</th></tr> 
 
</HeaderTemplate>
<ItemTemplate>
<tr id="Tr1" runat="server" style='<%#Eval("Color")%>' > 
<td><%#DataBinder.Eval(Container.DataItem,"ID") %></td>
<td><%#DataBinder.Eval(Container.DataItem, "First")%></td>
<td><%#DataBinder.Eval(Container.DataItem, "Last")%></td>
<td><%#DataBinder.Eval(Container.DataItem, "Email")%></td>
<td><%#DataBinder.Eval(Container.DataItem, "Phone")%></td> 
<td><%#DataBinder.Eval(Container.DataItem, "Cell")%></td> 
<td><%#DataBinder.Eval(Container.DataItem,"Shift") %></td>
<td><%#DataBinder.Eval(Container.DataItem,"Role") %></td>
<td><%#DataBinder.Eval(Container.DataItem,"RecordStatus") %></td>
</tr>
</ItemTemplate>
<FooterTemplate></table>
</div></FooterTemplate>
</asp:Repeater>
<cc2:NQNButton runat="server" OnClick="DoMerge" Text="Save Records" /> &nbsp;&nbsp;
<cc2:NQNButton ID="InactiveButton" runat="server" OnClick="DoInactive" Text="Change status to Inactive" /> &nbsp;&nbsp;
<cc2:NQNButton ID="DeleteButton" runat="server" OnClick="DoDelete" OnClientClick="return javascript:Confirm('Delete all matches in the batch?')" Text="Delete Records" />
</asp:Content>
