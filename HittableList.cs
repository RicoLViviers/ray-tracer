public class HittableList : Hittable
{
    public List<Hittable> Objects = new();

    public HittableList()
    {
    }

    public HittableList(Hittable obj)
    {
        Add(obj);
    }

    public void Clear()
    {
        Objects.Clear();
    }

    public void Add(Hittable obj)
    {
        Objects.Add(obj);
    }

    public override bool Hit(
        Ray r,
        Interval rayT,
        HitRecord rec)
    {
        HitRecord tempRec = new();
        bool hitAnything = false;
        double closestSoFar = rayT.Max;

        foreach (Hittable obj in Objects)
        {
            if (obj.Hit(
                r,
                new Interval(rayT.Min, closestSoFar),
                tempRec))
            {
                hitAnything = true;
                closestSoFar = tempRec.T;

                rec.P = tempRec.P;
                rec.Normal = tempRec.Normal;
                rec.T = tempRec.T;
                rec.front_face = tempRec.front_face;
            }
        }

        return hitAnything;
    }
}