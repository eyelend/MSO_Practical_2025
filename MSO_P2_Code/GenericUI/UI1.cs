using MSO_P2_Code.Applic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.GenericUI
{
    public class UI1
    {
        public interface IDataBridge
        {
            // Allows the model's generic UI to somewhat interact with the actual UI-elements.
            void SetTextBoxProgram(string text);
            void SetTextBoxOutput(string text);
            string ReadTextBoxProgram();
        }

        private readonly ExamplePrograms examplePrograms;
        private readonly ProgramParser programParser;
        private readonly IOutputLanguage outputLanguage;
        protected readonly IDataBridge dataBridge;
        public UI1(IDataBridge dataBridge)
        {
            this.dataBridge = dataBridge;
            examplePrograms = ExamplePrograms.Instance;
            programParser = ProgramParser.Instance;
            outputLanguage = OutputLanguage1.Instance;
        }


        private void SelectHardcodedProgram(InnerProgram program)
        {
            try
            {
                dataBridge.SetTextBoxProgram(programParser.UnParseProgram(program));
            }
            catch (NotImplementedException e)
            {
                dataBridge.SetTextBoxProgram("Error: \n" + e.Message);
            }
        }
        public void SelectProgramBasic()
        {
            SelectHardcodedProgram(examplePrograms.basic1);
        }
        public void SelectProgramAdvanced()
        {
            SelectHardcodedProgram(examplePrograms.advanced1);

        }
        public void SelectProgramExpert()
        {
            SelectHardcodedProgram(examplePrograms.expert1);
        }

        public void ClickRun()
        {
            InnerProgram programFromBox;
            try
            {
                programFromBox = programParser.ParseProgram(dataBridge.ReadTextBoxProgram());
                string output = outputLanguage.Execute(programFromBox);
                dataBridge.SetTextBoxOutput(output);

                //todo: show path on the form's grid.
            }
            catch (ParseFailException e)
            {
                dataBridge.SetTextBoxOutput("Parse error: " + e.Message);
            }
        }
        public void ClickMetrics()
        {
            //todo
            dataBridge.SetTextBoxOutput("Metrics not implemented yet.");
        }

        protected string ReadTextBoxProgram()
        {
            return dataBridge.ReadTextBoxProgram();
        }

        public void SelectExercise(string fileContent)
        {
            //todo
            dataBridge.SetTextBoxOutput("Exercise feature not implemented yet.");
        }
    }
}
