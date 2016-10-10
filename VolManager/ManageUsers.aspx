<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="VolManager.ManageUsers" Title="Manage Users" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="SelectAll" TypeName="NQN.Bus.MembershipBusiness" UpdateMethod="UpdateRole"
         DeleteMethod="Delete" OnDeleted="OnDeleted">
      
     <DeleteParameters>
       <asp:Parameter Name="UserName" Type="string" />
     </DeleteParameters>   
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="RolesDataSource" runat="server" SelectMethod="SelectAll"
        TypeName="NQN.Bus.RolesBusiness"  >
        </asp:ObjectDataSource>
  
    <asp:ObjectDataSource ID="InsertDataSource" runat="server" InsertMethod="Insert"
      TypeName="NQN.Bus.MembershipBusiness"   OnInserted="OnInserted">
    
      </asp:ObjectDataSource>
  
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
 
  <asp:Button ID="Button1" runat="server" Text="Add new" OnClick="AddButtonClick"/><br />
  
        <cc2:NQNGridView ID="NQNGridView1" runat="server" AllowMultiColumnSorting="False" OnSelectedIndexChanged="GridView_Selected"
            AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" DataKeyNames="UserName" Privilege="" AllowSorting="True"
             PageSize="25">
            <Columns>
                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/delete.gif" 
                ShowDeleteButton="True" CancelImageUrl="~/Images/cancel.gif" 
                EditImageUrl="~/Images/iedit.gif"  
                UpdateImageUrl="~/Images/save.gif"  />
                <asp:CommandField ButtonType="Button"  HeaderText="Reset Password" 
                      ShowSelectButton="True" SelectText="Reset" />
                <asp:BoundField DataField="UserName" HeaderText="User Name" ReadOnly="True"   />
                <asp:BoundField DataField="Email" HeaderText="Email" ReadOnly="True"  />
                <asp:TemplateField HeaderText="Role">
                    
                    <itemtemplate>
                        <asp:Label runat="server" Text='<%# Bind("Comment") %>' id="Label1"></asp:Label>
                    </itemtemplate>
                </asp:TemplateField>
                 <asp:BoundField DataField="LastLoginDate" HeaderText="Last Login" ReadOnly="True" />
                <asp:CheckBoxField DataField="IsOnline" HeaderText="IsOnline" ReadOnly="True"   />
                 <asp:CheckBoxField DataField="IsLockedOut" HeaderText="Locked" ReadOnly="True"   />
                 
            </Columns>
        </cc2:NQNGridView>
    </asp:View>
         
        <asp:View ID="View3" runat="server">
            <asp:FormView ID="FormView2" runat="server" DataSourceID="InsertDataSource" DefaultMode="Insert">
        
        <InsertItemTemplate>
        <table><tr><td class="formlabel">
            User Name: </td><td> <asp:TextBox ID="UserNameTextBox" runat="server" Text='<%# Bind("UserName") %>'></asp:TextBox><br />
           </td></tr>
           <tr><td class="formlabel">
            Email:</td><td>
         
            <asp:TextBox ID="EmailTextBox" runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
              </td></tr>
           <tr><td class="formlabel">
             Role:</td><td>
            <asp:DropDownList ID="DropDownList1" runat="server" SelectedValue='<%#Bind("role") %>' DataSourceID="RolesDataSource"
              DataTextField="RoleName" DataValueField="RoleName"   >
            </asp:DropDownList>
            </td></tr></table>
            <cc2:NQNLinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                Text="Insert"></cc2:NQNLinkButton>
            <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                Text="Cancel" OnClick="InsertCancelButton_Click"></asp:LinkButton>
        </InsertItemTemplate>
        
    </asp:FormView>
        </asp:View>
    </asp:MultiView> 
</asp:Content>
