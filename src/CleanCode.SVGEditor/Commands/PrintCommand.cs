using CleanCode.SVGEditor.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCode.SVGEditor.Commands
{
    internal class PrintCommand : ICommand
    {
        private readonly IShapeContainer _shapeContainer;

        public PrintCommand(IShapeContainer shapeContainer)
        {
            _shapeContainer = shapeContainer;
        }

        public void Execute(IList<string> arguments)
        {
            _shapeContainer.PrintShapes();
        }
    }
}
