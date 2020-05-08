﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using StockAnalyzer.StockLogging;

namespace StockAnalyzer.StockDrawing
{
    [Serializable]
    public class CupHandle2D : Segment2D
    {
        public CupHandle2D()
        {

        }
        public CupHandle2D(PointF point1, PointF point2, Pen pen)
           : base(point1, point2, pen)
        {
        }
        public override void Draw(Graphics g, Pen pen, Matrix matrixValueToScreen, Rectangle2D graphRectangle, bool isLog)
        {
            // Calculate intersection with bounding rectangle
            Segment2D newSegment = (Segment2D)this.Transform(matrixValueToScreen, isLog);
            Segment2D trimmedSegment = newSegment.Trim(graphRectangle);

            if (trimmedSegment.Point1 != PointF.Empty && trimmedSegment.Point2 != PointF.Empty)
            {
                g.DrawLine(this.Pen, trimmedSegment.Point1, trimmedSegment.Point2);
            }
        }
        public override void Draw(Graphics g, Matrix matrixValueToScreen, Rectangle2D graphRectangle, bool isLog)
        {
            this.Draw(g, this.Pen, matrixValueToScreen, graphRectangle, isLog);
        }
    }
}
