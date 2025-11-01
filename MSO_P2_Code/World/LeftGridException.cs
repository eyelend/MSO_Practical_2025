using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.World
{
    internal class LeftGridException : Exception
    {
        public readonly (int x, int y) pos;
        public LeftGridException((int x, int y) pos) : base("Left grid at " + pos)
        {
            this.pos = pos;
        }

        public LeftGridException((int x, int y) pos, string? message) : base(message)
        {
            this.pos = pos;
        }
    }
}
