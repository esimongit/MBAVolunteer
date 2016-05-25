<%@ Page Title="" Language="C#" MasterPageFile="~/Secondary.Master" AutoEventWireup="true" CodeBehind="SumReport.aspx.cs" Inherits="MBAV.SumReport" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
 <%@ Register Src="~/DateSelector.ascx" TagName="DateSelect" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="SubReport" TypeName="NQN.DB.GuideSubstituteDM">
        
    </asp:ObjectDataSource>
    
     <atk:ToolkitScriptManager ID="ScriptManager1" runat="server">
    
    </atk:ToolkitScriptManager>
    <div style="width:600px; margin-left:auto; margin-right:auto; padding-bottom: 10px">
<h2 style="color:Black">Monterey Bay Aquarium<br />Volunteer Substitution Status</h2></div>
   
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
        <LocalReport ReportPath="SumReport.rdlc">
            <DataSources>
                <rsweb:ReportDataSource  DataSourceId="ObjectDataSource1" Name="GuideSubstituteObject" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>

</asp:Content>
