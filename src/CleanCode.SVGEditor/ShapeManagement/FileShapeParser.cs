using CleanCode.SVGEditor.Interfaces;
using CleanCode.SVGEditor.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CleanCode.SVGEditor.ShapeManagement
{
    internal class FileShapeParser : IShapeParser
    {
        private string _filePath;
        
        public async Task<IList<Shape>> GetShapesAsync(string filePath)
        {
            if(!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File with path: {filePath} doesn't exist!");
            }

            var fileBytes = await File.ReadAllBytesAsync(filePath);
            _filePath = filePath;
            var fileText = System.Text.Encoding.UTF8.GetString(fileBytes);
            var tags = GetTags(fileText);

            return new List<Shape>();
        }

        public void SaveShapes()
        {
            throw new NotImplementedException();
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
    }
}
