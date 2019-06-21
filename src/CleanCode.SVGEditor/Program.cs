using CleanCode.SVGEditor.Constants;
using CleanCode.SVGEditor.Interfaces;
using CleanCode.SVGEditor.NinjectModules;
using CleanCode.SVGEditor.ShapeManagement;
using Ninject;
using System;

namespace CleanCode.SVGEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel(new MainModule());
            var invoker = kernel.Get<ICommandInvoker>();

            string command = Console.ReadLine();
            while (command != CommandValues.Exit)
            {
                invoker.InvokeCommand(command);
                command = Console.ReadLine();
            } 
        }
    }
}
