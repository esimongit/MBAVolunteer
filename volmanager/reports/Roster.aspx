<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Roster.aspx.cs" Inherits="VolManager.Reports.Roster" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="RosterList" TypeName="NQN.Bus.GuidesBusiness">
        <SelectParameters>
         <asp:QueryStringParameter QueryStringField="ShiftID" Name="ShiftID" Type="Int32" DefaultValue="0" />
   <asp:QueryStringParameter QueryStringField="ShiftDate" Name="dt" Type="DateTime" DefaultValue="1/2/2016" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
     <atk:ToolkitScriptManager ID="ScriptManager1" runat="server">
    
    </atk:ToolkitScriptManager>
    <div style="float:left">
<h2 style="color:Black">Roster</h2></div>
    <div style="float:left; padding-left: 320px; padding-top:20px">
        <div style="float:right"><asp:Button runat="server" ID="RunButton" OnClick="DoReport" Text="Run Report" /></div>
    </div>
   
     <div class="clear"></div>
      
     <div class="clear" style="padding-top:10px"></div>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana"  Width="1200" Height="1000"    
        Font-Size="10pt" InteractiveDeviceInfos="(Collection)"   
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
        <LocalReport ReportPath="Reports/Roster.rdlc" >
            <DataSources>
                <rsweb:ReportDataSource  DataSourceId="ObjectDataSource1" Name="GuidesObject" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
</asp:Content>
