public class Interval
{
    public double Min, Max;

    public Interval()
    {
        Min = double.PositiveInfinity;
        Max = double.NegativeInfinity;
    }

    public Interval(double min, double max)
    {
        Min = min;
        Max = max;
    }

    public double Size()
    {
        return Max - Min;
    }

    public bool Contains(double x)
    {
        return Min <= x && x <= Max;
    }

    public bool Surrounds(double x)
    {
        return Min < x && x < Max;
    }

    public double Clamp(double x)
    {
        if (x < Min) return Min;
        if (x > Max) return Max;
        return x;
    }

    public static readonly Interval Empty = new Interval(double.PositiveInfinity, double.NegativeInfinity);

    public static readonly Interval Universe = new Interval(double.NegativeInfinity, double.PositiveInfinity);
}