using CleanCode.SVGEditor.Commands;
using CleanCode.SVGEditor.Constants;
using CleanCode.SVGEditor.Interfaces;
using CleanCode.SVGEditor.ShapeManagement;
using CleanCode.SVGEditor.Utils;
using Ninject.Modules;

namespace CleanCode.SVGEditor.NinjectModules
{
    class MainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWriter>().To<ConsoleWriter>().InSingletonScope();
            Bind<IShapeContainer>().To<ShapeContainer>().InSingletonScope();
            Bind<ICommand>().To<PrintCommand>().Named(CommandValues.Print);
        }
    }
}
