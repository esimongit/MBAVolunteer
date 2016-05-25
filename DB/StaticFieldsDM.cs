
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using NQN.Core;

namespace NQN.DB
{	
    public class StaticFieldsDM : DBAccess<StaticFieldsObject>
	{
 
        public StaticFieldsDM()
        {
        }
        public StaticFieldsDM(string db)
        {
            DB = db;
        }
        public override ObjectList<StaticFieldsObject> FetchAll()
        {
            ObjectList<StaticFieldsObject> Results = new ObjectList<StaticFieldsObject>();
            string qry = ReadAllCommand() + " order by st.FieldName, sf.Sequence ";
            using (SqlConnection conn = ConnectionFactory.getNew(DB))
            {
                SqlCommand myc = new SqlCommand(qry, conn);

                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    StaticFieldsObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }
         

        public ObjectList<StaticFieldsObject>FetchForService()
        {
             ObjectList<StaticFieldsObject> Results = new ObjectList<StaticFieldsObject>();
            string sql = @"
			SELECT
                sf.[StaticFieldID]
				,st.[FieldName]
                ,st.[Description]
				,sf.[FieldValue]
                ,sf.[Sequence]
				FROM    StaticTags st join StaticFields sf on st.FieldName = sf.FieldName";
            using (SqlConnection conn = ConnectionFactory.getNew(DB))
            {
                SqlCommand myc = new SqlCommand(sql, conn);

                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    StaticFieldsObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }
        public ObjectList<StaticFieldsObject> FetchValue(string FieldName)
        {
            string sql = ReadAllCommand() + " WHERE st.FieldName = @FieldName order by Sequence";
            ObjectList<StaticFieldsObject> Results = new ObjectList<StaticFieldsObject>();
            using (SqlConnection conn = ConnectionFactory.getNew(DB))
            {
                SqlCommand myc = new SqlCommand(sql, conn);
                myc.Parameters.Add(new SqlParameter("FieldName", FieldName));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    StaticFieldsObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }

                }
            }

            return Results;
        }
        public void Update(StaticFieldsObject obj)
        {
            if (obj.StaticFieldID == 0)
            {
                Save(obj);
                return;
            }
            if (obj.Sequence == 0)
                obj.Sequence = 1;
            if (obj.FieldValue == null)
            {
                Delete(obj);
                return;
            }
            
            string qry = @"UPDATE StaticFields 
				set [FieldValue]= @FieldValue
                ,[FieldName] = @FieldName
                ,[Sequence] = @Sequence
                WHERE StaticFieldID = @StaticFieldID";
			
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("StaticFieldID", obj.StaticFieldID));
                myc.Parameters.Add(new SqlParameter("FieldName", obj.FieldName));
                myc.Parameters.Add(new SqlParameter("FieldValue", obj.FieldValue));
                myc.Parameters.Add(new SqlParameter("Sequence", obj.Sequence));
                myc.ExecuteNonQuery();
            }
        }
        public void Insert(string FieldName, string FieldValue, int Sequence)
        {
            StaticFieldsObject obj = new StaticFieldsObject();
            obj.FieldName = FieldName;
            obj.FieldValue = FieldValue;
            obj.Sequence = Sequence;
            Save(obj);
        }
        public void Save(StaticFieldsObject obj)
		{
            string qry = @"INSERT INTO StaticFields (
				[FieldName], [FieldValue], [Sequence]
				)
			VALUES(
				@FieldName, @FieldValue, @Sequence
				)";
			 using (SqlConnection conn = ConnectionFactory.getNew(DB))
			{
				SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("FieldName", obj.FieldName));
				myc.Parameters.Add(new SqlParameter("FieldValue",obj.FieldValue));
                myc.Parameters.Add(new SqlParameter("Sequence", obj.Sequence));
				myc.ExecuteNonQuery();
			}
		}
        public void SaveWithKey(StaticFieldsObject obj)
        {
            string qry = @"INSERT INTO StaticFields (
				[StaticFieldID], [FieldName], [FieldValue], [Sequence]
				)
			VALUES(
				 @StaticFieldID, @FieldName, @FieldValue, @Sequence
				)";
            using (SqlConnection conn = ConnectionFactory.getNew(DB))
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("StaticFieldID", obj.StaticFieldID));
                myc.Parameters.Add(new SqlParameter("FieldName", obj.FieldName));
                myc.Parameters.Add(new SqlParameter("FieldValue", obj.FieldValue));
                myc.Parameters.Add(new SqlParameter("Sequence", obj.Sequence));
                myc.ExecuteNonQuery();
            }
        }

        public void Delete(StaticFieldsObject obj)
        {
            Delete(obj.StaticFieldID);
        }
        public void Delete(int StaticFieldID)
		{
            string qry = @"DELETE FROM StaticFields WHERE [StaticFieldID] = @StaticFieldID";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("StaticFieldID", StaticFieldID));
				 myc.ExecuteNonQuery();
			}
		}
        // Only works in LOCAL db with no constraints.   
        public void Clear()
        {
            string qry = @"DELETE FROM StaticFields";
            using (SqlConnection conn = ConnectionFactory.getNew(DB))
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.ExecuteNonQuery();
            }
        }
        protected override StaticFieldsObject LoadFrom(SqlDataReader reader)
		{
			if (reader == null) return null;
			if (!reader.Read()) return null;
			StaticFieldsObject obj = new StaticFieldsObject();
            obj.StaticFieldID = GetNullableInt32(reader, "StaticFieldID", 0);
            obj.FieldName = GetNullableString(reader, "FieldName", String.Empty);
            obj.Description = GetNullableString(reader, "Description", String.Empty);
			obj.FieldValue = GetNullableString(reader, "FieldValue",String.Empty);
            obj.Sequence = GetNullableInt32(reader, "Sequence", 0);
			return obj;
		}

		protected override string ReadAllCommand()
		{
			return @"
			SELECT
                sf.[StaticFieldID]
				,st.[FieldName]
                ,st.[Description]
				,sf.[FieldValue]
                ,sf.[Sequence]
				FROM    StaticTags st left join StaticFields sf on st.FieldName = sf.FieldName";
		}
        public void ClearTags()
        {
            string qry = @"Delete from StaticTags";
            using (SqlConnection conn = ConnectionFactory.getNew(DB))
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.ExecuteNonQuery();
            }
        }
        public ObjectList<StaticTagsObject> FetchTags()
        {
            ObjectList<StaticTagsObject> Results = new ObjectList<StaticTagsObject>();
            string qry = "SELECT FieldName, Description FROM StaticTags order by FieldName";
            using (SqlConnection conn = ConnectionFactory.getNew(DB))
            {
                SqlCommand myc = new SqlCommand(qry, conn);

                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        StaticTagsObject obj = new StaticTagsObject();
                        obj.FieldName = GetNullableString(reader, "FieldName", String.Empty);
                        obj.Description = GetNullableString(reader, "Description", String.Empty);
                        Results.Add(obj);
                    }
                }
            }
            return Results;
        }
        public void SaveTags(StaticTagsObject obj)
        {
            string qry = @"INSERT INTO StaticTags (
				  [FieldName], [Description]
				)
			VALUES(
				   @FieldName, @Description 
				)";
            using (SqlConnection conn = ConnectionFactory.getNew(DB))
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                 myc.Parameters.Add(new SqlParameter("FieldName", obj.FieldName));
                 myc.Parameters.Add(new SqlParameter("Description", obj.Description)); 
                myc.ExecuteNonQuery();
            }
        }
	}
}
