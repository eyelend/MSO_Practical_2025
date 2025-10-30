using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.World
{
    internal class LeftGridException : Exception
    {
        public LeftGridException()
        {
        }

        public LeftGridException(string? message) : base(message)
        {
        }
    }
}
