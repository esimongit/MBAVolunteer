using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using NQN.Core;
using NQN.Bus;

namespace VolManager
{
    public class TreeViewState
    {

        public void SaveTreeView(TreeView treeView)
        {
            List<bool> list = new List<bool>();
            SaveTreeViewExpandedState(treeView.Nodes, list);
            HttpContext.Current.Session["NavTree"] = list;
            //CookieManager.SetCookie("VolNav", "NavTree", Serialize(list));
        }

        private int RestoreTreeViewIndex;
        // Read the session first.  If no session, look for a cookie.  If no cookie, re-init.
        public void RestoreTreeView(TreeView treeView)
        {
            RestoreTreeViewIndex = 0;
            RestoreTreeViewExpandedState(treeView.Nodes,
               (List<bool>)HttpContext.Current.Session["NavTree"] ??
                 (UnSerializeNavCookie() ??
                    (new List<bool>())));
        }

        private string Serialize(List<bool> list)
        {
            string s = String.Empty;
            foreach (bool b in list)
            {
                s += b ? "1" : "0";
            }
            return s;
        }
        private List<bool> UnSerializeNavCookie()
        {
            string s = CookieManager.ReadCookie("VolNav","NavTree");
            if (s == String.Empty) return null;

            List<bool> list = new List<bool>();
            foreach (char c in s.ToCharArray())
            {
                list.Add(c == '1' ? true : false);
            }
            return list;
        }
        private void SaveTreeViewExpandedState(TreeNodeCollection nodes, List<bool> list)
        {
            foreach (TreeNode node in nodes)
            {
                list.Add(node.Expanded ?? false);
                if (node.ChildNodes.Count > 0)
                {
                    SaveTreeViewExpandedState(node.ChildNodes, list);
                }
            }
        }

        private void RestoreTreeViewExpandedState(TreeNodeCollection nodes, List<bool> list)
        {
            foreach (TreeNode node in nodes)
            {
                if (RestoreTreeViewIndex >= list.Count) break;

                node.Expanded = list[RestoreTreeViewIndex++];
                if (node.ChildNodes.Count > 0)
                {
                    RestoreTreeViewExpandedState(node.ChildNodes, list);
                }
            }
        }
    }
}