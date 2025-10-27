using MSO_P2_Code.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.Command.Condition
{
    internal class Not : ICondition
    {
        ICondition input;
        public Not(ICondition input)
        {
            this.input = input;
        }

        public bool Check(ActualWorld world)
        {
            return !input.Check(world);
        }

        public Result Fold<Result>(ICondition.Algebra<Result> algebra)
            => algebra.foldNot(input.Fold(algebra));

    }
}
