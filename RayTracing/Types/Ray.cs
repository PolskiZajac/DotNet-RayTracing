namespace RayTracing.Types
{
    public struct Ray
    {
        public const float Epsilon = 0.00001f;
        public const float Huge = float.MaxValue;

        public Vector3 Origin { get; set; }
        public Vector3 Direction { get; set; }

        public Ray(Vector3 origin, Vector3 direction) : this()
        {
            this.Origin = origin;
            this.Direction = direction.Normalized;
        }
    }
}
