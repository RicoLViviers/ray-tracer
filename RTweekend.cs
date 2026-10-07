public static class RTweekend
{
    private static Random rnd = new Random();
    public const double Infinity = double.PositiveInfinity;
    public const double Pi = Math.PI;

    public static double DegreesToRadians(double degrees)
    {
        return degrees * Pi / 180.0;
    }

    public static double RandomDouble()
    {
        return rnd.NextDouble();
    }

    public static double RandomDouble(double min, double max)
    {
        return rnd.NextDouble() * (max-min) + min;
    }
}