using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCode.SVGEditor.Interfaces
{
    internal interface ICommand
    {
        void Execute(IList<string> arguments);
    }
}
