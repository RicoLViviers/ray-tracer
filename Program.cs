using StreamWriter output = new("image.ppm");

int image_width = 256;
int image_height = 256;

// Write the PPM header.
output.WriteLine("P3");
output.WriteLine($"{image_width} {image_height}");
output.WriteLine("255");

for (int j = 0; j < image_height; j++)
{
    Console.WriteLine($"Scanlines remaining {image_height - j}");

    for (int i = 0; i < image_width; i++)
    {
        var pixelColor = new Vec3(
            (double)i/(image_width-1), 
            (double)j/(image_height-1), 
            0);

        Color.WriteColor(output, pixelColor);
    }
}

Console.WriteLine("Done");