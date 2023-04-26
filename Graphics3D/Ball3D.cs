using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Graphics2D;

namespace Graphics3D
{
    class Ball3D : Sphere
    {
        #region Parameters
        double mass = 0;
        #endregion

        #region Constructors
        public Ball3D(Point3D center, double radius):base(center, radius)
        {
            mass = radius * radius;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Get/set the mass of the ball
        /// </summary>
        public double Mass
        {
            get { return mass; }
            set { mass = value; }
        }

        /// <summary>
        /// Get/set the friction of the ball
        /// </summary>
        public double Friction { get; set; } = 1;

        public Point3D Acceleration { get; set; } = new Point3D();

        /// <summary>
        /// Get/set the elasticity of the ball
        /// </summary>
        public double Elasticity { get; set; } = 0.8;

        /// <summary>
        /// Get/set the velocity
        /// </summary>
        public Point3D Velocity { get; set; } = new Point3D();

        /// <summary>
        /// Get/set the bursh of the ball
        /// </summary>
        public Brush Brush { get; set; } = new SolidBrush(Color.DarkSlateBlue);
        #endregion

        #region Methods
        public void Draw(Graphics gr, double distance)
        {
            Point2D center = Projection(distance);
            Ball2D ball2D = new Ball2D(center, Radius * distance / (distance - Z));
            ball2D.Draw(gr, Brush);
        }

        public void Move()
        {
            //Velocity += Acceleration;
            X += Velocity.X;
            Y += Velocity.Y;
            Z += Velocity.Z;
            Velocity *= Friction;
        }

        /// <summary>
        /// Bounce this ball off of another ball
        /// </summary>
        /// <param name="otherBall"></param>
        public void Bounce(Ball3D otherBall)
        {
            if (!IsColliding(otherBall))
                return;

            Point3D difference = this - otherBall;
            double distance = difference.Magnitude;
            // mtd = minimum translation distance
            // we fudge the mtd by a small factor 1.1 to force them to move apart by at least slight gap
            Point3D mtd = difference * (this.Radius + otherBall.Radius - distance) / distance * 1.01;

            // get the reciprocal of the masses
            double thisMassReciprocal = 1 / Mass;
            double otherMassReciprocal = 1 / otherBall.Mass;

            // push the balls apart by the minimum translation distance
            Point3D center = mtd * (thisMassReciprocal / (thisMassReciprocal + otherMassReciprocal));
            this.X += center.X;
            this.Y += center.Y;
            this.Z += center.Z;
            Point3D otherBallCenter = mtd * (otherMassReciprocal / (thisMassReciprocal + otherMassReciprocal));
            otherBall.X -= otherBallCenter.X;
            otherBall.Y -= otherBallCenter.Y;
            otherBall.Z -= otherBallCenter.Z;

            // now we "normalize" the mtd to get a unit vector of length 1 in the mtd direction
            mtd.Normalize();

            // impact the velocity due to the collision
            Point3D v = this.Velocity - otherBall.Velocity;
            double vDotMtd = v * mtd;
            if (double.IsNaN(vDotMtd))
                return;
            if (vDotMtd > 0)
                return; // the balls are already moving in opposite direction

            // work the collision effect
            double i = -(1 + Elasticity) * vDotMtd / (thisMassReciprocal + otherMassReciprocal);
            Point3D impulse = mtd * i;

            // change the balls velocities
            this.Velocity += impulse * thisMassReciprocal;
            otherBall.Velocity -= impulse * otherMassReciprocal;
        }

        /// <summary>
        /// Determines if this ball is colliding with the other ball
        /// </summary>
        /// <param name="otherball"></param>
        /// <returns>true if colliding, false otherwise</returns>
        public bool IsColliding(Ball3D otherball)
        {
            return ((this - otherball).Magnitude < this.Radius + otherball.Radius);
        }
        #endregion
    }
}
