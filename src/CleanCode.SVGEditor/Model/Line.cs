using System;
using System.Text;
using CleanCode.SVGEditor.Constants;
using CleanCode.SVGEditor.Interfaces;

namespace CleanCode.SVGEditor.Model
{
    internal class Line : Shape
    {
        public const string TagName = "line";

        public Line() : base()
        {
            EndLocation = new Location(0, 0);
        }

        public Location EndLocation { get; private set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append($"Line X1: {Location.X}, Y1: {Location.Y}, X2: {EndLocation.X}, Y2: {EndLocation.Y}");

            if (Stroke != null)
            {
                builder.Append($", Stroke: {Stroke}, Stroke Width: {StrokeWidth}");
            }

            return builder.ToString();
        }

        public override string GetTag()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"<{TagName} ");
            builder.Append($"{SVGShapeAttributes.X1}=\"{Location.X}\" ");
            builder.Append($"{SVGShapeAttributes.Y1}=\"{Location.Y}\" ");
            builder.Append($"{SVGShapeAttributes.X2}=\"{EndLocation.X}\" ");
            builder.Append($"{SVGShapeAttributes.Y2}=\"{EndLocation.Y}\" ");

            if (!string.IsNullOrEmpty(Stroke))
            {
                builder.Append($"{SVGShapeAttributes.Stroke}=\"{Stroke}\" ");
                builder.Append($"{SVGShapeAttributes.StrokeWidth}=\"{StrokeWidth}\"");
            }

            builder.Append(" />");
            return builder.ToString();
        }

        public override bool IsWithin(IContainable shape)
        {
            bool isWithin = shape.DoesContainThePoint(Location) && shape.DoesContainThePoint(EndLocation);
            return isWithin;
        }

        protected override string GetXAttribute()
        {
            return SVGShapeAttributes.X1;
        }

        protected override string GetYAttribute()
        {
            return SVGShapeAttributes.Y1;
        }

        public override void SetAttribute(string attribute, string value)
        {
            base.SetAttribute(attribute, value);

            if (int.TryParse(value, out int result))
            {
                switch (attribute)
                {
                    case SVGShapeAttributes.X2:
                    {
                        EndLocation.X = result;
                        return;
                    }

                    case SVGShapeAttributes.Y2:
                    {
                        EndLocation.Y = result;
                        return;
                    }

                }
            }

        }

        public override void Translate(int dX, int dY)
        {
            base.Translate(dX, dY);
            EndLocation.Translate(dX, dY);
        }
    }
}
