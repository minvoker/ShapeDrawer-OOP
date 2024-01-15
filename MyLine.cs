using System;
using System.Net;
using SplashKitSDK;

namespace ShapeDrawer
{
    public class MyLine : Shape
    {
        private int _length;

        public MyLine(Color clr, int length) : base(clr)
        {
            _length = length;
        }

        public MyLine() : this(Color.Black, 100) { }

        public override void Draw()
        {
            if (Selected)
            {
                DrawOutline();
            }
            SplashKit.DrawLine(Color.Black, X, Y, X + _length, Y);
        }

        public override void DrawOutline()
        {
            Point2D startPoint = new Point2D() { X = X, Y = Y };
            Point2D endPoint = new Point2D() { X = X + _length, Y = Y };
            SplashKit.FillCircle(Color.Black, startPoint.X, startPoint.Y, 5);
            SplashKit.FillCircle(Color.Black, endPoint.X, endPoint.Y, 5);
        }

        public override bool IsAt(Point2D p)
        {
            return SplashKit.PointOnLine(p, SplashKit.LineFrom(X, Y, X + _length, Y));
        }

        public override void SaveTo(StreamWriter writer)
        {
            writer.WriteLine("Line");
            base.SaveTo(writer);
            writer.WriteLine(_length);
        }

        public override void LoadFrom(StreamReader reader)
        {
            base.LoadFrom(reader);
            _length = reader.ReadInteger();
        }

    }
}