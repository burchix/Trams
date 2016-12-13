using Microsoft.DirectX;
using System;
using System.Collections.Generic;

namespace Tram.Common.Models
{
    public class Vehicle : ModelBase
    {
        public TramLine Line { get; set; }
        
        public int Passengers { get; set; }

        public DateTime StartTime { get; set; }

        public bool IsOnStop { get; set; }

        public Location Position { get; set; }

        public float Speed { get; set; }

        public Node LastVisitedStop { get; set; }

        public TramsIntersection CurrentIntersection { get; set; }

        public List<Node> VisitedNodes { get; set; }

        public class Location
        {
            public Node Node1 { get; set; }

            public Node Node2 { get; set; }

            // 0 - 100%
            public float Displacement { get; set; }

            public Vector2 Coordinates { get; set; }
        }
    }
}
