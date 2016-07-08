<%@ Control Language="C#" AutoEventWireup="True" Inherits="VolManager.UserControls.DateRangeSelector" Codebehind="DateRangeSelector.ascx.cs" %>
   
    <div style="float:left">    <asp:Label ID="Label2" runat="server" Text="Select Date Range:" ></asp:Label> 
            <atk:CalendarExtender runat="server"
                    ID="CalendarExtender1" 
                    TargetControlID="bDateTextBox"
                    Format="M/d/yyyy"
                    PopupButtonID="CalImage1" /></div>
    <div style="float:left">             
   <asp:TextBox ID="bDateTextBox" runat="server" 
          Width="100px" ontextchanged="ChangeEvent"></asp:TextBox>
     </div>
     <div style="float:left; padding-left:4px; padding-top:2px;padding-right:4px">
      <asp:Image ID="CalImage1" Height="21px" ImageAlign="Top"  runat="server" ImageUrl="~/Images/calendar.gif" 
                    Width="26px" CausesValidation="false" Style="position: static" />
              
              
     </div> 

            <atk:CalendarExtender runat="server"
                    ID="CalendarExtender2" 
                    TargetControlID="eDateTextBox"
                    Format="M/d/yyyy"
                    PopupButtonID="CalImage2" />
        <div style="float:left; padding-left:15px;padding-right:15px" >to </div>
      <div style="float:left">     
   <asp:TextBox ID="eDateTextBox" runat="server" 
          Width="100px" ontextchanged="ChangeEvent"></asp:TextBox>
     </div>
     <div style="float:left; padding-left:4px; padding-top:2px;padding-right:4px">
      <asp:Image ID="CalImage2" Height="21px" ImageAlign="Top"  runat="server" ImageUrl="~/Images/calendar.gif" 
                    Width="26px" CausesValidation="false" Style="position: static" />
                    
     </div>  
    <div style="clear:both"></div> 
