using CleanCode.SVGEditor.Constants;
using CleanCode.SVGEditor.Interfaces;

namespace CleanCode.SVGEditor.Model
{
    internal abstract class Shape
    {
        public Location Location { get; private set; }
        public string Stroke { get; private set; }
        public int StrokeWidth { get; private set; }

        public virtual void SetAttribute(string attribute, string value)
        {
            switch (attribute)
            {
                case SVGShapeAttributes.Stroke:
                {
                        this.Stroke = value;
                        return;
                }
                case SVGShapeAttributes.StrokeWidth:
                {
                    if (int.TryParse(value, out int result))
                    {
                        this.StrokeWidth = result;
                    }
                    return;
                }
                case SVGShapeAttributes.X:
                {
                    if (int.TryParse(value, out int result))
                    {
                        this.Location.X = result;
                    }
                    return;
                }
                case SVGShapeAttributes.Y:
                {
                    if (int.TryParse(value, out int result))
                    {
                        this.Location.Y = result;
                    }
                    return;
                }
            }
        }

        public virtual void Translate(int dX, int dY)
        {
            this.Location.Translate(dX, dY);   
        }

        public abstract void Print(IWriter writer);
        public abstract string GetTag();
        public abstract bool IsWithin(IContainable shape);
        
    }
}
