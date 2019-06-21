using CleanCode.SVGEditor.Commands;
using CleanCode.SVGEditor.Constants;
using CleanCode.SVGEditor.Interfaces;
using CleanCode.SVGEditor.ShapeManagement;
using CleanCode.SVGEditor.Utils;
using Ninject.Modules;
using Ninject;
using System.Collections.Generic;

namespace CleanCode.SVGEditor.NinjectModules
{
    class MainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IShapeFactory>().To<ShapeFactory>();
            Bind<IWriter>().To<ConsoleWriter>().InSingletonScope();
            Bind<IShapeContainer>().To<ShapeContainer>().InSingletonScope();
            Bind<IFileShapeParser>().To<FileShapeParser>().InSingletonScope();

            Bind<ICommand>().To<PrintCommand>().Named(CommandValues.Print);
            Bind<ICommand>().To<OpenCommand>().Named(CommandValues.Open);
            Bind<ICommand>().To<EraseCommand>().Named(CommandValues.Erase);
            Bind<ICommand>().To<CloseCommand>().Named(CommandValues.Close);
            Bind<ICommand>().To<CreateCommand>().Named(CommandValues.Create);
            Bind<ICommand>().To<WithinCommand>().Named(CommandValues.Within);
            Bind<ICommand>().To<TranslateCommand>().Named(CommandValues.Translate);
            Bind<ICommand>().To<SaveCommand>().Named(CommandValues.Save);


            var printCommand = Kernel.Get<ICommand>(CommandValues.Print);
            var loadShapesCommand = Kernel.Get<ICommand>(CommandValues.Open);
            var eraseCommand = Kernel.Get<ICommand>(CommandValues.Erase);
            var closeCommand = Kernel.Get<ICommand>(CommandValues.Close);
            var createCommand = Kernel.Get<ICommand>(CommandValues.Create);
            var withinCommand = Kernel.Get<ICommand>(CommandValues.Within);
            var translateCommand = Kernel.Get<ICommand>(CommandValues.Translate);
            var saveCommand = Kernel.Get<ICommand>(CommandValues.Save);


            var commandMaps = new Dictionary<string, ICommand> {
                { CommandValues.Print, printCommand },
                { CommandValues.Open, loadShapesCommand },
                { CommandValues.Erase, eraseCommand },
                { CommandValues.Close, closeCommand },
                { CommandValues.Create, createCommand },
                { CommandValues.Within, withinCommand },
                { CommandValues.Translate, translateCommand },
                { CommandValues.Save, saveCommand }
            };

            Kernel.Bind<IDictionary<string, ICommand>>().ToConstant(commandMaps);
            Bind<ICommandInvoker>().To<CommandInvoker>();
        }
    }
}
