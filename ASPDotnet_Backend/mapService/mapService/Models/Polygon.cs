using System;
namespace mapService.Models
{

    public class GeoPoly
    {
        public string type { get; set; }
        public double[][][] coordinates { get; set; }
    }

    public class GeoMultipoly
    {
        public string type { get; set; }
        public double[][][][] coordinates { get; set; }
    }

 
}
