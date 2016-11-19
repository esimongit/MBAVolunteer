using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NQN.Bus;

namespace NQN.Controls
{
    /// <summary>
    /// Summary description for NQNGridView
    /// </summary>
    public class NQNGridView : GridView
    {
        string SORT_ASC = @"~/Images/ArrowUp.gif";
        string SORT_DESC = @"~/Images/ArrowDown.gif";
        string SORT_NONE = @"~/SiteImages/sort_none.gif";
        string PAGE_PREV = @"~/Images/previous.gif";
        string PAGE_NEXT = @"~/Images/next.gif";
        string m_Privilege = "";
        bool _selectable = false;
        bool _useprivs = true;
        string _deletemessage = "Are you sure you want to delete?";
        #region Properties
        public bool Selectable
        {
            set
            {
                _selectable = value;
            }
        }
        public string DeleteMessage
        {
            get
            {
                return _deletemessage;
            }
            set
            {
                _deletemessage = value;
            }
        }
        public bool UsePrivs
        {
            set
            {
                _useprivs = value;
            }
        }
        public bool AllowMultiColumnSorting
        {
            get
            {
                object o = ViewState["EnableMultiColumnSorting"];
                return (o != null ? (bool)o : false);
            }
            set
            {
                AllowSorting = true;
                ViewState["EnableMultiColumnSorting"] = value;
            }
        }
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
        #endregion
        #region Life Cycle
        protected override void InitializePager(GridViewRow row, int columnSpan, PagedDataSource pagedDataSource)
        {
            
                // if PagerType is not DropDownList
                // render the regular GridView Pager
               // base.InitializePager(row, columnSpan, pagedDataSource);
           
                // our Pager with DropDownList control

                // create our DropDownList control
                DropDownList ddl = new DropDownList();
                ddl.Width = Unit.Pixel(60);
                // populate it with the number of Pages of our GridView
                for (int i = 0; i < PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(Convert.ToString(i + 1), i.ToString()));
                }
                ddl.AutoPostBack = true;
                // assign an Event Handler when its Selected Index Changed
                ddl.SelectedIndexChanged += new EventHandler(ddl_SelectedIndexChanged);
                // synchronize its selected index to GridView's current PageIndex
                ddl.SelectedIndex = PageIndex;

                DropDownList ddl2 = new DropDownList();
                // populate it with the number of Pages of our GridView
                ddl2.Width = Unit.Pixel(60);
                ddl2.Items.Add(new ListItem("10"));
                ddl2.Items.Add(new ListItem("15"));
                ddl2.Items.Add(new ListItem("20"));
                ddl2.Items.Add(new ListItem("25"));
                ddl2.Items.Add(new ListItem("50"));
                ddl2.Items.Add(new ListItem("100"));
                ddl2.AutoPostBack = true;
                // assign an Event Handler when its Selected Index Changed
                ddl2.SelectedIndexChanged += new EventHandler(ddl2_SelectedIndexChanged);
         
                // synchronize its selected index to GridView's current PageIndex
                ddl2.SelectedValue = PageSize.ToString();

                // create a Table that will replace entirely our GridView's Pager section            
                Table tbl = new Table();
                tbl.CellSpacing = 0;
                tbl.BorderWidth = 0;
                tbl.BorderStyle = BorderStyle.None;
                tbl.ForeColor = PagerStyle.ForeColor;
                tbl.BackColor = PagerStyle.BackColor;
                tbl.Width = Unit.Percentage(100);
                // add one TableRow to our Table
                tbl.Rows.Add(new TableRow());

                // add our first TableCell which will contain the DropDownList 
                TableCell cell_1 = new TableCell();
                
                // add new image button 'previous page'
                ImageButton imgPrevButton = new ImageButton();
                imgPrevButton.ImageUrl = PAGE_PREV;
                imgPrevButton.Click += new ImageClickEventHandler(imgPrevButton_Click);
                cell_1.Controls.Add(imgPrevButton);

                // we just add a Label with 'Page ' Text
                cell_1.Controls.Add(PageOf());

                // our DropDownList control here.
                cell_1.Controls.Add(ddl);

