using CleanCode.SVGEditor.Interfaces;
using System;
using System.Collections.Generic;

namespace CleanCode.SVGEditor.Commands
{
    internal class CloseCommand : ICommand
    {
        private readonly IFileShapeParser _shapeParser;
        private readonly IWriter _writer;
        private readonly IShapeContainer _shapeContainer;

        public CloseCommand(IFileShapeParser shapeParser, IWriter writer, IShapeContainer shapeContainer)
        {
            _shapeParser = shapeParser;
            _writer = writer;
            _shapeContainer = shapeContainer;
        }

        public void Execute(IList<string> arguments)
        {
            if (string.IsNullOrEmpty(_shapeParser.CurrentFile))
            {
                _writer.WriteLine("No file has been read!");
                return;
            }

            _shapeContainer.Shapes.Clear();
            _shapeParser.ClearFileName();
            _writer.WriteLine("Successfully closed file!");
        }
    }
}
