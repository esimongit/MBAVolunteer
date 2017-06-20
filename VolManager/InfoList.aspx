<%@ Page Title="Info List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InfoList.aspx.cs" 
    Inherits="VolManager.InfoList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%@ Register Src="~/UserControls/DateRangeSelector.ascx" TagName="DateRangeSelector" TagPrefix="uc3" %> 
     <atk:ToolkitScriptManager ID="ScriptManager1" runat="server"    ></atk:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="NQN.Bus.InfoBusiness" SelectMethod="InfoList">
        <SelectParameters>
            <asp:ControlParameter ControlID="DateSelect" Name="StartDate" Type="DateTime" PropertyName="bDate" />
             <asp:ControlParameter ControlID="DateSelect" Name="EndDate" Type="DateTime" PropertyName="eDate" />
        </SelectParameters>
    </asp:ObjectDataSource>
 
    <uc3:DateRangeSelector ID="DateSelect" runat="server" AutoPostBack="true" />


   


    <cc2:NQNGridView ID="NQNGridView1" runat="server" AllowMultiColumnSorting="False" AutoGenerateColumns="False" Selectable="true"
        DataSourceID="ObjectDataSource1" DeleteMessage="Are you sure you want to delete?" Privilege=""  PageSize="100"  
        DataKeyNames="InfoGuideID, ShiftDate, ShiftID"  OnSelectedIndexChanged="GuideSelected"   >
             <Columns> <asp:BoundField DataField="ShiftDate" HeaderText="Date" DataFormatString="{0:d}" SortExpression="ShiftDate" />
                 <asp:BoundField DataField="ShiftName" HeaderText="Shift " SortExpression="ShiftName" />
                 <asp:BoundField DataField="ShortName" HeaderText="Short Name" SortExpression="ShortName" />
                 <asp:BoundField DataField="Type" HeaderText="Type" ReadOnly="True" SortExpression="Type" /> 
                 <asp:BoundField DataField="InfoVolID" HeaderText="Vol ID" SortExpression="InfoVolID" />
                 <asp:BoundField DataField="InfoFirst" HeaderText="First" SortExpression="InfoFirst" />
                 <asp:BoundField DataField="InfoLast" HeaderText="Last" SortExpression="InfoLast" />
             </Columns> </cc2:NQNGridView>


   


</asp:Content>
