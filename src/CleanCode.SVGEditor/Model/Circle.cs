using System;
using System.Collections.Generic;
using System.Text;
using CleanCode.SVGEditor.Interfaces;

namespace CleanCode.SVGEditor.Model
{
    internal class Circle : Shape
    {
        public int Radius { get; private set; }
        public string Fill { get; private set; }

        public override string GetTag()
        {
            throw new NotImplementedException();
        }

        public override bool IsWithin(IContainable shape)
        {
            throw new NotImplementedException();
        }

        public override void Print(IWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
