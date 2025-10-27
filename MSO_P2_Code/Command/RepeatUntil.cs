using MSO_P2_Code.World;
using MSO_P2_Code.Command.Condition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.Command
{
    internal class RepeatUntil : ICommand
    {
        const int limit = 100;
        ICondition stopCondition;
        Body body;
        public RepeatUntil(ICondition stopCondition, Body body)
        {
            this.stopCondition = stopCondition;
            this.body = body;
        }
        public void ApplyOnWorld(ref ActualWorld world)
        {
            //while (stopCondition.Check(world))
            for (int i = 0; !stopCondition.Check(world) && i < limit; i++)
                body.ApplyOnWorld(ref world);
        }

        public T Fold<T,C>(ICommand.IAlgebra<T,C> algebra)
        {
            ICondition.Algebra<C> commandAlgebra = new(algebra.facingBlock(), algebra.facingGridEdge(), algebra.Not);
            return algebra.repeatUntil(stopCondition.Fold(commandAlgebra), body.Fold(algebra));
            throw new NotImplementedException();
        }

        public ProgramMetrics GetMetrics()
        {
            ProgramMetrics bodyMet = body.GetMetrics();
            return new ProgramMetrics(1 + bodyMet.commandCount, 1 + bodyMet.maxNestingLevel, 1 + bodyMet.repeatCommandCount);
        }
    }
}
