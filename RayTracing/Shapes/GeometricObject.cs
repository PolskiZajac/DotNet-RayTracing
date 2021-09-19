using RayTracing.Materials;
using RayTracing.Types;

namespace RayTracing.Shapes
{
    public abstract class GeometricObject
    {
        public IMaterial Material { get; set; }
        public abstract bool HitTest(Ray ray, ref double distance, ref Vector3 normal);
    }
}
