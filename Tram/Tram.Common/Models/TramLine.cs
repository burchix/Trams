using System;
using System.Collections.Generic;

namespace Tram.Common.Models
{
    public class TramLine : ModelBase
    {
        public List<Node> MainNodes { get; set; }
        
        public List<Departure> Departures { get; set; }

        public Departure LastDeparture { get; set; }

        public class Departure
        {
            public DateTime StartTime { get; set; }
            
            public List<float> NextStopIntervals { get; set; }
            //w capacity controller ma zwrócić to
        }

        public Dictionary<string, TramCapacity> Capacity { get; set; }
    }
}
