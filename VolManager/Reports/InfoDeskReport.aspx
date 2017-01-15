<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InfoDeskReport.aspx.cs" Inherits="VolManager.Reports.InfoDeskReport" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="FetchInfoForShift" TypeName="NQN.DB.GuidesDM">
        <SelectParameters>
        <asp:Parameter Name="ShiftID" Type="Int32" DefaultValue="0" />
        
        </SelectParameters>
    </asp:ObjectDataSource>
    
     <atk:ToolkitScriptManager ID="ScriptManager1" runat="server">
    
    </atk:ToolkitScriptManager>
    <div style="width:600px; margin-left:auto; margin-right:auto; padding-bottom: 10px">
<h2 style="color:Black">Info Center List</h2></div>
   
     <div class="clear"></div>
      
     <div class="clear" style="padding-top:10px"></div>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana"  Width="800" Height="800"    
        Font-Size="10pt" InteractiveDeviceInfos="(Collection)" 
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
        <LocalReport ReportPath="Reports/Captains.rdlc">
            <DataSources>
                <rsweb:ReportDataSource  DataSourceId="ObjectDataSource1" Name="GuidesObject" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
</asp:Content>
