public class Sphere : Hittable
{
    private Vec3 Center;
    private double Radius;
    public Sphere(Vec3 center, double radius) 
    {
        Center = center;
        Radius = radius;
    }

    public override bool Hit(Ray r, double rayTMin, double rayTMax, HitRecord rec)
    {
        Vec3 oc = Center - r.Origin();
        var a = r.Direction().LengthSquared();
        var h = Vec3.Dot(r.Direction(), oc);
        var c = oc.LengthSquared() - Radius * Radius;

        var discriminant = h*h - a*c;
        if (discriminant < 0)
            return false;

        var sqrtd = Math.Sqrt(discriminant);

        // Find the nearest root that lies in the acceptable range.
        var root = (h - sqrtd) / a;
        if (root <= rayTMin || rayTMax <= root) {
            root = (h + sqrtd) / a;
            if (root <= rayTMin || rayTMax <= root)
                return false;
        }

        rec.T = root;
        rec.P = r.At(rec.T);
        Vec3 outwardNormal = (rec.P - Center) / Radius;
        rec.SetFaceNormal(r, outwardNormal);

        return true;
    }
}