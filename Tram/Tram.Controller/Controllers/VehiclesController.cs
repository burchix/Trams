using System.Collections.Generic;
using System.Linq;
using Tram.Common.Consts;
using Tram.Common.Enums;
using Tram.Common.Extensions;
using Tram.Common.Helpers;
using Tram.Common.Models;

namespace Tram.Controller.Controllers
{
    public class VehiclesController
    {
        private MainController mainController;

        #region Public Methods

        public void Update(float deltaTime)
        {
            if (mainController == null)
            {
                mainController = Kernel.Get<MainController>();
            }

            foreach (var vehicle in mainController.Vehicles)
            {
                CalculateSpeed(vehicle, mainController.Vehicles, mainController.Map, deltaTime);
                if (!vehicle.IsOnStop)
                {
                    CalculatePosition(vehicle, mainController.Map, deltaTime);
                }
            }
        }

        // Check if there is 'length' free space (in meters), starts from 'coordinates'
        public bool IsFreeSpace(Node node, List<Vehicle> vehicles, int length)
        {
            return !vehicles.Any(v => GeometryHelper.GetRealDistance(node.Coordinates, v.Position.Coordinates) <= length && v.VisitedNodes.Any(n => n.Equals(node)));
        } 

        #endregion Public Methods

        #region Private Methods
        
        private void CalculateSpeed(Vehicle vehicle, List<Vehicle> vehicles, List<Node> map, float deltaTime)
        {
            //TODO: OBSŁUGA CZEKANIA NA SKRZYZOWANIU SAMOCHODOWYM
            float distance;
            TramsIntersection tramIntersection;
            //Check if is on stop
            if (vehicle.Speed < CalculationConsts.EPSILON && !vehicle.IsOnStop && vehicle.IsBusStopReached())
            {
                vehicle.IsOnStop = true;
                vehicle.Speed = 50; //TODO: tutaj przypisujemy szybkość - czyli własciwie to ile czasu będzie się ładował (kod Zuzy) - jak dojdzie do 0, można ruszać
            }
            else if (vehicle.Speed < CalculationConsts.EPSILON && !vehicle.IsOnStop && vehicle.IsIntersectionReached(out tramIntersection))
            {
                if (tramIntersection.CurrentVehicle == null && tramIntersection.Vehicles.Count == 0)
                {
                    tramIntersection.CurrentVehicle = vehicle;
                }

                if (tramIntersection.CurrentVehicle.Equals(vehicle))
                {
                    vehicle.Speed += VehicleConsts.ACCELERATION * deltaTime;
                    vehicle.CurrentIntersection = tramIntersection;
                }
                else if (!tramIntersection.Vehicles.Any(v => v.Equals(vehicle)))
                {
                    tramIntersection.Vehicles.Enqueue(vehicle);
                }
            }
            else if (vehicle.CurrentIntersection != null)
            {
                if (vehicle.Speed < VehicleConsts.MAX_CROSS_SPEED)
                {
                    vehicle.Speed += VehicleConsts.ACCELERATION * deltaTime;
                }
                else
                {
                    vehicle.Speed -= VehicleConsts.ACCELERATION * deltaTime;
                }
            }
            //When is on stop, check if can run 
            else if (vehicle.IsOnStop)
            {
                if (vehicle.Speed < CalculationConsts.EPSILON)
                {
                    vehicle.IsOnStop = false;
                    vehicle.LastVisitedStop = vehicle.Position.Node1 != null && vehicle.Position.Node1.Type == NodeType.TramStop ? vehicle.Position.Node1 : vehicle.Position.Node2;
                    vehicle.Speed += VehicleConsts.ACCELERATION * deltaTime;
                }
                else
                {
                    vehicle.Speed -= VehicleConsts.ACCELERATION * deltaTime;
                }
            }
            //Check if there is any obstacle on road (intersection, stop)
            else if (!vehicle.IsStraightRoad(out distance))
            {
                if (distance > VehicleConsts.DISTANCE_TO_MAX_CROSS_SPEED && vehicle.Speed < VehicleConsts.MAX_CROSS_SPEED)
                {
                    vehicle.Speed += VehicleConsts.ACCELERATION * deltaTime;
                }
                else
                {
                    vehicle.Speed -= VehicleConsts.ACCELERATION * deltaTime;
                }
            }
            else if (vehicle.IsAnyVehicleClose(vehicles, out distance))
            {
                if (distance > VehicleConsts.DISTANCE_TO_MAX_CROSS_SPEED && vehicle.Speed < VehicleConsts.MAX_CROSS_SPEED)
                {
                    vehicle.Speed += VehicleConsts.ACCELERATION * deltaTime;
                }
                else
                {
                    vehicle.Speed -= VehicleConsts.ACCELERATION * deltaTime;
                }
            }
            else if (vehicle.Speed < VehicleConsts.MAX_SPEED)
            {
                vehicle.Speed += VehicleConsts.ACCELERATION * deltaTime;
            }
            else
            {
                vehicle.Speed -= VehicleConsts.ACCELERATION * deltaTime;
            }

            vehicle.NormalizeSpeed();
        }

        private void CalculatePosition(Vehicle vehicle, List<Node> map, float deltaTime)
        {
            float translation = vehicle.Speed * deltaTime;
            if (vehicle.Position.Node2 != null)
            {
                float distanceToNextPoint = 0;
                if ((distanceToNextPoint = vehicle.DistanceTo(vehicle.Position.Node2)) > translation)
                {
                    vehicle.Position.Displacement += translation * 100 / vehicle.Position.Node1.GetDistanceToChild(vehicle.Position.Node2);
                }
                else
                {
                    Node.Next newNode = vehicle.Line.GetNextNode(vehicle.Position.Node2);
                    if (newNode != null)
                    {
                        vehicle.VisitedNodes.Add(newNode.Node);
                        vehicle.Position.Node1 = vehicle.Position.Node2;
                        vehicle.Position.Node2 = newNode.Node;
                        vehicle.Position.Displacement = 0;
                        vehicle.Position.Displacement += (translation - distanceToNextPoint) * 100 / vehicle.Position.Node1.GetDistanceToChild(vehicle.Position.Node2);
                    }
                }
            }

            vehicle.Position.Coordinates = vehicle.Position.Node2 == null || vehicle.Position.Displacement < CalculationConsts.EPSILON ?
                                           vehicle.Position.Node1.Coordinates :
                                           GeometryHelper.GetLocactionBetween(vehicle.Position.Displacement, vehicle.Position.Node1.Coordinates, vehicle.Position.Node2.Coordinates);

            //Check intersection
            if (vehicle.CurrentIntersection != null && !vehicle.IsStillOnIntersection())
            {
                vehicle.CurrentIntersection.CurrentVehicle = vehicle.CurrentIntersection.Vehicles.Any() ? vehicle.CurrentIntersection.Vehicles.Dequeue() : null;
                vehicle.CurrentIntersection = null;
            }
        }

        #endregion Private Methods
    }
}
