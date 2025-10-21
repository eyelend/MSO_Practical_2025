using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSO_P2_Code.GenericUI;

namespace MSO_P2_Code.Applic
{
    internal class Application
    {
        protected readonly ProgramImporter programImporter = new();
        protected readonly ExamplePrograms examplePrograms = ExamplePrograms.Instance;
        protected readonly IOutputLanguage outputLanguage = OutputLanguage1.Instance;

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
                program = programImporter.ImportProgram(userInput);

            return program;

            bool tryGetExampleProgram(string input, out InnerProgram foundProgram)
            {
                (string, InnerProgram)[] matches = new (string, InnerProgram)[]
                {
                    ("basic1", examplePrograms.basic1),
                    ("basic2", examplePrograms.basic2),
                    ("advanced1", examplePrograms.advanced1),
                    ("advanced2", examplePrograms.advanced2),
                    ("expert1", examplePrograms.expert1),
                    ("expert2", examplePrograms.expert2)
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
            Console.WriteLine("Wanna execute (press E) or calculate metrics (press M)?");
            bool stayInLoop;
            do
            {
                stayInLoop = false;
                ConsoleKey userInput = Console.ReadKey().Key;
                Console.WriteLine();
                switch (userInput)
                {
                    case ConsoleKey.E:
                        Console.WriteLine("\n" + outputLanguage.Execute(program) + "\n");
                        break;

                    case ConsoleKey.M:
                        Console.WriteLine("\n" + outputLanguage.ShowMetrics(program) + "\n");
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
