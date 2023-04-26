using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Graphics2D;

namespace Graphics3D
{
    class Point3D
    {

        #region Parameters

        #endregion

        #region Constructors

        /// <summary>
        /// Construct a Point3D object of type Point3D
        /// </summary>
        public Point3D () { }

        /// <summary>
        /// construct a new object of type Point3D
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x"></param>
        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        #endregion

        #region Properties

        public double X { get; set; } = 0;
        public double Y { get; set; } = 0;
        public double Z { get; set; } = 0;

        /// <summary>
        /// Calculate the magnitude of the vector of point
        /// </summary>
        public double Magnitude
        {
            get { return Math.Sqrt(X * X + Y * Y + Z*Z); }
        }

        #endregion

        #region Operators

        /// <summary>
        /// Add two points and return their sum
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Point3D operator +(Point3D p1, Point3D p2)
        {
            return new Point3D(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
        }

        /// <summary>
        /// Subtract two points and return their difference
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Point3D operator -(Point3D p1, Point3D p2)
        {
            return new Point3D(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
        }

        /// <summary>
        /// Multiply a point by a double value
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Point3D operator *(Point3D p1, double multiplier)
        {
            return new Point3D(p1.X * multiplier, p1.Y * multiplier, p1.Z* multiplier);
        }
        /// <summary>
        /// Multiply a point by a double value
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Point3D operator *(double multiplier, Point3D p1)
        {
            return new Point3D(p1.X * multiplier, p1.Y * multiplier, p1.Z* multiplier);
        }

        /// <summary>
        /// Divide a point by a double value
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Point3D operator /(Point3D p1, double divisor)
        {
            return new Point3D(p1.X / divisor, p1.Y / divisor, p1.Z/divisor);
        }

        /// <summary>
        /// Multiply a point by a point
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double operator *(Point3D p1, Point3D p2)
        {
            return p1.X * p2.X + p1.Y * p2.Y + p1.Z*p2.Z;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Normalize the point to a unit vector (magnitude = 1)
        /// </summary>
        public void Normalize()
        {
            double magnitude = Magnitude;
            X /= magnitude;
            Y /= magnitude;
            Z /= magnitude;
        }

        /// <summary>
        /// Projects a 3D point onto a 2D surface
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        public Point2D Projection(double distance)
        {
            return new Point2D(X, Y) * distance / (distance - Z);
        }

        /// <summary>
        /// Rotate the point by 3D axis theta
        /// </summary>
        /// <param name="theta"></param>
        public void Rotate(Point3D theta)
        {
            //rotate in the z-axis
            double x2 = (X * Math.Cos(theta.Z)) - (Y * Math.Sin(theta.Z));
            double y2 = (X * Math.Sin(theta.Z)) + (Y * Math.Cos(theta.Z));

            X = x2;
            Y = y2;

            //rotate in the y-axis
            double z2 = (Z * Math.Cos(theta.Y)) - (X * Math.Sin(theta.Y));
            x2 = (Z * Math.Sin(theta.Y)) + (X * Math.Cos(theta.Y));

            Z = z2;
            X = x2;

            //rotate in the x-axis
            y2 = (Y * Math.Cos(theta.X)) - (Z * Math.Sin(theta.X));
            z2 = (Y * Math.Sin(theta.X)) + (Z * Math.Cos(theta.X));

            Y = y2;
            Z = z2;
        }

        public void UnRotate(Point3D theta)
        {
            theta *= -1;
            
            //rotate in the x-axis
            double y2 = (Y * Math.Cos(theta.X)) - (Z * Math.Sin(theta.X));
            double z2 = (Y * Math.Sin(theta.X)) + (Z * Math.Cos(theta.X));

            Y = y2;
            Z = z2;

            //rotate in the y-axis
            z2 = (Z * Math.Cos(theta.Y)) - (X * Math.Sin(theta.Y));
            double x2 = (Z * Math.Sin(theta.Y)) + (X * Math.Cos(theta.Y));

            Z = z2;
            X = x2;

            //rotate in the z-axis
            x2 = (X * Math.Cos(theta.Z)) - (Y * Math.Sin(theta.Z));
            y2 = (X * Math.Sin(theta.Z)) + (Y * Math.Cos(theta.Z));

            X = x2;
            Y = y2;
        }

        /// <summary>
        /// Draw the point to the user interface
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="distance">The distance the user is from the 2D surface</param>
        public void Draw(Graphics gr, double distance)
        {
            // we need to project the 3d point onto a 2D surface
            Projection(distance).Draw(gr, Color.White);
        }
        #endregion
    }
}
