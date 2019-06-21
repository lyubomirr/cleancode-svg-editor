using System.Collections.Generic;
using CleanCode.SVGEditor.Interfaces;
using CleanCode.SVGEditor.Model;
using CleanCode.SVGEditor.Utils;

namespace CleanCode.SVGEditor.Commands
{
    class CreateCommand : ICommand
    {
        private readonly IFileShapeParser _shapeParser;
        private readonly IShapeContainer _shapeContainer;
        private readonly IWriter _writer;
        private readonly IShapeFactory _shapeFactory;

        public CreateCommand(
            IFileShapeParser shapeParser, 
            IShapeContainer shapeContainer,
            IWriter writer,
            IShapeFactory shapeFactory)
        {
            _shapeParser = shapeParser;
            _shapeContainer = shapeContainer;
            _writer = writer;
            _shapeFactory = shapeFactory;
        }
        public void Execute(IList<string> arguments)
        {
            if (string.IsNullOrEmpty(_shapeParser.CurrentFile))
            {
                _writer.WriteLine("No file loaded!");
                return;
            }

            if (arguments.Count == 1)
            {
                _writer.WriteLine("Specify figure type!");
                return;
            }

            var shapeTypeArgument = arguments[1];

            if (ShapeMappings.ShapeNameToTypeMap.TryGetValue(shapeTypeArgument, out ShapeType reslovedShapeType))
            {
                var shapeAttributes = TextProcessingUtils.SplitAttributes(arguments);

                Shape newShape = _shapeFactory.CreateShape(reslovedShapeType, shapeAttributes);
                _shapeContainer.Shapes.Add(newShape);
                _writer.WriteLine("Successfully created shape!");
            }
            else
            {
                _writer.WriteLine("No such shape supported!");
            }
        }
    }
}
