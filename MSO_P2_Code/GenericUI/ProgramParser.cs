using MSO_P2_Code.Command;
using MSO_P2_Code.Command.Condition;
using MSO_P2_Code.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.GenericUI
{
    internal class ProgramParser : IParser<InnerProgram>
    {
        class BodyUnparser1 : ICommand.IAlgebra<string, string>
        {
            public string body(string[] parsedElements)
            {
                if (parsedElements.Length == 0) return "";

                StringBuilder sb = new StringBuilder(parsedElements[0]);
                foreach (string element in parsedElements[1..])
                    sb.Append("\n" + element);
                return sb.ToString();
            }

            public string facingBlock()
            {
                return "WallAhead";
            }

            public string facingGridEdge()
            {
                return "GridEdge";
            }

            public string Not(string input)
            {
                return "Not " + input;
            }

            public string move(int stepCount)
            {
                return "Move " + stepCount;
            }

            public string repeat(int count, string body)
            {
                /*StringBuilder sb = new StringBuilder("Repeat " + count);
                foreach (string line in body.Split("\n"))
                {
                    sb.Append("\n    " + line);
                }
                return sb.ToString();*/
                return "Repeat " + count + AddBody(body);
            }

            public string repeatUntil(string conditionResult, string body)
            {
                /*StringBuilder sb = new StringBuilder("RepeatUntil " + conditionResult);
                foreach (string line in body.Split("\n"))
                {
                    sb.Append("\n    " + line);
                }
                return sb.ToString();*/
                return "RepeatUntil " + conditionResult + AddBody(body);
            }

            public string turn(Dir2 dir)
            {
                return "Turn " + dir switch { Dir2.Left => "left", Dir2.Right => "right" };
            }

            private string AddBody(string bodyBeforeTab)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string line in bodyBeforeTab.Split("\n"))
                {
                    sb.Append("\n    " + line);
                }
                return sb.ToString();
            }
        }

        public static readonly ProgramParser Instance = new();
        private ProgramParser() { }

        public InnerProgram Parse(string code, WorldSettings worldSettings)
        {
            try
            {
                string[] codeLines = code.Split('\n');
                Body programCommands = ParseCommandBody(codeLines).Build();
                return new InnerProgram(programCommands, new ActualWorld(worldSettings, new WorldState()));
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
        public InnerProgram Parse(string code)
            => Parse(code, new WorldSettings());
        public string Unparse(InnerProgram program)
        {
            return program.FoldCommands(new BodyUnparser1());
        }


        private Body.Builder ParseCommandBodyOld(string[] lines) //returns nested ICommand[] by using recursion
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
                                commands.repeat(int.Parse(line.Split(' ')[1]), ParseCommandBodyOld(subset));
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

        private Body.Builder ParseCommandBody(string[] lines)
        {
            Body.Builder builder = new Body.Builder();
            for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
            {
                int bodySize;
                string currentLine = lines[lineIndex];
                if (tryParseMove(currentLine, ref builder)) ;
                else if (tryParseTurn(currentLine, ref builder)) ;
                else if (tryParseRepeat(lines[lineIndex..], ref builder, out bodySize))
                    lineIndex += bodySize;
                else if (tryParseRepeatUntil(lines[lineIndex..], ref builder, out bodySize))
                    lineIndex += bodySize;
                else throw new ParseFailException("Parse error at command " + lineIndex);
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

                    /*string tab = "    ";
                    int endOfBody;
                    for (endOfBody = 1; endOfBody < lines.Length && lines[endOfBody].StartsWith(tab); endOfBody++) ;

                    string[] bodyAsText = TrimFront(lines[1..endOfBody], tab.Length);
                    Body.Builder bodyAsBuilder = ParseCommandBody(bodyAsText);
                    bodySize = endOfBody - 1;*/
                    tryParseTabbedBody(lines[1..], out Body.Builder bodyAsBuilder, out bodySize);

                    builder.repeat(count, bodyAsBuilder);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            bool tryParseRepeatUntil(string[] lines, ref Body.Builder builder, out int bodySize)
            {
                bodySize = int.MinValue;
                try
                {
                    // parse first line
                    string[] line0 = lines[0].Split(' ');
                    if (line0.Length < 2) return false;
                    if (!(line0[0] == "RepeatUntil")) return false;
                    if (!tryParseCondition(line0[1..], builder, out Command.Condition.ICondition condition)) return false;

                    tryParseTabbedBody(lines[1..], out Body.Builder bodyAsBuilder, out bodySize);

                    builder.repeatUntil(condition, bodyAsBuilder);
                    return true;
                }
                catch
                {
                    throw;
                    return false;
                }
            }
            bool tryParseTabbedBody(string[] lines, out Body.Builder bodyAsBuilder, out int bodySize)
            {
                try
                {
                    string tab = "    ";
                    for (bodySize = 0; bodySize < lines.Length && lines[bodySize].StartsWith(tab); bodySize++) ;

                    string[] bodyAsText = TrimFront(lines[0..bodySize], tab.Length);

                    bodyAsBuilder = ParseCommandBody(bodyAsText);
                    return true;
                }
                catch
                {
                    bodyAsBuilder = null;
                    bodySize = int.MinValue;
                    return false;
                }
            }
            bool tryParseCondition(string[] words, Body.Builder bodyAsBuilder, out Command.Condition.ICondition? condition)
            {
                condition = null;
                try
                {
                    if (words.Length == 0) return false;
                    string word0 = words[0];

                    if (word0.StartsWith("WallAhead"))
                        condition = bodyAsBuilder.facingBlock();
                    else if (word0.StartsWith("GridEdge"))
                        condition = bodyAsBuilder.facingGridEdge();
                    else if (word0.StartsWith("Not") && tryParseCondition(words[1..], bodyAsBuilder, out Command.Condition.ICondition inputCondition))
                        condition = bodyAsBuilder.not(inputCondition);
                    else return false;

                    return true;
                }
                catch
                {
                    throw;
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
