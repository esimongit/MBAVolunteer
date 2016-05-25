using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using NQN.DB;

namespace NQN.Controls
{
    public class CalendarLink : HyperLink
    {
        string _type = String.Empty;
        public string CalendarType
        {
            get { return _type; }
            set { _type = value; }
        }
        string _hostname = String.Empty;
        public string HostName
        {
            get { return _hostname; }
            set { _hostname = value; }
        }
        string _title = String.Empty;
        public string Title
        {
            set
            {
                _title = value;
            }
        }
        string _description = String.Empty;
        public string Description
        {
            set
            {
                _description = value;
            }
        }
        string _location = String.Empty;
        public string Location
        {
            set
            {
                _location = value;
            }
        }
        string _notes = String.Empty;
        public string Notes
        {
            set
            {
                _notes = value;
            }
        }
        DateTime _dt = new DateTime();
        public DateTime Date
        {
            set
            {
                _dt = value;
            }
        }
        bool _usetimes = false;
        public bool UseTimes
        {
            set { _usetimes = value; }
        }
        DateTime _starttime = new DateTime();
        public DateTime StartTime
        {
            set { _starttime = value; }
        }
        DateTime _endtime = new DateTime();
        public DateTime EndTime
        {
            set { _endtime = value; }
        }
        private string _link = String.Empty;

        public CalendarLink()
        {
           
            BorderStyle = BorderStyle.None;
            Target = "_blank";
            EnableTheming = false;
            
            
           
        }
        protected override void OnPreRender(EventArgs e)
        {
            string dtstring = String.Empty;
            _starttime = new DateTime(_dt.Year, _dt.Month, _dt.Day, _starttime.Hour, _starttime.Minute, _starttime.Second);
            _endtime = new DateTime(_dt.Year, _dt.Month, _dt.Day, _endtime.Hour, _endtime.Minute, _endtime.Second);
            DateTime dt = _dt.ToUniversalTime();
            DateTime starttime = _starttime.ToUniversalTime();
            DateTime endtime = _endtime.ToUniversalTime();
            switch (_type)
            {
                case "Google":
                    _link = "http://www.google.com/calendar/event?action=TEMPLATE";
                    ToolTip = _description;
                    ImageUrl = "http://www.google.com/calendar/images/ext/gc_button6.gif";
                    //http://www.google.com/calendar/event?action=TEMPLATE&text=Volunteer%20Substitute&dates=20160813T160000Z/20160813T190000Z&details=MBAVolunteer&location=MBA&trp=false&sprop=mba.netqnet.com&sprop=name:MBA:VolunteerSubstitute
                    base.OnPreRender(e);
                    if (_link == String.Empty) return;
                    //"20110507T160000Z/20110507T180000Z"
                    
                    dtstring = starttime.ToString("yyyyMMddTHHmmssZ");
                    dtstring += endtime.ToString("/yyyyMMddTHHmmssZ");
                    
                     //dtstring += _dt.ToUniversalTime().ToString("/yyyyMMdd");
                    
                    NavigateUrl = _link;

                    NavigateUrl += String.Format("&text={0}&dates={1}&details={2}&location={3}",
                        _title, dtstring, _description + _notes, _location);

                    string Ref = "MBA:VolunteerSubstitute";
                    NavigateUrl += String.Format("&trp=false&sprop={0}&sprop=name:{1}", _hostname, Ref);
                    break;
                case "Yahoo":
                    BorderStyle = BorderStyle.Solid;
                    BorderWidth = 1;
                   
                    System.Drawing.ColorConverter cv = new System.Drawing.ColorConverter();
                    BackColor = (System.Drawing.Color) cv.ConvertFromString("bisque");
                    ForeColor = (System.Drawing.Color)cv.ConvertFromString("black");
                    Text = "Add to Yahoo Calendar";
                    _link = "http://calendar.yahoo.com/?v=60&view=d&type=20";
                    //https://calendar.yahoo.com/?v=60&view=d&type=20&title=MBASub&_in_loc=MBA&st=20070201T110000&dur=0330&desc=VolCalendarSub
                    base.OnPreRender(e);
         
                    //"20070201T110000/20110507T180000Z"
                    dtstring = starttime.ToString("yyyyMMddTHHmmssZ");
          
                    string duration = endtime.Subtract(starttime).ToString("hhmm");

                    NavigateUrl = _link;

                    NavigateUrl += String.Format("&title={0}&desc={1}&in_loc={2}&st={3}&dur={4}",
                        _title,  _description, _location, dtstring, duration);
                    break;
                default:
                    Visible = false;
                    break;
            
            }

        }

    }
}
              
 
