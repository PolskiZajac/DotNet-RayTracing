using RayTracing.Types;


namespace RayTracing.Samplers
{
    class SquareDistributor : ISampleDistributor
    {
        public Vector2 MapSample(Vector2 sample)
        {
            return sample;
        }
    }
}
