
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using NQN.Core;

namespace  NQN.DB
{
	public class BatchImportDM : DBAccess<BatchImportObject>
	{

        public ObjectList<BatchImportObject> FetchDecoded()
        {
            ObjectList<BatchImportObject> Results = new ObjectList<BatchImportObject>();
            string qry = @"SELECT
				[ImportID]
				,[ID]
				,[Last]
				,[First]
				,[Email]
				,[Phone]
                ,[Cell]
				,i.[Shift]
				,i.[Role]
				,[ImportStatus]
                , s.ShiftID
                ,r.RoleID
				FROM BatchImport i join Shifts s on i.Shift = s.ShortName join Roles r on r.RoleName = i.Role ";

            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    BatchImportObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        obj.ShiftID = GetNullableInt32(reader, "ShiftID", 0);
                        obj.RoleID = GetNullableInt32(reader, "RoleID", 0);
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }

		public void Update(BatchImportObject obj)
		{
			 string qry = @"UPDATE  BatchImport SET 
				ID=@ID
				,Last=@Last
				,First=@First
				,Email=@Email
				,Phone=@Phone
                ,Cell=@Cell
				,Shift=@Shift
				,Role=@Role
				,ImportStatus=@ImportStatus
				 WHERE ImportID = @ImportID";
      
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("ImportID",obj.ImportID));
				myc.Parameters.Add(new SqlParameter("ID",obj.ID));
				myc.Parameters.Add(new SqlParameter("Last",obj.Last));
				myc.Parameters.Add(new SqlParameter("First",obj.First));
				myc.Parameters.Add(new SqlParameter("Email",obj.Email));
				myc.Parameters.Add(new SqlParameter("Phone",obj.Phone));
                myc.Parameters.Add(new SqlParameter("Cell", obj.Cell));
                myc.Parameters.Add(new SqlParameter("Shift",obj.Shift));
				myc.Parameters.Add(new SqlParameter("Role",obj.Role));
				myc.Parameters.Add(new SqlParameter("ImportStatus",obj.ImportStatus));
				myc.ExecuteNonQuery();
			}
		}

		public void Save(BatchImportObject obj)
		{
			 string qry = @"INSERT INTO BatchImport (
				[ID]
				,[Last]
				,[First]
				,[Email]
				,[Phone]
                ,[Cell]
				,[Shift]
				,[Role]
				,[ImportStatus]
				)
			VALUES(
				@ID
				,@Last
				,@First
				,@Email
				,@Phone
                ,@Cell
				,@Shift
				,@Role
				,@ImportStatus
				)";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("ID",obj.ID));
				myc.Parameters.Add(new SqlParameter("Last",obj.Last));
				myc.Parameters.Add(new SqlParameter("First",obj.First));
				myc.Parameters.Add(new SqlParameter("Email",obj.Email));
				myc.Parameters.Add(new SqlParameter("Phone",obj.Phone));
                myc.Parameters.Add(new SqlParameter("Cell", obj.Cell));
                myc.Parameters.Add(new SqlParameter("Shift",obj.Shift));
				myc.Parameters.Add(new SqlParameter("Role",obj.Role));
				myc.Parameters.Add(new SqlParameter("ImportStatus",obj.ImportStatus));
				myc.ExecuteNonQuery();
			}
		}
        public void Clear()
        {
            string qry = @"DELETE FROM BatchImport";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.ExecuteNonQuery();
            }
        }

		public void Delete(int pkey)
		{
			string qry = @"DELETE FROM BatchImport WHERE [ImportID] = @ImportID";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("ImportID",pkey));
				 myc.ExecuteNonQuery();
			}
		}

		protected override BatchImportObject LoadFrom(SqlDataReader reader)
		{
			if (reader == null) return null;
			if (!reader.Read()) return null;
			BatchImportObject obj = new BatchImportObject();
			obj.ImportID = GetNullableInt32(reader, "ImportID",0);
			obj.ID = GetNullableString(reader, "ID",String.Empty);
			obj.Last = GetNullableString(reader, "Last",String.Empty);
			obj.First = GetNullableString(reader, "First",String.Empty);
			obj.Email = GetNullableString(reader, "Email",String.Empty);
			obj.Phone = GetNullableString(reader, "Phone",String.Empty);
            obj.Cell = GetNullableString(reader, "Cell", String.Empty);
            obj.Shift = GetNullableString(reader, "Shift",String.Empty);
			obj.Role = GetNullableString(reader, "Role",String.Empty);
			obj.ImportStatus = GetNullableInt32(reader, "ImportStatus",0);
            
			return obj;
		}

		protected override string ReadAllCommand()
		{
			return @"
			SELECT
				[ImportID]
				,[ID]
				,[Last]
				,[First]
				,[Email]
				,[Phone]
                ,[Cell]
				,[Shift]
				,[Role]
				,[ImportStatus]
				FROM BatchImport ";
		}
		public int GetLast()
		{
			string qry = "SELECT IDENT_CURRENT('BatchImport')";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				return  Convert.ToInt32( myc.ExecuteScalar());
			}
		}

	}
}
