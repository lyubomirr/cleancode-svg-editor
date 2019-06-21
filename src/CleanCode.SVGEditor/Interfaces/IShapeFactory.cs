using CleanCode.SVGEditor.Model;
using System.Collections.Generic;

namespace CleanCode.SVGEditor.Interfaces
{
    internal interface IShapeFactory
    {
        Shape CreateShape(IDictionary<string, string> commandLineArguments);
    }
}