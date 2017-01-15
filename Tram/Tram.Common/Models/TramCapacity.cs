using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tram.Common.Models
{
   public class TramCapacity
    {
        public List<int> gotIn;
        public List<int> gotOut;
        public List<int> currentState;

        public TramCapacity()
        {
            gotIn = new List<int>();
            gotOut = new List<int>();
            currentState = new List<int>();
        }

    }


}
