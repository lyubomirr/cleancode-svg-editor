using CleanCode.SVGEditor.Interfaces;
using CleanCode.SVGEditor.Model;
using CleanCode.SVGEditor.Utils;
using System;
using System.Collections.Generic;

namespace CleanCode.SVGEditor.Commands
{
    internal class WithinCommand : ICommand
    {
        private readonly IWriter _writer;
        private readonly IShapeContainer _shapeContainer;
        private readonly IShapeFactory _shapeFactory;

        public WithinCommand(
            IWriter writer, 
            IShapeContainer shapeContainer,
            IShapeFactory shapeFactory)
        {
            _writer = writer;
            _shapeContainer = shapeContainer;
            _shapeFactory = shapeFactory;
        }

        public void Execute(IList<string> arguments)
        {
            if (arguments.Count == 1)
            {
                _writer.WriteLine("Specify shape!");
                return;
            }

            var containableShape = arguments[1];
            if (ShapeMappings.ShapeNameToTypeMap.TryGetValue(containableShape, out ShapeType type))
            {
                var shapeAttributes = TextProcessingUtils.SplitAttributes(arguments);
                Shape newShape  = _shapeFactory.CreateShape(type, shapeAttributes);
                if (newShape is IContainable)
                {
                    var parsedShape = newShape as IContainable;
                    _shapeContainer.CheckWithin(parsedShape);
                }
                else
                {
                    _writer.WriteLine("Can't use this shape!");
                }
            }
            else
            {
                _writer.WriteLine("Shape not supported!");
            }            
        }
    }
}
