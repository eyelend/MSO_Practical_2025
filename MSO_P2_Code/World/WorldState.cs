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
        public void AddToTrace(IEventTrace eventTrace)
        {
            trace.Enqueue(eventTrace);
        }

        public WorldState(PlayerState playerState)
        {
            this.playerState = playerState;
            trace = new();
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
        }
        #endregion commands

        public WorldState Copy()
        {
            throw new NotImplementedException();
        }
    }
}
