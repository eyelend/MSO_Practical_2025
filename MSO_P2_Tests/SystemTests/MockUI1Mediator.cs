using MSO_P2_Code.GenericUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Tests.SystemTests
{
    internal class MockUI1Mediator : UI1.IMediator
    {
        private string textBoxProgramContent = "";
        public string ReadTextBoxProgram()
            => textBoxProgramContent;
        public void SetTextBoxProgram(string text)
            => textBoxProgramContent = text;



        public void AddGridTraceHorizontal(int y, int x0, int x1) { }

        public void AddGridTraceVertical(int x, int y0, int y1) { }

        public void BlockCell((int x, int y) p) { }

        public void ClearExerciseStuff() { }

        public void ClearTrace() { }

        public (int x, int y) CharacterPos { get; private set; }
        public void SetCharacterPos((int x, int y) p)
            => CharacterPos = p;

        public (int x, int y) Destination { get; private set; }
        public void SetDestination((int x, int y) p)
            => Destination = p;

        public void SetTextBoxOutput(string text) { }
    }
}
