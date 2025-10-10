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
            commands = new ICommand[strings.Length];

            int i = 0;
            foreach (string s in strings)
            {
                string[] com = s.Split(' ');
                switch (com[0])
                {
                    case "Move":
                        commands[i] = new Move(int.Parse(com[1]));
                        break;
                    case "Turn":
                        if (com[1] == "right")
                        {
                            commands[i] = new Turn(Dir2.Right);
                        }
                        else { commands[i] = new Turn(Dir2.Left); }
                        break;
                        //case "Repeat":
                        //    break;
                }
                i++;
            }

            innerProgram = new InnerProgram(commands, new World.ActualWorld(null, new WorldState(new PlayerState())));
            return innerProgram;
        }

    }
}
