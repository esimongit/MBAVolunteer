
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using NQN.Core;

namespace  NQN.DB
{
   
	public class ShiftTimesDM : DBAccess<ShiftTimesObject>
	{
        public override ObjectList<ShiftTimesObject> FetchAll()
        {
            return Fetch(" Order by ShiftStart ");
        }
		public void Update(ShiftTimesObject obj)
		{
			 string qry = @"UPDATE  ShiftTimes SET 
				ShiftStart=@ShiftStart
				,ShiftEnd=@ShiftEnd
				,TimeSlotName=@TimeSlotName
				 WHERE ShiftTimeID = @ShiftTimeID";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("ShiftTimeID",obj.ShiftTimeID));
				myc.Parameters.Add(new SqlParameter("ShiftStart",obj.ShiftStart));
				myc.Parameters.Add(new SqlParameter("ShiftEnd",obj.ShiftEnd));
				myc.Parameters.Add(new SqlParameter("TimeSlotName",obj.TimeSlotName));
				myc.ExecuteNonQuery();
			}
		}

		public void Save(ShiftTimesObject obj)
		{
			 string qry = @"INSERT INTO ShiftTimes (
				[ShiftStart]
				,[ShiftEnd]
				,[TimeSlotName]
				)
			VALUES(
				@ShiftStart
				,@ShiftEnd
				,@TimeSlotName
				)";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("ShiftStart",obj.ShiftStart));
				myc.Parameters.Add(new SqlParameter("ShiftEnd",obj.ShiftEnd));
				myc.Parameters.Add(new SqlParameter("TimeSlotName",obj.TimeSlotName));
				myc.ExecuteNonQuery();
			}
		}

		public void Delete(int pkey)
		{
			string qry = @"DELETE FROM ShiftTimes WHERE [ShiftTimeID] = @ShiftTimeID";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("ShiftTimeID",pkey));
				 myc.ExecuteNonQuery();
			}
		}

		protected override ShiftTimesObject LoadFrom(SqlDataReader reader)
		{
			if (reader == null) return null;
			if (!reader.Read()) return null;
			ShiftTimesObject obj = new ShiftTimesObject();
			obj.ShiftTimeID = GetNullableInt32(reader, "ShiftTimeID",0);
			obj.ShiftStart = GetNullableDateTime(reader, "ShiftStart",DateTime.Now);
			obj.ShiftEnd = GetNullableDateTime(reader, "ShiftEnd",DateTime.Now);
			obj.TimeSlotName = GetNullableString(reader, "TimeSlotName",String.Empty);
			return obj;
		}

		protected override string ReadAllCommand()
		{
			return @"
			SELECT
				[ShiftTimeID]
				 ,ShiftStart=cast (ShiftStart as DateTime)
                , ShiftEnd=cast (ShiftEnd as DateTime)
				,[TimeSlotName]
				FROM ShiftTimes ";
		}
		public int GetLast()
		{
			string qry = "SELECT IDENT_CURRENT('ShiftTimes')";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				return  Convert.ToInt32( myc.ExecuteScalar());
			}
		}

	}
}
