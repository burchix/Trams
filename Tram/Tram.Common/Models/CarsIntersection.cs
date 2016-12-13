using Tram.Common.Enums;

namespace Tram.Common.Models
{
    public class CarsIntersection : ModelBase
    {
        public int GreenLightPeriod { get; set; }

        public int RedLightPeriod { get; set; }

        public LightState State { get; set; }
    }
}
