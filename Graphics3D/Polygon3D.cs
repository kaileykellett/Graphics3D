using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphics2D;
using System.Drawing;

namespace Graphics3D
{
    class Polygon3D
    {
        #region Class Parameters
        List<Line3D> edges = new List<Line3D>();
        List<Point3D> pts = new List<Point3D>();
        Brush brFront = Brushes.DeepSkyBlue;
        Brush brBack = Brushes.LightSkyBlue;
        #endregion

        #region Class Constructors
        /// <summary>
        /// Create an empty polygon
        /// </summary>
        public Polygon3D() { }
        #endregion

        #region Class Properties
        public List<Line3D> Edges { get { return edges; } }

        /// <summary>
        /// Get/set the visibility of the polygon
        /// </summary>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// Get the midpoint of the polygon
        /// </summary>
        public Point3D Midpoint
        {
            get
            {
                Point3D temp = new Point3D();
                foreach (Point3D p in pts)
                    temp += p;
                return temp / pts.Count;
            }
        }
        #endregion

        #region Class Methods

        public void Scale(double amount)
        {
            for (int i = 0; i < pts.Count; i++)
                pts[i] *= amount;
            for (int i = 0; i < edges.Count; i++)
                edges[i].Scale(amount);
        }
        /// <summary>
        /// Draw the polygon outline
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="distance"></param>
        public void Draw(Graphics gr, double distance)
        {
            if (!Visible) return;
            foreach (Line3D edge in edges)
                edge.Draw(gr, distance);

            Polygon2D poly2D = new Polygon2D();
            foreach (Point3D pt in pts)
                poly2D.AddPt(pt.Projection(distance));
            poly2D.Draw(gr);
        }

        /// <summary>
        /// Fill the polygon with the brush
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="distance"></param>
        public void Fill(Graphics gr, double distance, Face whichFace, bool showOutline = false)
        {
            if (!Visible) return;
            Polygon2D poly2D = new Polygon2D();
            foreach (Point3D pt in pts)
                poly2D.AddPt(pt.Projection(distance));

            if (poly2D.Face != whichFace)
                return;

            if (poly2D.Face == Face.front)
            {
                //poly2D.Fill(gr, brFront);
                if (showOutline)
                    poly2D.Draw(gr);
            }

            else
            {
                //poly2D.Fill(gr, brBack);
                if (showOutline)
                    poly2D.Draw(gr);
            }

        }
        /// <summary>
        /// Add a point to the polygon
        /// </summary>
        /// <param name="pt"></param>
        public void AddPt(Point3D pt)
        {
            pts.Add(pt);
            if (pts.Count >= 3)
            {
                edges.Clear();
                for (int i = 0; i < pts.Count; i++)
                    edges.Add(new Line3D(pts[i], pts[(i + 1) % pts.Count]));
            }
        }

        /// <summary>
        /// Rotate the polygon by a Point3D angle
        /// </summary>
        /// <param name="theta"></param>
        public void Rotate(Point3D theta)
        {
            foreach (Point3D point in pts)
                point.Rotate(theta);
            foreach (Line3D edge in edges)
                edge.Rotate(theta);
        }

        /// <summary>
        /// Unrotate the polygon by a Point3D angle
        /// </summary>
        /// <param name="theta"></param>
        public void UnRotate(Point3D theta)
        {
            foreach (Point3D point in pts)
                point.UnRotate(theta);
            foreach (Line3D edge in edges)
                edge.UnRotate(theta);
        }
        #endregion
    }
}
