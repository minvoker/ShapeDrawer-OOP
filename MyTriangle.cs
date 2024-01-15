using System;
using System.Net;
using System.Runtime.ConstrainedExecution;
using SplashKitSDK;
namespace ShapeDrawer

{
	public class MyTriangle : Shape
    {
        public double X1, X2, X3, Y1, Y2, Y3;

        public MyTriangle(Color clr, double x1, double y1, double x2, double y2, double x3, double y3) : base(clr)
        {
            X1 = x1;
            X2 = x2;
            X3 = x3;
            Y1 = y1;
            Y2 = y2;
            Y3 = y3;
        }

        public MyTriangle() : this(Color.Orange, 0, 0, 0, 0, 5, 100) { }


        public override void Draw()
        {
            if (Selected)
            { DrawOutline();}
            SplashKit.FillTriangle(Color, X1, Y1, X2, Y2, X3, Y3);
        }

        public override void DrawOutline()
        {
            // Calculate the center of the triangle
            double centerX = (X1 + X2 + X3) / 3;
            double centerY = (Y1 + Y2 + Y3) / 3;

            // Calculate new points for the outline, moving each point away from the center
            double outlineX1 = X1 + (X1 - centerX) * 0.05;
            double outlineY1 = Y1 + (Y1 - centerY) * 0.05;
            double outlineX2 = X2 + (X2 - centerX) * 0.05;
            double outlineY2 = Y2 + (Y2 - centerY) * 0.05;
            double outlineX3 = X3 + (X3 - centerX) * 0.05;
            double outlineY3 = Y3 + (Y3 - centerY) * 0.05;

            SplashKit.DrawTriangle(Color.Black, outlineX1, outlineY1, outlineX2, outlineY2, outlineX3, outlineY3);
        }

        public override bool IsAt(Point2D pt) // true if point is in shape
        {
            // Function to check if a point is on the right side of a line
            bool IsOnRightSide(Point2D p, Point2D lineStart, Point2D lineEnd)
            {
                return ((lineEnd.X - lineStart.X) * (p.Y - lineStart.Y) - (lineEnd.Y - lineStart.Y) * (p.X - lineStart.X)) > 0;
            }

            Point2D v1 = new Point2D { X = X1, Y = Y1 };
            Point2D v2 = new Point2D { X = X2, Y = Y2 };
            Point2D v3 = new Point2D { X = X3, Y = Y3 };

            bool b1 = IsOnRightSide(pt, v1, v2);
            bool b2 = IsOnRightSide(pt, v2, v3);
            bool b3 = IsOnRightSide(pt, v3, v1);

            // Check if the point is on the same side of each of the triangle's edges
            return ((b1 == b2) && (b2 == b3));
        }


        public override void SaveTo(StreamWriter writer)
        {
            writer.WriteLine("Triangle");
            base.SaveTo(writer);
            writer.WriteLine(X1);
            writer.WriteLine(Y1);
            writer.WriteLine(X2);
            writer.WriteLine(Y2);
            writer.WriteLine(X3);
            writer.WriteLine(Y3);
        }

        public override void LoadFrom(StreamReader reader)
        {
            base.LoadFrom(reader);
            X1 = reader.ReadDouble();
            Y1 = reader.ReadDouble();
            X2 = reader.ReadDouble();
            Y2 = reader.ReadDouble();
            X3 = reader.ReadDouble();
            Y3 = reader.ReadDouble();
        }

        public void SetTriangle(Point2D mousePosition) //Set the coordinates of Triangle points based on mouse pos 
        { 
            double baseLength = 100; 
            X1 = mousePosition.X;
            Y1 = mousePosition.Y;
            X2 = mousePosition.X + baseLength;
            Y2 = mousePosition.Y;
            X3 = mousePosition.X + (baseLength / 2);
            Y3 = mousePosition.Y - (Math.Sqrt(3) / 2 * baseLength);
        }
    }
}

