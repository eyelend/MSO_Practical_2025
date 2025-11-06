using MSO_P2_Code.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.Command
{
    internal interface ICommand
    {
        void ApplyOnWorld(ref ActualWorld world);
        T Fold<T, Cond>(IAlgebra<T, Cond> algebra);
        T Fold<T>(IAlgebraNoCondition<T> algebra);

        public interface IAlgebra<Result, Cond>
        {
            // Allows external classes to distinguish between ICommand-types without depending on those types.
            // If ICommand gets more realizations, you can add functions here to represent them.
            Result FoldTurn(Dir2 dir);
            Result FoldMove(int stepCount);
            Result FoldRepeat(int count, Result foldedBody);
            Result FoldBody(Result[] foldedCommands);
            Result FoldRepeatUntil(Cond foldedCondition, Result foldedBody);
            Result FoldIf(Cond foldedCondition, Result foldedBody);
            Cond FoldFacingBlock();
            Cond FoldFacingGridEdge();
            Cond FoldNot(Cond foldedInput);
        }
        public interface IAlgebraNoCondition<Result>
        {
            // Same idea as IAlgebra, but conditionals get ignored.
            Result FoldTurn(Dir2 dir);
            Result FoldMove(int stepCount);
            Result FoldRepeat(int count, Result foldedBody);
            Result FoldBody(Result[] foldedCommands);
            Result FoldRepeatUntil(Result foldedBody);
            Result FoldIf(Result foldedBody);
        }
    }
}
