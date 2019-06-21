using System.Text;
using CleanCode.SVGEditor.Constants;
using CleanCode.SVGEditor.Interfaces;

namespace CleanCode.SVGEditor.Model
{
    internal class Rectangle : Shape, IContainable
    {
        public const string TagName = "rect";

        public Rectangle() : base()
        {
            Fill = string.Empty;
        }

        public int Width { get; private set; }
        public int Height { get; private set; }
        public string Fill { get; set; }       

        public bool DoesContainThePoint(Location coordinates)
        {
            var topLeft = GetBottomLeftPoint();
            var bottomRight = GetBottomRightPoint();

            bool doesContain = (topLeft.X <= coordinates.X && coordinates.X <= bottomRight.X)
                && (topLeft.Y <= coordinates.Y && coordinates.Y <= bottomRight.Y);

            return doesContain;
        }

        public override string GetTag()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"<{TagName} ");
            builder.Append($"{SVGShapeAttributes.X}=\"{Location.X}\" ");
            builder.Append($"{SVGShapeAttributes.Y}=\"{Location.Y}\" ");
            builder.Append($"{SVGShapeAttributes.Width}=\"{Width}\" ");
            builder.Append($"{SVGShapeAttributes.Height}=\"{Height}\" ");
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
            var topLeft = GetTopLeftPoint();
            var topRight = GetTopRightPoint();
            var bottomLeft = GetBottomLeftPoint();
            var bottomRight = GetBottomRightPoint();

            bool isWithin =
                (
                    shape.DoesContainThePoint(topLeft)
                    && shape.DoesContainThePoint(topRight)
                    && shape.DoesContainThePoint(bottomLeft)
                    && shape.DoesContainThePoint(bottomRight)
                );

            return isWithin;
        }

        protected override string GetXAttribute()
        {
            return SVGShapeAttributes.X;
        }

        protected override string GetYAttribute()
        {
            return SVGShapeAttributes.Y;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append($"Rectangle X: {Location.X}, Y: {Location.Y}, Width: {Width}, " +
                $"Height: {Height}, Fill: {Fill}");

            if (!string.IsNullOrEmpty(Stroke))
            {
                builder.Append($", Stroke: {Stroke}, Stroke Width: {StrokeWidth}");
            }

            return builder.ToString();
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
                case SVGShapeAttributes.Width:
                {
                    if (int.TryParse(value, out int result))
                    {
                        Width = result;
                    }
                    return;
                }
                case SVGShapeAttributes.Height:
                {
                    if (int.TryParse(value, out int result))
                    {
                        Height = result;
                    }
                    return;
                }
            }
        }

        private Location GetTopLeftPoint()
        {
            return Location;
        }

        private Location GetTopRightPoint()
        {
            var topRight = new Location(Location.X + Width, Location.Y);
            return topRight;
        }

        private Location GetBottomLeftPoint()
        {
            var bottomLeft = new Location(Location.X, Location.Y + Height);
            return bottomLeft;
        }

        private Location GetBottomRightPoint()
        {
            var bottomRight = new Location(Location.X + Width, Location.Y + Height);
            return bottomRight;
        }
    }
}
