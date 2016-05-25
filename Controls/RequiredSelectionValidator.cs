using System;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace NQN.Controls
{  
    public class RequiredSelectedItemValidator : BaseValidator
    {
        int _minvalue = 0;
        public int MinValue {
            get { return _minvalue;}
            set {_minvalue = value;}
        }
        private ListControl _listctrl;

        protected override bool ControlPropertiesValid()
        {
            Control ctrl = FindControl(ControlToValidate);

            if (ctrl != null)
            {
                _listctrl = (ListControl) ctrl ;
                return (_listctrl != null);
            }

            else
                return false;  // raise exception
        }

        protected override bool EvaluateIsValid()
        {

            return (_listctrl.SelectedIndex > _minvalue) ;
        }
    }
}
