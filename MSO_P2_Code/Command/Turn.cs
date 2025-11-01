using MSO_P2_Code.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.Command
{
    internal class Turn : ICommand
    {
        Dir2 dir;
        public Turn(Dir2 dir)
        {
            this.dir = dir;
        }
        public void ApplyOnWorld(ref ActualWorld world)
        {
            switch (dir)
            {
                case Dir2.Left: world.TurnLeft(); break;
                case Dir2.Right: world.TurnRight(); break;
            }
        }

        public T Fold<T,C>(ICommand.IAlgebra<T,C> algebra)
            => algebra.FoldTurn(dir);

        public T Fold<T>(ICommand.IAlgebraNoCondition<T> algebra)
            => algebra.FoldTurn(dir);

    }
}
