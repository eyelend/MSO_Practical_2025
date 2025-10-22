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
        ProgramMetrics GetMetrics();
        T Fold<T>(IAlgebra<T> algebra);

        public interface IAlgebra<Result>
        {
            // Allows external classes to distinguish between ICommand-types without depending on those types.
            // If ICommand gets more realizations, you can add functions here to represent them.
            Result turn(Dir2 dir);
            Result move(int stepCount);
            Result repeat(int count, Result body);
            Result body(Result[] commands);
        }
    }
}
