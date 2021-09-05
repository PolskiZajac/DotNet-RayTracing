using RayTracing.Types;


namespace RayTracing.Samplers
{
    public interface ISampleDistributor
    {
        Vector2 MapSample(Vector2 sample);
    }
}
