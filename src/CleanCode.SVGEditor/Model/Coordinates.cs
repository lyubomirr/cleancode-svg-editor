using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCode.SVGEditor.Model
{
    internal class Location
    {
        public Location(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public void Translate(int dX, int dY)
        {
            this.X += dX;
            this.Y += dY;
        }
    }
}
