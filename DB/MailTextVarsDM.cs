
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using NQN.Core;

namespace  NQN.DB
{
	public class MailTextVarsDM : DBAccess<MailTextVarsObject>
	{
        public ObjectList<MailTextVarsObject> FetchForMailText(int MailTextID)
        {
             
            ObjectList<MailTextVarsObject> Results = new ObjectList<MailTextVarsObject>();
            string qry = ReadAllCommand() + @" WHERE ObjectType in (select ObjectType from MailText where MailTextID = @MailTextID) 
                order by VarDescription ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("MailTextID", MailTextID));

                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    MailTextVarsObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }
       
		public void Update(MailTextVarsObject obj)
		{
			 string qry = @"UPDATE  MailTextObjectVars SET 
				ObjectType=@ObjectType
				,VarSymbol=@VarSymbol
				,VarDescription=@VarDescription
				 WHERE MailTextObjectVarID = @MailTextObjectVarID";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("MailTextObjectVarID",obj.ObjectType));
				myc.Parameters.Add(new SqlParameter("ObjectType",obj.ObjectType));
				myc.Parameters.Add(new SqlParameter("VarSymbol",obj.VarSymbol));
				myc.Parameters.Add(new SqlParameter("VarDescription",obj.VarDescription));
				myc.ExecuteNonQuery();
			}
		}

		public void Save(MailTextVarsObject obj)
		{
            string qry = @"INSERT INTO MailTextObjectVars (
				[ObjectType]
				,[VarSymbol]
				,[VarDescription]
				)
			VALUES(
				@ObjectType
				,@VarSymbol
				,@VarDescription
				)";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("ObjectType",obj.ObjectType));
				myc.Parameters.Add(new SqlParameter("VarSymbol",obj.VarSymbol));
				myc.Parameters.Add(new SqlParameter("VarDescription",obj.VarDescription));
				myc.ExecuteNonQuery();
			}
		}
        
		public void Delete(int pkey)
		{
            string qry = @"DELETE FROM MailTextObjectVars WHERE [MailTextObjectVarID] = @MailTextObjectVarID";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("MailTextObjectVarID",pkey));
				 myc.ExecuteNonQuery();
			}
		}

		protected override MailTextVarsObject LoadFrom(SqlDataReader reader)
		{
			if (reader == null) return null;
			if (!reader.Read()) return null;
			MailTextVarsObject obj = new MailTextVarsObject();
			obj.MailTextObjectVarID = GetNullableInt32(reader, "MailTextObjectVarID",0);
            obj.ObjectType = GetNullableString(reader, "ObjectType", String.Empty);
			obj.VarSymbol = GetNullableString(reader, "VarSymbol",String.Empty);
			obj.VarDescription = GetNullableString(reader, "VarDescription",String.Empty);
			return obj;
		}

		protected override string ReadAllCommand()
		{
			return @"
			SELECT
				[MailTextObjectVarID]
				,[ObjectType]
				,[VarSymbol]
				,[VarDescription]
				FROM MailTextObjectVars ";
		}
		public int GetLast()
		{
			string qry = "SELECT IDENT_CURRENT('MailTextObjectVars')";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				return  Convert.ToInt32( myc.ExecuteScalar());
			}
		}

	}
}
