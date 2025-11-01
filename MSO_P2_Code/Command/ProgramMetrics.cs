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
        public static ProgramMetrics FromBody(ICommand body)
            => body.Fold(new Algebra());

        private static readonly ProgramMetrics basicCommandmetrics = new ProgramMetrics(1, 0, 0);
        private static ProgramMetrics RepeatMetrics(ProgramMetrics bodyMetrics)
            => new ProgramMetrics(
                1 + bodyMetrics.commandCount,
                1 + bodyMetrics.maxNestingLevel,
                1 + bodyMetrics.repeatCommandCount);


        class Algebra : ICommand.IAlgebra<ProgramMetrics, object>
        {
            public ProgramMetrics FoldBody(ProgramMetrics[] foldedCommands)
            {
                int max(int x1, int x2) => x1 >= x2 ? x1 : x2;
                ProgramMetrics acc = new ProgramMetrics(0, 0, 0);
                foreach (ProgramMetrics cMet in foldedCommands)
                {
                    acc = new(
                        acc.commandCount + cMet.commandCount,
                        max(acc.maxNestingLevel, cMet.maxNestingLevel),
                        acc.repeatCommandCount + cMet.repeatCommandCount);
                }
                return acc;
            }

            public ProgramMetrics FoldIf(object foldedCondition, ProgramMetrics foldedBody)
            {
                return new ProgramMetrics(1 + foldedBody.commandCount, foldedBody.maxNestingLevel, foldedBody.repeatCommandCount);
            }

            public ProgramMetrics FoldRepeat(int count, ProgramMetrics foldedBody)
                => RepeatMetrics(foldedBody);
            public ProgramMetrics FoldRepeatUntil(object foldedCondition, ProgramMetrics foldedBody)
                => RepeatMetrics(foldedBody);


            public ProgramMetrics FoldMove(int stepCount)
                => basicCommandmetrics;
            public ProgramMetrics FoldTurn(Dir2 dir)
                => basicCommandmetrics;


            public object FoldFacingBlock() => null;
            public object FoldFacingGridEdge() => null;
            public object FoldNot(object foldedInput) => null;

        }
    }
}
