using System;

public class Color
{
    // Writes a color to the output stream as RGB values in the range [0, 255].
    public static void WriteColor(TextWriter output, Vec3 pixelColor)
    {
        double r = pixelColor.x();
        double g = pixelColor.y();
        double b = pixelColor.z();

        // Translate the [0,1] component values to the byte range [0,255].\
        Interval intensity = new(0.000, 0.999);
        int rByte = (int)(256 * intensity.Clamp(r));
        int gByte = (int)(256 * intensity.Clamp(g));
        int bByte = (int)(256 * intensity.Clamp(b));

        // Write out the pixel color components.
        output.WriteLine($"{rByte} {gByte} {bByte}");
    }
}