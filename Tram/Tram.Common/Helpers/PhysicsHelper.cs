using System;
using Tram.Common.Consts;

namespace Tram.Common.Helpers
{
    public static class PhysicsHelper
    {
        public static float GetNewSpeed(float oldSpeed, float deltaTime, bool increase = true) => oldSpeed + (increase ? 1 : -1) * deltaTime * VehicleConsts.ACCELERATION;

        public static float GetBrakingDistance(float speed) => (speed * speed) / (2 * VehicleConsts.ACCELERATION);

        public static float GetTranslation(float oldSpeed, float newSpeed) => Math.Abs(newSpeed * newSpeed - oldSpeed * oldSpeed) / (2 * VehicleConsts.ACCELERATION);
    }
}
