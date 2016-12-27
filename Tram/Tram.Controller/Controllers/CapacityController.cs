using System.Drawing;
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

            return 0;

            //UZYWAMY mainController.ActualRealTime, na podstawie godziny ustalamy nowy stan, 
            //w późniejszym czasie dorobimy całą historię przejazdu, ile osób wsiadło i wysiadło

            //zwraca czas potrzebny na wejscie i wyjscie wszystkich - uzaleznimy go od jakiejs stałej, 
            //którą kiedys sie wyznaczy podczas jakiejś przejażdżki tramwajem xD
        }

        public Color GetTramColor(int capacity)
        {
            return Color.Red;
        }
    }
}
