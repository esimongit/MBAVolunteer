<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MBASecurityManager.Recovery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
   <asp:Label ID="LoginLabel" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label><br />
   <asp:Label ID="ErrorLabel" Font-Bold= "true" ForeColor="Red" Font-Size="Large" runat="server"></asp:Label><br />
   <table><tr><td>
    Enter New Password: </td><td><asp:TextBox ID="pw1" runat="server" TextMode="Password"></asp:TextBox>
    </td></tr>
    <tr><td>
  Re-enter Password: </td><td><asp:TextBox ID="pw2" runat="server" TextMode="Password"></asp:TextBox>
  </td></tr></table>
  <asp:Button ID="SubmitButton" runat="server" Text="Submit" OnClick="Change" />
    </div>
    </form>
</body>
</html>
