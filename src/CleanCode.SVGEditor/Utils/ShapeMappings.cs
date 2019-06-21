using CleanCode.SVGEditor.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCode.SVGEditor.Utils
{
    internal static class ShapeMappings
    {
        public static readonly IDictionary<string, ShapeType> ShapeNameToTypeMap =
            new Dictionary<string, ShapeType>
        {
                { "rectangle", ShapeType.Rectangle },
                { "circle", ShapeType.Circle },
                { "line", ShapeType.Line }
        };

        public static readonly IDictionary<string, ShapeType> TagToShapeTypeMap =
            new Dictionary<string, ShapeType> {
                {"<rect", ShapeType.Rectangle},
                {"<circle", ShapeType.Circle },
                {"<line", ShapeType.Line }
            };
    }
}
