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

        public static bool IsOnLightsAndHasRedLight(this Vehicle vehicle, float deltaTime)
        {
            if (vehicle.Position.Node2.Type == NodeType.CarCross && vehicle.Position.Node2.LightState != LightState.Green)
            {
                float speed = vehicle.Speed + deltaTime * VehicleConsts.ACCELERATION;
                float brakingDistance = (speed * speed) / (2 * VehicleConsts.ACCELERATION);
                float distance = vehicle.RealDistanceTo(vehicle.Position.Node2) - 1;
                if (vehicle.Position.Node2.LightState == LightState.Red)
                {
                    return distance <= brakingDistance;
                }
                else if (vehicle.Position.Node2.LightState == LightState.Orange)
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
            if (vehicle.Id.Contains("2 (Salwator - Cmentarz Rakowicki) - 07:30"))
            {
                var a = 3;
            }
            var node1 = vehicle.Position.Node1;
            if (node1.Type != NodeType.CarCross && StraightRoadCorrectStopPredicate(vehicle, node1))
            {
                return false;
            }

            var node = vehicle.Position.Node2;
            if (node == null)
            {
                return true;
            }

            float speed = vehicle.Speed + deltaTime * VehicleConsts.ACCELERATION;
            float distance = vehicle.RealDistanceTo(node) - 1;
            float brakingDistance = (speed * speed) / (2 * VehicleConsts.ACCELERATION);

            if (distance <= brakingDistance && StraightRoadCorrectStopPredicate(vehicle, node))
            {
                return false;
            }

            while (distance < brakingDistance)
            {
                var newNode = vehicle.Line.GetNextNode(node);
                if (newNode == null)
                {
                    return true;
                }

                distance += newNode.Distance;
                if (distance <= brakingDistance && newNode.Node.Type != NodeType.Normal)
                {
                    return false;
                }

                node = newNode.Node;
            }

            return true;
        }

        public static bool IsAnyVehicleClose(this Vehicle vehicle, List<Vehicle> vehicles, float deltaTime)
        {
            float speed = vehicle.Speed + deltaTime * VehicleConsts.ACCELERATION;
            float brakingDistance = (speed * speed) / (2 * VehicleConsts.ACCELERATION);

            Vehicle result = vehicles.Where(v => !v.Equals(vehicle) &&
                                                 vehicle.RealDistanceTo(v) <= (brakingDistance + VehicleConsts.SAFE_SPACE) &&
                                                 v.RealDistanceTo(v.Position.Node2) <= vehicle.RealDistanceTo(v.Position.Node2) && vehicle.RealDistanceTo(vehicle.Position.Node1) <= v.RealDistanceTo(vehicle.Position.Node1) && // check if object is ahead of vehicle
                                                 v.VisitedNodes.Any(vn => vn.Equals(vehicle.Position.Node2)) &&
                                                 (v.Line.Equals(vehicle.Line) ||
                                                 v.VisitedNodes.Intersect(vehicle.VisitedNodes).Any()))
                                     .FirstOrDefault();
                        
            return result != null;
        }

        private static bool StraightRoadCorrectStopPredicate(Vehicle vehicle, Node node)
        {
            return node != null && 
                   node.Type == NodeType.TramStop && 
                   !vehicle.LastVisitedStop.Equals(node) &&
                   !vehicle.LastVisitedStops.Any(n => n.Equals(node)) &&
                   vehicle.Line.MainNodes.Any(n => n.Equals(node));
        }
    }
}
