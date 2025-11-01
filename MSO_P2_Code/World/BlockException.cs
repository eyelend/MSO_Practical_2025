using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.World
{
    internal class BlockException : Exception
    {
        public readonly (int x, int y) pos;
        public BlockException((int x, int y) pos) : base("Hit wall at " + pos)
        {
            this.pos = pos;
        }

        public BlockException((int x, int y) pos, string? message) : base(message)
        {
            this.pos = pos;
        }
    }
}
