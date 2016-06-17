<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageVolAccess.aspx.cs" Inherits="VolManager.ManageVolAccess" 
Title="Manage Volunteer Logins" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="SelectVols" TypeName="NQN.Bus.MembershipBusiness"  
         DeleteMethod="DeleteVols" OnDeleted="OnDeleted">
    <SelectParameters>
     <asp:ControlParameter ControlID="PatternTextBox" Name="Pattern" Type="String" DefaultValue="" />
     <asp:ControlParameter ControlID="DupCheckBox" Name="DupsOnly" Type="Boolean" DefaultValue="0" />
    </SelectParameters>
      
    </asp:ObjectDataSource>
     
    <asp:ObjectDataSource ID="InsertDataSource" runat="server" InsertMethod="InsertVols"
      TypeName="NQN.Bus.MembershipBusiness"   OnInserted="OnInserted">
      <InsertParameters>
         <asp:Parameter Name="UserName" Type="String" />
          <asp:Parameter Name="Email" Type="String" />
          
      </InsertParameters>
      </asp:ObjectDataSource>
  
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
 <table id="searchtable" width="100%" runat="server" border="1"  >
        <tr>
            <td>
                <asp:Label ID="SearchLabel" runat="server"  ForeColor="White" Font-Bold="True" Text="Search Criteria" ></asp:Label></td><td colspan="2">
               <asp:Button ID="Button2" runat="server" Text="Add new" OnClick="AddButtonClick"/> 
                <asp:Button ID="Button1" runat="server" Text="Generate All" OnClick="GenerateAll"/> 
               </td></tr>
                
    <tr><td style="height: 51px">
        <asp:Button ID="SearchButton" runat="server" Text="Search" />
          
      
        <asp:TextBox ID="PatternTextBox" runat="server"  Width="200px"  AutoPostBack="true"   ></asp:TextBox>
        Search by First, Last, ID or Email.
    </td><td style="height: 51px" > <asp:CheckBox ID="DupCheckBox" runat="server" Text="Duplicates Only" AutoPostBack="true" />
        </td>
      </tr>
    </table>
    <cc2:NQNGridView ID="NQNGridView1" runat="server" AllowMultiColumnSorting="False" OnSelectedIndexChanged="GridView_Selected" PageSize="50"
            AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" DataKeyNames="UserName" Privilege="" AllowSorting="True" BackColor="#ffffff"
             CellPadding="2" ForeColor="Black">
            <Columns>
                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/delete.gif" 
                ShowDeleteButton="True" CancelImageUrl="~/Images/cancel.gif" 
                EditImageUrl="~/Images/iedit.gif" ShowEditButton="False" 
                UpdateImageUrl="~/Images/save.gif"  />
                <asp:CommandField ButtonType="Button"  HeaderText="Reset Password" 
                      ShowSelectButton="True" SelectText="Reset" />
                 <asp:TemplateField  HeaderText="Name  ">
                  <ItemTemplate>
                       <asp:Label ID="nameLabel"
                          Text='<%#Eval("First") + " " + Eval("Last") %>' runat="server" ></asp:Label>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="UserName" HeaderText="Login" ReadOnly="True"  SortExpression="UserName"  />          
                 <asp:BoundField DataField="Email" HeaderText="Email" ReadOnly="True"  SortExpression="Email"  />                 
                 <asp:BoundField DataField="LastLoginDate" HeaderText="Last Login" ReadOnly="True"  SortExpression="LastLoginDate" />
                <asp:CheckBoxField DataField="IsOnline" HeaderText="IsOnline" ReadOnly="True"   SortExpression="IsOnLine" />
                <asp:CheckBoxField DataField="IsLockedOut" HeaderText="Locked" ReadOnly="True"  SortExpression="IsLockedOut"  />
            </Columns>
        </cc2:NQNGridView>
    </asp:View>
         
        <asp:View ID="View3" runat="server">
            <asp:FormView ID="FormView2" runat="server" DataSourceID="InsertDataSource" DefaultMode="Insert">
        
        <InsertItemTemplate>
            User Name:
            <asp:TextBox ID="UserNameTextBox" runat="server" Text='<%# Bind("UserName") %>'></asp:TextBox><br />
            Email:
            <asp:TextBox ID="EmailTextBox" runat="server" Text='<%# Bind("Email") %>'></asp:TextBox><br />
             
            <cc2:NQNLinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                Text="Insert"></cc2:NQNLinkButton>
            <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                Text="Cancel" OnClick="InsertCancelButton_Click"></asp:LinkButton>
        </InsertItemTemplate>
        
    </asp:FormView>
        </asp:View>
    </asp:MultiView> 
</asp:Content>
