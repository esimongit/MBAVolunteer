<%@ Page Title="" Language="C#" MasterPageFile="~/Secondary.Master" AutoEventWireup="true" CodeBehind="ContactInfo.aspx.cs" Inherits="MBAV.ContactInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="NQN.DB.GuidesDM" SelectMethod="FetchGuide">
 <SelectParameters>
  
   <asp:QueryStringParameter QueryStringField="ID" Name="GuideID" Type="Int32" DefaultValue="0" />
     <asp:SessionParameter SessionField="IsCaptain" Name="IsCaptain" Type="Boolean" DefaultValue="false" />
 </SelectParameters>
</asp:ObjectDataSource>
   <style type="text/css">
 td, th
 {
     padding: 4px 4px 4px 4px
     
 }
 </style>
 
     <div class="row">
         <div class="col-xs-offset-1 col-xs-10" style="align-content:center">
<h3 ">Volunteer Guide Contact Info</h3>
         </div>
         </div>
     <div class="row"> 
         </div>
<asp:FormView ID="FormView1" runat="server" DefaultMode="ReadOnly" DataSourceID="ObjectDataSource1" >
<ItemTemplate>
<table border="1" class="table-responsive">
  <tbody>
    <tr style="background-color:aqua">
      <th><font size="-1">Name</font></th>
      <th><font size="-1">ID</font></th>
      <th><font size="-1">Phone</font></th>

<th><font size="-1">Email</font></th>
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
     </div></div>
     
    <div class="row"> 
        <div class="col-xs-offset-1 col-xs-10" >
 <input style="align-self:center" type="button" value="Close Window" onclick="self.close()" />
            </div></div>

 
 

 
</asp:Content>
