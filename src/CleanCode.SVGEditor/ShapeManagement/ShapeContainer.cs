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
            int number = 1;
            foreach (var shape in Shapes)
            {
                _writer.WriteLine($"{number}. {shape.ToString()}");
            }
        }

        public void EraseShape(int number)
        {
            if (number > Shapes.Count)
            {
                _writer.WriteLine("No shape with this number!");
                return;
            }

            Shapes.RemoveAt(number - 1);
        }

        public void TranslateAll(int dX, int dY)
        {
            foreach(var shape in Shapes)
            {
                shape.Translate(dX, dY);
            }
        }

        public void Translate(int dX, int dY, int shapeNumber)
        {
            if (shapeNumber > Shapes.Count)
            {
                _writer.WriteLine("No shape with this number!");
                return;
            }

            Shapes[shapeNumber - 1].Translate(dX, dY);
        }

        public void CheckWithin(IContainable containableShape)
        {
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
                _writer.Write("No shapes within the entered one!");
            }
        }
    }
}
