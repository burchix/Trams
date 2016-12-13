using System.Collections.Generic;

namespace Tram.Common.Models
{
    public class TramsIntersection : ModelBase
    {
        public List<Node> Nodes { get; set; }

        public Vehicle CurrentVehicle { get; set; }

        public Queue<Vehicle> Vehicles { get; set; }
    }
}
