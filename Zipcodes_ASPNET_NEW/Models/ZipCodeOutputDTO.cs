using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zipcodes_ASPNET.Controllers;

namespace Zipcodes_ASPNET.Models
{
	public class ZipCodeOutputDTO
	{
		public bool success { get; set; }
		public string errorMessage { get; set; }
		public List<OutputZipCodeWithDistance> data { get; set; }
	}
}
