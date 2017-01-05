using System.Collections.Generic;
using System.Linq;
using Tram.Common.Consts;
using Tram.Common.Enums;
using Tram.Common.Helpers;
using Tram.Common.Interfaces;
using Tram.Common.Models;

namespace Tram.Common.Extensions
{
    public static class VehicleExtensions
    {
        public static bool IsBusStopReached(this Vehicle vehicle)
        {
            var node1 = vehicle.Position.Node1;
            var node2 = vehicle.Position.Node2;
            return (node1 != null && node1.Type == NodeType.TramStop && node1 != vehicle.LastVisitedStop && vehicle.RealDistanceTo(node1) <= CalculationConsts.DISTANCE_EPSILON && vehicle.Line.MainNodes.Any(mn => mn.Equals(node1))) ||
                   (node2 != null && node2.Type == NodeType.TramStop && node2 != vehicle.LastVisitedStop && vehicle.RealDistanceTo(node2) <= CalculationConsts.DISTANCE_EPSILON && vehicle.Line.MainNodes.Any(mn => mn.Equals(node2)));
        }

        public static bool IsIntersectionReached(this Vehicle vehicle, out TramIntersection intersection)
        {
            var node1 = vehicle.Position.Node1;
            var node2 = vehicle.Position.Node2;
            if (node1 != null && node1.Type == NodeType.TramCross)
            {
                intersection = node1.Intersection;
                return true;
            }
            else if (node2 != null && node2.Type == NodeType.TramCross)
            {
                intersection = node2.Intersection;
                return true;
            }

            intersection = null;
            return false;
        }

        public static void aaa(this string s)
        {
            s += "aaa";
        }

        public static bool IsOnLights(this Vehicle vehicle) => vehicle.Position.Node2.Type == NodeType.CarCross;

        public static bool IsOnLightsAndHasRedLight(this Vehicle vehicle, float deltaTime)
        {
            if (vehicle.Position.Node2.Type == NodeType.CarCross && vehicle.Position.Node2.LightState != LightState.Green)
            {
                float speed = PhysicsHelper.GetNewSpeed(vehicle.Speed, deltaTime);
                float brakingDistance = PhysicsHelper.GetBrakingDistance(speed);
                float distance = vehicle.RealDistanceTo(vehicle.Position.Node2) - 1;
                if (vehicle.Position.Node2.LightState == LightState.Red)
                {
                    return distance <= brakingDistance;
                }
                else if (vehicle.Position.Node2.LightState == LightState.Yellow)
                {
                    return distance >= brakingDistance;
                }
            }

            return false;
        }

        public static bool IsStillOnIntersection(this Vehicle vehicle) => vehicle.CurrentIntersection.Nodes.Any(n => (vehicle.RealDistanceTo(n) - VehicleConsts.LENGTH) < 0);

        public static float RealDistanceTo(this Vehicle vehicle, IObjWithCoordinates point) => GeometryHelper.GetRealDistance(vehicle.Position.Coordinates, point.Coordinates);

        public static void NormalizeSpeed(this Vehicle vehicle)
        {
            if (vehicle.Speed < 0)
            {
                vehicle.Speed = 0;
            }
            else if (vehicle.Speed > VehicleConsts.MAX_SPEED)
            {
                vehicle.Speed = VehicleConsts.MAX_SPEED;
            }
        }

        public static bool IsStraightRoad(this Vehicle vehicle, float deltaTime)
        {
            var node1 = vehicle.Position.Node1;
            if (node1 != null && CorrectStopPredicate(vehicle, node1))
            {
                return false;
            }

            var node = vehicle.Position.Node2;
            if (node == null)
            {
                return true;
            }

            float speed = PhysicsHelper.GetNewSpeed(vehicle.Speed, deltaTime);
            float distance = vehicle.RealDistanceTo(node) - 1;
            float brakingDistance = PhysicsHelper.GetBrakingDistance(speed);
            
            if (IsNotStraightRoadPredicate(vehicle, node, distance, brakingDistance))
            {
                return false;
            }

            while (distance <= brakingDistance)
            {
                var newNode = vehicle.Line.GetNextNode(node);
                if (newNode == null)
                {
                    return true;
                }

                distance += newNode.Distance;
                node = newNode.Node;
                if (IsNotStraightRoadPredicate(vehicle, node, distance, brakingDistance))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsNotStraightRoadPredicate(Vehicle vehicle, Node node, float distance, float brakingDistance) => 
                            distance <= brakingDistance && 
                            node.Type != NodeType.Normal && 
                            (CorrectTramCrossPredicate(vehicle, node) || CorrectStopPredicate(vehicle, node) || CorrectNotGreenCarCrossPredicate(node));

        public static bool IsAnyVehicleClose(this Vehicle vehicle, List<Vehicle> vehicles, float deltaTime)
        {
            float speed = PhysicsHelper.GetNewSpeed(vehicle.Speed, deltaTime);
            float brakingDistance = PhysicsHelper.GetBrakingDistance(speed);

            Vehicle result = vehicles.Where(v => !v.Equals(vehicle) &&
                                                 vehicle.RealDistanceTo(v) <= (brakingDistance + VehicleConsts.SAFE_SPACE) &&
                                                 v.RealDistanceTo(v.Position.Node2) <= vehicle.RealDistanceTo(v.Position.Node2) && vehicle.RealDistanceTo(vehicle.Position.Node1) <= v.RealDistanceTo(vehicle.Position.Node1) && // check if object is ahead of vehicle
                                                 v.VisitedNodes.Any(vn => vn.Equals(vehicle.Position.Node2)) &&
                                                 (v.Line.Equals(vehicle.Line) ||
                                                 v.VisitedNodes.Intersect(vehicle.VisitedNodes).Any()))
                                     .FirstOrDefault();
                        
            return result != null;
        }

        private static bool CorrectStopPredicate(Vehicle vehicle, Node node)
        {
            return node.Type == NodeType.TramStop && 
                   !vehicle.LastVisitedStop.Equals(node) &&
                   !vehicle.LastVisitedStops.Any(n => n.Equals(node)) &&
                   vehicle.Line.MainNodes.Any(n => n.Equals(node));
        }

        private static bool CorrectTramCrossPredicate(Vehicle vehicle, Node node)
        {
            return node.Type == NodeType.TramCross && 
                   (vehicle.CurrentIntersection == null || !vehicle.CurrentIntersection.Equals(node.Intersection));
        }

        private static bool CorrectNotGreenCarCrossPredicate(Node node)
        {
            return node.Type == NodeType.CarCross && node.LightState != LightState.Green;
        }
    }
}
