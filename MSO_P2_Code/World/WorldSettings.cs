using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.World
{
    internal class WorldSettings
    {
        private WorldCell[,] worldGrid;


        public bool IsInside((int x, int y) point)
            => point.x >= 0 && point.y >= 0 && point.x < worldGrid.GetLength(0) && point.y < worldGrid.GetLength(1);
        public WorldCell GetCell((int x, int y) pos)
            => IsInside(pos) ? throw new IndexOutOfRangeException("pos = " + pos) : worldGrid[pos.x, pos.y];
    }
}
