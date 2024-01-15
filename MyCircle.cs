using System;
using SplashKitSDK;

namespace ShapeDrawer
{
	public class MyCircle : Shape
	{
		private int _radius;

        public MyCircle() : this(Color.Blue, 50)
        {
        }

        public MyCircle(Color clr, int radius) : base (clr)
        {
            Color = clr;
            _radius = radius;
        }

        public int Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }

        public override void Draw()
        {
            if (Selected)
            { DrawOutline(); }
            SplashKit.FillCircle(Color, X, Y, _radius);
        }

        public override void DrawOutline()
        {
            SplashKit.DrawCircle(Color.Black, X, Y, (_radius + 2));
        }

        public override bool IsAt(Point2D pt)
        {
            double distanceSquared = Math.Pow(pt.X - X, 2) + Math.Pow(pt.Y - Y, 2);
            return distanceSquared <= (_radius * _radius);
        }

        public override void SaveTo(StreamWriter writer)
        {
            writer.WriteLine("Circle");
            base.SaveTo(writer);
            writer.WriteLine(Radius);
          
        }

        public override void LoadFrom(StreamReader reader)
        {
            base.LoadFrom(reader);
            Radius = reader.ReadInteger();
        }

    }
}

