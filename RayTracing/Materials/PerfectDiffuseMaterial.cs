using RayTracing.Stuff;
using RayTracing.Types;


namespace RayTracing.Materials
{
    class PerfectDiffuseMaterial : IMaterial
    {
        ColorRgb materialColor;
        public PerfectDiffuseMaterial(ColorRgb materialColor)
        {
            this.materialColor = materialColor;
        }

        public ColorRgb Shade(RayTracer tracer, HitInfo hit)
        {
            ColorRgb totalColor = ColorRgb.Black;
            foreach (var light in hit.World.Lights)
            {
                Vector3 inDirection = (light.Position - hit.HitPoint).Normalized;
                double diffuseFactor = inDirection.Dot(hit.Normal);

                if (diffuseFactor >= 0 && !hit.World.AnyObstacleBetween(hit.HitPoint, light.Position))
                    totalColor += light.Color * materialColor * diffuseFactor;
            }
            return totalColor;
        }
    }
}
