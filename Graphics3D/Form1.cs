using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Graphics2D;

namespace Graphics3D
{
    public partial class Form1 : Form
    {
        List<Cube> cubes = new List<Cube>();
        List<Ball3D> balls = new List<Ball3D>();
        double distance = 1000;
        double delta = 0.05; //a small angle rotation for a key press
        Point3D angleRotation = new Point3D();

        public Form1()
        {
            InitializeComponent();
            this.MouseWheel += Form1_MouseWheel;
        }

        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                distance *= 1.1;
            else if (e.Delta < 0)
                distance /= 1.1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cubes.Add(new Cube());
            cubes[0].Scale(200);

            Ball3D ball = new Ball3D(new Point3D(-30, 0, 0), 30);
            ball.Velocity = new Point3D(2, 1, 0);
            ball.Acceleration = new Point3D(0, 1, 0);
            balls.Add(ball);

            ball = new Ball3D(new Point3D(30, 1, 0), 20);
            ball.Velocity = new Point3D(-2, 0, -1);
            ball.Acceleration = new Point3D(0, 1, 0);
            ball.Brush = Brushes.Beige;
            balls.Add(ball);

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //we want to show the cube rotated at the beginning of paint, then unrotate it at the end
            //rotate balls and cube here
            foreach (Cube cube in cubes)
                cube.Rotate(angleRotation);
            foreach (Ball3D ball in balls)
                ball.Rotate(angleRotation);

            Graphics gr = e.Graphics;
            gr.TranslateTransform(ClientSize.Width / 2, ClientSize.Height / 2);
            foreach (Cube cube in cubes)
                cube.Draw(e.Graphics, distance, Face.back);
            foreach (Ball3D ball in balls.OrderBy(x => x.Z))
                ball.Draw(gr, distance);
            foreach (Cube cube in cubes)
                cube.Draw(e.Graphics, distance, Face.front);

            //unrotate here
            foreach (Cube cube in cubes)
                cube.UnRotate(angleRotation);
            foreach (Ball3D ball in balls)
                ball.UnRotate(angleRotation);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Escape:
                    Dispose();
                    break;
                case Keys.Space:
                    timer1.Enabled = !timer1.Enabled;
                    break;
                case Keys.M:
                    timer1_Tick(null, null);
                    break;
                case Keys.X:
                    if(e.Modifiers == Keys.Shift)
                    {
                        angleRotation += new Point3D(-1, 0, 0) * delta;


                    }


                        //cubes[0].Rotate(new Point3D(-1, 0, 0) * delta);
                    else
                        angleRotation += new Point3D(1, 0, 0) * delta;
                    break;
                case Keys.Y:
                    if (e.Modifiers == Keys.Shift)
                        angleRotation += new Point3D(0, -1, 0) * delta;
                    else
                        angleRotation += new Point3D(0, 1, 0) * delta;
                    break;
                case Keys.Z:
                    if (e.Modifiers == Keys.Shift)
                        angleRotation += new Point3D(0, 0, -1) * delta;
                    else
                        angleRotation += new Point3D(0, 0, 1) * delta;
                    break;
                case Keys.F:
                    if (e.Modifiers == Keys.Shift)
                        angleRotation += new Point3D(-1, -1, -1) * delta;
                    else
                        angleRotation += new Point3D(1, 1, 1) * delta;
                    break;

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Point3D gravity = new Point3D(0, 1, 0);
            gravity.UnRotate(angleRotation);
            foreach (Ball3D ball in balls)
            {
                ball.Velocity += gravity;
            }

            foreach (Ball3D ball in balls)
                ball.Move();

            for (int i = 0; i < balls.Count; i++)
            {
                for (int j = i+1; j < balls.Count; j++)
                {
                    balls[i].Bounce(balls[j]);
                }
            }

            foreach (Cube cube in cubes)
                foreach (Ball3D ball in balls)
                    cube.Bounce(ball);

            this.Invalidate();

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

        }
    }
}
