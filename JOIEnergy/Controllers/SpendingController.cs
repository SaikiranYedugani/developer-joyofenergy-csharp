using JOIEnergy.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace JOIEnergy.Controllers
{
    [Route("Spendings")]
    public class SpendingController : Controller
    {

        private readonly ISpendings _spendings;
        public SpendingController(ISpendings spendings)
        {
            _spendings = spendings;
        }

        [HttpGet("LastWeek/{smartMeterId}")]
        public ObjectResult UsersWeekSpendings(string smartMeterId)
        {
            var result = _spendings.WeeklySpendings(smartMeterId);
            if(result == null)
            {
                return new ObjectResult(new { StatusCode=HttpStatusCode.NotFound, Error="Could not find the supplier or the priceplan" });
              
            }
            return new OkObjectResult(result);

        }
    }
}
