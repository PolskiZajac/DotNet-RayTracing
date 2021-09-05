using RayTracing.Types;


namespace RayTracing.Samplers
{
    public interface ISampleGenerator
    {
        Vector2[] Sample(int count);
    }
}
