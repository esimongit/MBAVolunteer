<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageList.ascx.cs" Inherits="VolManager.MessageList" %>
 Mail Message to Edit: 
    <cc2:NQNGridView ID="MailSelect" runat="server" DataSourceID = "ObjectDataSource1"
       OnSelectedIndexChanged="MailSelected" AutoGenerateColumns="False" Selectable="true"
        DataKeyNames="MailTextID" AllowMultiColumnSorting="False" 
    EnableModelValidation="True" Privilege="">
        <Columns>
         <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/delete.gif" 
                ShowDeleteButton="True" />
            <asp:BoundField DataField="Symbol" HeaderText="Symbol"  />
            <asp:BoundField DataField="Description" HeaderText="Description"  />
            <asp:BoundField DataField="MailFrom" HeaderText="MailFrom"  />
            <asp:BoundField DataField="Subject" HeaderText="Subject"  />
            <asp:CheckBoxField DataField="Enabled" HeaderText="Enabled" />
            <asp:CheckBoxField DataField="IsHtml" HeaderText="HTML" />
           
        </Columns>
       </cc2:NQNGridView>