<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Generator.aspx.cs" Inherits="CodeGenerator.Generator" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       
    </div>
        Select a Table:&nbsp;
        <asp:DropDownList ID="DropDownList1" runat="server"   AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
        <asp:ListItem>(Select a Table)</asp:ListItem>
        </asp:DropDownList>
        <br />
        Object ClassName: &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:TextBox ID="ObjectClassTextBox" runat="server" Width="222px"></asp:TextBox><br />
        Data Access Class Name:
        <asp:TextBox ID="DMClassTextBox" runat="server" Width="214px" ></asp:TextBox><br />
        Output Folder: &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:TextBox ID="FolderTextBox" runat="server" Width="281px"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Generate Code" />
        <asp:Label ID="Label1" runat="server" ></asp:Label>
    </form>
</body>
</html>
