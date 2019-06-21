using CleanCode.SVGEditor.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCode.SVGEditor.Commands
{
    class SaveCommand : ICommand
    {
        private readonly IFileShapeParser _fileShapeParser;
        private readonly IShapeContainer _shapeContainer;

        public SaveCommand(IFileShapeParser fileShapeParser,  IShapeContainer shapeContainer)
        {
            _fileShapeParser = fileShapeParser;
            _shapeContainer = shapeContainer;
        }
        public void Execute(IList<string> arguments)
        {
            var currentShapes = _shapeContainer.Shapes;
            _fileShapeParser.SaveShapes(currentShapes);
            _shapeContainer.Shapes.Clear();
        }
    }
}
