using RayTracing.Types;

namespace RayTracing.Cameras
{
    public interface ICamera
    {
        Ray GetRayTo(Vector2 relativeLocation);
    }
}
