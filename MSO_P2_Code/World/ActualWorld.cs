using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.World
{
    internal class ActualWorld
    {
        WorldSettings settings;
        public readonly WorldState state;

        public ActualWorld(WorldSettings settings, WorldState state)
        {
            this.settings = settings;
            this.state = state;
        }

        public ActualWorld CopyState()
        {
            return new ActualWorld(settings, state.Copy());
        }

        #region commands
        public void TurnLeft()
        {
            state.TurnLeft();
        }
        public void TurnRight()
        {
            state.TurnRight();
        }
        public void MoveForward(int dist)
        {
            // This is where settings might affect this event.
            state.MoveForward(dist);
        }
        #endregion commands
    }
}
