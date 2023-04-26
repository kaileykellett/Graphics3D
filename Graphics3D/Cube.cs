using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphics2D;
using System.Drawing;

namespace Graphics3D
{
    enum Side { front = 0, back = 1, right = 2, left = 3, top = 4, bottom = 5 }

    class Cube
    {
        #region Parameters
        List<Point3D> corners = new List<Point3D>();
        List<Line3D> edges = new List<Line3D>();
        List<Polygon3D> faces = new List<Polygon3D>();
        #endregion

        #region Constructors
        /// <summary>
        /// Create a basic cube, centered at the origin, of size 1x1x1
        /// </summary>
        public Cube()
        {
            for (int i = -1; i <=1; i+=2)
                for (int j = -1; j <=1; j+=2)
                    for (int k = -1; k <=1; k+=2)
                        corners.Add(new Point3D(i, j, k));

            for (int i = 0; i < corners.Count; i++)
                for (int j = i+1; j < corners.Count; j++)
                    if ((corners[i] - corners[j]).Magnitude == 2)
                        edges.Add(new Line3D(corners[i], corners[j]));

            Polygon3D face;
            //make the front face
            //counter-clockwise rotation for all of them
            face = new Polygon3D();
            face.AddPt(new Point3D(-1, -1, 1));
            face.AddPt(new Point3D(1, -1, 1));
            face.AddPt(new Point3D(1, 1, 1));
            face.AddPt(new Point3D(-1, 1, 1));
            faces.Add(face);
            face.Visible = false;

            //back face
            face = new Polygon3D();
            face.AddPt(new Point3D(-1, 1, -1));
            face.AddPt(new Point3D(1, 1, -1));
            face.AddPt(new Point3D(1, -1, -1));
            face.AddPt(new Point3D(-1, -1, -1));
            faces.Add(face);

            //right face
            face = new Polygon3D();
            face.AddPt(new Point3D(1, -1, -1));
            face.AddPt(new Point3D(1, 1, -1));
            face.AddPt(new Point3D(1, 1, 1));
            face.AddPt(new Point3D(1, -1, 1));
            faces.Add(face);

            //left face
            face = new Polygon3D();
            face.AddPt(new Point3D(-1, -1, 1));
            face.AddPt(new Point3D(-1, 1, 1));
            face.AddPt(new Point3D(-1, 1, -1));
            face.AddPt(new Point3D(-1, -1, -1));
            faces.Add(face);

            //top face
            face = new Polygon3D();
            face.AddPt(new Point3D(-1, -1, -1));
            face.AddPt(new Point3D(1, -1, -1));
            face.AddPt(new Point3D(1, -1, 1));
            face.AddPt(new Point3D(-1, -1, 1));
            faces.Add(face);
            //face.Visible = false;

            //bottom face
            face = new Polygon3D();
            face.AddPt(new Point3D(-1, 1, 1));
            face.AddPt(new Point3D(1, 1, 1));
            face.AddPt(new Point3D(1, 1, -1));
            face.AddPt(new Point3D(-1, 1, -1));
            faces.Add(face);
        }
        #endregion

        #region Properties
        #endregion

        #region Methods
        public void Scale(double amount)
        {
            for (int i = 0; i < corners.Count; i++)
                corners[i] *= amount;
            for (int i = 0; i < edges.Count; i++)
                edges[i].Scale(amount);
            for (int i = 0; i < faces.Count; i++)
                faces[i].Scale(amount);
        }

        /// <summary>
        /// Rotate the cube by the 3-Dimensional angle theta
        /// </summary>
        /// <param name="theta"></param>
        public void Rotate(Point3D theta)
        {
            for (int i = 0; i < corners.Count; i++)
                corners[i].Rotate(theta);
            for (int i = 0; i < edges.Count; i++)
                edges[i].Rotate(theta);
            foreach (Polygon3D face in faces)
                face.Rotate(theta);
        }

        public void UnRotate(Point3D theta)
        {
            for (int i = 0; i < corners.Count; i++)
                corners[i].UnRotate(theta);
            for (int i = 0; i < edges.Count; i++)
                edges[i].UnRotate(theta);
            foreach (Polygon3D face in faces)
                face.UnRotate(theta);
        }

        public void Draw(Graphics gr, double distance, Face whichFace)
        {
            foreach (Polygon3D face in faces)
                face.Fill(gr, distance, whichFace, true);

        }

        /// <summary>
        /// Bounce the ball off the inside sides of the cube.
        /// </summary>
        /// <param name="ball"></param>
        /// <returns></returns>
        public bool Bounce(Ball3D ball)
        {
            bool didBounce = false;
            //left side bounce
            if(ball.X - ball.Radius < faces[(int)Side.left].Midpoint.X)
            {
                ball.X += 2*(faces[(int)Side.left].Midpoint.X + ball.Radius - ball.X);
                ball.Velocity.X *= -1 * ball.Elasticity;
            }

            //right side bounce
            if (ball.X + ball.Radius > faces[(int)Side.right].Midpoint.X)
            {
                ball.X += 2*(faces[(int)Side.right].Midpoint.X - ball.Radius - ball.X);
                ball.Velocity.X *= -1 * ball.Elasticity;
            }

            //top bounce
            if (ball.Y - ball.Radius < faces[(int)Side.top].Midpoint.Y)
            {
                ball.Y += 2 * (faces[(int)Side.top].Midpoint.Y + ball.Radius - ball.Y);
                ball.Velocity.Y *= -1 * ball.Elasticity;
            }

            //bottom bounce
            if (ball.Y + ball.Radius > faces[(int)Side.bottom].Midpoint.Y)
            {
                ball.Y += 2 * (faces[(int)Side.bottom].Midpoint.Y - ball.Radius - ball.Y);
                ball.Velocity.Y *= -1 * ball.Elasticity;
            }

            //back bounce
            if (ball.Z - ball.Radius < faces[(int)Side.back].Midpoint.Z)
            {
                ball.Z += 2 * (faces[(int)Side.back].Midpoint.Z + ball.Radius - ball.Z);
                ball.Velocity.Z *= -1 * ball.Elasticity;
            }

            //front bounce
            if (ball.Z + ball.Radius > faces[(int)Side.front].Midpoint.Z)
            {
                ball.Z += 2 * (faces[(int)Side.front].Midpoint.Z - ball.Radius - ball.Z);
                ball.Velocity.Z *= -1 * ball.Elasticity;
            }

            return didBounce;
        }
        #endregion
    }
}
