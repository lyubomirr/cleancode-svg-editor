namespace CleanCode.SVGEditor.Model
{
    internal class Location
    {
        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public void Translate(int dX, int dY)
        {
            X += dX;
            Y += dY;
        }
    }
}