            // and our Total number of Pages
                cell_1.Controls.Add(PageTotal());

                // add new image button 'next page'
                ImageButton imgNextButton = new ImageButton();
                imgNextButton.ImageUrl = PAGE_NEXT;
                imgNextButton.Click += new ImageClickEventHandler(imgNextButton_Click);
                cell_1.Controls.Add(imgNextButton);

                // the third TableCell will display the Record number you are currently in.
                TableCell cell_2 = new TableCell();
                TableCell cell_3 = new TableCell();
                cell_3.Controls.Add(PageInfo(pagedDataSource.DataSourceCount));
                cell_2.Controls.Add(ddl2);
                cell_2.Controls.Add(RecordsPerPage());
                
                // add now the 2 cell to our created row
                tbl.Rows[0].Cells.Add(cell_1);
                tbl.Rows[0].Cells.Add(cell_2);
                tbl.Rows[0].Cells.Add(cell_3);
                tbl.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Left;
                tbl.Rows[0].Cells[1].HorizontalAlign = HorizontalAlign.Center;
                tbl.Rows[0].Cells[2].HorizontalAlign = HorizontalAlign.Right;

                // in Pager's Row of our GridView add a TableCell                 
                row.Controls.AddAt(0, new TableCell());
                // sets it span to GridView's number of columns
                row.Cells[0].ColumnSpan = Columns.Count;
                // finally add our created Table
                row.Cells[0].Controls.AddAt(0, tbl);
            
        }

