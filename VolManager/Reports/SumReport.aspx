<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="/Reports/SumReport.aspx.cs" Inherits="VolManager.SumReport" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
 <%@ Register Src="~/UserControls/DateSelector.ascx" TagName="DateSelect" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="server">
  
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="SubReport" TypeName="NQN.Bus.SubstitutesBusiness">
        
    </asp:ObjectDataSource>
    
     <atk:ToolkitScriptManager ID="ScriptManager1" runat="server">
    
    </atk:ToolkitScriptManager>
    <div style="width:600px; margin-left:auto; margin-right:auto; padding-bottom: 10px">
<h2 style="color:Black">Monterey Bay Aquarium<br />Volunteer Substitution Status</h2></div>
   
     <div class="clear"></div>
      
     <div class="clear" style="padding-top:10px"></div>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana"  Width="1200" Height="800"    
        Font-Size="10pt" InteractiveDeviceInfos="(Collection)" 
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
        <LocalReport ReportPath="Reports/SumReport.rdlc">
            <DataSources>
                <rsweb:ReportDataSource  DataSourceId="ObjectDataSource1" Name="GuideSubstituteObject" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>

</asp:Content>
