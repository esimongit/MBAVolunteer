
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using NQN.Core;

namespace  NQN.DB
{
	public class MailTextObjectVarsDM : DBAccess<MailTextObjectVarsObject>
	{
		public void Update(MailTextObjectVarsObject obj)
		{
			 string qry = @"UPDATE  MailTextObjectVars SET 
				ObjectType=@ObjectType
				,VarSymbol=@VarSymbol
				,VarDescription=@VarDescription
				 WHERE MailTextObjectVarID = @MailTextObjectVarID";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("MailTextObjectVarID",obj.MailTextObjectVarID));
				myc.Parameters.Add(new SqlParameter("ObjectType",obj.ObjectType));
				myc.Parameters.Add(new SqlParameter("VarSymbol",obj.VarSymbol));
				myc.Parameters.Add(new SqlParameter("VarDescription",obj.VarDescription));
				myc.ExecuteNonQuery();
			}
		}

		public void Save(MailTextObjectVarsObject obj)
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

		protected override MailTextObjectVarsObject LoadFrom(SqlDataReader reader)
		{
			if (reader == null) return null;
			if (!reader.Read()) return null;
			MailTextObjectVarsObject obj = new MailTextObjectVarsObject();
			obj.MailTextObjectVarID = GetNullableInt32(reader, "MailTextObjectVarID",0);
			obj.ObjectType = GetNullableString(reader, "ObjectType",String.Empty);
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
