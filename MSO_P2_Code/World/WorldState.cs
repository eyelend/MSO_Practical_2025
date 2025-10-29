using MSO_P2_Code.World.EventTrace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.World
{
    internal class WorldState
    {
        public readonly PlayerState playerState;
        Queue<IEventTrace> trace;
        public IEventTrace[] Trace => trace.ToArray();

        Queue<(int x, int y)> posTrace;
        public (int x, int y)[] PosTrace => posTrace.ToArray();

        private void AddToTrace(IEventTrace eventTrace)
        {
            trace.Enqueue(eventTrace);
        }

        public WorldState(PlayerState playerState)
        {
            this.playerState = playerState;
            trace = new();
            posTrace = new();
            posTrace.Enqueue(playerState.Pos);
        }
        public WorldState() : this(new PlayerState()) { }

        #region commands
        public void TurnLeft()
        {
            AddToTrace(new TurnTrace(Dir2.Left));
            playerState.TurnLeft();
        }
        public void TurnRight()
        {
            AddToTrace(new TurnTrace(Dir2.Right));
            playerState.TurnRight();
        }
        public void MoveForward(int dist)
        {
            AddToTrace(new MoveTrace(dist));
            playerState.MoveForward(dist);
            posTrace.Enqueue(playerState.Pos);
        }
        #endregion commands

        public WorldState Copy()
        {
            WorldState result = new(playerState.Copy());
            foreach (IEventTrace eventTrace in this.trace)
            {
                result.trace.Enqueue(eventTrace);
            }
            return result;
        }
    }
}
