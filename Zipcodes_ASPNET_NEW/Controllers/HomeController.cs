using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Zipcodes_ASPNET.Models;

namespace Zipcodes_ASPNET.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly zipCodesDBContext _zipCodeDBContext;
        public HomeController(ILogger<HomeController> logger)
        {
            _zipCodeDBContext = new zipCodesDBContext();
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetZipCodesWithInRangeRadius(string zipCode, int radiusInMiles)
        {
            try
            {
				//LINQ query to fetch data from the database.
				List<ZipCodesWithDistance> result = _zipCodeDBContext.ZipCodesWithDistances
                    .Where(w => (w.Zip1 == zipCode || w.Zip2 == zipCode) && w.miToZcta5 <= radiusInMiles)
                    .ToList();
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [Route("NearestZipCodes/{zipCode}/miles/{radius}")]
        [HttpGet]
        public JsonResult GetNearestZipCodesMiles(string zipCode, int radiusInMiles)
		{
            ZipCodeOutputDTO result = new ZipCodeOutputDTO();
            try
			{
				List<OutputZipCodeWithDistance> data = _zipCodeDBContext.ZipCodesWithDistances
                    .Where(w => (w.Zip1 == zipCode || w.Zip2 == zipCode) && w.miToZcta5 < radiusInMiles)
                    .Select(w => new OutputZipCodeWithDistance(w, zipCode))
                    .ToList();
                result.success = true;
                result.errorMessage = string.Empty;
                result.data = data;
			}
            catch (Exception ex)
			{
                result.success = false;
                result.errorMessage = ex.Message;
			}
            return Json(result);
        }
    }
}
