<%@ Page Language="C#" ValidateRequest="false" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="StaticFields.aspx.cs" Inherits="VolManager.StaticFields" Title="Static Fields" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="Delete"  UpdateMethod="Update" 
     OnUpdated="OnUpdated" SelectMethod="FetchAll" TypeName="NQN.DB.StaticFieldsDM" 
      OnDeleted="OnDeleted" DataObjectTypeName="NQN.DB.StaticFieldsObject">
       
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" TypeName="NQN.DB.StaticFieldsDM"
      SelectMethod="FetchTags"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" DataObjectTypeName="NQN.DB.StaticFieldsObject"
        TypeName="NQN.DB.StaticFieldsDM" OnInserted="OnInserted" InsertMethod="Save"></asp:ObjectDataSource>
    <asp:Button ID="AddButton" runat="server" Text="Add New Record" OnClick="AddButton_Clicked" />
     <asp:FormView ID="FormView2" runat="server" DataSourceID="ObjectDataSource3" DefaultMode="Insert" Visible="false"
      Caption="Insert New Record">
        <InsertItemTemplate>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter a Field Name "
             ControlToValidate="TagSelect" Display="Dynamic"></asp:RequiredFieldValidator>
           Field Name:
            <asp:DropDownList ID="TagSelect" runat="server" DataSourceID = "ObjectDataSource2" SelectedValue='<%#Bind("FieldName") %>'
             DataValueField="FieldName" DataTextField="FieldName" ></asp:DropDownList><br />
              Sequence:
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter a Sequence" ControlToValidate="SequenceTextBox">*</asp:RequiredFieldValidator>
            <asp:TextBox ID="SequenceTextBox" runat="server" Text ='<%#Bind("Sequence") %>'></asp:TextBox>
            <br />
             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter a Field Value "
             ControlToValidate="FieldValueTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
        
             Field Value:
            <asp:TextBox ID="FieldValueTextBox" runat="server" TextMode="MultiLine" Rows="10" Columns="80"
             Text='<%# Bind("FieldValue") %>'>
            </asp:TextBox><br />
            
            <cc2:NQNLinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                Text="Insert">
            </cc2:NQNLinkButton>
            <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                Text="Cancel" OnClick="InsertCancel">
            </asp:LinkButton>
        </InsertItemTemplate>
      
    </asp:FormView>
    <br />
    <cc2:NQNGridView ID="NQNGridView1" runat="server" AllowMultiColumnSorting="False"
        AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" Privilege="" DataKeyNames="StaticFieldID"
        AllowSorting="True">
        <Columns>
            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/delete.gif" SelectImageUrl="~/Images/select.gif"
                ShowDeleteButton="True" CancelImageUrl="~/Images/cancel.gif" 
                EditImageUrl="~/Images/iedit.gif" ShowEditButton="True" 
                UpdateImageUrl="~/Images/save.gif" />
            <asp:BoundField DataField="FieldName" HeaderText="Field Name" SortExpression="FieldName" 
            ReadOnly="true" />
            <asp:BoundField DataField="Description" HeaderText="Description"  ReadOnly="True" SortExpression="Description" />
            <asp:BoundField DataField="Sequence" HeaderText="Sequence" SortExpression="Sequence" />
            <asp:TemplateField HeaderText="Field Value" SortExpression="FieldValue">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server"  TextMode="MultiLine" Rows="4" Columns="80" Text='<%# Bind("FieldValue") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("FieldValue") %>'></asp:Label>
                </ItemTemplate>
                
            </asp:TemplateField>
        </Columns>
    </cc2:NQNGridView>

</asp:Content>
