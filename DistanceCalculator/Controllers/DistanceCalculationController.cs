using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DistanceCalculator.Models;
using DistanceCalculator.Services;
using DistanceCalculator.Validators;
using Microsoft.AspNetCore.Mvc;

namespace DistanceCalculator.Controllers
{
    [Route("api/[controller]")]
    public class DistanceCalculationController : Controller
    {
       private DistanceCalculationService _distanceCalculationService = new DistanceCalculationService();
       private CoordinatesValidator _coordinatesValidator = new CoordinatesValidator();

        [HttpPost]
        public CalculationDetails Post([FromBody]Coordinates coordinates)
        {
            _coordinatesValidator.AreCoordinatesValid(coordinates);
            return _distanceCalculationService.GetDistanceBetweenTwoCoordinates(coordinates.latitudeA, coordinates.longitudeA, coordinates.latitudeB, coordinates.longitudeB, coordinates.unitType);
        }

        //[HttpGet("[action]/latA=${latA}&longA=${longA}&latB=${latB}&longB=${longB}&unit=${unit}")]
        //public CalculationDetails Distance(double latA, double longA, double latB, double longB, string unit)
        //{
        //    return _distanceCalculationService.GetDistanceBetweenTwoCoordinates(latA, longA, latB, longB, unit);
        //}

        //[HttpGet("[action]")]
        //public CalculationDetails EmpireStateToTheSpireInKm()
        //{
        //    var empireStateLongitude = 40.748446;
        //    var empireStateLatitude = -73.985442;
        //    var dublinSpireLongitude = 53.3497625;
        //    var dublinSpireLatitude = -6.26027;
        //    return _distanceCalculationService.GetDistanceBetweenTwoCoordinates(empireStateLongitude, empireStateLatitude, dublinSpireLongitude, dublinSpireLatitude, "kilometres");
        //}
    }
}
