using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Zipcodes_ASPNET.Models
{
	public class ZipCodeSQLiteDBContext
	{
		private SQLiteConnection connection;
		private string testingDBFilename = @"..\..\..\..\Data\zipCodesDB.db";
		private string runningDBFilename = @"..\Data\zipCodesDB.db";
		private string dbFilename;

		internal ZipCodeSQLiteDBContext()
		{
			if (System.IO.File.Exists(testingDBFilename))
			{
				dbFilename = testingDBFilename;
			}
			else if (System.IO.File.Exists(runningDBFilename))
			{
				dbFilename = runningDBFilename;
			}
			else
			{
				throw new System.IO.FileNotFoundException("DB File Not Found");
			}

			connection = new SQLiteConnection(string.Format(@"Data Source={0};Version={1}", dbFilename, 3));
		}

		internal List<ZipCodesWithDistance> GetZipCodesWithinRadius(string inputZipCode, int radiusInMiles)
		{
			connection.Open();

			List<ZipCodesWithDistance> result = new List<ZipCodesWithDistance>();

			SQLiteCommand command = GetZipCodesWithinRadiusCommand(connection, inputZipCode, radiusInMiles);

			SQLiteDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				result.Add(new ZipCodesWithDistance()
				{
					Zip1 = reader.GetString(0),
					Zip2 = reader.GetString(1),
					miToZcta5 = (double)reader.GetValue(2)
				});
			}

			connection.Close();
			return result;
		}

		private static SQLiteCommand GetZipCodesWithinRadiusCommand(SQLiteConnection connection, string inputZipCode, int radiusInMiles)
		{
			string selectQuery = "SELECT zip1,zip2,mi_to_zcta5 FROM zipCodesWithDistances WHERE zip1=$zip AND mi_to_zcta5 <= $radius UNION ALL SELECT zip2,zip1,mi_to_zcta5 FROM zipCodesWithDistances WHERE zip2=$zip AND mi_to_zcta5 <= $radius";
			SQLiteCommand command = new SQLiteCommand(selectQuery, connection);
			command.Parameters.AddWithValue("$zip", inputZipCode);
			command.Parameters.AddWithValue("$radius", radiusInMiles);
			return command;
		}

	}
}
