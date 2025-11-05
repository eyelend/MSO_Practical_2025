using MSO_P2_Code.GenericUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Tests.SystemTests
{
    public class UI1_TextBoxProgram_Tests
    {
        [Fact]
        public void TestProgram1()
        {
            //arrange
            MockUI1Mediator mediator = new();
            UI1 ui = new(mediator);
            string inputCode = "Move 3\nTurn right\nMove 1";
            (int x, int y) expectation = (3, 1);

            //act
            mediator.SetTextBoxProgram(inputCode);
            ui.ClickRun();

            //assert
            Assert.Equal(mediator.CharacterPos, expectation);
        }
        [Fact]
        public void TestProgram2()
        {
            //arrange
            MockUI1Mediator mediator = new();
            UI1 ui = new(mediator);
            string inputCode = "Repeat 3\n    Move 3\n    Turn right\nMove 1";
            (int x, int y) expectation = (0, 2);

            //act
            mediator.SetTextBoxProgram(inputCode);
            ui.ClickRun();

            //assert
            Assert.Equal(mediator.CharacterPos, expectation);
        }
    }
}
