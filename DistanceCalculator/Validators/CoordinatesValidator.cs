using DistanceCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistanceCalculator.Validators
{
    public interface ICoordinatesValidator
    {
        void AreCoordinatesValid(Coordinates coordinates);
    }
    public class CoordinatesValidator : ICoordinatesValidator
    {
        public void AreCoordinatesValid(Coordinates coordinates)
        {
            if (coordinates.latitudeA > 90 || coordinates.latitudeA < -90)
                throw new Exception("From latitude must be within the range of -90 to 90 degrees");
            if (coordinates.longitudeA > 180 || coordinates.longitudeA < -180)
                throw new Exception("From longitude must be within the range of -180 to 180 degrees");
            if (coordinates.latitudeB > 90 || coordinates.latitudeB < -90)
                throw new Exception("To latitude must be within the range of -90 to 90 degrees");
            if (coordinates.longitudeB > 180 || coordinates.longitudeB < -180)
                throw new Exception("To longitude must be within the range of -180 to 180 degrees");
            if(coordinates.unitType != "metres" && coordinates.unitType != "kilometres" && coordinates.unitType != "miles")
                throw new Exception("The unit type must be one of the currently supported types: metres, kilometres or miles");
        }
    }
}
