using CleanCode.SVGEditor.Interfaces;
using CleanCode.SVGEditor.Model;
using System;
using System.Collections.Generic;

namespace CleanCode.SVGEditor.ShapeManagement
{
    class ShapeFactory : IShapeFactory
    {
        public Shape CreateShape(ShapeType type, IDictionary<string, string> properties)
        {
            Shape newShape = null;
            switch (type)
            {
                case ShapeType.Circle:
                    newShape = new Circle();
                    break;
                case ShapeType.Line:
                    newShape = new Line();
                    break;
                case ShapeType.Rectangle:
                    newShape = new Rectangle();
                    break;
            }

            foreach (var property in properties)
            {
                newShape.SetProperty(property.Key, property.Value);
            }

            return newShape;
        }
    }
}
