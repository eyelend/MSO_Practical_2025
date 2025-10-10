using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSO_P2_Code.Command;
using MSO_P2_Code.World;

namespace MSO_P2_Code.Applic
{
    internal class ProgramImporter
    {
        public string importFromtxt(string fileName)
        {
            string code;
            StreamReader stream = new StreamReader(fileName);
            code = stream.ReadToEnd();
            stream.Close();
            return code;
        }

        public InnerProgram Parse(string fileName)
        {
            string code = importFromtxt(fileName);
            InnerProgram innerProgram;
            ICommand[] commands;
            string[] strings = code.Split('\n');

            commands = ParseCommands(strings); //converts string[] to ICommand[], deals with nested loops

            innerProgram = new InnerProgram(commands, new World.ActualWorld(null, new WorldState(new PlayerState())));
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
                            if (line.Split(' ')[1] == "right")
                            {
                                commands[j] = new Turn(Dir2.Right);
                            }
                            else { commands[j] = new Turn(Dir2.Left); }
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
