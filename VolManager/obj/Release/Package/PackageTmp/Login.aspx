<%@ Page Title="Log In" Language="C#"  AutoEventWireup="true"  CodeBehind="Login.aspx.cs" Inherits="VolManager.Login" %>
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
  <title>MBA Volunteer Substitute Management</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
   <style>
   body
   {
        background: #b6b7bc;
    font-size: .80em;
    font-family: "Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
    margin: 0px;
    padding: 0px;
    color: #323232;
   }
   fieldset.login label, fieldset.register label, fieldset.changePassword label
    {
        display: block;
         
    }

    fieldset label.inline 
    {
        display: inline;
    }
</style>
</head>
<body >
 <form runat="server">
    <h2>
        Log In
    </h2>
    <p>
        Please enter your username and password.
        
    </p>
    <asp:Login ID="LoginUser" runat="server" EnableViewState="false"  ARenderOuterTable="false" >
        <LayoutTemplate>
            <span class="failureNotification">
                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
            </span>
            <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification" 
                 ValidationGroup="LoginUserValidationGroup"/>
            <div class="accountInfo">
                <fieldset class="login">
                    <legend>Account Information</legend>
                    <p>
                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username:</asp:Label>
                        <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                             CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                        <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry"   TextMode="Password"  ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                             CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:CheckBox ID="RememberMe" runat="server"/>
                        <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Keep me logged in</asp:Label>
                    </p>
                </fieldset>
                 
                <div style="clear:both"></div>
                <div >
                    <div style="float:left; padding-left: 20px">
                    <asp:Button ID="LoginButton" runat="server"  OnClick="Login_OnClick" Text="Log In" ValidationGroup="LoginUserValidationGroup"/>
                        </div>
                    
              </div>
            </div>
     </LayoutTemplate>
    </asp:Login>
      <div style="padding-top:40px" >
                    <asp:Button ID="Button1" runat="server"  Text="Forgot my Password" OnClick="ResetPW"/>
                         </div>
 </form>
 </body>
 </html>