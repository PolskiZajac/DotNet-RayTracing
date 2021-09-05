using RayTracing.Types;
using RayTracing.Stuff;


namespace RayTracing.Materials
{
    class ReflectiveMaterial : IMaterial
    {
        PhongMaterial direct; // do bezpośredniego oświetlenia
        double reflectivity;
        ColorRgb reflectionColor;

        public ReflectiveMaterial(
            ColorRgb materialColor,
            double diffuse,
            double specular,
            double exponent,
            double reflectivity)
        {
            this.direct = new PhongMaterial(materialColor, diffuse, specular, exponent);
            this.reflectivity = reflectivity;
            this.reflectionColor = materialColor;
        }
        public ColorRgb Shade(RayTracer tracer, HitInfo hit)
        {
            Vector3 toCameraDirection = -hit.Ray.Direction;
            ColorRgb radiance = direct.Shade(tracer, hit);
            Vector3 reflectionDirection = Vector3.Reflect(toCameraDirection, hit.Normal);
            Ray reflectedRay = new Ray(hit.HitPoint, reflectionDirection);
            radiance += tracer.ShadeRay(reflectedRay, hit.Depth) * reflectionColor * reflectivity;
            return radiance;
        }
    }
}
