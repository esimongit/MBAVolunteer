
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using NQN.Core;

namespace  NQN.DB
{
	public class MenuNavigationDM : DBAccess<MenuNavigationObject>
	{
        public ObjectList<MenuNavigationObject> FetchForPrivs()
        {
            ObjectList<MenuNavigationObject> Results = new ObjectList<MenuNavigationObject>();
            string qry = ReadAllCommand() + @" WHERE mn.Selectable = 1
                order by p.sequence, s.text  ";

            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    MenuNavigationObject obj = LoadFrom(reader);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }
        public ObjectList<MenuNavigationObject> FetchMenu(int MenuID)
        {
            ObjectList<MenuNavigationObject> Results = new ObjectList<MenuNavigationObject>();
            string qry = ReadAllCommand() + @" WHERE mn.MenuID = @MenuID and mn.enabled = 1
                order by p.sequence, mn.sequence ";

            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    MenuNavigationObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }
        public ObjectList<MenuNavigationObject> FetchLevel(int MenuID, int ParentID)
        {
            ObjectList<MenuNavigationObject> Results = new ObjectList<MenuNavigationObject>();
            string qry = ReadAllCommand() + @" WHERE mn.MenuID = @MenuID and @ParentID = isnull(mn.ParentID,0) and mn.enabled = 1
                order by mn.sequence ";

            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("ParentID", ParentID));
                myc.Parameters.Add(new SqlParameter("MenuID", MenuID));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    MenuNavigationObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }
		public void Update(MenuNavigationObject obj)
		{
			 string qry = @"UPDATE  MenuNavigation SET  
				,ParentID=@ParentID
				,Selectable=@Selectable
				,Sequence=@Sequence
				,Enabled=@Enabled
				 WHERE  = MenuID =@MenuID and ScreenID = @ScreenID ";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("MenuID",obj.MenuID));
				myc.Parameters.Add(new SqlParameter("ScreenID",obj.ScreenID));
				myc.Parameters.Add(new SqlParameter("ParentID",obj.ParentID));
				myc.Parameters.Add(new SqlParameter("Selectable",obj.Selectable));
				myc.Parameters.Add(new SqlParameter("Sequence",obj.Sequence));
				myc.Parameters.Add(new SqlParameter("Enabled",obj.Enabled));
				myc.ExecuteNonQuery();
			}
		}

		public void Save(MenuNavigationObject obj)
		{
			 string qry = @"INSERT INTO MenuNavigation (
				[MenuID]
				,[ScreenID]
				,[ParentID]
				,[Selectable]
				,[Sequence]
				,[Enabled]
				)
			VALUES(
				@MenuID
				,@ScreenID
				,@ParentID
				,@Selectable
				,@Sequence
				,@Enabled
				)";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("MenuID",obj.MenuID));
				myc.Parameters.Add(new SqlParameter("ScreenID",obj.ScreenID));
				myc.Parameters.Add(new SqlParameter("ParentID",obj.ParentID));
				myc.Parameters.Add(new SqlParameter("Selectable",obj.Selectable));
				myc.Parameters.Add(new SqlParameter("Sequence",obj.Sequence));
				myc.Parameters.Add(new SqlParameter("Enabled",obj.Enabled));
				myc.ExecuteNonQuery();
			}
		}

		public void Delete(int MenuID, int ScreenID)
		{
            string qry = @" Delete from MenuNavigation where MenuID = @MenuID and ScreenID = @ScreenID";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("MenuID", MenuID));
                myc.Parameters.Add(new SqlParameter("ScreenID", ScreenID));
				 myc.ExecuteNonQuery();
			}
		}

		protected override MenuNavigationObject LoadFrom(SqlDataReader reader)
		{
			if (reader == null) return null;
			if (!reader.Read()) return null;
			MenuNavigationObject obj = new MenuNavigationObject();
			obj.MenuID = GetNullableInt32(reader, "MenuID",0);
			obj.ScreenID = GetNullableInt32(reader, "ScreenID",0);
			obj.ParentID = GetNullableInt32(reader, "ParentID",0);
			obj.Selectable = GetNullableBoolean(reader, "Selectable",false);
			obj.Sequence = GetNullableInt32(reader, "Sequence",0);
			obj.Enabled = GetNullableBoolean(reader, "Enabled",false);
            obj.ScreenName = GetNullableString(reader, "ScreenName", String.Empty);
            obj.Text = GetNullableString(reader, "Text", String.Empty);
            obj.ToolTip = GetNullableString(reader, "ToolTip", String.Empty);
            obj.Menu = GetNullableString(reader, "Menu", String.Empty);
            //obj.Category = GetNullableString(reader, "Category", String.Empty);
			return obj;
		}

		protected override string ReadAllCommand()
		{
			return @"
			SELECT
				mn.[MenuID]
				,mn.[ScreenID]
				,mn.[ParentID]
				,mn.[Selectable]
				,mn.[Sequence]
				,mn.[Enabled]
                ,s.ScreenName 
				,s.[Text]
				,s.[ToolTip]
                ,m.Menu
               
				FROM MenuNavigation mn join Screens s on s.ScreenID = mn.ScreenID
                join NavigationMenus m on m.MenuID = mn.MenuID 
                left join ( MenuNavigation p  join Screens s2 on p.ScreenID = s2.ScreenID)
                    on mn.ParentID = p.ScreenID and mn.MenuID = p.MenuID ";
		}
		public int GetLast()
		{
			string qry = "SELECT IDENT_CURRENT('MenuNavigation')";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				return  Convert.ToInt32( myc.ExecuteScalar());
			}
		}

	}
}
