using MSO_P2_Code;
using MSO_P2_Code.Command;
using MSO_P2_Code.GenericUI.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Tests
{
    using BB = Body.Builder;
    public class ProgramUnparseHTML_Tests
    {
        private void TestBodyUnparse(Body.Builder body, string expectation)
        {
            //continue arrange
            ProgramParserHTML parser = new ProgramParserHTML();
            InnerProgram program = new InnerProgram(body.Build());

            //act
            string result = parser.UnparseProgramBody(program);

            //assert
            Assert.Equal(expectation, result);
        }

        [Fact]
        public void TestBodyUnparseBasic1()
        {
            //arrange
            BB body = new BB().move(4).turn(Dir2.Left);
            string expectation = "<ul><li>\"Move 4\"</li><li>\"Turn left\"</li></ul>";

            TestBodyUnparse(body, expectation);
        }
        [Fact]
        public void TestBodyUnparseAdvanced1()
        {
            //arrange
            BB body = new BB().move(14).repeat(2, new BB().turn(Dir2.Left)).move(7);
            string expectation =
                "<ul><li>\"Move 14\"</li><li>\"Repeat 2\"<ul><li>\"Turn left\"</li></ul></li><li>\"Move 7\"</li></ul>";

            TestBodyUnparse(body, expectation);
        }
        [Fact]
        public void TestBodyUnparseAdvanced2()
        {
            //arrange
            BB body = new BB().move(14)._if(BB.facingBlock(), new BB().turn(Dir2.Left)).move(7);
            string expectation =
                "<ul><li>\"Move 14\"</li><li>\"If WallAhead\"<ul><li>\"Turn left\"</li></ul></li><li>\"Move 7\"</li></ul>";

            TestBodyUnparse(body, expectation);
        }
    }
}
