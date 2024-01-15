using System;
using SplashKitSDK;
using System.IO;

namespace ShapeDrawer
{
    public class Program
    {
        private enum ShapeKind
        {
            Rectangle,
            Circle,
            Line,
            Triangle
        }

        public static void Main()
        {
            Drawing myDrawing = new Drawing();
            Window window = new Window("Shape Drawer", 800, 600);
            ShapeKind kindtoAdd = ShapeKind.Circle;

            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();

                //Choose Kind
                if (SplashKit.KeyTyped(KeyCode.RKey))
                {
                    kindtoAdd = ShapeKind.Rectangle;
                }
                if (SplashKit.KeyTyped(KeyCode.CKey))
                {
                    kindtoAdd = ShapeKind.Circle;
                }
                if (SplashKit.KeyTyped(KeyCode.LKey))
                {
                    kindtoAdd = ShapeKind.Line;
                }
                if (SplashKit.KeyTyped(KeyCode.TKey))
                {
                    kindtoAdd = ShapeKind.Triangle;
                }

                // Save to File
                if (SplashKit.KeyTyped(KeyCode.SKey))
                {
                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string filePath = Path.Combine(desktopPath, "ShapeDrawing.txt");

                    try
                    {
                        myDrawing.Save(filePath);
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine("Error saving file: {0}", e.Message);
                    }
                }

                // Load From File
                if (SplashKit.KeyTyped(KeyCode.OKey))
                {
                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string filePath = Path.Combine(desktopPath, "ShapeDrawing.txt");

                    if (File.Exists(filePath))
                    {
                        try
                        {
                            myDrawing.Load(filePath);
                        }
                        catch (Exception e)
                        {
                            Console.Error.WriteLine("Error loading file: {0}", e.Message);
                        }
                    }
                    else
                    {
                        Console.Error.WriteLine("File not found: {0}", filePath);
                    }
                }


                // Draw Shape
                if (SplashKit.MouseClicked(MouseButton.LeftButton))
                {
                    Shape newShape;
                    Point2D mousePosition = SplashKit.MousePosition();
                    if (kindtoAdd == ShapeKind.Circle)
                    {
                        MyCircle newCircle = new MyCircle();
                        newShape = newCircle;
                    }
                    else if(kindtoAdd == ShapeKind.Rectangle)
                    {
                        MyRectangle newRect = new MyRectangle();
                        newShape = newRect;
                    }
                    else if (kindtoAdd == ShapeKind.Triangle)
                    {
                        MyTriangle newTri = new MyTriangle();
                        newTri.SetTriangle(mousePosition); // Set the points based on mouse position
                        newShape = newTri;
                    }
                    else
                    {
                        MyLine newLine = new MyLine();
                        newShape = newLine;
                    }

                    newShape.X = (float)mousePosition.X;
                    newShape.Y = (float)mousePosition.Y;
                    myDrawing.AddShape(newShape);
                }

                if (SplashKit.KeyTyped(KeyCode.SpaceKey))
                {
                    myDrawing.Background = Color.RandomRGB(255);
                }

                if (SplashKit.MouseClicked(MouseButton.RightButton))
                {
                    Point2D mousePosition = SplashKit.MousePosition();
                    myDrawing.SelectShapesAt(mousePosition);
                }

                // Delete Shapes
                if (SplashKit.KeyTyped(KeyCode.DeleteKey) || SplashKit.KeyTyped(KeyCode.BackspaceKey))
                {
                    foreach (Shape s in myDrawing.SelectedShapes)
                    {
                        myDrawing.RemoveShape(s);
                    }
                }

                myDrawing.Draw();
                SplashKit.RefreshScreen();
            } while (!window.CloseRequested);
        }
        

    }
}
