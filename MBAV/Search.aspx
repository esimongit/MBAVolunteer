<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="MBAV.Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="NQN.Bus.GuidesBusiness" SelectMethod="Search" >
    <SelectParameters>
      <asp:ControlParameter ControlID="PatternTextBox" Name="Pattern" Type="String" DefaultValue="" />
     
    </SelectParameters>
   
    </asp:ObjectDataSource>
       <style type="text/css">
 td, th
 {
     padding: 4px 4px 4px 4px
     
 }
 </style>
   <div class="row"> 
 <div class="col-md-10 col-md-offset-2">
<asp:Label ID="TitleLabel" runat="server" Font-Bold="true" ForeColor="Black" Font-Size="X-Large">Find Volunteers</asp:Label>
   </div>
      </div>
 <div class="row"> 
 <div class="col-xs-6 col-xs-offset-1 " style="font-size:large; font-weight:bold">
     Search for ID, first or last name: <asp:TextBox ID="PatternTextBox" runat="server"  Width="200px"  AutoCompleteType="None" ></asp:TextBox>
          <asp:Button ID="Button1" runat="server" Text="Search" OnClick="DoSearch"  /> 
   </div></div>
   
       <div class="row" style="padding-top:10px"> 
 <div class="col-xs-12">
   
    <cc2:NQNGridView ID="GridView1" runat="server" AutoGenerateColumns="False"  RowStyle-ForeColor="Black"  PageSize="100"   CssClass="table-responsive"
        DataKeyNames="GuideID"    AlternatingRowStyle-BackColor="White" AllowSorting="True" AllowMultiColumnSorting="False" DataSourceID="ObjectDataSource1" DeleteMessage="Are you sure you want to delete?" Privilege=""  >
<AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
        <Columns>
             
            <asp:BoundField DataField="VolID" HeaderText="ID" SortExpression="VolID" />
            <asp:BoundField DataField="FirstName" HeaderText="First Name" 
                SortExpression="FirstName" />
            <asp:BoundField DataField="LastName" HeaderText="Last Name" 
                SortExpression="LastName" />
             <asp:BoundField DataField="ShiftName" HeaderText="Shift" 
                SortExpression="Sequence" />
           <asp:BoundField DataField="RoleName" HeaderText="Role" 
                SortExpression="RoleName" />
            <asp:TemplateField HeaderText="Phone" SortExpression="Phone">
                
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("PreferredPhone") %>' Visible='<%#Eval("ShowContactInfo")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email" SortExpression="Email">
               
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Email") %>' Visible='<%#Eval("ShowContactInfo")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
   
        </Columns>

<RowStyle ForeColor="Black"></RowStyle>
        <EmptyDataTemplate>  
           
         <div class="col-md-10 col-md-offset-2"><h3 style="color:red">No matches found</h3>
             </div> 

        </EmptyDataTemplate>
         </cc2:NQNGridView>
     </div></div>
     <div class="row" style="padding-top:10px">
        
        
         <div class="col-xs-3 col-xs-offset-2"><asp:Button ID="UpdateCancelButton" runat="server" CssClass="btn btn-info"
              Text="Return to Calendar"  OnClick="ClosePage" />
        </div>
         </div>
</asp:Content>
