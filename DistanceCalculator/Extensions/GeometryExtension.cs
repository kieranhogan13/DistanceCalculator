using NetTopologySuite;
using NetTopologySuite.CoordinateSystems.Transformations;
using System.Collections.Generic;
using GeoAPI;
using GeoAPI.Geometries;
using ProjNet;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;

namespace DistanceCalculator.Extensions
{
    static class GeometryExtensions
    {
        static readonly IGeometryServices _geometryServices = NtsGeometryServices.Instance;
        static readonly ICoordinateSystemServices _coordinateSystemServices
            = new CoordinateSystemServices(
                new CoordinateSystemFactory(),
                new CoordinateTransformationFactory(),
                new Dictionary<int, string>
                {
                    // Coordinate systems:

                    // (3857 and 4326 included automatically)

                    // This coordinate system covers the area of our data.
                    // Different data requires a different coordinate system.
                    [2855] =
                    @"
                    PROJCS[""NAD83(HARN) / Washington North"",
                        GEOGCS[""NAD83(HARN)"",
                            DATUM[""NAD83_High_Accuracy_Regional_Network"",
                                SPHEROID[""GRS 1980"",6378137,298.257222101,
                                    AUTHORITY[""EPSG"",""7019""]],
                                AUTHORITY[""EPSG"",""6152""]],
                            PRIMEM[""Greenwich"",0,
                                AUTHORITY[""EPSG"",""8901""]],
                            UNIT[""degree"",0.01745329251994328,
                                AUTHORITY[""EPSG"",""9122""]],
                            AUTHORITY[""EPSG"",""4152""]],
                        PROJECTION[""Lambert_Conformal_Conic_2SP""],
                        PARAMETER[""standard_parallel_1"",48.73333333333333],
                        PARAMETER[""standard_parallel_2"",47.5],
                        PARAMETER[""latitude_of_origin"",47],
                        PARAMETER[""central_meridian"",-120.8333333333333],
                        PARAMETER[""false_easting"",500000],
                        PARAMETER[""false_northing"",0],
                        UNIT[""metre"",1,
                            AUTHORITY[""EPSG"",""9001""]],
                        AUTHORITY[""EPSG"",""2855""]]
                "
                });

        public static IGeometry ProjectTo(this IGeometry geometry, int srid)
        {
            var geometryFactory = _geometryServices.CreateGeometryFactory(srid);
            var transformation = _coordinateSystemServices.CreateTransformation(geometry.SRID, srid);

            return GeometryTransform.TransformGeometry(
                geometryFactory,
                geometry,
                transformation.MathTransform);
        }
    }
}
