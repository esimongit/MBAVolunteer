<%@ Page Title="" Language="C#" MasterPageFile="~/Secondary.Master" AutoEventWireup="true" CodeBehind="ShiftRoster.aspx.cs" Inherits="MBAV.ShiftRoster" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
 <%@ Register Src="~/DateSelector.ascx" TagName="DateSelect" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="Roster" TypeName="NQN.Bus.GuidesBusiness">
        <SelectParameters>
            <asp:ControlParameter Name="ShiftID" ControlID="ShiftSelect" Type="Int32"  PropertyName="SelectedValue"/>
            <asp:ControlParameter Name="dt" Type="DateTime" ControlID="DateSelect1" PropertyName="bDate" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ShiftsDataSource" runat="server" TypeName="NQN.DB.ShiftsDM" SelectMethod="ShiftsForDate">
     <SelectParameters>
      <asp:ControlParameter  ControlID="DateSelect1" PropertyName="bDate" Name="dt" Type="DateTime" />
     </SelectParameters>
    </asp:ObjectDataSource>
     <atk:ToolkitScriptManager ID="ScriptManager1" runat="server">
    
    </atk:ToolkitScriptManager>
    <div style="width:200px; margin-left:auto; margin-right:auto; padding-bottom: 10px">
<h2 style="color:Black">Shift Roster</h2></div>
    <div style="float:left; font-size:16pt">
  <uc3:DateSelect ID="DateSelect1" runat="server" OnDateChanged="RefreshShifts"  AutoPostBack="true" />
        <asp:TextBox runat="server"    AutoPostBack ="true"></asp:TextBox>
    </div>
     <div style="float:left; padding-left: 100px; font-size:16pt;">Select shift: 
     <asp:DropDownList ID="ShiftSelect" runat="server" DataSourceID="ShiftsDataSource" DataTextField="Sequence" DataValueField="ShiftID"
      AppendDataBoundItems="true">
      <asp:ListItem Value="0" Text="(Select a Shift)"></asp:ListItem>
     
     </asp:DropDownList></div>
     <div class="clear"></div>
     <div style="float:left; padding-top:10px; padding-left:100px">
     <asp:Button ID="SubmitButton" runat="server" Text="Submit" OnClick="DoReport" /></div>
       <div style="float:left; padding-top:10px; padding-left:100px">
     <input type="button" value="Close Window" onclick="self.close()"/>
     </div>
     <div class="clear" style="padding-top:10px"></div>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana"  Width="800" Height="800"    
        Font-Size="10pt" InteractiveDeviceInfos="(Collection)" 
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
        <LocalReport ReportPath="Roster.rdlc">
            <DataSources>
                <rsweb:ReportDataSource  DataSourceId="ObjectDataSource1" Name="GuidesObject" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>

</asp:Content>
