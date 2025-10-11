using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.Geometry2D
{
    internal abstract class Dir4 //using singleton- and state-pattern 
    {
        public abstract (int x, int y) ToVector(); // returns a vector pointing in that direction
        public abstract Dir4 Rotated(Dir2 dir);

        #region instances
        public static readonly Dir4 North = new _North();
        public static readonly Dir4 East = new _East();
        public static readonly Dir4 South = new _South();
        public static readonly Dir4 West = new _West();
        private class _North : Dir4
        {
            public override Dir4 Rotated(Dir2 dir) => dir switch { Dir2.Left => West, Dir2.Right => East };
            public override (int x, int y) ToVector() => (0, -1);
        }
        private class _East : Dir4
        {
            public override Dir4 Rotated(Dir2 dir) => dir switch { Dir2.Left => North, Dir2.Right => South };
            public override (int x, int y) ToVector() => (1, 0);
        }
        private class _South : Dir4
        {
            public override Dir4 Rotated(Dir2 dir) => dir switch { Dir2.Left => East, Dir2.Right => West };
            public override (int x, int y) ToVector() => (0, 1);
        }
        private class _West : Dir4
        {
            public override Dir4 Rotated(Dir2 dir) => dir switch { Dir2.Left => South, Dir2.Right => North };
            public override (int x, int y) ToVector() => (-1, 0);
        }
        #endregion instances

        public (int x, int y) MovePoint((int x, int y) point, int dist)
        {
            (int x, int y) dirAsVec = this.ToVector();
            return (point.x + dirAsVec.x * dist, point.y + dirAsVec.y * dist);
        }
        public T Match<T>(T caseNorth, T caseEast, T caseSouth, T caseWest)
            => this == North ? caseNorth :
            this == East ? caseEast :
            this == South ? caseSouth :
            this == West ? caseWest :
            throw new Exception("Invalid direction");
    }
}
