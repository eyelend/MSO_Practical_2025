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

            void BlockCell((int x, int y) p);
            void AddGridTraceHorizontal(int y, int x0, int x1);
            void AddGridTraceVertical(int x, int y0, int y1);
            void SetCharacterPos((int x, int y) p);
            void ClearGrid();
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
                dataBridge.SetTextBoxProgram(programParser.Unparse(program));
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

        private bool TryParseTextBoxProgram(out InnerProgram programFromBox)
        {
            try
            {
                programFromBox = programParser.Parse(dataBridge.ReadTextBoxProgram());
                return true; //success
            }
            catch (ParseFailException e)
            {
                dataBridge.SetTextBoxOutput("Parse error: " + e.Message);
                programFromBox = null;
                return false; // failure
            }

        }
        public void ClickRun()
        {
            if (!TryParseTextBoxProgram(out InnerProgram program))
                return;
            dataBridge.SetTextBoxOutput(outputLanguage.Execute(program));

            //todo: show path on the form's grid.

            dataBridge.SetCharacterPos(program.Execute().playerState.Pos);
        }
        public void ClickMetrics()
        {
            if (!TryParseTextBoxProgram(out InnerProgram program))
                return;
            dataBridge.SetTextBoxOutput(outputLanguage.ShowMetrics(program));
        }

        protected string ReadTextBoxProgram()
        {
            return dataBridge.ReadTextBoxProgram();
        }

        public void SelectExercise(string fileContent)
        {
            dataBridge.ClearGrid();
            dataBridge.SetCharacterPos((0, 0));
            //todo
            dataBridge.SetTextBoxOutput("Exercise feature not implemented yet.");
        }
    }
}
