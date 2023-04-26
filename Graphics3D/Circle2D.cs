using System;
using System.Drawing;
using Graphics2D;

namespace Graphics2D
{
    class Circle2D : Point2D
    {
        #region Class Parameters
        double radius = 0; //the radius of the circle
        #endregion

        #region Class Constructors
        /// <summary>
        /// Create a default circle, at (0,0) with 0 radius
        /// </summary>
        public Circle2D() { }
        /// <summary>
        /// Construct a circle with a specified center and radius
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        public Circle2D(Point2D center, double radius)
        {
            X = center.X;
            Y = center.Y;
            this.radius = radius;
        }

        /// <summary>
        /// Construct a circle with specified x,y,radius values
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radius"></param>
        public Circle2D(double x, double y, double radius)
        {
            X = x;
            Y = y;
            this.radius = radius;
        }
        #endregion

        #region Class Properties
        /// <summary>
        /// Get/Set the center of the circle
        /// </summary>
        public Point2D Center
        {
            get { return this; }
            set { X = value.X; Y = value.Y; }
        }
        /// <summary>
        /// Get/Set the radius of the circle
        /// </summary>
        public double Radius
        {
            get { return radius; }
            set { radius = value; }
        }
        #endregion

        #region Class Methods
        /// <summary>
        /// Draw the circle to the graphics device with this specified color
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="color"></param>
        public void Draw(Graphics gr, Color color)
        {
            gr.DrawEllipse(new Pen(color), (float)(X - radius), (float)(Y - radius), (float)(2 * radius), (float)(2 * radius));
        }
        /// <summary>
        /// Draw the circle to the graphics device with this specified pen
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="color"></param>
        public void Draw(Graphics gr, Pen pen)
        {
            gr.DrawEllipse(pen, (float)(X - radius), (float)(Y - radius), (float)(2 * radius), (float)(2 * radius));
        }
        /// <summary>
        /// Fill the circle to the graphics device with this specified color
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="color"></param>
        public void Fill(Graphics gr, Color color)
        {
            gr.FillEllipse(new SolidBrush(color), (float)(X - radius), (float)(Y - radius), (float)(2 * radius), (float)(2 * radius));
        }
        /// <summary>
        /// Fill the circle to the graphics device with this specified brush
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="color"></param>
        public void Fill(Graphics gr, Brush brush)
        {
            gr.FillEllipse(brush, (float)(X - radius), (float)(Y - radius), (float)(2 * radius), (float)(2 * radius));
        }
        #endregion
    }
}
