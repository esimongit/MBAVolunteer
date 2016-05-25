<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ChangePasswordSuccess.aspx.cs" Inherits="MBAV.Account.ChangePasswordSuccess" %>

 
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Change Password
    </h2>
    <p>
        Your password has been changed successfully.
    </p>

     <asp:HyperLink ID="HyperLink3" runat="server"  ForeColor="Black" Font-Underline="false"  Font-Size="11" BorderColor="Gray"
           BorderStyle="Solid" BorderWidth="1" BackColor="ButtonFace"  Text="Return to Calendar"  NavigateUrl="SubstituteCalendar.aspx"/>
</asp:Content>
