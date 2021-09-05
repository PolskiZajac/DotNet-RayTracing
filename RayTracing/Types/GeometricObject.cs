using RayTracing.Materials;


namespace RayTracing.Types
{
    abstract public class GeometricObject
    {
        public IMaterial Material { get; set; }
        public abstract bool HitTest(Ray ray, ref double distance, ref Vector3 normal);
    }
}
