﻿using CleanCode.SVGEditor.Constants;
using CleanCode.SVGEditor.Interfaces;

namespace CleanCode.SVGEditor.Model
{
    internal abstract class Shape
    {
        protected Shape()
        {
            Location = new Location(0, 0);
            Stroke = string.Empty;
        }

        public Location Location { get; private set; }
        public string Stroke { get; private set; }
        public int StrokeWidth { get; private set; }
        
        public virtual void SetProperty(string svgProperty, string value)
        {
            //X and Y attributes are different for different shapes so we get the one we need and reuse the code.
            string xAttributeForElement = GetXAttribute();
            string yAttributeForElement = GetYAttribute();
            
            if (svgProperty.Equals(xAttributeForElement))
            {
                if (int.TryParse(value, out int result))
                {
                    Location.X = result;
                }
                return;
            }

            if (svgProperty.Equals(yAttributeForElement))
            {
                if (int.TryParse(value, out int result))
                {
                    Location.Y = result;
                }
                return;
            }

            switch (svgProperty)
            {
                case SVGShapeAttributes.Stroke:
                {
                        Stroke = value;
                        return;
                }
                case SVGShapeAttributes.StrokeWidth:
                {
                    if (int.TryParse(value, out int result))
                    {
                        StrokeWidth = result;
                    }
                    return;
                }
            }
        }
        public virtual void Translate(int dX, int dY)
        {
            Location.Translate(dX, dY);   
        }

        public abstract string GetTag();
        public abstract bool IsWithin(IContainable shape);


        //X and Y location attributes have different names for the different shapes.
        protected abstract string GetXAttribute();
        protected abstract string GetYAttribute();

    }
}
