using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistanceCalculator.Models
{
    public class Coordinates
    {
        public double latitudeA { get; set; } 
        public double longitudeA { get; set; }
        public double latitudeB { get; set; }
        public double longitudeB { get; set; }
        public string unitType { get; set; }
    }
}
