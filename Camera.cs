using System;

public class Camera
{
    public double aspect_ratio = 1.0;  // Ratio of image width over height
    public int image_width = 100;      // Rendered image width in pixels
    public int samples_per_pixel = 10; // Number of random samples per pixel

    private int image_height;          // Rendered image height
    private double pixel_samples_scale;// Color scale factor for accumulated samples

    private Vec3 camera_center;        // Camera position
    private Vec3 pixel00_loc;          // Location of pixel 0, 0
    private Vec3 pixel_delta_u;        // Offset to pixel to the right
    private Vec3 pixel_delta_v;        // Offset to pixel below

    public void Render(Hittable world)
    {
        Initialize();

        // Create the output image.
        using StreamWriter output = new("image.ppm");

        // Write the PPM header.
        output.WriteLine("P3");
        output.WriteLine($"{image_width} {image_height}");
        output.WriteLine("255");

        // Render each scanline from top to bottom.
        for (int j = 0; j < image_height; j++)
        {
            Console.Write($"\rScanlines remaining: {image_height - j} ");

            for (int i = 0; i < image_width; i++)
            {
                Vec3 pixel_color = new(0, 0, 0);

                // Take multiple randomly sampled rays for this pixel.
                for (int sample = 0; sample < samples_per_pixel; sample++)
                {
                    Ray r = GetRay(i, j);
                    pixel_color += RayColor(r, world);
                }

                // Average all samples and write the resulting pixel.
                WriteColor(
                    output,
                    pixel_samples_scale * pixel_color);
            }
        }

        Console.WriteLine("\rDone.                 ");
    }

    private void Initialize()
    {
        // Calculate the image height from the aspect ratio.
        image_height = (int)(image_width / aspect_ratio);
        image_height = image_height < 1 ? 1 : image_height;

        // Scale accumulated samples into their average color.
        pixel_samples_scale = 1.0 / samples_per_pixel;

        // Place the camera at the origin.
        camera_center = new Vec3(0, 0, 0);

        // Determine viewport dimensions.
        var focal_length = 1.0;
        var viewport_height = 2.0;

        var viewport_width =
            viewport_height * ((double)image_width / image_height);

        // Calculate the horizontal and vertical viewport vectors.
        var viewport_u = new Vec3(viewport_width, 0, 0);
        var viewport_v = new Vec3(0, -viewport_height, 0);

        // Calculate the distance between neighboring pixels.
        pixel_delta_u = viewport_u / image_width;
        pixel_delta_v = viewport_v / image_height;

        // Calculate the location of the upper-left corner of the viewport.
        var viewport_upper_left =
            camera_center
            - new Vec3(0, 0, focal_length)
            - viewport_u / 2
            - viewport_v / 2;

        // Calculate the center of the first pixel.
        pixel00_loc =
            viewport_upper_left
            + 0.5 * (pixel_delta_u + pixel_delta_v);
    }

    private Ray GetRay(int i, int j)
    {
        // Construct a ray from the camera through a randomly
        // sampled point inside the pixel.
        var offset = SampleSquare();

        var pixel_sample =
            pixel00_loc
            + ((i + offset.x()) * pixel_delta_u)
            + ((j + offset.y()) * pixel_delta_v);

        var ray_origin = camera_center;
        var ray_direction = pixel_sample - ray_origin;

        return new Ray(ray_origin, ray_direction);
    }

    private Vec3 SampleSquare()
    {
        // Return a random point inside a unit square centered on the origin.
        return new Vec3(
            RandomDouble() - 0.5,
            RandomDouble() - 0.5,
            0);
    }

    private Vec3 RayColor(Ray r, Hittable world)
    {
        HitRecord rec = new();

        // Check whether the ray hits an object in the world.
        if (world.Hit(
            r,
            new Interval(0, double.PositiveInfinity),
            rec))
        {
            // Convert the surface normal into a visible RGB color.
            return 0.5 * (rec.Normal + new Vec3(1, 1, 1));
        }

        // If nothing was hit, create the sky gradient.
        Vec3 unit_direction = Vec3.UnitVector(r.Direction());

        var a = 0.5 * (unit_direction.y() + 1.0);

        return
            (1.0 - a) * new Vec3(1.0, 1.0, 1.0) +
            a * new Vec3(0.5, 0.7, 1.0);
    }

    private double RandomDouble()
    {
        return Random.Shared.NextDouble();
    }

    private void WriteColor(StreamWriter output, Vec3 pixel_color)
    {
        // Convert the floating-point color to [0, 255] RGB values.
        int r = (int)(255.999 * pixel_color.x());
        int g = (int)(255.999 * pixel_color.y());
        int b = (int)(255.999 * pixel_color.z());

        output.WriteLine($"{r} {g} {b}");
    }
}