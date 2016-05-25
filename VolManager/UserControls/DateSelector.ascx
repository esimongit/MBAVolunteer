<%@ Control Language="C#" AutoEventWireup="True" Inherits="VolManager.DateSelector" Codebehind="/UserControls/DateSelector.ascx.cs" %>
       <div style="float:left">     <asp:Label ID="Label2" runat="server" Text="Select Date:" ></asp:Label> 
           
    </div>
   <div style="float:left">
      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="bDateTextBox"
         ErrorMessage="Date is Required">*</asp:RequiredFieldValidator>
   <atk:CalendarExtender runat="server"
                    ID="CalendarExtender1"   
                    TargetControlID="bDateTextBox"
                    Format="M/d/yyyy"
                    PopupButtonID="CalImage1" />
                
   <asp:TextBox ID="bDateTextBox" runat="server"  Font-Size="14"
          Width="100px" ontextchanged="ChangeEvent"></asp:TextBox>
     </div>
     <div style="float:left; padding-left:4px; padding-top:4px;padding-right:4px">
      <asp:Image ID="CalImage1" Height="21px" ImageAlign="Top"  runat="server" ImageUrl="~/Images/calendar.gif" 
                    Width="26px"   Style="position: static" />
              
     </div> 
 
