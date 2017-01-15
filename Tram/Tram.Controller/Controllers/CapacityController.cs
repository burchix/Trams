using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Tram.Common.Models;

namespace Tram.Controller.Controllers
{
    public class CapacityController
    {
        private MainController mainController;

        // sets the new capacity of vehicle, based on actual time, line and current stop; returns the time of boarding (in seconds)
        public float SetTramCapacity(Vehicle vehicle)
        {
            //zainicjować pole Passangers w Vehicle w zależności od czasu kursu
            //weź currentState dla danego kursu, wszystkich przystanków
            if (vehicle.Line.Capacity.ContainsKey(vehicle.StartTime.ToString()))
            {
               vehicle.Passengers = vehicle.Line.Capacity[vehicle.StartTime.ToString()].currentState[vehicle.LastVisitedStops.Count];
            }
            if (mainController == null)
            {
                mainController = Kernel.Get<MainController>();
            }

            return 0;

            //UZYWAMY mainController.ActualRealTime, na podstawie godziny ustalamy nowy stan, 
            //w późniejszym czasie dorobimy całą historię przejazdu, ile osób wsiadło i wysiadło

            //zwraca czas potrzebny na wejscie i wyjscie wszystkich - uzaleznimy go od jakiejs stałej, 
            //którą kiedys sie wyznaczy podczas jakiejś przejażdżki tramwajem xD
        }

        public Color GetTramColor(int capacity)
        {
            //w zależności od capacity- green- mało pasażerów, yellow- średnio dużo, red- dużo
            // wyznacznik- największa znaleziona capacity
            return Color.Red;
        }

        private IEnumerable<string> GetFileLines(StreamReader file)
        {
            string textLine = null;
            while ((textLine = file.ReadLine()) != null)
            {
                yield return textLine;
            }
        }
    }
}
