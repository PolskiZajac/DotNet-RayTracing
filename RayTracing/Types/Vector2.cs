namespace RayTracing.Types
{
    public struct Vector2
    {
        public double x;
        public double y;

        public double X { get { return x; } set { x = value; } }
        public double Y { get { return y; } set { y = value; } }

        public Vector2(double x, double y) : this()
        {
            this.X = x;
            this.Y = y;
        }

        public static Vector2 operator *(Vector2 vec, double val) => new Vector2(
                vec.X * val,
                vec.Y * val
            );

        public static Vector2 operator /(Vector2 vec, double val) => new Vector2(
                vec.X / val,
                vec.Y / val
            );
    }
}
