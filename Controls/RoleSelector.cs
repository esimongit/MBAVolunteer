using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NQN.DB;
using NQN.Core;

namespace NQN.Controls
{
    public class RoleSelector : DropDownList
    {

        public RoleSelector()
            {
                Items.Add(new ListItem("(Select a Role)", "0"));
                AppendDataBoundItems = true;
                ObjectDataSource ods = new ObjectDataSource("NQN.DB.RolesDM", "FetchAll");

                DataSource = ods;
                DataTextField = "RoleName";
                DataValueField = "RoleID";
                //DataBind();

            }
    
    }
}
