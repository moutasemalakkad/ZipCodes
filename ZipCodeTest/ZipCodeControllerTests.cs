using Xunit;
using Zipcodes_ASPNET.Controllers;
using Microsoft.Extensions.Logging;
using Zipcodes_ASPNET.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ZipCodeTest
{
	public class ZipCodeControllerTests
	{

		Logger<HomeController> logger = new Logger<HomeController>(new LoggerFactory());
		HomeController controller;

		private string validZipCode = "00622";

		public ZipCodeControllerTests()
		{
			controller = new HomeController(logger);
		}

		[Fact(DisplayName ="When Valid Request, Expect Data")]
		public void GivenZipCodesRequestedWhenValidRequestFetchZipCodes()
		{
			Microsoft.AspNetCore.Mvc.ActionResult response = controller.GetZipCodesWithInRangeRadius(validZipCode, 50);
			OkObjectResult result = Assert.IsType<OkObjectResult>(response);
			Assert.Equal(200, result.StatusCode);

			object resultObject = result.Value;
			ZipCodeOutputDTO resultData = Assert.IsType<ZipCodeOutputDTO>(resultObject);

			Assert.True(resultData.success);
			Assert.Empty(resultData.errorMessage);
			Assert.NotNull(resultData.data);
			Assert.NotEmpty(resultData.data);
		}

		[Theory(DisplayName = "Expected Results")]
		[InlineData("00622", 50, 45)]
		[InlineData("00601", 10, 5)]
		[InlineData("60640", 10, 56)]
		[InlineData("63130", 10, 48)]
		public void CheckExpectedResults(string zipCode, int miles, int expected)
		{
			Microsoft.AspNetCore.Mvc.ActionResult response = controller.GetZipCodesWithInRangeRadius(zipCode, miles);
			OkObjectResult result = Assert.IsType<OkObjectResult>(response);
			ZipCodeOutputDTO resultData = Assert.IsType<ZipCodeOutputDTO>(result.Value);
			Assert.Equal(expected, resultData.data.Count);
		}

		[Fact(DisplayName = "When Results Returned, Expected Ordered By Distance Ascending")]
		public void GivenZipCodesRequestedWhenZipCodesReturnedExpectOrderedByDistanceAscending()
		{
			Microsoft.AspNetCore.Mvc.ActionResult response = controller.GetZipCodesWithInRangeRadius(validZipCode, 50);
			OkObjectResult result = Assert.IsType<OkObjectResult>(response);
			ZipCodeOutputDTO resultData = Assert.IsType<ZipCodeOutputDTO>(result.Value);

			for (int i = 0; i < resultData.data.Count - 1; i++)
			{
				Assert.True(resultData.data[i].Distance <= resultData.data[i + 1].Distance);
			}
		}

		[Fact(DisplayName = "When Results Returned, Expected Distinct Results")]
		public void GivenZipCodesRequestedWhenZipCodesReturnedExpectDistinctResults()
		{
			Microsoft.AspNetCore.Mvc.ActionResult response = controller.GetZipCodesWithInRangeRadius(validZipCode, 50);
			OkObjectResult result = Assert.IsType<OkObjectResult>(response);
			ZipCodeOutputDTO resultData = Assert.IsType<ZipCodeOutputDTO>(result.Value);

			HashSet<string> zipCodes = new HashSet<string>();

			for (int i = 0; i < resultData.data.Count; i++)
			{
				Assert.DoesNotContain(resultData.data[i].ZipCode, zipCodes);
				zipCodes.Add(resultData.data[i].ZipCode);
			}
		}

		[Fact(DisplayName = "When No Data, Expect No Content")]
		public void GivenZipCodesRequestedWhenNoDataExpectNoContent()
		{
			Microsoft.AspNetCore.Mvc.ActionResult response = controller.GetZipCodesWithInRangeRadius("00601", 5);
			Assert.IsType<NoContentResult>(response);
		}

		[Theory(DisplayName = "When Invalid Request, Expect Bad Request")]
		[InlineData("00622-22", 50)]
		[InlineData("0062", 50)]
		[InlineData("0062A", 50)]
		[InlineData("00622", 500)]
		public void GivenZipCodesRequestedWhenInvalidRequestExpectBadRequest(string zipCode, int radiusInMiles)
		{
			Microsoft.AspNetCore.Mvc.ActionResult response = controller.GetZipCodesWithInRangeRadius(zipCode, radiusInMiles);
			BadRequestResult result = Assert.IsType<BadRequestResult>(response);
			Assert.Equal(400, result.StatusCode);
		}
	}
}
