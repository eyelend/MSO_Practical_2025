using MSO_P2_Code.Command.Condition;
using MSO_P2_Code.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.Command
{
    internal class If : ICommand
    {
        ICondition condition;
        Body body;
        public If(ICondition condition, Body body)
        {
            this.condition = condition;
            this.body = body;
        }
        public void ApplyOnWorld(ref ActualWorld world)
        {
            if (condition.Check(world))
                body.ApplyOnWorld(ref world);
        }

        public T Fold<T, C>(ICommand.IAlgebra<T, C> algebra)
        {
            ICondition.Algebra<C> commandAlgebra = new(algebra.FoldFacingBlock(), algebra.FoldFacingGridEdge(), algebra.FoldNot);
            return algebra.FoldIf(condition.Fold(commandAlgebra), body.Fold(algebra));
        }

        public ProgramMetrics GetMetrics()
        {
            ProgramMetrics bodyMet = body.GetMetrics();
            return new ProgramMetrics(1 + bodyMet.commandCount, bodyMet.maxNestingLevel, bodyMet.repeatCommandCount);
        }
    }
}
