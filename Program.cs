using StreamWriter output = new("image.ppm");


Vec3 RayColor(Ray r, HittableList world)
{
    HitRecord rec = new();
    if (world.Hit(r, 0, RTweekend.Infinity, rec))
    {
        return 0.5 * (rec.Normal + new Vec3(1, 1, 1));
    }

    Vec3 unit_direction = Vec3.UnitVector(r.Direction());   
    var a = 0.5 * (unit_direction.y() + 1.0);
    return (1.0 - a) * new Vec3(1.0, 1.0, 1.0) + a * new Vec3(0.5, 0.7, 1.0);
}

var aspect_ratio = 16.0 / 9.0;
int image_width = 400;

int image_height = (int)(image_width/aspect_ratio);
image_height = (image_height < 1) ? 1 : image_height;

HittableList world = new();

world.Add(new Sphere(new Vec3(0, 0, -1), 0.5));
world.Add(new Sphere(new Vec3(0, -100.5, -1), 100));

var focal_length = 1.0;
var viewport_height = 2.0;
var viewport_width = viewport_height * ((double)(image_width)/image_height);
var camera_center = new Vec3(0, 0, 0);

var viewport_u = new Vec3(viewport_width, 0, 0);
var viewport_v = new Vec3(0, viewport_height, 0);

var pixel_delta_u = viewport_u/image_width;
var pixel_delta_v = viewport_v/image_height;

var viewport_upper_left = camera_center - new Vec3(0, 0, focal_length) - viewport_u/2 - viewport_v/2;
var pixel00_loc = viewport_upper_left + 0.5 * (pixel_delta_u + pixel_delta_v);

// Write the PPM header.
output.WriteLine("P3");
output.WriteLine($"{image_width} {image_height}");
output.WriteLine("255");

for (int j = image_height; j > 0; j--)
{
    Console.WriteLine($"Scanlines remaining {image_height - j}");

    for (int i = 0; i < image_width; i++)
    {
        var pixel_center = pixel00_loc + (i * pixel_delta_u) + (j * pixel_delta_v);
        var ray_direction = pixel_center - camera_center;
        Ray r = new Ray(camera_center, ray_direction);

        var pixelColor = RayColor(r, world);
        Color.WriteColor(output, pixelColor);
    }
}

Console.WriteLine("Done");