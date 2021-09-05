using System;
using System.Collections.Generic;
using System.Linq;
using RayTracing.Types;


namespace RayTracing.Samplers
{
    public class Sampler
    {
        readonly Random r;
        readonly List<Vector2[]> sets;
        readonly ISampleGenerator sampleGenerator;
        readonly ISampleDistributor sampleDistributor;
        int sampleNdx;
        int setNdx;

        public int SampleCount { get; private set; }

        public Sampler(
            ISampleGenerator sampleGenerator,
            ISampleDistributor sampleDistributor,
            int sampleCount,
            int setCount
        )
        {
            this.sampleGenerator = sampleGenerator;
            this.sampleDistributor = sampleDistributor;
            this.sets = new List<Vector2[]>(setCount);
            this.r = new Random(0);
            this.SampleCount = sampleCount;
            for (int i = 0; i < setCount; i++)
            {
                var samples = sampleGenerator.Sample(sampleCount);
                var mappedSamples = samples.Select((x) => sampleDistributor.MapSample(x)).ToArray();
                sets.Add(mappedSamples);
            }
        }

        public Vector2 Single()
        {
            Vector2 sample;
            sample = sets[setNdx][sampleNdx];
            sampleNdx++;

            if (sampleNdx > sets[setNdx].Length - 1)
            {
                sampleNdx = 0;
                setNdx = r.Next(sets.Count);
            }
            return sample;
        }

        public Sampler DeepCopy()
        {
            return new Sampler(
                this.sampleGenerator,
                this.sampleDistributor,
                this.SampleCount,
                this.sets.Count
                );
        }
    }
}
