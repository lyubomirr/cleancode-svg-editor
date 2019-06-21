using CleanCode.SVGEditor.Model;
using System.Collections.Generic;

namespace CleanCode.SVGEditor.Interfaces
{
    internal interface IFileShapeParser
    {
        string CurrentFile { get; }
        IList<Shape> GetShapes(string filePath);
        void SaveShapes(IList<Shape> shapes);
        void ClearFileName();
    }
}
