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

        public void ApplyOnWorld(ref ActualWorld world)
        {
            world.MoveForward(stepCount);
        }
    }
}
