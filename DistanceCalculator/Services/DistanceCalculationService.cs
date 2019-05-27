using DistanceCalculator.Models;
using NetTopologySuite;
using DistanceCalculator.Extensions;
using System;

namespace DistanceCalculator.Services
{
    public interface IDistanceCalculationService
    {
        CalculationDetails GetDistanceBetweenTwoCoordinates(int latA, int longA, int latB, int longB, string measurement);
    }

    public class DistanceCalculationService
    {
        public CalculationDetails GetDistanceBetweenTwoCoordinates(double latA, double longA, double latB, double longB, string measurement)
        {
            //Do calculation

            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            GeoAPI.Geometries.Coordinate coord1 = new GeoAPI.Geometries.Coordinate(longA, latA);
            var location1 = geometryFactory.CreatePoint(coord1);
            GeoAPI.Geometries.Coordinate coord2 = new GeoAPI.Geometries.Coordinate(longB, latB);
            var location2 = geometryFactory.CreatePoint(coord2);

            var distanceInMetres = location1.ProjectTo(2855).Distance(location2.ProjectTo(2855));
            var details = string.Empty;
            double distance = 0;

            switch (measurement)
            {
                case "metres":
                    distance = distanceInMetres;
                    details = "Distance in metres";
                    break;
                case "kilometres":
                    distance = (distanceInMetres / 1000);
                    details = "Distance in kilometres";
                    break;
                case "miles":
                    distance = (distanceInMetres / 1609.344);
                    details = "Distance in miles";
                    break;
            }
            distance = Math.Round(distance, 4, MidpointRounding.AwayFromZero);
            
            return new CalculationDetails(distance, details);
        }
    }



}
