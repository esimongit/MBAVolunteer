using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NQN.Bus;

namespace NQN.Controls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:NQNHyperLink runat=server></{0}:NQNHyperLink>")]
    public class NQNHyperLink : HyperLink
    {
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        private string m_Privilege = "";

        public string Privilege
        {
            get
            {
                return m_Privilege;
            }
            set
            {
                m_Privilege = value;
            }
        }
        protected override void OnPreRender(EventArgs e)
        {
            PagePrivilege p = PagePrivilege.PagePrivilegeFactory();
            if (m_Privilege != "")
            {
                Enabled = p.HasPriv(m_Privilege);
            }
            else
            {
                Enabled = p.HasPriv();
            }
            if (!Enabled)
            {
                ForeColor = System.Drawing.Color.SlateGray;
            }
            base.OnPreRender(e);
        }
    }
}
