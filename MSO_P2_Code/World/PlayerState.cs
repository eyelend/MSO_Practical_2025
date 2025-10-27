using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSO_P2_Code.Geometry2D;

namespace MSO_P2_Code.World
{
    internal class PlayerState
    {
        public (int x, int y) Pos { get; private set; }
        public Dir4 Dir { get; private set; }

        public PlayerState((int x, int y) pos, Dir4 dir)
        {
            Pos = pos;
            Dir = dir;
        }
        public PlayerState() : this((0, 0), Dir4.East) { }
        public PlayerState Copy() => new PlayerState(Pos, Dir);


        #region commands
        public void TurnLeft()
        {
            Dir = Dir.Rotated(Dir2.Left);
        }
        public void TurnRight()
        {
            Dir = Dir.Rotated(Dir2.Right);
        }
        public void MoveForward(int dist)
        {
            Pos = Dir.MovePoint(Pos, dist);
        }
        #endregion commands
        public (int x, int y) GetFacedPoint()
        {
            return Dir.MovePoint(Pos, 1);
        }

    }
}
