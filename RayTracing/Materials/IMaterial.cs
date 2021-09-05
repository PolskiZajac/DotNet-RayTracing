using RayTracing.Types;
using RayTracing.Stuff;


namespace RayTracing.Materials
{
    public interface IMaterial
    {
        ColorRgb Shade(RayTracer tracer, HitInfo hit);
    }
}
