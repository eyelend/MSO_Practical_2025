using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSO_P2_Code.Command;
using MSO_P2_Code.World;

namespace MSO_P2_Code.Applic
{
    internal class ProgramImporter
    {
        string codeFolderPath = "..\\..\\..\\ExampleFiles\\";
        public string importFromtxt(string fileName)
        {
            StreamReader stream;
            if (!TryFindPath(fileName, out stream))
            {
                Console.WriteLine("Error: Failed to find the given file.");
                Console.WriteLine("Continuing with another program.");
                stream = new StreamReader(codeFolderPath + "Code.txt");
            }
            string code = stream.ReadToEnd();
            stream.Close();
            return code;
        }
        private bool TryFindPath(string file, out StreamReader output)
        {
            string[] attempts = {
                file,
                codeFolderPath + file,
                codeFolderPath + file + ".txt",
            };
            foreach (string path in attempts)
            {
                try
                {
                    output = new StreamReader(path);
                    return true; //success
                }
                catch
                {
                }
            }
            output = null;
            return false; // failed to find the file
        }

        public InnerProgram Parse(string fileName)
        {
            string code = importFromtxt(fileName);
            InnerProgram innerProgram;
            ICommand[] commands;
            string[] strings = code.Split('\n');

            commands = ParseCommands(strings); //converts string[] to ICommand[], deals with nested loops

            innerProgram = new InnerProgram(new Body.Builder().FromCommands(commands).Build());
            return innerProgram;
        }

        public ICommand[] ParseCommands(string[] lines) //returns nested ICommand[] by using recursion
        {
            int index = 0;
            int i = 0; // counts operations for array
            bool nest = false;
            (int start, int end) repeatOp = (0, 0);
            Queue<(int, int)> nests = new Queue<(int, int)>();

            foreach (string line in lines) //count number of direct operations
            {
                if (line[0] != ' ' && nest == false)
                {
                    i++;
                }
                else if (line[0] == ' ' && nest == false)
                {
                    repeatOp.start = index;
                    nest = true;
                }
                else if (line[0] != ' ' && nest == true)
                {
                    repeatOp.end = index;
                    nests.Enqueue(repeatOp);
                    i++;
                    nest = false;
                }
                index++;
            }
            if (nest == true) // in case the program ends with a repeat operation
            {
                repeatOp.end = index;
                nests.Enqueue(repeatOp);
                nest = false;
            }

            ICommand[] commands = new ICommand[i];

            int j = 0;
            while (j < i)    //add the commands to the ICommand array
            {
                foreach (string line in lines)
                {
                    switch (line[0])
                    {
                        case 'M':
                            commands[j] = new Move(int.Parse(line.Split(' ')[1]));
                            j++; break;
                        case 'T':
                            string word1 = line.Split(' ')[1];
                            if (word1.Substring(0,5) == "right")
                            {
                                commands[j] = new Turn(Dir2.Right);
                            }
                            else if (word1.Substring(0,4) == "left")
                            {
                                commands[j] = new Turn(Dir2.Left);
                            }
                            else throw new ParseFailException($"parse error in line {j}: {line}.   word1 = {word1}.");
                            j++; break;
                        case 'R':
                            (int x, int y) hole = nests.Dequeue();
                            string[] subset = TrimFront(lines[hole.x..hole.y]);
                            commands[j] = new Repeat(int.Parse(line.Split(' ')[1]), ParseCommands(subset));
                            j++; break;
                        case ' ':
                            break;
                    }
                }

            }

            return commands;
        }


        private string[] TrimFront(string[] lines) // removes 4 white spaces from the front of each string
        {
            string[] result = new string[lines.Length];
            int i = 0;
            foreach (string line in lines)
            {
                string s = line.Substring(4);
                result[i] = s;
                i++;
            }
            return result;
        }


    }
}
