using Microsoft.DirectX;
using System;
using Tram.Common.Consts;

namespace Tram.Common.Helpers
{
    public static class GeometryHelper
    {
        private const double PIx = 3.141592653589793;
        private const double RADIUS = 6378.16;

        public static float GetDistance(Vector2 pA, Vector2 pB) => (float)Math.Sqrt((pB.X - pA.X) * (pB.X - pA.X) + (pB.Y - pA.Y) * (pB.Y - pA.Y));

        public static float GetRealDistance(Vector2 pA, Vector2 pB)
        {
            double dlon = Radians(pB.Y - pA.Y);
            double dlat = Radians(pB.X - pA.X);
            double a = (Math.Sin(dlat / 2) * Math.Sin(dlat / 2)) + Math.Cos(Radians(pA.X)) * Math.Cos(Radians(pB.X)) * (Math.Sin(dlon / 2) * Math.Sin(dlon / 2));
            double angle = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return (float)(angle * RADIUS) * 1000;
        }

        // Finds the straight line (|AC|) that is perpendicular to |AB| line and cuts point A, then returns 2 points located on this line with 'distance' from point A 
        public static Tuple<Vector2, Vector2> GetPerpendicularPoints(Vector2 pA, Vector2 pB, float distance)
        {
            if (Math.Abs(pA.Y - pB.Y) < CalculationConsts.EPSILON)
            {
                return Tuple.Create(new Vector2(pA.X, pA.Y - distance), new Vector2(pA.X, pA.Y + distance));
            }
            else if (Math.Abs(pA.X - pB.X) < CalculationConsts.EPSILON)
            {
                return Tuple.Create(new Vector2(pA.X - distance, pA.Y), new Vector2(pA.X + distance, pA.Y));
            }

            // Computes |AB| lines equation
            double aAB = (pB.Y - pA.Y) / (pB.X - pA.X);
            //double bAB = ((-pA.X) * (pB.Y - pA.Y) - (-pA.Y) * (pB.X - pA.X)) / (pB.X - pA.X);
            // Computes |AC| lines equation
            double aAC = (-1) / aAB;
            double bAC = pA.Y - (aAC * pA.X);
            // Computes quadratic equation for x-coordinates
            double xA = 1 + Math.Pow(aAC, 2);
            double xB = (-2) * pA.X + (2) * aAC * (bAC - pA.Y);
            double xC = Math.Pow(pA.X, 2) + Math.Pow(bAC - pA.Y, 2) - Math.Pow(distance, 2);
            double delta = Math.Pow(xB, 2) - 4 * xA * xC;
            float x1 = (float)((-xB - Math.Sqrt(delta)) / (2 * xA));
            float x2 = (float)((-xB + Math.Sqrt(delta)) / (2 * xA));
            // Computes y-coordinates
            float y1 = (float)(aAC * x1 + bAC);
            float y2 = (float)(aAC * x2 + bAC);

            return Tuple.Create(new Vector2(x1, y1), new Vector2(x2, y2));
        }

        // The point that is on |AB| line, with displacment from pA (in %)
        public static Vector2 GetLocactionBetween(float displacment, Vector2 pA, Vector2 pB)
        {
            if (Math.Abs(pA.X - pB.X) < CalculationConsts.EPSILON)
            {
                double d = GetDistance(pA, pB) * displacment / 100;
                return new Vector2(pA.X, pA.Y > pB.Y ? pA.Y - (float)d : pA.Y + (float)d);
            }
            else if (Math.Abs(pA.Y - pB.Y) < CalculationConsts.EPSILON)
            {
                double d = GetDistance(pA, pB) * displacment / 100;
                return new Vector2(pA.X > pB.X ? pA.X - (float)d : pA.X + (float)d, pA.Y);
            }

            float x = Math.Abs(pB.X - pA.X) * displacment / 100;
            float y = Math.Abs(pB.Y - pA.Y) * displacment / 100;
            float cX = pA.X > pB.X ? pA.X - x : pA.X + x;
            float cY = pA.Y > pB.Y ? pA.Y - y : pA.Y + y;

            return new Vector2(cX, cY);
        }

        private static double Radians(double x) => x * PIx / 180;

        private static double Degrees(double x) => x * 180 / PIx;
    }
}
