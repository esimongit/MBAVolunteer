using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NQN.DB;
using NQN.Core;

namespace NQN.Controls
{
    public class ShiftSelector : DropDownList
    {

        public ShiftSelector()
            {
                Items.Add(new ListItem("(Select a Shift)", "0"));
                AppendDataBoundItems = true;
                ObjectDataSource ods = new ObjectDataSource("NQN.DB.ShiftsDM", "FetchRecurring");

                DataSource = ods;
                DataTextField = "ShiftName";
                DataValueField = "ShiftID";
                //DataBind();

            }
    
    }
}
