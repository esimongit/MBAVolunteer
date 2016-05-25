<%@ Page Title="" Language="C#" MasterPageFile="~/Secondary.Master" AutoEventWireup="true" CodeBehind="ContactInfo.aspx.cs" Inherits="MBAV.ContactInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="NQN.DB.GuidesDM" SelectMethod="FetchGuide">
 <SelectParameters>
  
   <asp:QueryStringParameter QueryStringField="ID" Name="GuideID" Type="Int32" DefaultValue="0" />
 </SelectParameters>
</asp:ObjectDataSource>
<center>
<h3>Volunteer Guide Contact Info</h3>
<asp:FormView ID="FormView1" runat="server" DefaultMode="ReadOnly" DataSourceID="ObjectDataSource1" >
<ItemTemplate>
<table cellpadding="5">
  <tbody>
    <tr>
      <td><font size="-1">&nbsp;Name</font></td>
      <td><font size="-1">&nbsp;ID Nbr.</font></td>
      <td><font size="-1">Phone Nbr.</font></td>

<td><font size="-1">&nbsp;E-mail address</font></td>
    </tr>
    <tr>
      <td><asp:Label ID="NameLabel" runat="server"  Font-Bold="true" Text='<%#Eval("GuideName") %>'></asp:Label></td>
      <td align="center"><b><asp:Label ID="Label1"  Font-Bold="true" runat="server" Text='<%#Eval("VolID") %>'></asp:Label></b></td>
      <td><b><asp:Label ID="Label2" runat="server"   Font-Bold="true" Text='<%#Eval("Phone") %>'></asp:Label></b></td>

<td><asp:Label ID="Label3" runat="server" Font-Bold="true" Text='<%#Eval("Email") %>'></asp:Label></td>
    </tr>
      
    
  </tbody>
</table>
</ItemTemplate>
</asp:FormView>
<p></p> <input type="button" value="Close Window" onclick="self.close()" />
 <p></p>
</center>

 
</asp:Content>
