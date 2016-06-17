<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="MBAV.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  
 <asp:ObjectDataSource ID="ObjectDataSource1" runat='server' TypeName="NQN.DB.GuidesDM" SelectMethod="FetchGuide"
  UpdateMethod="Update" OnUpdated ="OnUpdated" >
 <SelectParameters>
   <asp:SessionParameter SessionField="GuideID" Name="GuideID" Type="Int32" DefaultValue="0" />
 </SelectParameters>
 </asp:ObjectDataSource>
 <asp:ObjectDataSource ID="ShiftsDataSource" runat="server" TypeName="NQN.DB.ShiftsDM" SelectMethod="FetchWithSubOffers">
  <SelectParameters>
   <asp:SessionParameter SessionField="GuideID" Name="GuideID" Type="Int32" DefaultValue="0" />
 </SelectParameters>
</asp:ObjectDataSource>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
 <asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" DataSourceID="ObjectDataSource1"  DataKeyNames="GuideID">
     <EditItemTemplate>
        <div class="row">  
        <h3>Please correct this information</h3>
        </div>
        <div class="row" style="padding-bottom:4px">
        <div class="col-md-5">
        <asp:RequiredFieldValidator ID="FirstRequired" ControlToValidate="FirstNameTextBox" runat="server" ErrorMessage="First Name is required"
          Display="Dynamic">*</asp:RequiredFieldValidator>
         First Name: </div>
         <div class="col-md-4"> 
         <asp:TextBox ID="FirstNameTextBox" runat="server" 
             Text='<%# Bind("FirstName") %>' />
           </div>
         </div>
        <div class="row" style="padding-bottom:4px">
        <div class="col-md-5">
         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="LastNameTextBox" runat="server" ErrorMessage="Last Name is required"
          Display="Dynamic">*</asp:RequiredFieldValidator>
         Last Name:</div>
         <div class="col-md-4"> 
         <asp:TextBox ID="LastNameTextBox" runat="server" 
             Text='<%# Bind("LastName") %>' />
       </div>
       </div>
        <div class="row" style="padding-bottom:4px">
        <div class="col-md-5"> <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="PhoneTextBox" runat="server"
             ErrorMessage="Phone is required"
          Display="Dynamic">*</asp:RequiredFieldValidator>
          <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="PhoneTextBox"
                    ValidationExpression='<%# NQN.DB.GuidesObject.ValidPhone %>' ErrorMessage="Phone must have 10 digits"
             Display="Dynamic">*</asp:RegularExpressionValidator>
         Phone:</div>
         <div class="col-md-4"> 
         <asp:TextBox ID="PhoneTextBox" runat="server" TextMode="Phone"  Text='<%# Bind("Phone") %>' />
        </div>
        </div>
        <div class="row" style="padding-bottom:4px">
        <div class="col-md-5">
         <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="EmailTextBox"
                    ValidationExpression='<%# NQN.DB.GuidesObject.ValidEmail %>' ErrorMessage="Email is not Valid"
             Display="Dynamic">*</asp:RegularExpressionValidator>
         Email Address:</div>
         <div class="col-md-4"> 
         <asp:TextBox ID="EmailTextBox"  TextMode="Email" Width="200" runat="server" Text='<%# Bind("Email") %>' />
        </div></div>
         <div class="row" style="padding-bottom:4px">
        <div class="col-md-5">
         Personal Calendar:</div>
         <div class="col-md-4"> 
         <asp:DropDownList ID="CalendarSelect" runat="server" SelectedValue='<%#Bind("CalendarType") %>'>
         <asp:ListItem Value="" Text="(None)"></asp:ListItem>
           <asp:ListItem Value="Google" Text="Google"></asp:ListItem>
             <asp:ListItem Value="Yahoo" Text="Yahoo"></asp:ListItem>
         </asp:DropDownList>
        </div></div>
        <div class="row" style="padding-bottom:4px">
        <div class="col-md-8">
        
        
         ID: 
         
         <asp:Label ID="VolIDTextBox" runat="server"   Font-Bold="true" Text='<%# Eval("VolID") %>' />
       </div>
        </div> 
        <div class="row" style="padding-bottom:4px">
        <div class="col-md-8">
         Shift: 
       
         <asp:Label ID="Label1" runat="server"  Font-Bold="true" Text='<%# Eval("ShiftName") %>' />
       </div>
       </div>
    
         <asp:LinkButton ID="UpdateButton" runat="server"  CausesValidation="True" 
             CommandName="Update" Text="Update"  ForeColor="Black" Font-Underline="false"  Font-Size="11" BorderColor="Gray"
           BorderStyle="Solid" BorderWidth="1" BackColor="ButtonFace"/>
         &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CssClass="Button"
             CausesValidation="False" CommandName="Cancel" Text="Return to Calendar"  ForeColor="Black" Font-Underline="false"  Font-Size="11" BorderColor="Gray"
           BorderStyle="Solid" BorderWidth="1" BackColor="ButtonFace" OnClick="ClosePage" />
     </EditItemTemplate>
    
 </asp:FormView>
 
  
  <hr/>
<h3>In the table below,  check all the boxes for shifts on which 
you can substitute.  No need to click "Update."</h3><hr/>
  
<asp:Repeater ID="Repeater1" runat="server" DataSourceID="ShiftsDataSource" >
<HeaderTemplate><table cellpadding="5"></HeaderTemplate>
<ItemTemplate><tr style='<%#Container.ItemIndex %2==0 ? "background-color:#efefef":"background-color:#ffffff" %>'> <td><asp:Label runat="server" Text='<%#Eval("ShiftName") %>'></asp:Label></td><td>
<asp:CheckBox ID="ShiftCheckBox" runat="server" Checked='<%#Eval("Selected") %>' OnCheckedChanged="CheckChanged"  AutoPostBack="true" />
<asp:HiddenField ID="ShiftIDHidden" runat="server" Value='<%#Eval("ShiftID") %>' />
</td></tr>
</ItemTemplate>
<FooterTemplate></table></FooterTemplate>
</asp:Repeater>
</asp:Content>
