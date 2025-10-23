using MSO_P2_Code.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.GenericUI
{
    internal class ProgramParser : IParser<InnerProgram>
    {
        class BodyUnparser1 : ICommand.IAlgebra<string>
        {
            public string UnparseBody(Body body)
                => body.Fold(this);

            public string body(string[] parsedElements)
            {
                if (parsedElements.Length == 0) return "";

                StringBuilder sb = new StringBuilder(parsedElements[0]);
                foreach (string element in parsedElements[1..])
                    sb.Append("\n" + element);
                return sb.ToString();
            }

            public string move(int stepCount)
            {
                return "Move " + stepCount;
            }

            public string repeat(int count, string parsedBody)
            {
                StringBuilder sb = new StringBuilder("Repeat " + count);
                foreach (string line in parsedBody.Split("\n"))
                {
                    sb.Append("\n    " + line);
                }
                return sb.ToString();
            }

            public string turn(Dir2 dir)
            {
                return "Turn " + dir switch { Dir2.Left => "left", Dir2.Right => "right" };
            }
        }

        public static readonly ProgramParser Instance = new();
        private ProgramParser() { }

        public InnerProgram Parse(string code)
        {
            string[] strings = code.Split('\n');

            try
            {
                InnerProgram program = new InnerProgram(ParseCommandBody(strings).Build());
                return program;
            }
            catch (ParseFailException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ParseFailException("Unknown parse error.\n" + e.Message);
            }
        }
        public string Unparse(InnerProgram program)
        {
            return program.FoldCommands(new BodyUnparser1());
        }


        private Body.Builder ParseCommandBody(string[] lines) //returns nested ICommand[] by using recursion
        {
            // todo: increase cohesion in this method.

            int index = 0;
            int i = 0; // counts operations for array
            bool nest = false;
            (int start, int end) repeatOp = (0, 0);
            Queue<(int, int)> nests = new Queue<(int, int)>();

            foreach (string line in lines) //count number of direct operations
            {
                if (line.Length == 0) { }
                else if (line[0] != ' ' && nest == false)
                {
                    i++;
                }
                else if (line[0] == ' ' && nest == false)
                {
                    repeatOp.start = index;
                    nest = true;
                }
                else if (line[0] != ' ' && nest == true)
                {
                    repeatOp.end = index;
                    nests.Enqueue(repeatOp);
                    i++;
                    nest = false;
                }
                index++;
            }
            if (nest == true) // in case the program ends with a repeat operation
            {
                repeatOp.end = index;
                nests.Enqueue(repeatOp);
                nest = false;
            }

            Body.Builder commands = new Body.Builder();

            int j = 0;
            try
            {
                while (j < i)    //add the commands to the ICommand array
                {
                    foreach (string line in lines)
                    {
                        switch (line[0])
                        {
                            case 'M':
                                string distAsText = line.Split(' ')[1];
                                try { commands.move(int.Parse(distAsText)); }
                                catch { throw new ParseFailException($"Failed to parse '{distAsText}' as int in line {j}."); }
                                j++; break;
                            case 'T':
                                string word1 = line.Split(' ')[1];
                                if (word1.Substring(0, 5) == "right")
                                {
                                    commands.turn(Dir2.Right);
                                }
                                else if (word1.Substring(0, 4) == "left")
                                {
                                    commands.turn(Dir2.Left);
                                }
                                else throw new ParseFailException($"parse error in line {j}: {line}.   word1 = {word1}.");
                                j++; break;
                            case 'R':
                                (int x, int y) hole = nests.Dequeue();
                                string[] subset = TrimFront(lines[hole.x..hole.y], 4);
                                commands.repeat(int.Parse(line.Split(' ')[1]), ParseCommandBody(subset));
                                j++; break;
                            case ' ':
                                break;
                            default:
                                throw new ParseFailException("Unreadable line: " + j);
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                throw new ParseFailException("Not done reading.");
            }

            return commands;
        }

        private Body.Builder ParseCommandBody2(string[] lines)
        {
            Body.Builder builder = new Body.Builder();
            for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
            {
                string currentLine = lines[lineIndex];
                if (tryParseMove(currentLine, ref builder)) ;
                else if (tryParseTurn(currentLine, ref builder)) ;
                else if (tryParseRepeat(lines[lineIndex..], ref builder, out int bodySize))
                    lineIndex += bodySize;
                else throw new ParseFailException("Parse error at line " + lineIndex);
            }

            return builder;


            bool tryParseMove(string line, ref Body.Builder builder)
            {
                try
                {
                    if (!(line[..4] == "Move")) return false;
                    int stepCount = int.Parse(line[5..]);
                    builder.move(stepCount);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            bool tryParseTurn(string line, ref Body.Builder builder)
            {
                try
                {
                    if (!(line[..4] == "Turn")) return false;

                    Dir2 dir;
                    if (line[5..9] == "left") dir = Dir2.Left;
                    else if (line[5..10] == "right") dir = Dir2.Right;
                    else return false;

                    builder.turn(dir);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            bool tryParseRepeat(string[] lines, ref Body.Builder builder, out int bodySize)
            {
                bodySize = int.MinValue;
                try
                {
                    // parse first line
                    string line0 = lines[0];
                    if (!(line0[..6] == "Repeat")) return false;
                    int count = int.Parse(line0.Split(' ')[1]); //int.Parse(line0[7..]);

                    string tab = "    ";
                    int endOfBody;
                    for (endOfBody = 1; lines[endOfBody].StartsWith(tab); endOfBody++) ;

                    string[] bodyAsText = TrimFront(lines[1..endOfBody], tab.Length);
                    Body.Builder bodyAsBuilder = ParseCommandBody2(bodyAsText);
                    bodySize = endOfBody - 1;

                    builder.repeat(count, bodyAsBuilder);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        private string[] TrimFront(string[] lines, int tabSize) // removes 4 white spaces from the front of each string
        {
            string[] result = new string[lines.Length];
            int i = 0;
            foreach (string line in lines)
            {
                string s = line.Substring(tabSize);
                result[i] = s;
                i++;
            }
            return result;
        }
    }
}
