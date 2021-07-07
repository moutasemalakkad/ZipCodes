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
		public void ZipCodesGet()
		{
			HomeController controller = new HomeController(logger);
			Microsoft.AspNetCore.Mvc.JsonResult response = controller.GetNearestZipCodesMiles("00601", 5);
			Assert.NotNull(response);
			ZipCodeOutputDTO responseObject = (ZipCodeOutputDTO)response.Value;
			Assert.True(responseObject.success);
			Assert.Empty(responseObject.errorMessage);
			Assert.NotNull(responseObject.data);
			Assert.NotEmpty(responseObject.data);
		}
	}
}
