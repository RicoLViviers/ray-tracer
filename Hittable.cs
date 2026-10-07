public class HitRecord
{
    public Vec3 P;
    public Vec3 Normal;
    public double T;
    public bool front_face;

    public void SetFaceNormal(Ray r, Vec3 outwardNormal) 
    {
        front_face = Vec3.Dot(r.Direction(), outwardNormal) < 0;
        Normal = front_face ? outwardNormal : -outwardNormal;
    }
}

public abstract class Hittable
{
    public abstract bool Hit(Ray r, Interval rayT, HitRecord rec);
}