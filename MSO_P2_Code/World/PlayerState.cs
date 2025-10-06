using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.World
{
    internal class PlayerState
    {
        public (int x, int y) Pos { get; private set; }
        public Dir4 Dir { get; private set; }
    }
}
