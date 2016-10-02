<%@ Page Title="" Language="C#" MasterPageFile="~/Secondary.Master" AutoEventWireup="true" CodeBehind="SubList.aspx.cs" Inherits="MBAV.SubList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<asp:ObjectDataSource ID="ShiftsDataSource" runat="server" TypeName="NQN.DB.ShiftsDM" SelectMethod="FetchAll">
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="NQN.DB.SubOffersDM" SelectMethod="FetchForShift">
<SelectParameters>
 <asp:ControlParameter ControlID="ShiftSelect" Name="ShiftID" Type="Int32" DefaultValue="0" />
    <asp:SessionParameter SessionField="IsCaptain" Name="IsCaptain" Type="Boolean" DefaultValue="false" />
</SelectParameters>
</asp:ObjectDataSource>
    <div class="row"> 
 <div class="col-md-10 col-md-offset-2">
<asp:Label ID="TitleLabel" runat="server" Font-Bold="true" ForeColor="Black" Font-Size="X-Large"></asp:Label>
   </div>
      </div>
 <div class="row"> 
 <div class="col-xs-3 col-xs-offset-6" style="font-size:large; font-weight:bold">
   Shift: <asp:DropDownList ID="ShiftSelect" runat="server" DataSourceID="ShiftsDataSource" DataValueField="ShiftID" DataTextField="ShiftName"
     AutoPostBack="true" OnSelectedIndexChanged="DoList"></asp:DropDownList>
   </div></div>
  <div class="row" style="padding-top:10px"> 
 <div class="col-xs-12">
 <asp:GridView ID="GridView1" runat="server" DataSourceID = "ObjectDataSource1"  Width="100%"
        DataKeyNames="GuideID" AutoGenerateColumns="False" HeaderStyle-Font-Size="Large"
    CellPadding="5" CellSpacing="4" GridLines="Both" HorizontalAlign="Center">
 <Columns>
 <asp:BoundField DataField="GuideName" HeaderText="Name" />
  <asp:BoundField DataField="HomeShift" HeaderText="Home Shift" />
   <asp:BoundField DataField="VolID" HeaderText="ID" />
     <asp:TemplateField HeaderText="E-mail address">
        
         <ItemTemplate>
             <asp:Label ID="Label1" runat="server" Visible='<%#Eval("ShowContactInfo") %>' Text='<%# Bind("Email", "<a href=mailto:{0}>{0}</a>") %>'></asp:Label>
         </ItemTemplate>
     </asp:TemplateField>
     <asp:TemplateField HeaderText="Phone">
        
         <ItemTemplate>
             <asp:Label ID="Label2"  Visible='<%#Eval("ShowContactInfo") %>' runat="server" Text='<%# Bind("Phone") %>'></asp:Label>
         </ItemTemplate>
     </asp:TemplateField>
 </Columns>

<HeaderStyle Font-Size="Large"></HeaderStyle>
     <RowStyle HorizontalAlign="Center" />
 </asp:GridView>
    </div></div>
<div class="row" style="padding-top:20px">
    <div class="col-md-5 col-md-offset-3">
<asp:Button ID="CloseButton" runat="server" CSSclass="btn btn-info" Text="Close Window"   OnClientClick='javascript:self.close()' />
</div> </div>
 
</asp:Content>
