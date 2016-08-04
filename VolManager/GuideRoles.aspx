<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GuideRoles.aspx.cs" Inherits="VolManager.GuideRoles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        DataObjectTypeName="NQN.DB.RolesObject" DeleteMethod="Delete"  OnDeleted="OnDeleted"
        InsertMethod="Save" SelectMethod="FetchAll" TypeName="NQN.DB.RolesDM" 
        UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="pkey" Type="Int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>
    <cc2:NQNGridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataSourceID="ObjectDataSource1" DataKeyNames="RoleID">
        <Columns>
            <asp:CommandField ButtonType="Image" CancelImageUrl="~/Images/cancel.gif" 
                DeleteImageUrl="~/Images/delete.gif" EditImageUrl="~/Images/iedit.gif" 
                ShowDeleteButton="True" ShowEditButton="True" 
                UpdateImageUrl="~/Images/save.gif" />
           
            <asp:BoundField DataField="RoleName" HeaderText="Role Name"  />
            <asp:CheckBoxField DataField="IsCaptain" HeaderText="Is Captain"  />
            <asp:CheckBoxField DataField="MaskContactInfo" HeaderText="Mask Info"  />
            <asp:BoundField DataField="Number" HeaderText="Current Count" 
               ReadOnly = "true" />
        </Columns>
    </cc2:NQNGridView>
    Add New Role:
    <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" DefaultMode="Insert">
        <InsertItemTemplate>
           
         
            Role Name:
            <asp:TextBox ID="RoleNameTextBox" runat="server" 
                Text='<%# Bind("RoleName") %>' />
            <br />
            Is Captain:
            <asp:CheckBox ID="IsCaptainCheckBox" runat="server" 
                Checked='<%# Bind("IsCaptain") %>' />
       <br />
            Mask Contact Info:
            <asp:CheckBox ID="CheckBox1" runat="server" 
                Checked='<%# Bind("MaskContactInfo") %>' />
            <br />
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                CommandName="Insert" Text="Insert" />
           
        </InsertItemTemplate>
  
    </asp:FormView>
</asp:Content>
