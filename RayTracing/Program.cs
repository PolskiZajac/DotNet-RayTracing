using System;
using System.Drawing;
using System.Windows.Forms;


namespace RayTracing
{
    static class Program
    {
        static void Main()
        {
            #region
            // Stworzenie świata (kolor tła = łagodny niebieski)
            // World world = new World(Color.PowderBlue);

            // Samplers
            /*Sampler antiAliasSampler = new Sampler(
                new RegularGenerator(),
                new SquareDistributor(),
                antiAliasSampleCount,
                1); */


            /*

            const int fieldOfDepthSampleCount = 1;
            Sampler fieldOfDepthSampler = new Sampler(
                new JitteredGenerator(0),
                new SquareDistributor(),
                fieldOfDepthSampleCount,
                1);

            const int areaLightSampleCount = 12;
            Sampler areaLightSampler = new Sampler(
                 new JitteredGenerator(0), // generator
                 new SquareDistributor(),// dystrybutor
                 areaLightSampleCount, // ilość sampli
                 97); // ilość zestawów sampli

            // Materiały
            IMaterial redMat = new ReflectiveMaterial(Color.LightCoral, 0.4, 1, 300, 0.6);
            IMaterial greenMat = new ReflectiveMaterial(Color.LightGreen, 0.4, 1, 300, 0.6);
            IMaterial blueMat = new ReflectiveMaterial(Color.LightBlue, 0.4, 1, 300, 0.6);
            IMaterial grayMat = new ReflectiveMaterial(Color.Gray, 0.8, 1, 30, 0.2);

            // Trzy różnokolorowe kule
            world.Add(new Sphere(new Vector3(-4, 0, 7), 2, redMat));
            world.Add(new Sphere(new Vector3(0, 0, 5), 2, greenMat));
            world.Add(new Sphere(new Vector3(4, 0, 3), 2, blueMat));

            // Płaszczyzna
            world.Add(new Plane(new Vector3(0, -2, 0), new Vector3(0, 1, 0), grayMat));

            // Światło
            world.AddLight(new Light(
                new Vector3(6, 2, 0), // pozycja światła
                Color.White, // kolor światła
                areaLightSampler, // sampler
                2));

            ICamera pinHoleCamera = new PinHoleCamera(
                new Vector3(0, 1, -3),
                new Vector3(0, 0, 1),
                new Vector3(0, -1, 0),
                new Vector2(1, 1),
                1
            );

            ICamera thinLensCamera = new ThinLens(
                new Vector3(0, 1, -3),
                new Vector3(0, 0, 1),
                new Vector3(0, -1, 0),
                new Vector2(1, 1),
                1,
                fieldOfDepthSampler,
                1,
                7.5
            );

            RayTracer tracer = new RayTracer(5);
            
            // Raytracing!
            Bitmap image = tracer.Raytrace(world, pinHoleCamera, new Size(256, 256), antiAliasSampler);
            */
            // Zapisanie obrazka w jakimś miłym miejscu na dysku.
            //image.Save("raytraced.png");
            #endregion

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
