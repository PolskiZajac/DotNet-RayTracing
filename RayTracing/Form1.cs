using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using RayTracing.Types;
using RayTracing.Cameras;
using RayTracing.Shapes;
using RayTracing.Stuff;
using RayTracing.Materials;
using RayTracing.Lights;
using RayTracing.Samplers;
using RayTracing.FormTypes;


namespace RayTracing
{
    partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            plane_mat_comboBox.DataSource = Enum.GetValues(typeof(MaterialsEnum));
            ssaa_generator_comboBox.DataSource = Enum.GetValues(typeof(GeneratorsEnum));
        }

        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            ssaa_generator_comboBox.Enabled = ssaa_checkbox.Checked;
            ssaa_seedValue.Enabled = ssaa_checkbox.Checked;
            ssaa_samplesCountValue.Enabled = ssaa_checkbox.Checked;
            ssaa_sampleSetsValue.Enabled = ssaa_checkbox.Checked;

            ssaa_generator_comboBox_SelectedIndexChanged(sender, e);
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            ColorDialog worldColorDialog = new()
            {
                Color = Color.PowderBlue,
                FullOpen = true
            };

            if (worldColorDialog.ShowDialog() == DialogResult.OK) 
                world_colorBtn.BackColor = worldColorDialog.Color;
        }

        private void plane_colorBtn_Click(object sender, System.EventArgs e)
        {
            ColorDialog planeColorDialog = new()
            {
                Color = Color.Gray,
                FullOpen = true
            };

            if (planeColorDialog.ShowDialog() == DialogResult.OK)
                plane_mat_colorBtn.BackColor = planeColorDialog.Color;
        }

        private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            MaterialsEnum selectedMaterial = (MaterialsEnum) plane_mat_comboBox.SelectedIndex;

            if (selectedMaterial == MaterialsEnum.REFLECTIVE)
                plane_mat_reflectivityValue.Enabled = true;
            else plane_mat_reflectivityValue.Enabled = false;

            if (selectedMaterial >= MaterialsEnum.PHONG)
            {
                plane_mat_diffuseValue.Enabled = true;
                plane_mat_specValue.Enabled = true;
                plane_mat_specExpoValue.Enabled = true;
            }
            else
            {
                plane_mat_diffuseValue.Enabled = false;
                plane_mat_specValue.Enabled = false;
                plane_mat_specExpoValue.Enabled = false;
            }
        }

        private void ssaa_generator_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GeneratorsEnum selectedGenerator = (GeneratorsEnum) ssaa_generator_comboBox.SelectedIndex;

            if (selectedGenerator == GeneratorsEnum.REGULAR) ssaa_seedValue.Enabled = false;
            else ssaa_seedValue.Enabled = true;
        }

        private void raytrace_btn_Click(object sender, EventArgs e)
        {
            World world = new(world_colorBtn.BackColor);

            Sampler antiAliasSampler = new(
                new RegularGenerator(),
                new SquareDistributor(),
                (int)ssaa_samplesCountValue.Value,
                (int)ssaa_sampleSetsValue.Value);

            IMaterial sph1Mat = new ReflectiveMaterial(
                sph1_mat_colorBtn.BackColor,
                (double)sph1_mat_diffuseValue.Value,
                (double)sph1_mat_specValue.Value,
                (double)sph1_mat_specExpoValue.Value,
                (double)sph1_mat_reflectivityValue.Value);

            IMaterial sph2Mat = new ReflectiveMaterial(
                Color.LightGreen,
                0.4,
                1,
                300,
                0.6);

            IMaterial sph3Mat = new ReflectiveMaterial(
                Color.LightBlue,
                0.4,
                1,
                300,
                0.6);

            IMaterial planeMat = new ReflectiveMaterial(
                plane_mat_colorBtn.BackColor,
                (double)plane_mat_diffuseValue.Value,
                (double)plane_mat_specValue.Value,
                (double)plane_mat_specExpoValue.Value,
                (double)plane_mat_reflectivityValue.Value);

            world.Add(new Sphere(
                new Vector3(
                    (double)sph1_center_xValue.Value,
                    (double)sph1_center_yValue.Value,
                    (double)sph1_center_zValue.Value),
                (float)sph1_radiusValue.Value,
                sph1Mat));
            world.Add(new Sphere(new Vector3(0, 0, 5), 2, sph2Mat));
            world.Add(new Sphere(new Vector3(4, 0, 3), 2, sph3Mat));

            world.Add(new Plane(
                new Vector3(
                    (double)plane_point_xValue.Value,
                    (double)plane_point_yValue.Value,
                    (double)plane_point_zValue.Value),
                new Vector3(
                    (double)plane_normal_xValue.Value,
                    (double)plane_normal_yValue.Value,
                    (double)plane_normal_zValue.Value),
                planeMat));

            world.AddLight(new PointLight(
                new Vector3(0, 5, -5),
                Color.White
                ));

            ICamera pinHoleCamera = new PinHoleCamera(
                new Vector3(0, 1, -3),
                new Vector3(0, 0, 1),
                new Vector3(0, -1, 0),
                new Vector2(1, 1),
                1
            );

            RayTracer tracer = new(3, world, pinHoleCamera);
            Stopwatch timer = new();
            timer.Start();
            Bitmap image = tracer.Render(new Size((int)image_width.Value, (int)image_height.Value), antiAliasSampler);
            timer.Stop();
            timeLabel.Text = timer.ElapsedMilliseconds + "ms";
            pictureBox1.Image = image;
            image.Save("rt-test.png");
        }

        private void sph1_mat_colorBtn_Click(object sender, EventArgs e)
        {
            ColorDialog sph1ColorDialog = new()
            {
                Color = Color.Gray,
                FullOpen = true
            };

            if (sph1ColorDialog.ShowDialog() == DialogResult.OK)
                sph1_mat_colorBtn.BackColor = sph1ColorDialog.Color;
        }
    }
}
