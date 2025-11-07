using MSO_P2_Code.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.GenericUI
{
    internal class OutputLanguage1 : IOutputLanguage
    {
        public static readonly OutputLanguage1 Instance = new();
        private OutputLanguage1() { }

        public string ExecutionResult(World.WorldState endState)
        {
            StringBuilder traceText = new StringBuilder();
            foreach (World.EventTrace.IEventTrace et in endState.Trace)
            {
                traceText.Append(et.TextualTrace() + ", ");
            }
            if (traceText.Length >= 2)
                traceText.Remove(traceText.Length - 2, 2);
            traceText.Append('.');

            string dirAsText = endState.playerState.Dir.Match("north", "east", "south", "west");
            traceText.Append($"\nEnd state {endState.playerState.Pos} facing {dirAsText}.");

            return traceText.ToString();
        }

        public string ExecutionResult(WorldState endState, bool reachedDest)
        {
            string result = ExecutionResult(endState);
            if (reachedDest) result += "\nSuccessfully reached the destination";
            return result;
        }

        public string ShowMetrics(InnerProgram program)
        {
            Command.ProgramMetrics metrics = program.GetMetrics();
            return
                $"number of commands = {metrics.commandCount}.\n" +
                $"maximum nesting level = {metrics.maxNestingLevel}.\n" +
                $"number of repeat-commands = {metrics.repeatCommandCount}.";
        }
    }
}
