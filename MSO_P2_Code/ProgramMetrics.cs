using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code
{
    internal readonly struct ProgramMetrics
    {
        public readonly int commandCount;
        public readonly int maxNestingLevel;
        public readonly int repeatCommandCount;

        public ProgramMetrics(int commandCount, int maxNestingLevel, int repeatCommandCount)
        {
            this.commandCount = commandCount;
            this.maxNestingLevel = maxNestingLevel;
            this.repeatCommandCount = repeatCommandCount;
        }
    }
}
