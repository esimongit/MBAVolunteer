<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ChangePassword.aspx.cs" Inherits="MBAV.Account.ChangePassword" %>

 
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<div class="container">
<div class="row">
    <h2>
        Change Password
    </h2></div>
<div class="row">
        Use the form below to change your password.
  </div>
<div class="row">
        New passwords are required to be a minimum of <%= Membership.MinRequiredPasswordLength %> characters in length.
  </div>
    <asp:ChangePassword ID="ChangeUserPassword" runat="server" CancelDestinationPageUrl="~/" EnableViewState="false" RenderOuterTable="false" 
         SuccessPageUrl="ChangePasswordSuccess.aspx" >
        <ChangePasswordTemplate>
            <span class="failureNotification">
                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
            </span>
            <asp:ValidationSummary ID="ChangeUserPasswordValidationSummary" runat="server" CssClass="failureNotification" 
                 />
            <div class="accountInfo">
                <fieldset class="changePassword">
                    <legend>Account Information</legend>
                  <div class="row">
                        <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword">Old Password:</asp:Label>
                        <asp:TextBox ID="CurrentPassword" runat="server"   TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword" 
                             CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Old Password is required." 
                              >*</asp:RequiredFieldValidator>
                    </div>
                   <div class="row">
                        <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword">New Password:</asp:Label>
                        <asp:TextBox ID="NewPassword" runat="server"   TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword" 
                             CssClass="failureNotification" ErrorMessage="New Password is required." ToolTip="New Password is required." 
                             >*</asp:RequiredFieldValidator>
                    </div>
                   <div class="row">
                        <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword">Confirm New Password:</asp:Label>
                        <asp:TextBox ID="ConfirmNewPassword" runat="server"   TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword" 
                             CssClass="failureNotification" Display="Dynamic" ErrorMessage="Confirm New Password is required."
                             ToolTip="Confirm New Password is required." ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" 
                             CssClass="failureNotification" Display="Dynamic" ErrorMessage="The Confirm New Password must match the New Password entry."
                             >*</asp:CompareValidator>
                   </div>
                </fieldset>
                <div class="row">
       
                  <div class="col-md-6"  style="padding-top:10px">
                    <asp:Button ID="CancelPushButton" runat="server" CausesValidation="False"   CssClass="btn btn-info" Text="Return to Calendar" OnClick="ClosePage"/> 
                    </div>
                    <div class="col-md-6"  style="padding-top:10px">
                    <asp:Button ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword" CssClass="btn btn-success"   Text="Change Password" 
                        />
         
                </div>
            </div>
        </ChangePasswordTemplate>
    </asp:ChangePassword>
    </div></div>
</asp:Content>
