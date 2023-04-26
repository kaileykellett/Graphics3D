using System;
using System.Drawing;
using Graphics2D;

namespace Graphics2D
{
    class Point2D
    {
        #region Class Parameters
        double x = 0;
        double y = 0;
        #endregion

        #region Class Constructors
        /// <summary>
        /// Create a default point (0,0)
        /// </summary>
        public Point2D() { }
        /// <summary>
        /// Create a new 2D point
        /// </summary>
        /// <param name="x">The x-coordinate</param>
        /// <param name="y">The y-coordinate</param>
        public Point2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        #endregion

        #region Class Properties
        /// <summary>
        /// Get/Set the x-value of the point
        /// </summary>
        public double X
        {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        /// Get/Set the y-value of the point
        /// </summary>
        public double Y
        {
            get { return y; }
            set { y = value; }
        }
        /// <summary>
        /// Return the magnitude of a point
        /// </summary>
        public double Magnitude
        {
            get { return Math.Sqrt(x * x + y * y); }
        }

        /// <summary>
        /// Get the "Normal" vector to the point2D
        /// </summary>
        public Point2D Normal
        {
            get { return new Point2D(-Y, X); }
        }

        #endregion

        #region Class Operators
        /// <summary>
        /// Add two points and return their sum
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Point2D operator +(Point2D p1, Point2D p2)
        {
            return new Point2D(p1.x + p2.x, p1.y + p2.y);
        }

        /// <summary>
        /// Subtract two points and return their difference
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Point2D operator -(Point2D p1, Point2D p2)
        {
            return new Point2D(p1.x - p2.x, p1.y - p2.y);
        }

        /// <summary>
        /// Multiply a point by a double value
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Point2D operator *(Point2D p1, double value)
        {
            return new Point2D(p1.x * value, p1.y * value);
        }
        /// <summary>
        /// Multiply a point by a double value
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Point2D operator *(double value, Point2D p1)
        {
            return new Point2D(p1.x * value, p1.y * value);
        }

        /// <summary>
        /// Divide a point by a double value
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Point2D operator /(Point2D p1, double value)
        {
            return new Point2D(p1.x / value, p1.y / value);
        }

        /// <summary>
        /// Multiply a point by a point
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double operator *(Point2D p1, Point2D p2)
        {
            return p1.x * p2.x + p1.y * p2.y;
        }

        /// <summary>
        /// Determine the cross-product of two points.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns>The resulting cross product is returned</returns>
        public static double operator ^(Point2D p1, Point2D p2)
        {
            return p1.X * p2.Y - p1.Y * p2.X;
        }
        #endregion

        #region Class Methods
        /// <summary>
        /// convert the point2d to a pointf
        /// </summary>
        /// <returns></returns>
        public PointF ToPointF()
        {
            return new PointF((float)x, (float)y);
        }
        /// <summary>
        /// Normalize the point to a unit vector (magnitude = 1)
        /// </summary>
        public void Normalize()
        {
            double magnitude = Magnitude;
            x /= magnitude;
            y /= magnitude;
        }

        public void Rotate (double theta)
        {
            double xNew = (x * Math.Cos(theta)) - (y * Math.Sin(theta));
            double yNew = (y * Math.Cos(theta)) + (x * Math.Sin(theta));

            x = xNew;
            y = yNew;

            //x2 = rcos(a+b)
            //=r(cosacosb - sinasinb)
            //=rcosacosb - rsinasinb
            //xnew =xcosb - ysinb

            //y2 = rsin(a+b)
            // = r(sinacosb + cosasinb)
            // = rsinacosb + rcosasinb
            //ynew = ycosb + xsinb
        }

        /// <summary>
        /// Draw the point on the graphics device with the specified color
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="color"></param>
        public void Draw(Graphics gr, Color color)
        {
            gr.FillEllipse(new SolidBrush(color), (float)x - 1, (float)y - 1, 2, 2);
        }
        #endregion


    }
}
