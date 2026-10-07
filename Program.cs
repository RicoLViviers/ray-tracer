HittableList world = new();

world.Add(new Sphere(new Vec3(0, 0, -1), 0.5));
world.Add(new Sphere(new Vec3(0, -100.5, -1), 100));

Camera cam = new();

cam.aspect_ratio = 16.0 / 9.0;
cam.image_width = 400;
cam.samples_per_pixel = 100;

cam.Render(world);

Ray testRay = new(
    new Vec3(0, 0, 0),
    new Vec3(0, 0, -1));

HitRecord testRecord = new();

bool hit = world.Hit(
    testRay,
    new Interval(0, double.PositiveInfinity),
    testRecord);

Console.WriteLine($"Hit: {hit}");

if (hit)
{
    Console.WriteLine($"T: {testRecord.T}");
    Console.WriteLine($"P: {testRecord.P}");
    Console.WriteLine($"Normal: {testRecord.Normal}");
}