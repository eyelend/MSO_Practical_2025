using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.Applic
{
    internal class Application
    {
        protected readonly ProgramImporter programImporter = new();
        protected readonly ExamplePrograms examplePrograms = ExamplePrograms.Instance;

        public void Run()
        {
            InnerProgram program = AskForProgram();
            UseProgram(program);
            Console.WriteLine("End of application.");
        }

        protected InnerProgram AskForProgram()
        {
            Console.WriteLine("Enter the file you want to use:");
            string userInput = Console.ReadLine();
            InnerProgram ip = programImporter.Parse(userInput);
            return ip;
        }

        protected void UseProgram(InnerProgram program)
        {
            Console.WriteLine("Wanna execute (E) or calculate metrics (M)?");
            bool stayInLoop;
            do
            {
                stayInLoop = false;
                ConsoleKey userInput = Console.ReadKey().Key;
                Console.WriteLine();
                switch (userInput)
                {
                    case ConsoleKey.E:
                        World.WorldState endState = program.Execute();

                        StringBuilder traceText = new StringBuilder();
                        foreach (World.EventTrace.IEventTrace et in endState.Trace)
                        {
                            traceText.Append(et.TextualTrace() + ", ");
                        }
                        traceText.Remove(traceText.Length - 2, 2);
                        traceText.Append('.');
                        Console.WriteLine(traceText.ToString());

                        string dirAsText = endState.playerState.Dir.Match("north", "east", "south", "west");
                        Console.WriteLine($"End state {endState.playerState.Pos} facing {dirAsText}.\n");
                        break;

                    case ConsoleKey.M:
                        Command.ProgramMetrics metrics = program.GetMetrics();
                        Console.WriteLine(
                            $"\nnumber of commands = {metrics.commandCount}.\n" +
                            $"maximum nesting level = {metrics.maxNestingLevel}.\n" +
                            $"number of repeat-commands = {metrics.repeatCommandCount}.\n");
                        break;

                    default:
                        Console.WriteLine("Invalid answer. Try again.");
                        stayInLoop = true;
                        break;
                }
            } while (stayInLoop);

        }
    }
}
