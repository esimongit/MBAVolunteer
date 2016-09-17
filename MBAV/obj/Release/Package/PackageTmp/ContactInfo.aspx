<%@ Page Title="" Language="C#" MasterPageFile="~/Secondary.Master" AutoEventWireup="true" CodeBehind="ContactInfo.aspx.cs" Inherits="MBAV.ContactInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="NQN.DB.GuidesDM" SelectMethod="FetchGuide">
 <SelectParameters>
  
   <asp:QueryStringParameter QueryStringField="ID" Name="GuideID" Type="Int32" DefaultValue="0" />
     <asp:SessionParameter SessionField="IsCaptain" Name="IsCaptain" Type="Boolean" DefaultValue="false" />
 </SelectParameters>
</asp:ObjectDataSource>
 <div style="width:400px; margin-left:auto; margin-right:auto"> 
<h3>Volunteer Guide Contact Info</h3>
<asp:FormView ID="FormView1" runat="server" DefaultMode="ReadOnly" DataSourceID="ObjectDataSource1" >
<ItemTemplate>
<table border="1">
  <tbody>
    <tr>
      <td><font size="-1">Name</font></td>
      <td><font size="-1">ID</font></td>
      <td><font size="-1">Phone</font></td>

<td><font size="-1">Email</font></td>
    </tr>
    <tr>
      <td><asp:Label ID="NameLabel" runat="server"  Font-Bold="true" Text='<%#Eval("GuideName") %>'></asp:Label></td>
      <td align="center"><b><asp:Label ID="Label1"  Font-Bold="true" runat="server" Text='<%#Eval("VolID") %>'></asp:Label></b></td>
      <td><b><asp:Label ID="Label2" runat="server"   Font-Bold="true" Visible ='<%#Eval("ShowContactInfo") %>' Text='<%#Eval("Phone") %>'></asp:Label></b></td>

<td><asp:Label ID="Label3" runat="server" Font-Bold="true" Visible ='<%#Eval("ShowContactInfo") %>'  Text='<%#Eval("Email") %>'></asp:Label></td>
    </tr>
      
    
  </tbody>
</table>
</ItemTemplate>
</asp:FormView>
     <br />
 <input type="button" value="Close Window" onclick="self.close()" />
 
</div>

 
</asp:Content>
