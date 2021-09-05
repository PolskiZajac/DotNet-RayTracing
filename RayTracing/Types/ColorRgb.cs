using System.Drawing;


namespace RayTracing.Types
{
    public struct ColorRgb
    {
        public double R { get; set; }
        public double G { get; set; }
        public double B { get; set; }
        public ColorRgb(double r, double g, double b) : this()
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }

        public static implicit operator ColorRgb(Color color)
        {
            return new ColorRgb(color.R / 255.0, color.G / 255.0, color.B / 255.0);
        }
        public static implicit operator Color(ColorRgb color)
        {
            color.R = color.R < 0 ? 0 : color.R > 1 ? 1 : color.R;
            color.G = color.G < 0 ? 0 : color.G > 1 ? 1 : color.G;
            color.B = color.B < 0 ? 0 : color.B > 1 ? 1 : color.B;
            return Color.FromArgb(
                (int)(color.R * 255),
                (int)(color.G * 255),
                (int)(color.B * 255));
        }
        public static ColorRgb operator + (ColorRgb col1, ColorRgb col2)
        {
            return new ColorRgb(col1.R + col2.R, col1.G + col2.G, col1.B + col2.B);
        }
        public static ColorRgb operator * (ColorRgb col1, double val)
        {
            return new ColorRgb(col1.R * val, col1.G * val, col1.B * val);
        }
        public static ColorRgb operator * (ColorRgb col1, ColorRgb col2)
        {
            return new ColorRgb(col1.R * col2.R, col1.G * col2.G, col1.B * col2.B);
        }
        public static ColorRgb operator / (ColorRgb col1, double val)
        {
            return col1 * (1 / val);
        }

        public static readonly ColorRgb White = new ColorRgb(1, 1, 1);
        public static readonly ColorRgb Black = new ColorRgb(0, 0, 0);
        public static readonly ColorRgb Red = new ColorRgb(1, 0, 0);
        public static readonly ColorRgb Green = new ColorRgb(0, 1, 0);
        public static readonly ColorRgb Blue = new ColorRgb(0, 0, 1);
    }
}
