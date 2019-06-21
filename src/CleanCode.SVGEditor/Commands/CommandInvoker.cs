using CleanCode.SVGEditor.Interfaces;
using CleanCode.SVGEditor.Utils;
using System.Collections.Generic;

namespace CleanCode.SVGEditor.Commands
{
    internal class CommandInvoker : ICommandInvoker
    {
        private readonly IDictionary<string, ICommand> _commandMap;
        private readonly IWriter _writer;

        public CommandInvoker(IDictionary<string, ICommand> commmandMap, IWriter writer)
        {
            _commandMap = commmandMap;
            _writer = writer;
        }
        public void InvokeCommand(string commandLine)
        {
            IList<string> arguments = TextProcessingUtils.SplitTokens(commandLine);
            if (arguments.Count == 0)
            {
                _writer.WriteLine("No command entered!");
                return;
            }

            if (_commandMap.TryGetValue(arguments[0], out ICommand mappedCommand))
            {
                mappedCommand.Execute(arguments);
            }
            else
            {
                _writer.WriteLine("Command not supported!");
            } 
        }
    }
}