        protected virtual void imgPrevButton_Click(object sender, ImageClickEventArgs e)
        {

            if (PageIndex > 0)
            {
                PageIndex = PageIndex - 1;
            }
        }
        protected virtual void imgNextButton_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex < PageCount)
            {
                PageIndex = PageIndex + 1;
            }
        }
        protected virtual void ddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // on our DropDownList SelectedIndexChanged event
           
            PageIndex = ((DropDownList)sender).SelectedIndex;
        }
        protected virtual void ddl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // on our DropDownList SelectedIndexChanged event
            PageSize = Int32.Parse(((DropDownList)sender).SelectedValue);
          
        }
        protected virtual Label PageOf()
        {
            // it is just a label
            Label lbl = new Label();
            lbl.Text = "Page ";
            return lbl;
        }
        protected virtual Label RecordsPerPage()
        {
            // it is just a label
            Label lbl = new Label();
            lbl.Text = " Records per Page ";
            return lbl;
        }
        protected virtual Label PageTotal()
        {
            // a label of GridView's Page Count
            Label lbl = new Label();
            lbl.Text = string.Format(" of {0}", PageCount);
            return lbl;
        }
        protected virtual Label PageInfo(int rowCount)
        {
            // create a label that will display the current Record you're in
            Label label = new Label();
            int currentPageFirstRow = ((PageIndex * PageSize) + 1);
            int currentPageLastRow = 0;
            int lastPageRemainder = rowCount % PageSize;
            currentPageLastRow = (PageCount == PageIndex + 1) ? (currentPageFirstRow + lastPageRemainder - 1) : (currentPageFirstRow + PageSize - 1);
            // This fails on the last page.  Recalculate
            //label.Text = String.Format("Records {0} to {1} of {2}", currentPageFirstRow, currentPageLastRow, rowCount);
            label.Text = String.Format(" {0} Records ",  rowCount);
            return label;
        }
        protected override void OnSorting(GridViewSortEventArgs e)
        {
            if (AllowMultiColumnSorting)
                e.SortExpression = GetSortExpression(e);

            base.OnSorting(e);
        }

        protected override void OnRowCreated(GridViewRowEventArgs e)
        {
            if (null == e.Row)
                return;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                DisplaySortOrderImages(SortExpression, e.Row);
            }
            else
            {
                if (_useprivs)
                {
                    PagePrivilege p = PagePrivilege.PagePrivilegeFactory();
                    WebControl ctr = (WebControl)FindChildButtonByCommand("Delete", (Control)e.Row);
                    if (null != ctr)
                    {
                        string confirm = String.Format("if (confirm('{0}?')==false)", _deletemessage) + " {return false}";
                        ctr.Attributes.Add("OnClick", confirm);
                        if (null != this.Context && !p.HasPriv(m_Privilege))
                        {
                            ctr.Enabled = false;
                            ctr.ToolTip = "Delete is disabled";
                        }
                    }

                    ctr = (WebControl)FindChildButtonByCommand("Edit", (Control)e.Row);
                    if (null != ctr)
                    {
                        if (null != this.Context && !p.HasPriv(m_Privilege))
                        {
                            ctr.Enabled = false;
                            ctr.ToolTip = "Edit is disabled";
                        }
                    }
                    ctr = (WebControl)FindChildButtonByCommand("Insert", (Control)e.Row);
                    if (null != ctr)
                    {
                        if (null != this.Context && !p.HasPriv(m_Privilege))
                        {
                            ctr.Enabled = false;
                            ctr.ToolTip = "Insert is disabled";
                        }
                    }
                }
    			
		    }
            base.OnRowCreated(e);
             //Make row selectable
            if (_selectable)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    foreach (TableCell cell in e.Row.Cells)
                    {
                        if (!ContainsLink(cell))
                        {
                            cell.Attributes.Add("onclick",
                                Page.ClientScript.GetPostBackEventReference(this,
                                "Select$" + e.Row.RowIndex.ToString()));
                            cell.Style.Add(HtmlTextWriterStyle.Cursor, "pointer");
                            cell.Attributes.Add("title", "Select");
                        }
                    }
                }
            }
        
        }
        Control ContainsControlType(Control control, params Type[] types)
        {
            foreach (Type type in types)
            {
                if (control.GetType().Equals(type))
                    return control;
                else
                    foreach (Control ctrl in control.Controls)
                    {
                        Control tmpCtrl = ContainsControlType(ctrl, type);
                        if (tmpCtrl != null)
                            return tmpCtrl;
                    }
            }
            return null;
        }

        bool ContainsLink(Control control)
        {
            bool ret = false;
            // Any button should be rejected
            if (FindChildButtonByCommand(String.Empty, control) != null)
                return true;
            Control ctrl = ContainsControlType(control, typeof(HyperLink),   typeof(CheckBoxField),  typeof(LinkButton), typeof(DataBoundLiteralControl));
            if (ctrl != null)
            {

                if (ctrl.GetType().Equals(typeof(DataBoundLiteralControl)))
                {
                    DataBoundLiteralControl dblc = (DataBoundLiteralControl)ctrl;
                    if (dblc.Text.Contains("href") || dblc.Text.Contains("onclick"))
                        ret = true;
                }
                else ret = true;
            }
            return ret;
        }
        Control FindChildButtonByCommand(string CommandName, Control Top)
	    {
		    Control ctr;
		    int i;
		    for (i = 0; i < Top.Controls.Count; i++)
		    {
			    ctr = Top.Controls[i];
			    Type t = ctr.GetType();
			    if (t.Name.Contains("Button"))
			    {
				    IButtonControl bctr = (IButtonControl)ctr;
                    if (CommandName == String.Empty || bctr.CommandName == CommandName)
				    {
					    return ctr;
				    }
			    }
			    ctr = FindChildButtonByCommand(CommandName, ctr);
			    if (null != ctr)
				    return ctr;
		    }

		    return null;
	    }

        #endregion
        #region Protected Methods
        /// <summary>
        ///  Get Sort Expression by Looking up the existing Grid View Sort Expression 
        /// </summary>
        protected string GetSortExpression(GridViewSortEventArgs e)
        {
            string[] sortColumns = null;
            string sortAttribute = SortExpression;

            //Check to See if we have an existing Sort Order already in the Grid View.	
            //If so get the Sort Columns into an array
            if (sortAttribute != String.Empty)
            {
                sortColumns = sortAttribute.Split(",".ToCharArray());
            }

            //if User clicked on the columns in the existing sort sequence.
            //Toggle the sort order or remove the column from sort appropriately

            if (sortAttribute.IndexOf(e.SortExpression) > 0 || sortAttribute.StartsWith(e.SortExpression))
                sortAttribute = ModifySortExpression(sortColumns, e.SortExpression);
            else
                sortAttribute += String.Concat(",", e.SortExpression, " ASC ");
            return sortAttribute.TrimStart(",".ToCharArray()).TrimEnd(",".ToCharArray());

        }
        /// <summary>
        ///  Toggle the sort order or remove the column from sort appropriately
        /// </summary>
        protected string ModifySortExpression(string[] sortColumns, string sortExpression)
        {

            string ascSortExpression = String.Concat(sortExpression, " ASC ");
            string descSortExpression = String.Concat(sortExpression, " DESC ");

            for (int i = 0; i < sortColumns.Length; i++)
            {

                if (ascSortExpression.Equals(sortColumns[i]))
                {
                    sortColumns[i] = descSortExpression;
                }

                else if (descSortExpression.Equals(sortColumns[i]))
                {
                    Array.Clear(sortColumns, i, 1);
                }
            }

            return String.Join(",", sortColumns).Replace(",,", ",").TrimStart(",".ToCharArray());

        }
        /// <summary>
        ///  Lookup the Current Sort Expression to determine the Order of a specific item.
        /// </summary>
        protected void SearchSortExpression(string[] sortColumns, string sortColumn, out string sortOrder, out int sortOrderNo)
        {
            sortOrder = "";
            sortOrderNo = -1;
            for (int i = 0; i < sortColumns.Length; i++)
            {
                if (sortColumns[i].StartsWith(sortColumn))
                {
                    sortOrderNo = i + 1;
                    if (AllowMultiColumnSorting)
                        sortOrder = sortColumns[i].Substring(sortColumn.Length).Trim();
                    else
                    {
                        switch (SortDirection)
                        {
                            case SortDirection.Ascending:
                                sortOrder = "ASC";
                                break;
                            case SortDirection.Descending:
                                sortOrder = "DESC";
                                break;
                            default:
                                sortOrder = "NONE";
                                break;
                        }
                    }
                }
            }
        }
        /// <summary>
        ///  Display a graphic image for the Sort Order along with the sort sequence no.
        /// </summary>
        protected void DisplaySortOrderImages(string sortExpression, GridViewRow dgItem)
        {
            string[] sortColumns = sortExpression.Split(",".ToCharArray());

            for (int i = 0; i < dgItem.Cells.Count; i++)
            {
                if (dgItem.Cells[i].Controls.Count > 0 && dgItem.Cells[i].Controls[0] is LinkButton)
                {
                    LinkButton lb = (LinkButton) dgItem.Cells[i].Controls[0];
                    string sortImgLoc = "";
                    string sortOrder;
                    int sortOrderNo;
                    string column = ((LinkButton)dgItem.Cells[i].Controls[0]).CommandArgument;
                    SearchSortExpression(sortColumns, column, out sortOrder, out sortOrderNo);
                    switch (sortOrder)
                    {
                        case "ASC":
                            sortImgLoc = SORT_ASC;
                            break;
                        case "DESC":
                            sortImgLoc = SORT_DESC;
                            break;
                        default:
                            sortImgLoc = SORT_NONE;
                            break;
                    }

                    if (sortImgLoc != String.Empty)
                    {
                        Image imgSortDirection = new Image();
                        imgSortDirection.ImageUrl = sortImgLoc;
                        Label lblSortOrder = new Label();
                        lblSortOrder.Font.Size = 12;
                        lblSortOrder.Text = ((LinkButton)dgItem.Cells[i].Controls[0]).Text;
                        if (sortImgLoc == SORT_NONE)
                        {
                            dgItem.Cells[i].Controls[0].Controls.Clear();
                            dgItem.Cells[i].Controls[0].Controls.AddAt(0, lblSortOrder);
                        }
                        else
                        {
                            dgItem.Cells[i].Controls[0].Controls.AddAt(0, imgSortDirection);
                            dgItem.Cells[i].Controls[0].Controls.AddAt(1, lblSortOrder);
                        }
                        
                        

                    }
                }
            }

        }
        #endregion
    }
}
