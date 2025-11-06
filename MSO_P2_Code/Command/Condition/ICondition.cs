using MSO_P2_Code.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.Command.Condition
{
    internal interface ICondition
    {
        bool Check(ActualWorld world);

        Result Fold<Result>(Algebra<Result> algebra);

        public class Algebra<Result>
        {
            public readonly Result foldFacingBlock;
            public readonly Result foldFacingGridEdge;
            public readonly Func<Result, Result> foldNot;

            public Algebra(Result foldFacingBlock, Result foldFacingGridEdge, Func<Result, Result> foldNot)
            {
                this.foldFacingBlock = foldFacingBlock;
                this.foldFacingGridEdge = foldFacingGridEdge;
                this.foldNot = foldNot;
            }
        }
    }
}
