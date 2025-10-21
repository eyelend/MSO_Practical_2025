using MSO_P2_Code.Command;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSO_P2_Code.GenericUI;

namespace MSO_P2_Code.Applic
{
    internal class ProgramImporter
    {
        ProgramParser parser = ProgramParser.Instance;
        string codeFolderPath = "..\\..\\..\\ExampleFiles\\";
        private string importFromtxt(string fileName)
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

        public InnerProgram ImportProgram(string fileName)
        {
            return parser.ParseProgram(importFromtxt(fileName));
        }



    }
}
