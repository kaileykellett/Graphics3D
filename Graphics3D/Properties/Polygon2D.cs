using System;
using System.Collections.Generic;
using System.Drawing;
using Graphics2D;

namespace Graphics2D
{
    enum Face { front = 1, back = 0 }

    class Polygon2D
    {
        #region Class Parameters
        List<Line2D> edges = new List<Line2D>();
        List<Point2D> pts = new List<Point2D>();
        #endregion

        #region Class Constructors
        /// <summary>
        /// Create an empty polygon
        /// </summary>
        public Polygon2D() { }
        #endregion

        #region Class Properties
        public List<Line2D> Edges { get { return edges; } }

        public Face Face
        {
            get
            {
                if (pts.Count < 3) return Face.front;
                //calculate two vectors
                Point2D v1 = pts[1] - pts[0];
                Point2D v2 = pts[2] - pts[0];
                if ((v1 ^ v2) > 0)
                    return Face.front;
                else
                    return Face.back;
            }
        }
        #endregion

        #region Class Methods
        public void Draw(Graphics gr)
        {
            foreach (Line2D edge in edges)
                edge.Draw(gr);
        }

        /// <summary>
        /// Fill the polygon with the desired brush
        /// </summary>
        /// <param name="gr"></param>
        public void Fill(Graphics gr, Brush br)
        {
            PointF[] pointF = new PointF[pts.Count];
            for (int i = 0; i < pts.Count; i++)
                pointF[i] = pts[i].ToPointF();

            gr.FillPolygon(br, pointF);
        }
        /// <summary>
        /// Add a point to the polygon
        /// </summary>
        /// <param name="pt"></param>
        public void AddPt(Point2D pt)
        {
            pts.Add(pt);
            if(pts.Count >= 3)
            {
                edges.Clear();
                for (int i = 0; i < pts.Count; i++)
                    edges.Add(new Line2D(pts[i], pts[(i + 1)%pts.Count]));
            }
        }
        #endregion
    }
}
