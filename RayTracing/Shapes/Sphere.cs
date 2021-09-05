using System;
using System.Drawing;

using RayTracing.Types;
using RayTracing.Materials;


namespace RayTracing.Shapes
{
    class Sphere : GeometricObject
    {
        Vector3 center;
        float radius;

        public Sphere(Vector3 center, float radius, IMaterial material)
        {
            this.center = center;
            this.radius = radius;
            base.Material = material;
        }


        public override bool HitTest(Ray ray, ref double minDistance, ref Vector3 outNormal)
        {
            double t;
            Vector3 distance = ray.Origin - center;

            double a = ray.Direction.LengthSq;
            double b = (distance * 2).Dot(ray.Direction);
            double c = distance.LengthSq - radius * radius;
            double disc = b * b - 4 * a * c;

            if (disc < 0)
            {
                return false;
            }

            double discSq = Math.Sqrt(disc);
            double denom = 2 * a;

            t = (-b - discSq) / denom;
            if (t < Ray.Epsilon)
            {
                t = (-b + discSq) / denom;
            }
            if (t < Ray.Epsilon)
            {
                return false;
            }

            Vector3 hitPoint = (ray.Origin + ray.Direction * t);
            outNormal = (hitPoint - center).Normalized;
            minDistance = t;
            return true;
        }
    }
}
