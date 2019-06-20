using System.Text;
using CleanCode.SVGEditor.Constants;
using CleanCode.SVGEditor.Interfaces;

namespace CleanCode.SVGEditor.Model
{
    internal class Rectangle : Shape, IContainable
    {
        public const string TagName = "rect";

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
            builder.Append($"{SVGShapeAttributes.X}=\"{this.Location.X}\" ");
            builder.Append($"{SVGShapeAttributes.Y}=\"{this.Location.Y}\" ");
            builder.Append($"{SVGShapeAttributes.Width}=\"{this.Width}\" ");
            builder.Append($"{SVGShapeAttributes.Height}=\"{this.Height}\" ");
            builder.Append($"{SVGShapeAttributes.Fill}=\"{this.Fill ?? "black"}\" ");

            if (this.Stroke != null)
            {
                builder.Append($"{SVGShapeAttributes.Stroke}=\"{this.Stroke}\" ");
                builder.Append($"{SVGShapeAttributes.StrokeWidth}=\"{this.StrokeWidth}\"");
            }

            builder.Append($" />");
            return builder.ToString();
        }

        public override bool IsWithin(IContainable shape)
        {
            var topLeft = GetBottomLeftPoint();
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

        public override void Print(IWriter writer)
        {
            writer.Write($@"Rectangle X: {Location.X}, Y: {Location.Y}, Width: {Width}, 
                Height: {Height}, Fill: {Fill}");

            if (Stroke != null)
            {
                writer.WriteLine($", Stroke: {Stroke}, Stroke Width: {StrokeWidth}");
            }
        }

        public override void SetAttribute(string attribute, string value)
        {
            base.SetAttribute(attribute, value);

            switch (attribute)
            {
                case SVGShapeAttributes.Fill:
                {
                    this.Fill = value;
                    return;
                }
                case SVGShapeAttributes.Width:
                {
                    if (int.TryParse(value, out int result))
                    {
                        this.Width = result;
                    }
                    return;
                }
                case SVGShapeAttributes.Height:
                {
                    if (int.TryParse(value, out int result))
                    {
                        this.Height = result;
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
            var topRight = new Location(this.Location.X + this.Width, this.Location.Y);
            return topRight;
        }

        private Location GetBottomLeftPoint()
        {
            var bottomLeft = new Location(this.Location.X, this.Location.Y + this.Height);
            return bottomLeft;
        }

        private Location GetBottomRightPoint()
        {
            var bottomRight = new Location(this.Location.X + this.Width, this.Location.Y + this.Height);
            return bottomRight;
        }
    }
}
