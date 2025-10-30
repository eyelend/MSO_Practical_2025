using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.World
{
    internal class WorldSettings
    {
        private WorldCell[,]? worldGrid;
        private (int x, int y)? destination;
        public (int x, int y)? Destination => destination;
        public (int x, int y)? Size => worldGrid == null ? null : (worldGrid.GetLength(0), worldGrid.GetLength(1));

        public WorldSettings()
        {
            worldGrid = null;
            destination = null;
        }
        public WorldSettings((int x, int y) gridSize)
        {
            if (gridSize.x <= 0 && gridSize.y <= 0) worldGrid = null;
            else
            {
                worldGrid = new WorldCell[gridSize.x, gridSize.y];
                for (int y = 0; y < worldGrid.GetLength(1); y++)
                    for (int x = 0; x < worldGrid.GetLength(0); x++)
                        worldGrid[x, y] = WorldCell.Empty;
            }
            destination = null;
        }
        private bool TrySetCell((int x, int y) pos, WorldCell item)
        {
            bool success = GridIsFinite && IsInside(pos);
            if (success) worldGrid[pos.x, pos.y] = item;
            return success;
        }
        public bool TryBlockCell((int x, int y) pos)
            => TrySetCell(pos, WorldCell.Blocked);
        public bool TryClearCell((int x, int y) pos)
            => TrySetCell(pos, WorldCell.Empty);
        public bool TrySetDestination((int x, int y) pos)
        {
            bool success = IsInside(pos);
            if (success) destination = pos;
            return success;
        }

        public bool GridIsFinite => worldGrid != null;
        public bool IsInside((int x, int y) point)
            => !GridIsFinite ||
                (point.x >= 0 && point.y >= 0 && point.x < worldGrid.GetLength(0) && point.y < worldGrid.GetLength(1));
        public WorldCell GetCell((int x, int y) pos)
            => !GridIsFinite ? WorldCell.Empty :
                !IsInside(pos) ? throw new IndexOutOfRangeException("pos = " + pos) :
                worldGrid[pos.x, pos.y];
    }
}
