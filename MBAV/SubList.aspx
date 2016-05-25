<%@ Page Title="" Language="C#" MasterPageFile="~/Secondary.Master" AutoEventWireup="true" CodeBehind="SubList.aspx.cs" Inherits="MBAV.SubList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<asp:ObjectDataSource ID="ShiftsDataSource" runat="server" TypeName="NQN.DB.ShiftsDM" SelectMethod="FetchAll">
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="NQN.DB.SubOffersDM" SelectMethod="FetchForShift">
<SelectParameters>
 <asp:ControlParameter ControlID="ShiftSelect" Name="ShiftID" Type="Int32" DefaultValue="0" />
</SelectParameters>
</asp:ObjectDataSource>
 <div style="margin-left:auto; margin-right:auto;width:300px">
<asp:Label ID="TitleLabel" runat="server" Font-Bold="true" ForeColor="Black" Font-Size="X-Large"></asp:Label>
   </div>

 <div style="float:right">
 
   Shift: <asp:DropDownList ID="ShiftSelect" runat="server" DataSourceID="ShiftsDataSource" DataValueField="ShiftID" DataTextField="ShiftName"
     AutoPostBack="true" OnSelectedIndexChanged="DoList"></asp:DropDownList>
   </div>
 <div class="clear"></div>
 <asp:GridView ID="GridView1" runat="server" DataSourceID = "ObjectDataSource1" 
        DataKeyNames="GuideID" AutoGenerateColumns="false"
  Data CellPadding="3" CellSpacing="4" GridLines="None">
 <Columns>
 <asp:BoundField DataField="GuideName" HeaderText="Name" />
  <asp:BoundField DataField="HomeShift" HeaderText="Home Shift" />
   <asp:BoundField DataField="VolID" HeaderText="ID Nbr." />
   <asp:BoundField  DataFormatString="&lt;a href=mailto:{0}&gt;{0}&lt;/a&gt;" HtmlEncodeFormatString="false" DataField="Email" HeaderText="E-mail address"  />
     <asp:BoundField DataField="Phone" HeaderText="Phone Nbr." />
 </Columns>
 </asp:GridView>

<p> <input type='button' value='Close Window' onClick='self.close()' />
 </p>
 
</asp:Content>
