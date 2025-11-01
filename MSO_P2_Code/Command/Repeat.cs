using MSO_P2_Code.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.Command
{
    internal class Repeat : ICommand
    {
        int count;
        Body body;
        public Repeat(int count, Body body)
        {
            this.count = count;
            this.body = body;
        }
        public void ApplyOnWorld(ref ActualWorld world)
        {
            for (int i = 0; i < count; i++)
                body.ApplyOnWorld(ref world);
        }

        public T Fold<T,C>(ICommand.IAlgebra<T,C> algebra)
        {
            return algebra.FoldRepeat(count, body.Fold(algebra));
        }
        public T Fold<T>(ICommand.IAlgebraNoCondition<T> algebra)
            => algebra.FoldRepeat(count, body.Fold(algebra));

        public ProgramMetrics GetMetrics()
        {
            ProgramMetrics bodyMet = body.GetMetrics();
            return new ProgramMetrics(1 + bodyMet.commandCount, 1 + bodyMet.maxNestingLevel, 1 + bodyMet.repeatCommandCount);
        }
    }
}
