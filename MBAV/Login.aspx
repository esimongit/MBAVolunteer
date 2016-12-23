<%@ Page Title="Log In" Language="C#"  AutoEventWireup="true"  CodeBehind="Login.aspx.cs" Inherits="MBAV.Login" %>
<!DOCTYPE html >
<html>
<head id="Head1" runat="server">
  <title>MBA Volunteer Substitute Calendar</title>
     <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" >
    <link href="Content/bootstrap.css" rel="Stylesheet" type="text/css" > 
    <script src="/Scripts/common.js" type="text/javascript" ></script>
     <script type="text/javascript" src="Scripts/jquery-1.9.1.min.js">    </script>   
     <script src="/Scripts/bootstrap.js" type="text/javascript" ></script>
 
</head>
<body style="font-family:Arial; background-image:url(/Images/GreenSeaTurtleX.jpg)  ">
 
<form runat="server">
<div class="container">
<div class="row">
    <div class="col-md-6 col-xs-10"  >
    <img src="/Images/VolunteerLogoColor.jpg" alt="Monterey Bay Aquarium Volunteer" class="img-responsive" width="300"   /></div>
   </div>
 
 
   

<div class="row">
  <div class="col-md-5 col-xs-10"  >
       <div class="row">
    <div class="col-md-5 col-xs-8"> <h2>
       Log In
    </h2>
       </div> </div> 
   <div class="row">
       <div class="col-xs-12">
    <asp:Login ID="LoginUser" runat="server" EnableViewState="false"    RenderOuterTable="false" OnLoggedIn="SaveUser" OnLoggingIn="OnLoggingIn"  >    
        <LayoutTemplate>       
            <div class="row">
             <div class="col-xs-12">     
                <asp:Label ID="FailureText"  runat="server" ForeColor="Red"></asp:Label>       
            <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"  />
 
            
                 <asp:Label ID="InstructionText" runat="server" Font-Bold="true" ForeColor="Gray" Text="Please enter your Guide ID and password."></asp:Label>
                          
               </div></div>
                     <div class="row">     
                     <div class="col-xs-12">   
                        <asp:Label ID="UserNameLabel" runat="server"   AssociatedControlID="UserName">Guide ID:</asp:Label>
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
    </div>
    </div>
    <div class="row" style="padding-top:20px">
            <div class="col-xs-12">
    <asp:Button ID="ResetButton" runat="server" Text="I give up! I will set a new Password" OnClick="ResetPW" CausesValidation="false" />
    </div>
    </div>
  </div>
 
 
    <div class="col-md-6 col-xs-10" style="background-color:White;"> 
       <asp:Label ID="AnnouncementLabel" runat="server"></asp:Label>

        </div>
  </div>
  </div>
</form>
</body>
</html>
