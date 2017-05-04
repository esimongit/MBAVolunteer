<%@ Page Title="Substitute History" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SubstituteHistory.aspx.cs" Inherits="VolManager.Reports.SubstituteHistory" %>
 <%@ Register Src="~/UserControls/DateRangeSelector.ascx" TagName="DateRangeSelector" TagPrefix="uc3" %> 
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="NQN.DB.GuideSubstituteDM" SelectMethod="SubstituteReport">
        <SelectParameters>
            <asp:ControlParameter Name="StartDate" Type="DateTime" ControlID="DateRangeSelect" PropertyName="bDate" DefaultValue="1/1/2016" />
            <asp:ControlParameter Name="EndDate" Type="DateTime" ControlID="DateRangeSelect" PropertyName="eDate" DefaultValue="1/1/2020" />

        </SelectParameters>
    </asp:ObjectDataSource>
     <atk:ToolkitScriptManager ID="ScriptManager1" runat="server">
    
    </atk:ToolkitScriptManager>
    <div style="float:left">
    <uc3:DateRangeSelector ID="DateRangeSelect" runat="server"   /></div>
    <div style="float:right"><asp:Button runat="server" ID="RunButton" OnClick="DoReport" Text="Run Report" /></div>
    <div style="clear:both"></div>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana"  Width="1000" Height="800"    
        Font-Size="10pt"  >
        <LocalReport ReportPath="Reports/SubstituteHistory.rdlc">
            <DataSources>
                <rsweb:ReportDataSource  DataSourceId="ObjectDataSource1" Name="GuideSubstituteObject"  />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
</asp:Content>
