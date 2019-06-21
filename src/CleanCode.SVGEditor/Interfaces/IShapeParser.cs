using CleanCode.SVGEditor.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanCode.SVGEditor.Interfaces
{
    internal interface IShapeParser
    {
        Task<IList<Shape>> GetShapesAsync(string filePath);
        void SaveShapes();
    }
}
