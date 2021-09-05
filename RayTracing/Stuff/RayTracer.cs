using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using RayTracing.Types;
using RayTracing.Cameras;
using RayTracing.Materials;
using RayTracing.Samplers;

namespace RayTracing.Stuff
{
    public class RayTracer
    {
        readonly int maxDepth;
        readonly World world;
        readonly ICamera camera;

        int imageHeightPerThread;
        int depth;
        byte[] buffer;
        Bitmap bitmap;
        BitmapData bitmapData;

        readonly int threadAmount = 16;

        public RayTracer(int maxDepth, World world, ICamera camera, int threadAmount=16)
        {
            this.maxDepth = maxDepth;
            this.world = world;
            this.camera = camera;
            this.threadAmount = threadAmount;
        }

        public Bitmap Render(Size size, Sampler sampler)
        {
            this.bitmap = new(size.Width, size.Height, PixelFormat.Format24bppRgb);
            CopyBitmapToBuffer();
            Parallel.For(0, threadAmount, threadNumber => Process(threadNumber, size, sampler));
            CopyBitmapFromBuffer();
            return bitmap;
        }

        private void CopyBitmapToBuffer()
        {
            Rectangle rect = new(0, 0, this.bitmap.Width, this.bitmap.Height);
            this.bitmapData = this.bitmap.LockBits(rect, ImageLockMode.ReadWrite, this.bitmap.PixelFormat);

            this.depth = (Image.GetPixelFormatSize(this.bitmapData.PixelFormat) / 8);
            this.buffer = new byte[this.bitmapData.Width * this.bitmapData.Height * depth];
            this.imageHeightPerThread = (this.bitmap.Height / this.threadAmount);

            Marshal.Copy(this.bitmapData.Scan0, this.buffer, 0, this.buffer.Length);
        }

        private void CopyBitmapFromBuffer()
        {
            Marshal.Copy(this.buffer, 0, this.bitmapData.Scan0, this.buffer.Length);
            bitmap.UnlockBits(this.bitmapData);
        }

        private void Process(int threadNumber, Size imageSize, Sampler antiAliasSampler)
        {
            int startHeight = this.imageHeightPerThread * threadNumber;
            int endHeight = startHeight + this.imageHeightPerThread;
            Sampler antiAliasSamplerCopy = antiAliasSampler.DeepCopy();

            for (int i = 0; i < imageSize.Width; i++)
                for (int j = startHeight; j < endHeight; j++)
                {
                    int offset = ((j * imageSize.Width) + i) * this.depth;
                    ColorRgb color = ColorRgb.Black;

                    for (int k = 0; k < antiAliasSamplerCopy.SampleCount; k++)
                    {
                        Vector2 sample = antiAliasSamplerCopy.Single();
                        Vector2 pictureCoordinates = new(
                            ((i + sample.X) / imageSize.Width) * 2 - 1,
                            ((j + sample.Y) / imageSize.Height) * 2 - 1);
                        Ray ray = camera.GetRayTo(pictureCoordinates);
                        color += ShadeRay(ray, 0) / antiAliasSamplerCopy.SampleCount;
                    }
                    SetColorsByBufferOffset(color, offset);
                }
        }

        public ColorRgb ShadeRay(Ray ray, int currentDepth)
        {
            if (currentDepth > maxDepth) return ColorRgb.Black;
            HitInfo hit = world.TraceRay(ray);
            if (hit.HitObject == null) return world.BackgroundColor;
            hit.Depth = currentDepth + 1;
            IMaterial material = hit.HitObject.Material;
            return material.Shade(this, hit);
        }

        private void SetColorsByBufferOffset(ColorRgb color, int offset)
        {
            Color finalColor = color;
            this.buffer[offset + 0] = finalColor.B;
            this.buffer[offset + 1] = finalColor.G;
            this.buffer[offset + 2] = finalColor.R;
        }
    }
}
