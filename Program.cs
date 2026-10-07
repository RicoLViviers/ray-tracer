using StreamWriter output = new("image.ppm");

Vec3 RayColor(Ray r)
{
    if (HitSphere(new Vec3(0, 0, -1), 0.5, r))
    {
        return new Vec3(1, 0, 0);
    }

    Vec3 unit_direction = Vec3.UnitVector(r.Direction());   
    var a = 0.5 * (unit_direction.y() + 1.0);
    return (1.0 - a) * new Vec3(1.0, 1.0, 1.0) + a * new Vec3(0.5, 0.7, 1.0);
}

var aspect_ratio = 16.0 / 9.0;
int image_width = 400;

int image_height = (int)(image_width/aspect_ratio);
image_height = (image_height < 1) ? 1 : image_height;

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

bool HitSphere(Vec3 center, double radius, Ray ray)
{
    Vec3 oc = center - ray.Origin();

    double a = Vec3.Dot(ray.Direction(), ray.Direction());
    double b = -2.0 * Vec3.Dot(ray.Direction(), oc);
    double c = Vec3.Dot(oc, oc) - radius * radius;

    double discriminant = b * b - 4 * a * c;

    return discriminant >= 0;
}

// Write the PPM header.
output.WriteLine("P3");
output.WriteLine($"{image_width} {image_height}");
output.WriteLine("255");

for (int j = 0; j < image_height; j++)
{
    Console.WriteLine($"Scanlines remaining {image_height - j}");

    for (int i = 0; i < image_width; i++)
    {
        var pixel_center = pixel00_loc + (i * pixel_delta_u) + (j * pixel_delta_v);
        var ray_direction = pixel_center - camera_center;
        Ray r = new Ray(pixel_center, ray_direction);

        var pixelColor = RayColor(r);
        Color.WriteColor(output, pixelColor);
    }
}

Console.WriteLine("Done");