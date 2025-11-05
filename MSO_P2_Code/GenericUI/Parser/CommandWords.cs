using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.GenericUI.Parser
{
    internal static class CommandWords
    {
        //todo: use this on ProgramParser
        public const string
            move = "Move",
            turn = "Turn",
            left = "left",
            right = "right",
            repeat = "Repeat",
            repeatUntil = "RepeatUntil",
            _if = "If",
            not = "Not",
            wallAhead = "WallAhead",
            gridEdge = "GridEdge";
    }
}
