using Spectre.Console;

using Chinook.Src.Utils;

namespace Chinook.Src
{
    class App
    {   
        /// <summary>
        /// Start function to be loaded as sole function within Program class
        /// </summary>
        public static void Start()
        {   
            // Initiate splash screen
            Splash();

            // Display main menu
            ConsoleUtils.MainMenu();
        }

        /// <summary>
        /// Splash function to load on start
        /// </summary>
        public static void Splash()
        {
            var figlet 
                = new FigletText("Chinook".ToUpper())
                    .Centered()
                    .Color(Color.Blue);
            
            AnsiConsole.Write(figlet);

            AnsiConsole.Progress()
                .Start(ctx => {
                    Random random = new(DateTime.Now.Millisecond);

                    var task = ctx.AddTask("[blue bold]Starting the program[/]");

                    while (!ctx.IsFinished)
                    {
                        task.Increment(random.NextDouble());

                        // Simulate delay
                        Thread.Sleep(random.Next(1, 30));
                    }
                });

            AnsiConsole.Write(new Markup("[blue bold]done;[/]"));
            
            AnsiConsole.WriteLine();

            Thread.Sleep(15);

            AnsiConsole.Clear();
        }
    }
}