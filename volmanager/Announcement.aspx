<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Announcement.aspx.cs" Inherits="VolManager.Announcement"
     ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"    >
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="NQN.DB.AnnouncementsObject" TypeName="NQN.DB.AnnouncementsDM" SelectMethod="FetchMain"
         UpdateMethod="Update"></asp:ObjectDataSource>
      <asp:FormView ID="FormView1" runat="server" Width="1000px" DefaultMode="Edit"  
       DataSourceID ="ObjectDataSource1" DataKeyNames="AnnouncementID">
        <EditItemTemplate>
       <asp:Literal ID="tinymceLiteral"  runat="server" Text='
             <script type="text/javascript" >
                 tinyMCE.init({
                     mode: "textareas",
                     plugins: "link, image, fullscreen",
                     theme: "modern",
                     menubar: "false",
                     toolbar: " undo redo | link unlink | image | styleselect | fontselect | fullscreen | fontsizeselect"
                 });
        </script>'  ></asp:Literal>
            <div style="float:left; font-weight:bold; font-size:large">
            Announcement Text:</div>
            <div style="float:left;padding-left:10px">
             <asp:TextBox ID="TextBox1" runat="server" Columns="80" Rows="30"  
            Text ='<%#Bind("AnnouncementText") %>' TextMode="MultiLine"></asp:TextBox>
                </div>
        <div style="clear:both"></div>
               <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                CommandName="Update" Text="Update" />
        </EditItemTemplate>
          </asp:FormView>
</asp:Content>
