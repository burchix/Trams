namespace Tram.Common.Models
{
    public class CarIntersection
    {
        public Node Node { get; set; }

        public int GreenInterval { get; set; }

        public int RedInterval { get; set; }

        public float TimeToChange { get; set; }
    }
}
