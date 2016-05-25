<%@ Page Title="Log In" Language="C#"  AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="MBAV.Account.Login" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
<head id="Head1" runat="server">
  <title>MBA Volunteer Substitute Calendar</title>
     <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="/Content/bootstrap.css" rel="Stylesheet" />
    <script src="/Scripts/common.js" type="text/javascript" ></script>
    
 
</head>
<body style="font-family:Arial">
 
<%--<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">--%>
<form runat="server">
<div class="container">
<div class="row">
    <h2>
       Monterey Bay Aquarium <br />
       Volunteer Guide Substitute Log In
    </h2>
    </div>
<div class="row">
<div class="col-xs-12">
    <p>
        Please enter your Guide ID and password.
    
    </p>
    </div>
    </div>
   <div class="row">
    <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false" OnLoggedIn="SaveUser"  >
        <LayoutTemplate>
            
                <asp:Label ID="FailureText"  runat="server" ForeColor="Red"></asp:Label>
        
            <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"  />
           <div class="row">
            <div class="col-xs-12">
                
                    <h2>Account Information</h2>
                          
               </div></div>
                     <div class="row">     
                     <div class="col-xs-12">   
                        <asp:Label ID="UserNameLabel" runat="server" Font AssociatedControlID="UserName">Username:</asp:Label>
                        <asp:TextBox ID="UserName" runat="server" CssClass="textEntry" Width="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                             CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                             >*</asp:RequiredFieldValidator>
               </div></div>
        <div class="row" style="padding-top:20px">
            <div class="col-xs-12">
                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                        <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" Width="140" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                             CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                             >*</asp:RequiredFieldValidator>
                   </div></div>
                     <div class="row">
            <div class="col-xs-12" style="padding-top:20px">
                        <asp:CheckBox ID="RememberMe" runat="server"/>
                        <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Remember me on this computer</asp:Label>
                </div></div>
            <div class="row">
            <div class="col-xs-12" style="padding-top:20px">
                
                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In"   />
                
            </div>
          </div>
        </LayoutTemplate>
    </asp:Login>
    <div class="row" style="padding-top:20px">
            <div class="col-xs-12">
    <asp:Button ID="ResetButton" runat="server" Text="I give up! I will set a new Password" OnClick="ResetPW" />
    </div>
    </div>
<%--</asp:Content>--%>
</div>
 </div>
</form>
</body>
</html>
