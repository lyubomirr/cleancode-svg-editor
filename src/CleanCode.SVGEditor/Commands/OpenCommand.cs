using CleanCode.SVGEditor.Interfaces;
using CleanCode.SVGEditor.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CleanCode.SVGEditor.Commands
{
    internal class OpenCommand : ICommand
    {
        private readonly IFileShapeParser _shapeParser;
        private readonly IShapeContainer _shapeContainer;
        private readonly IWriter _writer;

        public OpenCommand(IFileShapeParser shapeParser, 
            IShapeContainer shapeContainer, IWriter writer)
        {
            _shapeParser = shapeParser;
            _shapeContainer = shapeContainer;
            _writer = writer;
        }

        public void Execute(IList<string> arguments)
        {
            if (arguments.Count > 1)
            {
                //we get the first argument after the command as the filename.
                var filename = arguments[1];
                IList<Shape> shapes = new List<Shape>();
                try
                {
                    shapes = _shapeParser.GetShapes(filename);
                }
                catch (FileNotFoundException ex)
                {
                    _writer.WriteLine(ex.Message);
                    return;
                }
                catch (FileLoadException ex)
                {
                    _writer.WriteLine(ex.Message);
                    return;
                }


                _shapeContainer.Shapes = shapes;
                _writer.WriteLine($"Successfully loaded shapes from file {filename}.");
            }
            else
            {
                _writer.WriteLine("Please specify filename!");
            }
        }
    }
}
