using System;
using SplashKitSDK;
using System.Collections.Generic;
using System.IO;
namespace ShapeDrawer
{
	public class Drawing
	{
		private readonly List<Shape> _shapes;
		private Color _background;
        

        public Drawing(Color background)
		{
			_shapes = new List<Shape>();
			_background = background;

		}

        public Drawing() : this(Color.White) { }

        public List<Shape> SelectedShapes
        {
            get
            {
                List<Shape> result = new List<Shape>();
                foreach (Shape s in _shapes)
                {
                    if (s.Selected)
                    {
                        result.Add(s);
                    }
                }
                return result;
            }
        }

        public int ShapeCount
		{
			get { return _shapes.Count; }
		}

        public Color Background
        {
            get { return _background; }
            set { _background = value; }
        }

        public void Draw()
		{
            SplashKit.ClearScreen(Background);
			foreach (Shape s in _shapes)
			{
				s.Draw();
			}
        }

        public void SelectShapesAt(Point2D pt)
        {
            foreach (Shape s in _shapes)
            {
                if (s.IsAt(pt))
                {
                    s.Selected = !s.Selected;  // Toggle the selection
                }
            }
        }

        public void AddShape(Shape s)
        {
            _shapes.Add(s);
        }

        public void RemoveShape(Shape s)
		{
            _shapes.Remove(s);
        }

        public void Save(string filename)
        {
            StreamWriter writer = new StreamWriter(filename);
            writer.WriteColor(Background);
            writer.WriteLine(ShapeCount);
            try
            {
                foreach (Shape s in _shapes)
                {
                    s.SaveTo(writer);
                }
            } finally
            {
                writer.Close();
            }
        }

        public void Load(string filename)
        {
            Shape s;
            StreamReader reader = new StreamReader(filename);
            Background = reader.ReadColor();
            int count = reader.ReadInteger();
            _shapes.Clear();
            try
            {
                for (int i = 0; i < count; i++)
                {
                    string kind = reader.ReadLine();
                    switch (kind)
                    {
                        case "Rectangle":
                            s = new MyRectangle();
                            break;
                        case "Circle":
                            s = new MyCircle();
                            break;
                        case "Line":
                            s = new MyLine();
                            break;
                        case "Triangle":
                            s = new MyTriangle();
                            break;
                        default:
                            throw new InvalidDataException("Unknown shape kind: " + kind);
                    }

                    s.LoadFrom(reader);
                    AddShape(s);
                }
            } finally
            {
                reader.Close();
            }
        }
    }
}

