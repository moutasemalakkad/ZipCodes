using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Zipcodes_ASPNET.Models;

namespace Zipcodes_ASPNET.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("GetZipCodesWithInRangeRadius")]
        [Route("GetZipCodesWithInRangeRadius/{zipCode}/miles/{radiusInMiles}/")]
        [HttpGet]
        public ActionResult GetZipCodesWithInRangeRadius(string zipCode, int radiusInMiles)
        {
            Regex validZipCode = new Regex(@"^\d{5}$");
            if ((!validZipCode.IsMatch(zipCode)) || radiusInMiles > 200)
            {
                return BadRequest();
            }
            else
            {
                ZipCodeOutputDTO result = new ZipCodeOutputDTO();
                try
                {
                    List<OutputZipCodeWithDistance> data = ZipCodeSQLiteDBContext
                        .GetZipCodesWithinRadius(zipCode, radiusInMiles)
                        .Select(w => new OutputZipCodeWithDistance(w, zipCode))
                        .OrderBy(w => w.Distance)
                        .ToList();
                    result.success = true;
                    result.errorMessage = string.Empty;
                    result.data = data;
                    if (result.data.Any())
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.errorMessage = ex.Message;
                    return StatusCode(500, result);
                }
            }
        }
    }
}
