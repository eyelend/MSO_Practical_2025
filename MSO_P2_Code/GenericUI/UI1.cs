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
        private readonly ProgramImporter programImporter;
        private readonly IOutputLanguage outputLanguage;
        protected readonly IDataBridge dataBridge;
        public UI1(IDataBridge dataBridge)
        {
            this.dataBridge = dataBridge;
            examplePrograms = ExamplePrograms.Instance;
            programImporter = new ProgramImporter();
            outputLanguage = OutputLanguage1.Instance;
        }


        public void SelectProgramBasic()
        {
            //todo
            dataBridge.SetTextBoxProgram("There's supposed to be code here now.");
        }
        public void SelectProgramAdvanced()
        {
            //todo
            dataBridge.SetTextBoxProgram("There's supposed to be code here now.");
        }
        public void SelectProgramExpert()
        {
            //todo
            dataBridge.SetTextBoxProgram("There's supposed to be code here now.");
        }

        public void ClickRun()
        {
            InnerProgram programFromBox;
            try
            {
                programFromBox = programImporter.ParseProgram(dataBridge.ReadTextBoxProgram());
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
    }
}
