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
            // Check if we hit a wall or grid edge.
            (int x, int y) scout = state.playerState.Pos;
            for(int i = 0; i < dist; i++)
            {
                scout = state.playerState.Dir.MovePoint(scout, 1);
                if (!settings.IsInside(scout)) throw new LeftGridException(scout);
                else if (settings.GetCell(scout) == WorldCell.Blocked) throw new BlockException(scout);
            }

            state.MoveForward(dist);
        }
        public bool FacingBlock()
        {
            return TryGetFacedCell() == WorldCell.Blocked;
        }
        public bool FacingGridEdge()
        {
            (int x, int y) p = state.playerState.GetFacedPoint();
            return !settings.IsInside(p);
        }
        #endregion commands

        public bool AtDestination()
            => state.playerState.Pos == settings.Destination;
    }
}
