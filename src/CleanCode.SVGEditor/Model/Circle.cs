using System;
using System.Text;
using CleanCode.SVGEditor.Constants;
using CleanCode.SVGEditor.Interfaces;

namespace CleanCode.SVGEditor.Model
{
    internal class Circle : Shape, IContainable
    {
        public const string TagName = "circle";

        public Circle() : base()
        {
            Fill = string.Empty;
        }

        public int Radius { get; private set; }
        public string Fill { get; private set; } 


        public override string GetTag()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"<{TagName} ");
            builder.Append($"{SVGShapeAttributes.Cx}=\"{Location.X}\" ");
            builder.Append($"{SVGShapeAttributes.Cy}=\"{Location.Y}\" ");
            builder.Append($"{SVGShapeAttributes.Radius}=\"{Radius}\" ");
            builder.Append($"{SVGShapeAttributes.Fill}=\"{Fill ?? "black"}\" ");

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
            var leftTip = new Location(Location.X - Radius, Location.Y);
            var rightTip = new Location(Location.X + Radius, Location.Y);
            var topTip = new Location(Location.X, Location.Y + Radius);
            var bottomTip = new Location(Location.X,  Location.Y - Radius);

            bool isWithin =
            (
                shape.DoesContainThePoint(leftTip)
                && shape.DoesContainThePoint(rightTip)
                && shape.DoesContainThePoint(topTip)
                && shape.DoesContainThePoint(bottomTip)
            );

            return isWithin;
        }

        protected override string GetXAttribute()
        {
            return SVGShapeAttributes.Cx;
        }

        protected override string GetYAttribute()
        {
            return SVGShapeAttributes.Cy;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"Circle X: {Location.X}, Y: {Location.Y}, Radius: {Radius}, Fill: {Fill}");

            if (!string.IsNullOrEmpty(Stroke))
            {
                builder.Append($", Stroke: {Stroke}, Stroke Width: {StrokeWidth}");
            }

            return builder.ToString();
        }

        public bool DoesContainThePoint(Location coordinates)
        {
            bool doesContain = (GetDistanceToCenter(coordinates) <= Radius);
            return doesContain;
        }

        public override void SetProperty(string attribute, string value)
        {
            base.SetProperty(attribute, value);

            switch (attribute)
            {
                case SVGShapeAttributes.Fill:
                {
                    Fill = value;
                    return;
                }
                case SVGShapeAttributes.Cx:
                {
                    if (int.TryParse(value, out int result))
                    {
                        Location.X = result;
                    }
                    return;
                }
                case SVGShapeAttributes.Cy:
                {
                    if (int.TryParse(value, out int result))
                    {
                        Location.Y = result;
                    }
                    return;
                }

                case SVGShapeAttributes.Radius:
                {
                    if (int.TryParse(value, out int result))
                    {
                        Radius = result > 0 ? result : 0; //Negative radius is an error.
                    }
                    return;
                }
            }
        }

        private double GetDistanceToCenter(Location point)
        {
            int xDelta = Location.X - point.X;
            int yDelta = Location.Y - point.Y;

            double distance = Math.Sqrt(
                (xDelta * xDelta) + (yDelta * yDelta)
            );

            return distance;
        }
    }
}
