using Spectre.Console;

namespace Chinook.Src
{
    class App
    {
        public static void Start()
        {
            Splash();
        }

        public static void Splash()
        {
            var figlet 
                = new FigletText("Chinook".ToUpper())
                    .Centered()
                    .Color(Color.Red);
            
            AnsiConsole.Write(figlet);

            AnsiConsole.Progress()
                .Start(ctx => {
                    var task1 = ctx.AddTask("[red solid]Starting the program");
                });

            
        }
    }
}