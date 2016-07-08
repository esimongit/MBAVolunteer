using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NQN.Bus;

namespace VolManager.UserControls
{
    public partial class  DateRangeSelector : System.Web.UI.UserControl
    {
        public event EventHandler DateChanged;
        private bool _autopostback = false;
        private bool _autofill = true;

        public bool AutoFill
        {
            get { return _autofill; }
            set { _autofill = value; }
        }
        public bool AutoPostBack
        {
            get { return _autopostback; }
            set { _autopostback = value; }
        }

        public string ControlLabel
        {
            get { return Label2.Text; }
            set { Label2.Text = value; }
        }
        public string bDate
        {
            get
            {
                return bDateTextBox.Text;
            }
            set { 
                bDateTextBox.Text = DateTime.Parse(value).ToShortDateString(); }
        }
        public string eDate
        {
            get
            {
                return eDateTextBox.Text;
            }
            set { eDateTextBox.Text = DateTime.Parse(value).ToShortDateString(); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack && _autofill)
            {
                bDateTextBox.Text = DateRange.DateUtilities.GetStartOfCurrentYear().ToShortDateString();
                eDateTextBox.Text = DateTime.Today.ToShortDateString();
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            bDateTextBox.AutoPostBack = _autopostback;
            eDateTextBox.AutoPostBack = _autopostback;
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


