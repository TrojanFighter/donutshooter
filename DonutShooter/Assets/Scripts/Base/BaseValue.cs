namespace DonutShooter.Base
{

    public static class BaseValue
    {
        public static float shootingTimeGap = 0.4f;
        public static float lastTimeShot = 0f;
        public static uint lastScore = 0;
        public static uint lastLove = 0;
    }

    public enum ColorState
    {
        None=0,
        Red,
        Green,
        Blue
    }
}
