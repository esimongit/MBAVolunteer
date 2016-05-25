<%@ Page Language="C#" MasterPageFile="~/Site.master" ValidateRequest="false"  EnableEventValidation="false"
   AutoEventWireup="true" CodeBehind="TextEditor.aspx.cs" Inherits="VolManager.TextEditor" 
   Title="Mail Text Editor" %>
<%@ Register Src="~/UserControls/MessageList.ascx" TagName="MessageList" TagPrefix="uc3" %>
<%@ Register Src="~/UserControls/MessageEdit.ascx" TagName="MessageEdit" TagPrefix="uc3" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="NQN.DB.MailTextDM"
     SelectMethod="FetchAll" DeleteMethod="Delete" OnDeleted="OnDeleted">
     
     </asp:ObjectDataSource>
    
    <atk:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"  >
    </atk:ToolkitScriptManager>  
    <asp:MultiView ID="MultiView1" runat="server">
    <asp:View runat="server" ID="View1">
     
     <uc3:MessageList ID="MessageList1" runat="server"  OnMessageSelected="MailSelected"/>
   
    </asp:View>
    <asp:View ID="View2" runat="server">
     
    <uc3:MessageEdit id="MessageEdit1" runat="server" OnMessageChanged="RebindList" CanClone = "true"></uc3:MessageEdit>
    
<asp:Button ID="ReturnButton" Text="Return to List" runat="server" OnClick="ToView1" />
 </asp:View>
 </asp:MultiView> 
</asp:Content>
