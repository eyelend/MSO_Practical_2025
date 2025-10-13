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
            Console.Clear();
            InnerProgram program = AskForProgram();
            UseProgram(program);

            Console.WriteLine("End of application. Press 'r' to reset.");
            if (Console.ReadKey().Key == ConsoleKey.R)
                Run(); //another round
            else return;
        }

        protected InnerProgram AskForProgram()
        {
            InnerProgram program;
            Console.WriteLine("Enter the file you want to use. Examples of what you can ask:\nbasic2\nadvanced1\nCode.txt\nCode2\n");
            string userInput = Console.ReadLine();

            if(!tryGetExampleProgram(userInput, out program))
                program = programImporter.Parse(userInput);

            return program;

            bool tryGetExampleProgram(string input, out InnerProgram foundProgram)
            {
                (string, InnerProgram)[] matches = new (string, InnerProgram)[]
                {
                    ("basic1", examplePrograms.basic1),
                    ("basic2", examplePrograms.basic2),
                    ("advanced1", examplePrograms.advanced1),
                    ("advanced2", examplePrograms.advanced2),
                    ("expert1", examplePrograms.expert1)
                };
                foreach ((string name, InnerProgram prog) match in matches)
                    if (input == match.name)
                    {
                        foundProgram = match.prog;
                        return true;
                    }
                foundProgram = null;
                return false; // failed to find a match
            }
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
