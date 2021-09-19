namespace RayTracing.Types
{
    public struct Vector2
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector2(double x, double y) : this()
        {
            this.X = x;
            this.Y = y;
        }

        public static Vector2 operator *(Vector2 vec, double val)
        {
            return new Vector2(
                vec.X * val,
                vec.Y * val
            );
        }

        public static Vector2 operator /(Vector2 vec, double val)
        {
            return new Vector2(
                vec.X / val,
                vec.Y / val
            );
        }
    }
}
