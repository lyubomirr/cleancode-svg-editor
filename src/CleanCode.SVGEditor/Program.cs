using CleanCode.SVGEditor.Commands;
using CleanCode.SVGEditor.Interfaces;
using CleanCode.SVGEditor.NinjectModules;
using CleanCode.SVGEditor.ShapeManagement;
using CleanCode.SVGEditor.Utils;
using Ninject;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanCode.SVGEditor
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var cmd = Console.ReadLine();
            //var a = new CommandInvoker(new Dictionary<string, ICommand> { {"print", new PrintCommand(new ShapeContainer(new ConsoleWriter())) } }, new ConsoleWriter());
            //a.InvokeCommand(cmd);
            var a = new FileShapeParser();
            await a.GetShapesAsync("example.svg");
        }        
    }
}
