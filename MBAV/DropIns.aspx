<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DropIns.aspx.cs" Inherits="MBAV.DropIns" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  
 <asp:ObjectDataSource ID="ObjectDataSource1" runat='server' TypeName="NQN.Bus.SubstitutesBusiness" SelectMethod="FutureDatesForShift"
  >
 <SelectParameters>
   <asp:SessionParameter SessionField="GuideID" Name="GuideID" Type="Int32" DefaultValue="0" />
   <asp:ControlParameter ControlID="ShiftSelect" Name="ShiftID" Type="Int32" DefaultValue="0" />
 </SelectParameters>
 </asp:ObjectDataSource>
 <asp:ObjectDataSource ID="ShiftsDataSource" runat="server" TypeName="NQN.DB.ShiftsDM" SelectMethod="FetchWithSubOffers">
  <SelectParameters>
   <asp:SessionParameter SessionField="GuideID" Name="GuideID" Type="Int32" DefaultValue="0" />
 </SelectParameters>
</asp:ObjectDataSource>
 <asp:DropDownList ID="ShiftSelect" runat="server" DataSourceID="ShiftsDataSource" DataTextField="ShiftName" DataValueField="ShiftID" AppendDataBoundItems="true"
  AutoPostBack="true">
  <asp:ListItem Value="0" Text="(Select a shift)"></asp:ListItem>
 </asp:DropDownList> 
 
  
  <hr/>
<h3>In the table below,  check all the boxes for dates  
you plan to drop in for this shift. </h3><hr/>
  
<asp:Repeater ID="Repeater1" runat="server" DataSourceID="ObjectDataSource1" >
<HeaderTemplate><table cellpadding="5"></HeaderTemplate>
<ItemTemplate><tr style='<%#Container.ItemIndex %2==0 ? "background-color:#afcfdf":"background-color:#ffffff" %>'> 
<td style="padding:2px 4px 2px 2px; text-align:right"><asp:Label ID="DateLabel" runat="server" Text='<%#Eval("DropinDate",  "{0:d}") %>'></asp:Label></td><td>
<asp:CheckBox ID="DateCheckBox" runat="server" Checked='<%#Eval("Selected") %>'   />
<asp:HiddenField ID="DropinIDHidden" runat="server" Value='<%#Eval("GuideDropinID") %>' />
</td></tr>
</ItemTemplate>
<FooterTemplate></table></FooterTemplate>
</asp:Repeater>
<div class="row"><div class="col-md-4">
<asp:Button ID="SubmitButton" OnClick="DoSubmit" runat="server" Text="Submit" />
</div><div class="col-md-4">
<asp:LinkButton ID="UpdateCancelButton" runat="server" CssClass="Button"
             CausesValidation="False" CommandName="Cancel" Text="Return to Calendar"  ForeColor="Black" Font-Underline="false"  Font-Size="11" BorderColor="Gray"
           BorderStyle="Solid" BorderWidth="1" BackColor="ButtonFace" OnClick="ClosePage" />
</div></div>
</asp:Content>
