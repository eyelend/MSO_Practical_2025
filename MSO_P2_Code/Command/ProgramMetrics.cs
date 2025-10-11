using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.Command
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

        public static ProgramMetrics Comb(ProgramMetrics m1, ProgramMetrics m2)
        {
            int Max(int x1, int x2) => x1 >= x2 ? x1 : x2;
            return new ProgramMetrics(
                m1.commandCount + m2.commandCount,
                Max(m1.maxNestingLevel, m2.maxNestingLevel),
                m1.repeatCommandCount + m2.repeatCommandCount
                );
        }
    }
}
