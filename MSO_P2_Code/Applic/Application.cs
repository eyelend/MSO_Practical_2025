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
            Console.WriteLine("Wanna execute (E) or calculate metrics (C)?");
            bool stayInLoop;
            do
            {
                stayInLoop = false;
                ConsoleKey userInput = Console.ReadKey().Key;
                Console.WriteLine();
                switch (userInput)
                {
                    case ConsoleKey.E:
                        //todo
                        break;
                    case ConsoleKey.C:
                        //todo
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
