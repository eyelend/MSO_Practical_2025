using MSO_P2_Code.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.Command.Condition
{
    internal class FacingBlock : ICondition
    {
        public bool Check(ActualWorld world)
        {
            return world.FacingBlock();
        }

        //public Result Fold<Result>(Result foldFacingBlock, Result foldFacingGridEdge, Func<Result, Result> foldNot) => foldFacingBlock;

        public Result Fold<Result>(ICondition.Algebra<Result> algebra)
            => algebra.foldFacingBlock;
    }
}
