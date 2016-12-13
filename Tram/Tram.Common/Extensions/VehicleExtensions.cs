using Microsoft.DirectX;
using System.Collections.Generic;
using System.Linq;
using Tram.Common.Consts;
using Tram.Common.Enums;
using Tram.Common.Helpers;
using Tram.Common.Models;

namespace Tram.Common.Extensions
{
    public static class VehicleExtensions
    {
        public static bool IsBusStopReached(this Vehicle vehicle)
        {
            //TODO: żeby zatrzymywał sie tylko na swoich przystankach!! (limanowskiego xD)
            var node1 = vehicle.Position.Node1;
            var node2 = vehicle.Position.Node2;
            return (node1 != null && node1.Type == NodeType.TramStop && node1 != vehicle.LastVisitedStop && vehicle.DistanceTo(node1) <= CalculationConsts.DISTANCE_EPSILON) ||
                   (node2 != null && node2.Type == NodeType.TramStop && node2 != vehicle.LastVisitedStop && vehicle.DistanceTo(node2) <= CalculationConsts.DISTANCE_EPSILON);
        }

        public static bool IsIntersectionReached(this Vehicle vehicle, out TramsIntersection intersection)
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

        public static bool IsStillOnIntersection(this Vehicle vehicle) => vehicle.CurrentIntersection.Nodes.Any(n => (vehicle.DistanceTo(n) - VehicleConsts.LENGTH) < 0);

        public static float DistanceTo(this Vehicle vehicle, Vehicle vehicle2) => vehicle.DistanceTo(vehicle2.Position.Coordinates);

        public static float DistanceTo(this Vehicle vehicle, Node node) => vehicle.DistanceTo(node.Coordinates);

        public static float DistanceTo(this Vehicle vehicle, Vector2 coordinates) => GeometryHelper.GetRealDistance(vehicle.Position.Coordinates, coordinates);

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

        public static bool IsStraightRoad(this Vehicle vehicle, out float distance)
        {
            var node = vehicle.Position.Node2;
            if (node == null)
            {
                distance = -1;
                return true;
            }

            distance = vehicle.DistanceTo(node);
            if (distance <= VehicleConsts.DISTANCE_TO_MAX_SPEED && node.Type != NodeType.Normal)
            {
                return false;
            }

            while (distance < VehicleConsts.DISTANCE_TO_MAX_SPEED)
            {
                var newNode = vehicle.Line.GetNextNode(node);
                if (newNode == null)
                {
                    return true;
                }

                distance += newNode.Distance;
                if (distance <= VehicleConsts.DISTANCE_TO_MAX_SPEED && newNode.Node.Type != NodeType.Normal)
                {
                    return false;
                }

                node = newNode.Node;
            }

            return true;
        }

        public static bool IsAnyVehicleClose(this Vehicle vehicle, List<Vehicle> vehicles, out float distance)
        {
            Vehicle result = vehicles.Where(v => !v.Equals(vehicle) && 
                                                 vehicle.DistanceTo(v) <= (VehicleConsts.DISTANCE_TO_MAX_SPEED + VehicleConsts.LENGTH) && 
                                                 v.VisitedNodes.Any(vn => vn.Equals(vehicle.Position.Node1)))
                                     .OrderBy(v => vehicle.DistanceTo(v))
                                     .FirstOrDefault();
            if (result == null)
            {
                distance = -1;
                return false;
            }

            distance = vehicle.DistanceTo(result) - VehicleConsts.LENGTH;
            return true;
        }
    }
}
