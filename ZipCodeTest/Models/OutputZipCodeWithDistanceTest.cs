using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Zipcodes_ASPNET.Models;

namespace ZipCodeTest.Models
{
	public class OutputZipCodeWithDistanceTest
	{
		private string wrigleyville = "60613";
		private string andersonville = "60640";

		private ZipCodesWithDistance chi = new ZipCodesWithDistance()
		{
			Zip1 = "60640",
			Zip2 = "60613",
			miToZcta5 = 1
		};

		[Fact]
		public void UserZipCodeDoesNotMatchResult()
		{
			var result = new OutputZipCodeWithDistance(chi, wrigleyville);
			Assert.NotEqual(wrigleyville, result.ZipCode);
		}

		[Fact]
		public void WhenUserZipCodeMatchesZip1UseZip2()
		{
			var result = new OutputZipCodeWithDistance(chi, wrigleyville);
			Assert.Equal(andersonville, result.ZipCode);
		}

		[Fact]
		public void WhenUserZipCodeMatchesZip2UseZip1()
		{
			var result = new OutputZipCodeWithDistance(chi, andersonville);
			Assert.Equal(wrigleyville, result.ZipCode);
		}
	}
}
