using CleanCode.SVGEditor.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCode.SVGEditor.Interfaces
{
    interface IShapeContainer
    {
        IList<Shape> Shapes { get; set; }
        void PrintShapes();
        void EraseShape(int number);
        void TranslateAll(int dX, int dY);
        void Translate(int dX, int dY, int shapeNumber);
        void CheckWithin(IContainable containableShape);
    }
}
