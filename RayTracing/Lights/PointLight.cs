using RayTracing.Types;


namespace RayTracing.Lights
{
    public class PointLight
    {
        public PointLight(Vector3 position, ColorRgb color)
        {
            this.Position = position;
            this.Color = color;
        }
        public Vector3 Position { get; private set; }
        public ColorRgb Color { get; private set; }
    }
}
