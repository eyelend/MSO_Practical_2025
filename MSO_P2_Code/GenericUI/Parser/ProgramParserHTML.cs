using MSO_P2_Code.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.GenericUI.Parser
{
    using static System.Net.Mime.MediaTypeNames;
    using W = CommandWords;
    internal class ProgramParserHTML : IParser<InnerProgram>
    {
        class BodyUnparser1 : ICommand.IAlgebra<string, string>
        {
            public string FoldBody(string[] foldedCommands)
            {
                return Mark("ul", string.Concat(foldedCommands));
            }

            public string FoldFacingBlock()
                => W.wallAhead;

            public string FoldFacingGridEdge()
                => W.gridEdge;
            public string FoldNot(string foldedInput)
                => W.not + " " + foldedInput;


            private string Command(string text)
                => Mark("li", "\"" + text + "\"");
            public string FoldMove(int stepCount)
                => Command(W.move + " " + stepCount);
            public string FoldTurn(Dir2 dir)
                => Command(W.turn + " " + dir switch { Dir2.Left => W.left, Dir2.Right => W.right });


            private string CommandWithBody(string topText, string foldedBody)
                => Mark("li", "\"" + topText + "\"" + foldedBody);
            public string FoldIf(string foldedCondition, string foldedBody)
                => CommandWithBody(W._if + " " + foldedCondition, foldedBody);

            public string FoldRepeat(int count, string foldedBody)
                => CommandWithBody(W.repeat + " " + count, foldedBody);

            public string FoldRepeatUntil(string foldedCondition, string foldedBody)
                => CommandWithBody(W.repeatUntil + " " + foldedCondition, foldedBody);
        }

        public string Unparse(InnerProgram input)
        {
            string unparsedProgramBody = UnparseProgramBody(input);
            string htmlBody = "<body style=\"font-weight: bold;\">" + unparsedProgramBody + "</body>";
            string result = Mark("html", Mark("head", "") + htmlBody);
            return result;
        }
        public InnerProgram Parse(string text) // Not necessary yet in this version.
            => throw new NotImplementedException();


        private static string Mark(string mark, string text)
            => $"<{mark}>{text}</{mark}>";
        public string UnparseProgramBody(InnerProgram input)
            => input.FoldCommands(new BodyUnparser1());
    }
}
