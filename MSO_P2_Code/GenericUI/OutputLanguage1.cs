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
            //World.WorldState endState = program.Execute();

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

            (int x, int y)? destination = null; //todo: get destination from program's world
            if (endState.playerState.Pos == destination)
                traceText.Append("\nSuccessfully reached the destination");
            return traceText.ToString();
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
