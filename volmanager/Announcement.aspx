<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Announcement.aspx.cs" Inherits="VolManager.Announcement"
     ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"    >
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="NQN.DB.AnnouncementsObject" TypeName="NQN.DB.AnnouncementsDM" SelectMethod="FetchMain"
         UpdateMethod="Update"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="FileDataSource" runat="server" TypeName="NQN.Bus.FileBusiness" SelectMethod="ListFiles"
        DeleteMethod="RemoveFile" OnDeleted="OnChange">

    </asp:ObjectDataSource>
<asp:MultiView ID="MultiView1" runat="server">
    <asp:View ID="View1" runat="server">
        <div style="float:left">
      <asp:FormView ID="FormView1" runat="server"   DefaultMode="Edit"  
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
            <div style=" font-weight:bold; font-size:large">
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
        </div>
        <div style="float:left">
            <asp:Button ID="AddButton" runat="server" OnClick="ToView2" Text="Add/Edit File List" />
            <asp:Repeater runat="server" ID="Repeater1" DataSourceID="FileDataSource">
                <HeaderTemplate><ul></HeaderTemplate>
                <ItemTemplate>
                    <li><asp:TextBox ID="FileLabel" ReadOnly="true" runat="server" Width="250px" Text='<%#Eval("FileName","[{0}]") %>'></asp:TextBox></li>
                </ItemTemplate>
                <FooterTemplate></ul></FooterTemplate>
            </asp:Repeater>
        </div>
        </asp:View>
     <asp:View ID="View2" runat="server">
         <asp:Button ID="ReturnButton" runat="server" Text="Return to Announcements" OnClick="ToView1" />
         <br />
          <asp:FileUpload ID="FileUpload1" runat="server"  /> <asp:Button runat="server" ID="ImportButton" Text="Import" OnClick="ImportButton_Click" />
    <br />
         <cc2:NQNGridView ID="GridView1" runat="server" DataSourceID="FileDataSource" DataKeyNames="FileName">
             <Columns>
                 <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/delete.gif" ShowDeleteButton="true" HeaderText="Delete" />
                 <asp:BoundField DataField="FileName" HeaderText="Name" />
                 <asp:BoundField DataField="FileSize" HeaderText="Size" />
                  <asp:BoundField DataField="Extension" HeaderText="Type" />
             </Columns>
         </cc2:NQNGridView>
     </asp:View>
</asp:MultiView>
</asp:Content>
