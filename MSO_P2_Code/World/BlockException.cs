using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.World
{
    internal class BlockException : Exception
    {
        public BlockException()
        {
        }

        public BlockException(string? message) : base(message)
        {
        }
    }
}
