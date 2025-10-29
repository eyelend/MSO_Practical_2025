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
        public ActualWorld() : this(new WorldSettings(), new WorldState()) { }

        public ActualWorld CopyState()
        {
            return new ActualWorld(settings, state.Copy());
        }

        private WorldCell? TryGetFacedCell()
        {
            (int x, int y) facedPoint = state.playerState.GetFacedPoint();
            try { return settings.GetCell(facedPoint); }
            catch (IndexOutOfRangeException) { return null; }
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
            //todo: Check if there's a wall.
            state.MoveForward(dist);
            //todo: Check whether we left the grid.
        }
        public bool FacingBlock()
        {
            //todo
            return false;
            //return TryGetFacedCell() == WorldCell.Blocked;
        }
        public bool FacingGridEdge()
        {
            //todo
            (int x, int y) p = state.playerState.GetFacedPoint();
            return p.x < 0 || p.y < 0;
            //return !TryGetFacedCell().HasValue;
        }
        #endregion commands
    }
}
