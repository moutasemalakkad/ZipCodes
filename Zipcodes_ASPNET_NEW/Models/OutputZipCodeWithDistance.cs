using Zipcodes_ASPNET.Models;

namespace Zipcodes_ASPNET.Models
{
	public class OutputZipCodeWithDistance
	{
		public double Distance { get; private set; }
		public string ZipCode { get; private set; }

		internal OutputZipCodeWithDistance(ZipCodesWithDistance w, string userZipCode)
		{
			Distance = w.miToZcta5;
			ZipCode = w.Zip1.Equals(userZipCode) ? w.Zip2 : w.Zip1;
		}
	}
}