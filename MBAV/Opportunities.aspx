﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Opportunities.aspx.cs" Inherits="MBAV.Opportunities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="NQN.DB.GuideSubstituteDM" SelectMethod="FetchRequests">
        <SelectParameters>
            <asp:SessionParameter SessionField="GuideID" Name="SubstituteID" Type="Int32" DefaultValue="0" />
            <asp:SessionParameter SessionField="RoleID" Name="RoleID" Type="Int32" DefaultValue="0" />
        </SelectParameters>
    </asp:ObjectDataSource>
      <style type="text/css">
 td, th
 {
     padding: 4px 4px 4px 4px
     
 }
 </style>
    <div class="row">
        <div class="col-md-10">
            <h1>Available Requests for My Selected Shifts</h1>
        </div>
    </div>
      <div class="row">
          <div class="col-md-12">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" AlternatingRowStyle-BackColor="Beige"  
          DataKeyNames="GuideSubstituteID" CellPadding="0"  HeaderStyle-Font-Bold="true">
<AlternatingRowStyle BackColor="Beige"></AlternatingRowStyle>
        <Columns>
            <asp:HyperLinkField HeaderText="Date (Click to Sub)" DataNavigateUrlFormatString="~/SubRequest.aspx?dt={0}" Text="Select" DataNavigateUrlFields="dtString"  DataTextField="SubDate" DataTextFormatString="{0:D}" />
             <asp:BoundField DataField="ShortName" HeaderText="Shift" SortExpression="ShortName" />
            <asp:BoundField DataField="VolID" HeaderText="Volunteer #" SortExpression="VolID" />
            <asp:BoundField DataField="GuideName" HeaderText="Volunteer Name" ReadOnly="True" SortExpression="GuideName" />
            <asp:BoundField DataField="Role" HeaderText="Role" SortExpression="Role" />
            <asp:TemplateField HeaderText="Email" SortExpression="Email">
               
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Email") %>' Visible='<%#Eval("ShowContactInfo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Phone" SortExpression="Phone">
               
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Phone") %>' Visible='<%#Eval("ShowContactInfo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

<HeaderStyle Font-Bold="True"></HeaderStyle>
    </asp:GridView>
</div></div>
    <div class="row" style="padding-top:10px"><div class="col-md-6 col-md-offset-2">
        <asp:Button ID="Button1" runat="server" CSSclass="btn btn-info" Text="Close Window"   OnClientClick='javascript:self.close()' />

        <%-- <asp:HyperLink ID="ReturnButton" runat="server" CssClass="btn btn-info"
              Text="Return to Profile"   NavigateUrl="~/SubstituteCalendar.aspx" />--%>
  </div></div>
</asp:Content>
