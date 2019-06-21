using CleanCode.SVGEditor.Interfaces;
using System;
using System.Collections.Generic;

namespace CleanCode.SVGEditor.Commands
{
    internal class EraseCommand : ICommand
    {
        private readonly IShapeContainer _shapeContainer;
        private readonly IWriter _writer;

        public EraseCommand(IShapeContainer shapeContainer, IWriter writer)
        {
            _shapeContainer = shapeContainer;
            _writer = writer;
        }

        public void Execute(IList<string> arguments)
        {
            if (arguments.Count == 1)
            {
                _writer.WriteLine("Specify shape number!");
                return;
            }

            var shapeNumberArg = arguments[1];
            if (int.TryParse(shapeNumberArg, out int result))
            {
                _shapeContainer.EraseShape(result);
            }
            else
            {
                _writer.WriteLine("Specify valid number for shape index!");
            }
        }
    }
}