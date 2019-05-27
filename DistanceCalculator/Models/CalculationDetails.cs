using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistanceCalculator.Models
{
    public class CalculationDetails
    {
        public CalculationDetails(double distance, string details)
        {
            Distance = distance;
            Details = details;
        }
        public double Distance { get; }
        public string Details { get; }
    }
}
