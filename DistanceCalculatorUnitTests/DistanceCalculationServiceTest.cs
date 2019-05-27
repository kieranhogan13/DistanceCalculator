using DistanceCalculator.Models;
using DistanceCalculator.Services;
using DistanceCalculator.Validators;
using System;
using Xunit;

namespace XUnitTests
{
    public class DistanceCalculationServiceTest
    {
        private DistanceCalculationService _distanceCalculationService;
        private CoordinatesValidator _coordinatesValidator;

        public DistanceCalculationServiceTest()
        {
            _distanceCalculationService = new DistanceCalculationService();
            _coordinatesValidator = new CoordinatesValidator();
        }

        [Fact]
        public void TestDistanceInKilometres()
        {
            //Getting distance between the empire state and the spire
            //5124.64 kilometers NE (50°) via calculation
            //5,108 km via google

            //Arrange
            var empireStateLongitude = 40.748446;
            var empireStateLatitude = -73.985442;
            var dublinSpireLongitude = 53.3497625;
            var dublinSpireLatitude = -6.26027;

            //Act
            var result = _distanceCalculationService.GetDistanceBetweenTwoCoordinates(empireStateLongitude, empireStateLatitude, dublinSpireLongitude, dublinSpireLatitude, "kilometres");

            //Assert

            Assert.True(result.Distance > 5100);
            Assert.True(result.Distance < 5200);
        }


        [Fact]
        public void TestCoordinatesValidatorUnitTypeException()
        {
            //Arrange
            var coordinates = new Coordinates()
            {
                longitudeA = 40.748446,
                latitudeA = -73.985442,
                longitudeB = 53.3497625,
                latitudeB = -6.26027,
                unitType = "feet"
            };

            //Act
            var ex = Assert.Throws<Exception>(() => _coordinatesValidator.AreCoordinatesValid(coordinates));

            //Assert
            Assert.Equal(ex.Message, "The unit type must be one of the currently supported types: metres, kilometres or miles");
        }

        [Fact]
        public void TestCoordinatesValidatorLattitudeAException()
        {
            //Arrange
            var coordinates = new Coordinates()
            {
                longitudeA = 40.748446,
                latitudeA = -93.985442, //exceeds limit
                longitudeB = 53.3497625,
                latitudeB = -6.26027,
                unitType = "kilometres"
            };

            //Act
            var ex = Assert.Throws<Exception>(() => _coordinatesValidator.AreCoordinatesValid(coordinates));

            //Assert
            Assert.Equal(ex.Message, "From latitude must be within the range of -90 to 90 degrees");
        }

        [Fact]
        public void TestCoordinatesValidatorLongitudeAException()
        {
            //Arrange
            var coordinates = new Coordinates()
            {
                longitudeA = 200.748446, //exceeds limit
                latitudeA = -73.985442,
                longitudeB = 53.3497625,
                latitudeB = -6.26027,
                unitType = "kilometres"
            };

            //Act
            var ex = Assert.Throws<Exception>(() => _coordinatesValidator.AreCoordinatesValid(coordinates));

            //Assert
            Assert.Equal(ex.Message, "From longitude must be within the range of -180 to 180 degrees");
        }

        [Fact]
        public void TestCoordinatesValidatorLattitudeBException()
        {
            //Arrange
            var coordinates = new Coordinates()
            {
                longitudeA = 40.748446,
                latitudeA = -73.985442,
                longitudeB = 53.3497625,
                latitudeB = -126.26027, //exceeds limit
                unitType = "kilometres"
            };

            //Act
            var ex = Assert.Throws<Exception>(() => _coordinatesValidator.AreCoordinatesValid(coordinates));

            //Assert
            Assert.Equal(ex.Message, "To latitude must be within the range of -90 to 90 degrees");
        }

        [Fact]
        public void TestCoordinatesValidatorLongitudeBException()
        {
            //Arrange
            var coordinates = new Coordinates()
            {
                longitudeA = 40.748446,
                latitudeA = -73.985442,
                longitudeB = 193.3497625, //exceeds limit
                latitudeB = -6.26027,
                unitType = "kilometres"
            };

            //Act
            var ex = Assert.Throws<Exception>(() => _coordinatesValidator.AreCoordinatesValid(coordinates));

            //Assert
            Assert.Equal(ex.Message, "To longitude must be within the range of -180 to 180 degrees");
        }
    }
}
