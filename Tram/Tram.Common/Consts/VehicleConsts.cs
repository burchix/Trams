namespace Tram.Common.Consts
{
    public static class VehicleConsts
    {
        // m
        public const int LENGTH = 42;

        // km/h
        public const float MAX_SPEED = 50f;

        // km/h
        public const float MAX_CROSS_SPEED = 10f;

        // s
        public const float TIME_TO_MAX_SPEED = 12f;

        // m/s^2
        public const float ACCELERATION = MAX_SPEED * 1000 / 3600 / TIME_TO_MAX_SPEED;

        // m
        public const float DISTANCE_TO_MAX_SPEED = ACCELERATION * TIME_TO_MAX_SPEED * TIME_TO_MAX_SPEED / 2;
        
        // m
        public const float DISTANCE_TO_MAX_CROSS_SPEED = ACCELERATION * (MAX_CROSS_SPEED * TIME_TO_MAX_SPEED / MAX_SPEED) * (MAX_CROSS_SPEED * TIME_TO_MAX_SPEED / MAX_SPEED) / 2;
    }
}
