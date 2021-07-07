using Xunit;
using Zipcodes_ASPNET.Controllers;
using Microsoft.Extensions.Logging;
using Zipcodes_ASPNET.Models;

namespace ZipCodeTest
{
	public class ZipCodeControllerTests
	{

		Logger<HomeController> logger = new Logger<HomeController>(new LoggerFactory());

		[Fact]
		public void WhenZipCodesRequestedFetchZipCodes()
		{
			HomeController controller = new HomeController(logger);
			Microsoft.AspNetCore.Mvc.JsonResult response = controller.GetZipCodesWithInRangeRadius("00622", 50);
			Assert.NotNull(response);
			ZipCodeOutputDTO responseObject = (ZipCodeOutputDTO)response.Value;
			Assert.True(responseObject.success);
			Assert.Empty(responseObject.errorMessage);
			Assert.NotNull(responseObject.data);
			Assert.NotEmpty(responseObject.data);
		}

		[Fact]
		public void WhenZipCodesFetchedAssertOrderedByDistanceAscending()
		{
			HomeController controller = new HomeController(logger);
			Microsoft.AspNetCore.Mvc.JsonResult response = controller.GetZipCodesWithInRangeRadius("00622", 50);
			ZipCodeOutputDTO responseObject = (ZipCodeOutputDTO)response.Value;

			for (int i = 0; i < responseObject.data.Count - 1; i++)
			{
				Assert.True(responseObject.data[i].Distance <= responseObject.data[i + 1].Distance);
			}
		}

		[Theory]
		[InlineData("00622", 50, 45)]
		[InlineData("00601", 10, 5)]
		public void CheckExpectedResults(string zipCode, int miles, int expected)
		{
			HomeController controller = new HomeController(logger);
			Microsoft.AspNetCore.Mvc.JsonResult response = controller.GetZipCodesWithInRangeRadius(zipCode, miles);
			ZipCodeOutputDTO responseObject = (ZipCodeOutputDTO)response.Value;
			Assert.Equal(expected, responseObject.data.Count);
		}
	}
}
