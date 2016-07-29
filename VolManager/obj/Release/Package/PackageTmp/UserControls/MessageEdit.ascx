<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageEdit.ascx.cs" Inherits="VolManager.MessageEdit"   %>
  <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" TypeName="NQN.DB.MailTextDM"
     SelectMethod="FetchForEdit" UpdateMethod="Update" OnUpdated="CatchDB" >
      <SelectParameters>      
       <asp:SessionParameter SessionField="MailTextID" Name="MailTextID" Type="Int32" DefaultValue="0" />
      </SelectParameters>
     </asp:ObjectDataSource>
   <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" TypeName="NQN.DB.MailTextVarsDM"
     SelectMethod="FetchForMailText">
       <SelectParameters>       
      <asp:SessionParameter SessionField="MailTextID" Name="MailTextID" Type="Int32" DefaultValue="0" />
      </SelectParameters>
     </asp:ObjectDataSource>
     <asp:FormView ID="FormView1" runat="server" Width="1000px" DefaultMode="Edit" OnPreRender="Form_PreRender"
       DataSourceID ="ObjectDataSource2" DataKeyNames="MailTextID">
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
        </script>' Visible ='<%#Eval("IsHtml") %>' ></asp:Literal>
          <table><tr><td>
           
            Subject: <asp:TextBox ID="SubjectTextBox" runat="server" width="500" Text ='<%#Bind("Subject") %>'>
            </asp:TextBox><br />
            Mail From: <asp:TextBox ID="MailFromTextBox" runat="server"  Width="300" Text ='<%#Bind("MailFrom") %>'>
            </asp:TextBox> &nbsp;&nbsp;&nbsp;
            <asp:CheckBox ID="EnabledCheckBox" runat="server" Checked='<%#Bind("Enabled") %>' Text="Enabled" />&nbsp;&nbsp;
            <asp:CheckBox ID="HtmlCheckBox" runat="server" Checked='<%#Bind("IsHtml") %>' Text="HTML"  AutoPostBack="true" />
            <br />
            <asp:TextBox ID="TextBox1" runat="server" Columns="80" Rows="30"  
            Text ='<%#Bind("TextValue") %>' TextMode="MultiLine"></asp:TextBox>
           
           </td><td>Copy the [symbols] and paste them into the text to include a field:
            <ul>
            <asp:Repeater ID="Repeater1" runat="server" DataSourceID="ObjectDataSource3">
            <ItemTemplate>
              <li> <asp:TextBox ID="ArgLabel"  Text='<%#Eval("VarSymbol", "[{0}]") %>'
               runat="server" />  <asp:Label  Font-Bold="false" Font-Size="Small" id ="ArgDisplay" runat="server" Text='<%#Eval("VarDescription") %>'   /></div>
             </li>
              </ItemTemplate>
              </asp:Repeater>
           </ul>
           </td>
           </tr></table>
          
            <cc2:NQNLinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                 CommandName="Update" Text="Save" /> &nbsp;&nbsp;
          <%--  <cc2:NQNButton ID="CloneButton" Text="Clone" runat="server" OnClick="CloneMessage" />&nbsp;&nbsp;
            <asp:Panel ID="CloneTable" runat="server">
           
            New Symbol<asp:TextBox ID="NewSymbolTextBox" runat="server" MaxLength="20"></asp:TextBox>
            </asp:Panel>--%>
        </EditItemTemplate>
    </asp:FormView>