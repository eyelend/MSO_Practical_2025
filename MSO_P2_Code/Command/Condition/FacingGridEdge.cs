using MSO_P2_Code.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.Command.Condition
{
    internal class FacingGridEdge : ICondition
    {
        public bool Check(ActualWorld world)
        {
            return world.FacingGridEdge();
        }

        public Result Fold<Result>(ICondition.Algebra<Result> algebra)
            => algebra.foldFacingGridEdge;

    }
}
