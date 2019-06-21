using CleanCode.SVGEditor.Model;
using System.Collections.Generic;

namespace CleanCode.SVGEditor.Interfaces
{
    internal interface IShapeFactory
    {
        Shape CreateShape(ShapeType type, IDictionary<string, string> properties);
    }
}