using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P3_Forms
{
    internal class GridBase2D
    {
        public readonly (int x, int y) posTopLeft, cellSize, cellCount;

        public GridBase2D((int x, int y) posTopLeft, (int x, int y) cellSize, (int x, int y) cellCount)
        {
            this.posTopLeft = posTopLeft;
            this.cellSize = cellSize;
            this.cellCount = cellCount;
        }


        (int x, int y) TransfWindowToCell((int x, int y) windowPoint, out (int x, int y) relativeWindowPoint)
        {
            throw new NotImplementedException();
        }
        (int x, int y) TransfCellToWindow((int x, int y) cellPoint)
        {
            return (posTopLeft.x + cellSize.x * cellPoint.x, posTopLeft.y + cellSize.y * cellPoint.y);
        }
        (int x, int y) TransfCellToWindow((float x, float y) cellPoint)
        {
            //for a specific point inside a cell
            throw new NotImplementedException();
        }

        public (int x, int y) PutRectInMiddle((int x, int y) rectangleSize, (int x, int y) cellPoint)
        {
            (int x, int y) sizeDifference = (cellSize.x - rectangleSize.x, cellSize.y - rectangleSize.y);
            (int x, int y) cellPos = TransfCellToWindow(cellPoint);
            (int x, int y) newRectPos = (cellPos.x + sizeDifference.x / 2, cellPos.y + sizeDifference.y / 2);
            return newRectPos;
        }

        public bool IsInRange((int x, int y) p)
            => p.x >= 0 && p.y >= 0 && p.x < cellCount.x && p.y < cellCount.y;
        public (int x, int y) BringBackInRange((int x, int y) p)
        {
            if (cellCount.x <= 0) throw new Exception();
            else if (p.x < 0) p.x = 0;
            else if (p.x > cellCount.x) p.x = cellCount.x - 1;

            if (cellCount.y <= 0) throw new Exception();
            else if (p.y < 0) p.y = 0;
            else if (p.y > cellCount.y) p.y = cellCount.y - 1;

            return p;
        }
    }
}
