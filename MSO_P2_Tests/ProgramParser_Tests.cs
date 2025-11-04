using MSO_P2_Code.GenericUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Tests
{
    public class ProgramParser_Tests
    {
        [Theory]
        [InlineData("Move 4\nTurn left")]
        [InlineData("Repeat 10\nTurn left")]
        [InlineData("Repeat 10\n    Turn left")]
        [InlineData("If Not Not GridEdge\n    Turn right")]
        public void TestForParseSuccess(string code)
        {
            //arrange
            ProgramParser parser = ProgramParser.Instance;

            //act
            parser.Parse(code);

            //assert
            // If no exception is thrown by now, this test has succeeded. No assertion is needed.
        }

        [Theory]
        [InlineData("Move 4\nTurrn left")]
        [InlineData("Repeat 10\n  Turn left")]
        [InlineData("Turn left\n")]
        public void TestForParseFail(string code)
        {
            //arrange
            ProgramParser parser = ProgramParser.Instance;

            //assert
            Assert.Throws<ParseFailException>(() => parser.Parse(code));
        }

        [Theory]
        [InlineData("Turn right")]
        [InlineData("Repeat 10\n    Turn left")]
        [InlineData("If Not Not GridEdge\n    Turn right\nMove 31")]
        public void TestUnparse(string code)
        {
            //arrange
            ProgramParser parser = ProgramParser.Instance;

            //act
            string codeFromUnparse = parser.Unparse(parser.Parse(code));

            // assert
            Assert.Equal(code, codeFromUnparse);
        }
    }
}
