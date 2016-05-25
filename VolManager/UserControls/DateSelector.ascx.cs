using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NQN.Bus;

namespace VolManager
{
    public partial class DateSelector : System.Web.UI.UserControl
    {
        public event EventHandler DateChanged;
        private bool _autopostback = false;
        public bool AutoPostBack
        {
            get { return _autopostback; }
            set { _autopostback = value; }
        }
        public string bDate
        {
            get
            {
                return bDateTextBox.Text;
            }
            set
            {
                bDateTextBox.Text = DateTime.Parse(value).ToShortDateString();
                
            }
        }
        private bool _defaultblank = false;
        public bool DefaultBlank
        {
            get { return _defaultblank; }
            set { _defaultblank = value;}
        }
        private string _labeltext = "Select Date:";
        public string LabelText
        {
             
            set { _labeltext = value; }
        }
        private bool _required = false;
        public bool Required
        {
            get { return _required; }
            set { _required = value; }
        }
        protected override void OnInit(EventArgs e)
        {
            Label2.Text = _labeltext;
            if (!_defaultblank)
                bDateTextBox.Text = DateTime.Today.ToShortDateString();
            base.OnInit(e);
        }
        

        protected void Page_PreRender(object sender, EventArgs e)
        {
            bDateTextBox.AutoPostBack = _autopostback;
            RequiredFieldValidator1.Enabled = _required;
            
        }
       
        
        
        protected void ChangeEvent(object sender, EventArgs e)
        {
            OnDateChanged(new EventArgs());
        }
        protected virtual void OnDateChanged(EventArgs e)
        {
            if (DateChanged != null)
                DateChanged(this, e);
        }
       
    }
}
