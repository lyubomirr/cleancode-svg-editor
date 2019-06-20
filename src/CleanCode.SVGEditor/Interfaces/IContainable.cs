using CleanCode.SVGEditor.Model;

namespace CleanCode.SVGEditor.Interfaces
{
    internal interface IContainable
    {
        bool DoesContainThePoint(Location coordinates);
    }
}
