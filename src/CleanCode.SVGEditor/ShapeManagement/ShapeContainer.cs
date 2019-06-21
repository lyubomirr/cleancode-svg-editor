using CleanCode.SVGEditor.Interfaces;
using CleanCode.SVGEditor.Model;
using System.Collections.Generic;

namespace CleanCode.SVGEditor.ShapeManagement
{
    internal class ShapeContainer : IShapeContainer
    {
        private readonly IWriter _writer;
        
        public ShapeContainer(IWriter writer)
        {
            _writer = writer;
            Shapes = new List<Shape>();
        }

        public IList<Shape> Shapes { get; set; }

        public void PrintShapes()
        {
            if (Shapes.Count == 0)
            {
                _writer.WriteLine("No shapes loaded!");
                return;
            }

            int number = 1;
            foreach (var shape in Shapes)
            {
                _writer.WriteLine($"{number}. {shape.ToString()}");
                number++;
            }
        }

        public void EraseShape(int number)
        {
            if (Shapes.Count == 0)
            {
                _writer.WriteLine("No shapes loaded!");
                return;
            }

            if (number > Shapes.Count)
            {
                _writer.WriteLine("No shape with this number!");
                return;
            }

            Shapes.RemoveAt(number - 1);
            _writer.WriteLine($"Erased shape with number {number}.");

        }

        public void TranslateAll(int dX, int dY)
        {
            if (Shapes.Count == 0)
            {
                _writer.WriteLine("No shapes loaded! ");
                return;
            }


            foreach(var shape in Shapes)
            {
                shape.Translate(dX, dY);
            }

            _writer.WriteLine("Translated all figures.");
        }

        public void Translate(int dX, int dY, int shapeNumber)
        {
            if (shapeNumber > Shapes.Count || Shapes.Count == 0)
            {
                _writer.WriteLine("No shape with this number!");
                return;
            }

            Shapes[shapeNumber - 1].Translate(dX, dY);
            _writer.WriteLine($"Shape {shapeNumber} translated.");
        }

        public void CheckWithin(IContainable containableShape)
        {
            if (Shapes.Count == 0)
            {
                _writer.WriteLine("No shapes loaded!");
                return;
            }

            bool isThereWithin = false;

            foreach (var shape in Shapes)
            {
                if (shape.IsWithin(containableShape))
                {
                    _writer.WriteLine(shape.ToString());
                    isThereWithin = true;
                }
            }

            if (!isThereWithin)
            {
                _writer.WriteLine("No shapes within the entered one!");
            }
        }
    }
}
