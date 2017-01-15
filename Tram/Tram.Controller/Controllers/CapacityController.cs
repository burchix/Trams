using System;
using System.Drawing;
using Tram.Common.Consts;
using Tram.Common.Helpers;
using Tram.Common.Models;

namespace Tram.Controller.Controllers
{
    public class CapacityController
    {
        private MainController mainController;

        // sets the new capacity of vehicle, based on actual time, line and current stop; returns the time of boarding (in seconds)
        public float SetTramCapacity(Vehicle vehicle)
        {
            if (mainController == null)
            {
                mainController = Kernel.Get<MainController>();
            }

            //zainicjować pole Passangers w Vehicle w zależności od czasu kursu
            //weź currentState dla danego kursu, wszystkich przystanków
            if (vehicle.LastVisitedStops.Count < vehicle.Line.Capacity[TimeHelper.GetTimeStr(vehicle.StartTime)].CurrentState.Count)
            {
                vehicle.Passengers = vehicle.Line.Capacity[TimeHelper.GetTimeStr(vehicle.StartTime)].CurrentState[vehicle.LastVisitedStops.Count];
            }

            return 0; //TODO CZAS WSIADANIA!!!

            //UZYWAMY mainController.ActualRealTime, na podstawie godziny ustalamy nowy stan, 
            //w późniejszym czasie dorobimy całą historię przejazdu, ile osób wsiadło i wysiadło

            //zwraca czas potrzebny na wejscie i wyjscie wszystkich - uzaleznimy go od jakiejs stałej, 
            //którą kiedys sie wyznaczy podczas jakiejś przejażdżki tramwajem xD
        }

        public Color GetTramColor(int capacity)
        {
            int red = Math.Min(capacity, VehicleConsts.MAX_CAPACITY) * 255 / VehicleConsts.MAX_CAPACITY;
            int green = 255 - red;
            int blue = 0; // 255 - Math.Max(red, green);

            return Color.FromArgb(red, green, blue);
        }
    }
}
