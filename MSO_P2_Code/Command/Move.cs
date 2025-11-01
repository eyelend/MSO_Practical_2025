using MSO_P2_Code.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.Command
{
    internal class Move : ICommand
    {
        int stepCount;
        public Move(int stepcount)
        {
            this.stepCount = stepcount;
        }
        public void ApplyOnWorld(ref ActualWorld world)
        {
            world.MoveForward(stepCount);
        }

        public T Fold<T,C>(ICommand.IAlgebra<T,C> algebra)
            => algebra.FoldMove(stepCount);
        public T Fold<T>(ICommand.IAlgebraNoCondition<T> algebra)
            => algebra.FoldMove(stepCount);

        public ProgramMetrics GetMetrics()
        {
            return new ProgramMetrics(1, 0, 0);
        }
    }
}
