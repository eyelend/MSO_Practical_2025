using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.Applic
{
    internal class Application
    {
        public void Run()
        {
            Program program = new Program();

        }

        protected Program AskForProgram()
        {
            Console.WriteLine("Enter the file you want to use:");
            string userInput = Console.ReadLine();
            // todo
            return null;
        }

        protected void UseProgram(Program program)
        {
            Console.WriteLine("Wanna execute (E) or calculate metrics (C)?");
            bool stayInLoop;
            do
            {
                stayInLoop = false;
                switch (Console.ReadKey().Key)
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
