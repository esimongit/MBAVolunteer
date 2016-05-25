using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using NQN.DB;
using NQN.Core;


/// <summary>
/// Summary description for Hierarchy
/// </summary>
namespace NQN.Bus {
    public class NavHierarchy
    {

        //MenuItemCollection Tree;
       
        //ObjectList<NavigationObject> MenuList;

        public NavHierarchy()
        {
        }
          
        
        
        public  MenuItemCollection BuildMenu(int MenuID)
        {
            return BuildLevel(MenuID, 0);
        }
        public MenuItemCollection BuildLevel(int MenuID, int ParentID)
        {
            MenuItemCollection level = new MenuItemCollection();
            MenuNavigationDM dm = new MenuNavigationDM();
            ObjectList<MenuNavigationObject> dList = dm.FetchLevel(MenuID, ParentID);
            MenuItemCollection mitems = new MenuItemCollection();
            foreach (MenuNavigationObject obj in dList)
            {
                MenuItem m = new MenuItem();
                m.Text = obj.Text;
                m.NavigateUrl = obj.ScreenName.Contains("http") ? obj.ScreenName : String.Format("~/{0}.aspx", obj.ScreenName);
                m.ToolTip = obj.ToolTip;
                m.Selectable = obj.Selectable;
                m.Value = obj.ScreenName;
                foreach(MenuItem itm in  BuildLevel(MenuID, obj.NavID))
                    m.ChildItems.Add(itm);
                level.Add(m);
            }
            return level;
        }


       
        public  MenuItem FindNodeByLink(string Link,  MenuItemCollection nodes, ref string txt)
        {

            for (int i = 0; i < nodes.Count; i++)
            {
                if ((nodes[i].NavigateUrl) == Link)
                {
                    txt = nodes[i].Text;
                    return nodes[i];
                }
                else
                {
                    if (nodes[i].ChildItems.Count > 0)
                    {
                        MenuItem FoundNode = FindNodeByLink(Link, nodes[i].ChildItems, ref txt);
                        if (FoundNode != (MenuItem)null)
                        {
                            txt = nodes[i].Text + "/" + txt;
                            return FoundNode;
                        }
                    }
                }
            }
            return (MenuItem)null;
        }
        /* Same function using Value.  txt is used to parse nav tree */
        public static MenuItem FindNodeByValue(string Value, MenuItemCollection nodes, ref string txt)
        {

            for (int i = 0; i < nodes.Count; i++)
            {
                if ((nodes[i].Value) == Value)
                {
                    txt = nodes[i].Text;
                    return nodes[i];
                }
                else
                {
                    if (nodes[i].ChildItems.Count > 0)
                    {
                        MenuItem FoundNode = FindNodeByValue(Value, nodes[i].ChildItems, ref txt);
                        if (FoundNode != (MenuItem)null)
                        {
                            if (txt != "")
                            {
                                txt = nodes[i].Text + "/" + txt;
                            }
                            else
                            {
                                txt = nodes[i].Text;
                            }
                            return FoundNode;
                        }
                    }
                }
            }
            return (MenuItem)null;
        }


    }
}
