using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics3D
{
    class Sphere : Point3D
    {
        #region Parameters
        #endregion

        #region Constructors
        /// <summary>
        /// Create a default sphere, at (0,0) with 0 radius
        /// </summary>
        public Sphere() { }
        /// <summary>
        /// Construct a sphere with a specified center and radius
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        public Sphere(Point3D center, double radius)
        {
            X = center.X;
            Y = center.Y;
            Z = center.Z;
            Radius = radius;
        }

        /// <summary>
        /// Construct a sphere with an x, y, and z value and radius
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="radius"></param>
        public Sphere(double x, double y, double z, double radius)
        {
            X = x;
            Y = y;
            Z = z;
            Radius = radius;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Get/Set the radius of the circle
        /// </summary>
        public double Radius{ get; set; } = 0;

        public Point3D Center
        {
            get { return this; }
            set { X = value.X; Y = value.Y; Z = value.Z; }
        }
        #endregion

        #region Methods

        #endregion
    }
}
