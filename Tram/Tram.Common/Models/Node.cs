﻿using Microsoft.DirectX;
using System.Collections.Generic;
using Tram.Common.Enums;

namespace Tram.Common.Models
{
    public class Node : ModelBase
    {
        public Vector2 Coordinates { get; set; }

        public NodeType Type { get; set; }

        public TramsIntersection Intersection { get; set; }

        public bool IsUnderground { get; set; }

        public Next Child { get; set; }

        public List<Next> Children { get; set; }

        public class Next
        {
            public Node Node { get; set; }

            public float Distance { get; set; }
        }
    }
}
