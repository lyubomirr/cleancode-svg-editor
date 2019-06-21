using CleanCode.SVGEditor.Interfaces;
using CleanCode.SVGEditor.Model;
using CleanCode.SVGEditor.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.SVGEditor.ShapeManagement
{
    internal class FileShapeParser : IFileShapeParser
    {
        private IShapeFactory _shapeFactory;
        private IWriter _writer;

        public FileShapeParser(IShapeFactory shapeFactory, IWriter writer)
        {
            _shapeFactory = shapeFactory;
            _writer = writer;
        }

        public string CurrentFile { get; private set; }

        public void ClearFileName()
        {
            CurrentFile = string.Empty;
        }

        public IList<Shape> GetShapes(string filePath)
        {
            if(!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File with path: {filePath} doesn't exist!");
            }

            CurrentFile = filePath;
            var fileText = GetFileContents();

            var tags = GetTags(fileText);
            var shapes = ParseTagsToObjects(tags);

            return shapes;
        }

        public void SaveShapes(IList<Shape> shapes)
        {
            if (string.IsNullOrEmpty(CurrentFile))
            {
                _writer.WriteLine("No file loaded!");
                return;
            }

            var fileContent = GetFileContents();

            int svgStart = fileContent.IndexOf("<svg");
            int svgContentStart = fileContent.IndexOf('>', svgStart);
            int svgEnd = fileContent.IndexOf("</svg>");

            StringBuilder newContentBuilder = new StringBuilder();
            //Append everything before svg tag.
            newContentBuilder.Append(fileContent.Substring(0, svgContentStart + 1));

            //Append new tags.
            foreach (var shape in shapes)
            {
                newContentBuilder.Append(shape.GetTag());
            }

            //Append everything after svg tag.
            newContentBuilder.Append(fileContent.Substring(svgEnd, fileContent.Length - svgEnd));

            File.WriteAllText(CurrentFile, newContentBuilder.ToString());
            _writer.WriteLine("Successfully saved file!");
        }

        private IList<string> GetTags(string fileContent)
        {
            int svgStart = fileContent.IndexOf("<svg");
            int svgContentStart = fileContent.IndexOf('>', svgStart);
            int svgEnd = fileContent.IndexOf("</svg>");

            if (svgStart == -1)
            {
                throw new FileLoadException("No svg tag in file!");
            }

            if (svgEnd == -1)
            {
                throw new FileLoadException("Svg tag not closed!");
            }

            int pos = svgContentStart + 1;

            IList<string> tags = new List<string>();

            while (pos != svgEnd)
            {
                if (fileContent[pos] == '<') //We check for opening tag
                {
                    int endPosOfTag = fileContent.IndexOf("/>", pos); //We find the ending of the tag
                    tags.Add(fileContent.Substring(pos, endPosOfTag + 2 - pos)); //We push the whole tag.
                    pos = endPosOfTag + 1;
                }
                else pos++;
            }

            return tags;
        }

        private IList<Shape> ParseTagsToObjects(IList<string> tags)
        {
            IList<Shape> parsedShapes = new List<Shape>();

            foreach (var tag in tags)
            {
                IList<string> tagArguments = TextProcessingUtils.SplitTokens(tag);
                IDictionary<string, string> shapeAttributes = TextProcessingUtils.SplitAttributes(tagArguments);

                if (ShapeMappings.TagToShapeTypeMap.TryGetValue(tagArguments[0], out ShapeType type))
                {
                    Shape parsed = _shapeFactory.CreateShape(type, shapeAttributes);
                    parsedShapes.Add(parsed);
                }
            }

            return parsedShapes;
        }

        private string GetFileContents()
        {
            var fileBytes = File.ReadAllBytes(CurrentFile);
            var fileText = System.Text.Encoding.UTF8.GetString(fileBytes);
            return fileText;
        }
    }
}
