﻿using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;
using System.Collections.Generic;
using System.Linq;
using Tram.Common.Consts;
using Tram.Common.Enums;
using Tram.Common.Helpers;
using Tram.Common.Models;
using Tram.Controller.Repositories;

namespace Tram.Controller.Controllers
{
    public class MainController
    {
        private DirectxController directxController;
        private VehiclesController vehiclesController;

        private IRepository repository;

        private DateTime actualRealTime;
        private DateTime lastUpdateTime;
        private float simulationSpeed;

        public List<Node> Map { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public List<TramLine> Lines { get; set; }
        public List<CarIntersection> CarIntersections { get; set; }

        public MainController(DirectxController directxController, VehiclesController vehiclesController, IRepository repository)
        {
            this.directxController = directxController;
            this.vehiclesController = vehiclesController;
            this.repository = repository;
            repository.LoadData("Points.csv", "Lines.csv");
        }

        #region Public Methods

        public void StartSimulation(DateTime startTime, float simulationSpeed)
        {
            this.simulationSpeed = simulationSpeed;
            lastUpdateTime = DateTime.Now;
            actualRealTime = startTime;
            GetAndPrepareModels();
        }

        public void Render(Device device, Vector3 cameraPosition)
        {
            directxController.Render(device, cameraPosition, TimeHelper.GetTimeStr(actualRealTime));
        }

        public void Update()
        {
            // Get time interval since last update (in seconds)
            float deltaTime = (float)Math.Min((DateTime.Now - lastUpdateTime).TotalSeconds, CalculationConsts.MAX_TIME_INTERVAL) * simulationSpeed;
            lastUpdateTime = DateTime.Now;

            // Change time 
            actualRealTime += new TimeSpan(0, 0, 0, 0, (int)(deltaTime * 1000));

            //Remove finished courses
            Vehicles.RemoveAll(v => v.Position.Node1.Id == v.Line.MainNodes.Last().Id);

            // Update trams
            vehiclesController.Update(deltaTime);

            // Run new trains
            List<Node> startPoints = new List<Node>();
            foreach (var line in Lines)
            {
                for (int i = line.Departures.Count - 1; i >= 0; i--)
                {
                    if (line.Departures[i].StartTime <= actualRealTime)
                    {
                        if (line.Departures[i] != line.LastDeparture &&
                            !startPoints.Any(sp => sp.Equals(line.MainNodes.First())) &&
                            vehiclesController.IsFreeSpace(line.MainNodes.First(), VehicleConsts.SAFE_SPACE))
                        {
                            startPoints.Add(line.MainNodes.First());
                            line.LastDeparture = line.Departures[i];
                            Vehicles.Add(new Vehicle()
                            {
                                Id = line.Id + " - " + TimeHelper.GetTimeStr(actualRealTime),
                                Line = line,
                                Passengers = 0, //TODO: ustawić startową liczbę
                                Speed = 0f,
                                StartTime = actualRealTime,
                                IsOnStop = line.MainNodes.First().Type == NodeType.TramStop,
                                VisitedNodes = new List<Node>(),
                                Position = new Vehicle.Location()
                                {
                                    Node1 = line.MainNodes.First(),
                                    Node2 = line.MainNodes.First().Child.Node,
                                    Displacement = 0,
                                    Coordinates = line.MainNodes.First().Coordinates
                                }
                            });
                        }

                        break;
                    }
                }
            }

            // Check car intersections
            foreach (var intersection in CarIntersections)
            {
                intersection.TimeToChange -= deltaTime;
                if (intersection.TimeToChange <=0 )
                {
                    if (intersection.Node.LightState == LightState.Green)
                    {
                        intersection.Node.LightState = LightState.Red;
                        intersection.TimeToChange = intersection.RedInterval;
                    }
                    else
                    {
                        intersection.Node.LightState = LightState.Green;
                        intersection.TimeToChange = intersection.GreenInterval;
                    }
                }
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void GetAndPrepareModels()
        {
            Lines = repository.GetLines();
            Map = repository.GetMap();
            CarIntersections = repository.GetCarIntersections();
            Vehicles = new List<Vehicle>();
            directxController.InitMap();
        }

        #endregion Private Methods
    }
}
