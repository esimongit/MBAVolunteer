
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using NQN.Core;

namespace  NQN.DB
{
	public class MailTextDM : DBAccess<MailTextObject>
	{
        public ObjectList<MailTextObject> FetchForEdit(int MailTextID)
        {            
            ObjectList<MailTextObject> Results = new ObjectList<MailTextObject>();
            string qry = ReadAllCommand() + @" WHERE MailTextID = @MailTextID  ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("MailTextID", MailTextID));

                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    MailTextObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }
       
        public ObjectList<MailTextObject> FetchForObject(string ObjectType)
        {
            ObjectList<MailTextObject> Results = new ObjectList<MailTextObject>();
            string qry = ReadAllCommand() + @" WHERE ObjectType = @ObjectType  order by Symbol";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("ObjectType", ObjectType));

                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    MailTextObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }
        public MailTextObject FetchForSymbol(string Symbol)
        {
            MailTextObject obj = null;
            string qry = ReadAllCommand() + @" WHERE Symbol = @Symbol  ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("Symbol", Symbol));

                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    obj = LoadFrom(reader);                    
                }
            }
            
            return obj;
        }
        
        public void Update(string Subject, string MailFrom, string TextValue, int MailTextID, bool Enabled, bool IsHtml)
        {
            MailTextObject obj = FetchRecord("MailTextID", MailTextID);
            obj.Subject = Subject;
            obj.MailFrom = MailFrom;
            obj.TextValue = TextValue;
            obj.IsHtml = IsHtml;
            obj.Enabled = Enabled;
            Update(obj);
        }
		public void Update(MailTextObject obj)
		{
			 string qry = @"UPDATE  MailText SET 
				Description=@Description
				,TextValue=@TextValue
                ,Subject=@Subject
                ,MailFrom=@MailFrom
                ,Enabled =@Enabled
                ,IsHtml = @IsHtml
                ,ObjectType =@ObjectType
                ,CalendarID = @CalendarID
				 WHERE MailTextID = @MailTextID";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("MailTextID",obj.MailTextID));
				myc.Parameters.Add(new SqlParameter("Description",obj.Description));
                myc.Parameters.Add(new SqlParameter("TextValue", obj.TextValue));
                myc.Parameters.Add(new SqlParameter("Subject", obj.Subject));
                myc.Parameters.Add(new SqlParameter("MailFrom", obj.MailFrom));
                myc.Parameters.Add(new SqlParameter("Enabled", obj.Enabled));
                myc.Parameters.Add(new SqlParameter("IsHtml", obj.IsHtml));
                myc.Parameters.Add(new SqlParameter("ObjectType", obj.ObjectType));

                myc.Parameters.Add(new SqlParameter("CalendarID", obj.CalendarID));
				myc.ExecuteNonQuery();
			}
		}

		public void Save(MailTextObject obj)
		{
			 string qry = @"INSERT INTO MailText (
				[Symbol]
				,[Description]
				,[TextValue]
                ,[Subject]
                ,[MailFrom]
                ,[Enabled]
                ,[IsHtml]
                ,[ObjectType]
                ,[CalendarID]
				)
			VALUES(
				@Symbol
				,@Description
				,@TextValue
                ,@Subject
                ,@MailFrom
                ,@Enabled
                ,@IsHtml
                ,@ObjectType
                ,@CalendarID
				)";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("Symbol",obj.Symbol));
				myc.Parameters.Add(new SqlParameter("Description",obj.Description));
                myc.Parameters.Add(new SqlParameter("TextValue", obj.TextValue));
                myc.Parameters.Add(new SqlParameter("Subject", obj.Subject));
                myc.Parameters.Add(new SqlParameter("MailFrom", obj.MailFrom));
                myc.Parameters.Add(new SqlParameter("Enabled", obj.Enabled));
                myc.Parameters.Add(new SqlParameter("IsHtml", obj.IsHtml));
                myc.Parameters.Add(new SqlParameter("ObjectType", obj.ObjectType));
                myc.Parameters.Add(new SqlParameter("CalendarID", obj.CalendarID));
				myc.ExecuteNonQuery();
			}
		}
        public void Clone(int MailTextID, string Symbol)
        {
            string qry = @"INSERT INTO MailText (
				[Symbol]
				,[Description]
				,[TextValue]
                ,[Subject]
                ,[MailFrom]
                ,[Enabled] 
                ,[IsHtml]
                ,[ObjectType]
                ,[CalendarID]
				)
                SELECT @Symbol, '--New Message--', TextValue, Subject, MailFrom, 0,  IsHtml, ObjectType, CalendarID
                from MailText where MailTextID = @MailTextID and not exists (
                    select 1 from MailText where Symbol = @Symbol) ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("MailTextID", MailTextID));
                myc.Parameters.Add(new SqlParameter("Symbol", Symbol));
                myc.ExecuteNonQuery();
            }
            int NewTextID = GetLast();
            MailTextVarsDM dm = new MailTextVarsDM();
           
        }
		public void Delete(int MailTextID)
		{
            
			string qry = @"DELETE FROM MailText WHERE [MailTextID] = @MailTextID and Enabled = 0 ";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("MailTextID",MailTextID));
				 myc.ExecuteNonQuery();
			}
		}

		protected override MailTextObject LoadFrom(SqlDataReader reader)
		{
			if (reader == null) return null;
			if (!reader.Read()) return null;
			MailTextObject obj = new MailTextObject();
			obj.MailTextID = GetNullableInt32(reader, "MailTextID",0);
			obj.Symbol = GetNullableString(reader, "Symbol",String.Empty);
			obj.Description = GetNullableString(reader, "Description",String.Empty);
            obj.TextValue = GetNullableString(reader, "TextValue", String.Empty);
            obj.Subject = GetNullableString(reader, "Subject", String.Empty);
            obj.MailFrom = GetNullableString(reader, "MailFrom", String.Empty);
            obj.Enabled = GetNullableBoolean(reader, "Enabled", false);
            obj.IsHtml = GetNullableBoolean(reader, "IsHtml", false);
            obj.ObjectType = GetNullableString(reader, "ObjectType", String.Empty);
            obj.CalendarID = GetNullableInt32(reader, "CalendarID", 0);
			return obj;
		}

		protected override string ReadAllCommand()
		{
			return @"
			SELECT
				[MailTextID]
				,[Symbol]
				,[Description]
				,[TextValue]
                ,[Subject]
                ,[MailFrom]
                ,[Enabled]
                ,[IsHtml]
                ,[ObjectType]
                ,[CalendarID]
				FROM MailText ";
		}
		public int GetLast()
		{
			string qry = "SELECT IDENT_CURRENT('MailText')";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				return  Convert.ToInt32( myc.ExecuteScalar());
			}
		}

	}
}
