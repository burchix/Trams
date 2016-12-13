using Microsoft.DirectX;

namespace Tram.Common.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 ToVector3(this Vector2 v2)
        {
            return new Vector3(v2.X, v2.Y, 0);
        } 
    }
}
