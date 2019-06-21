using System.Collections.Generic;
using CleanCode.SVGEditor.Interfaces;
using CleanCode.SVGEditor.Utils;

namespace CleanCode.SVGEditor.Commands
{
    internal class TranslateCommand : ICommand
    {
        private readonly IWriter _writer;
        private readonly IShapeContainer _shapeContainer;

        public TranslateCommand(IWriter writer, IShapeContainer shapeContainer)
        {
            _writer = writer;
            _shapeContainer = shapeContainer;
        }
        public void Execute(IList<string> arguments)
        {
            int horizontal = 0;
            int vertical = 0;

            var attributes = TextProcessingUtils.SplitAttributes(arguments);
            if(attributes.TryGetValue("horizontal", out string value))
            {
                if (int.TryParse(value, out int parsed))
                {
                    horizontal = parsed;
                }
                else
                {
                    _writer.WriteLine("Invalid number for horizontal translation. Set to 0.");
                }
            }

            if (attributes.TryGetValue("vertical", out value))
            {
                if (int.TryParse(value, out int parsed))
                {
                    vertical = parsed;
                }
                else
                {
                    _writer.WriteLine("Invalid number for vertical translation. Set to 0.");
                }
            }

            if (arguments.Count > 3)
            {
                string shapeIndex = arguments[3];
                if (int.TryParse(shapeIndex, out int parsed))
                {
                    _shapeContainer.Translate(horizontal, vertical, parsed);
                }
                else
                {
                    _writer.WriteLine("Invalid shape number!");
                }

                return;
            }

            _shapeContainer.TranslateAll(horizontal, vertical);
        }
    }
}
