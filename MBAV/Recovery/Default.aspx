<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MBASecurityManager.Recovery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="font-size:large; font-family:arial">
    <form id="form1" runat="server">
    <div style="padding:4px 4px 4px 4px">
    
   <asp:Label ID="LoginLabel" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label><br />
   <asp:Label ID="ErrorLabel" Font-Bold= "true" ForeColor="Red" Font-Size="Large" runat="server"></asp:Label><br />
      <div style="padding:4px 4px 4px 4px">
    Enter New Password:  <asp:TextBox ID="pw1" runat="server" TextMode="Password"></asp:TextBox>
   </div>
 <div style="padding:4px 4px 4px 4px">
  Re-enter Password:&nbsp;&nbsp;&nbsp;  <asp:TextBox ID="pw2" runat="server" TextMode="Password"></asp:TextBox>
  </div>
   <div style="float:left; padding-left:150px; padding-top:10px">
  <asp:Button ID="SubmitButton" runat="server" Font-Bold="true" Font-Size="Large" Text="Submit" OnClick="Change" />
    </div>
        </div>
    </form>
</body>
</html>
