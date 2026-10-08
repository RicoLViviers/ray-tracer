using System;

public class Vec3
{
    public double[] e = { 0, 0, 0 };

    public Vec3()
    {
    }

    public Vec3(double e0, double e1, double e2)
    {
        e[0] = e0;
        e[1] = e1;
        e[2] = e2;
    }

    public double x() { return e[0]; }
    public double y() { return e[1]; }
    public double z() { return e[2]; }

    // Unary negation.
    public static Vec3 operator -(Vec3 v)
    {
        return new Vec3(-v.e[0], -v.e[1], -v.e[2]);
    }

    // Array-style component access.
    public double this[int i]
    {
        get { return e[i]; }
        set { e[i] = value; }
    }

    // Adds another vector to this vector.
    public Vec3 Add(Vec3 v)
    {
        e[0] += v.e[0];
        e[1] += v.e[1];
        e[2] += v.e[2];

        return this;
    }

    // Multiplies this vector by a scalar.
    public Vec3 Multiply(double t)
    {
        e[0] *= t;
        e[1] *= t;
        e[2] *= t;

        return this;
    }

    // Divides this vector by a scalar.
    public Vec3 Divide(double t)
    {
        return Multiply(1 / t);
    }

    public double Length()
    {
        return Math.Sqrt(LengthSquared());
    }

    public double LengthSquared()
    {
        return e[0] * e[0]
             + e[1] * e[1]
             + e[2] * e[2];
    }

    public static Vec3 Random()
    {
        return new Vec3(RTweekend.RandomDouble(), RTweekend.RandomDouble(), RTweekend.RandomDouble());
    }

    public static Vec3 Random(double min, double max)
    {
        return new Vec3(RTweekend.RandomDouble(min, max), RTweekend.RandomDouble(min, max), RTweekend.RandomDouble(min, max));
    }

    // Adds two vectors.
    public static Vec3 operator +(Vec3 u, Vec3 v)
    {
        return new Vec3(
            u.e[0] + v.e[0],
            u.e[1] + v.e[1],
            u.e[2] + v.e[2]
        );
    }

    // Subtracts two vectors.
    public static Vec3 operator -(Vec3 u, Vec3 v)
    {
        return new Vec3(
            u.e[0] - v.e[0],
            u.e[1] - v.e[1],
            u.e[2] - v.e[2]
        );
    }

    // Component-wise multiplication.
    public static Vec3 operator *(Vec3 u, Vec3 v)
    {
        return new Vec3(
            u.e[0] * v.e[0],
            u.e[1] * v.e[1],
            u.e[2] * v.e[2]
        );
    }

    // Scalar multiplication.
    public static Vec3 operator *(double t, Vec3 v)
    {
        return new Vec3(
            t * v.e[0],
            t * v.e[1],
            t * v.e[2]
        );
    }

    public static Vec3 operator *(Vec3 v, double t)
    {
        return t * v;
    }

    // Scalar division.
    public static Vec3 operator /(Vec3 v, double t)
    {
        return (1 / t) * v;
    }

    // Dot product.
    public static double Dot(Vec3 u, Vec3 v)
    {
        return u.e[0] * v.e[0]
             + u.e[1] * v.e[1]
             + u.e[2] * v.e[2];
    }

    // Cross product.
    public static Vec3 Cross(Vec3 u, Vec3 v)
    {
        return new Vec3(
            u.e[1] * v.e[2] - u.e[2] * v.e[1],
            u.e[2] * v.e[0] - u.e[0] * v.e[2],
            u.e[0] * v.e[1] - u.e[1] * v.e[0]
        );
    }

    // Returns a normalized vector.
    public static Vec3 UnitVector(Vec3 v)
    {
        return v / v.Length();
    }


    public static Vec3 RandomUnitVector() {
        while (true) {
            var p = Vec3.Random(-1,1);
            var lensq = p.LengthSquared();
            if (lensq <= 1)
                return p / Math.Sqrt(lensq);
        }
    }


    public static Vec3 RandomOnHemisphere(Vec3 normal) {
        Vec3 onUnitSphere = RandomUnitVector();
        if (Vec3.Dot(onUnitSphere, normal) > 0.0)
            return onUnitSphere;
        else
            return -onUnitSphere;
    }

    public override string ToString()
    {
        return $"{e[0]} {e[1]} {e[2]}";
    }
}